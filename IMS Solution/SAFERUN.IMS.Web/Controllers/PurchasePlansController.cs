


using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repository.Pattern.UnitOfWork;
using Repository.Pattern.Infrastructure;
using SAFERUN.IMS.Web.Models;
using SAFERUN.IMS.Web.Services;
using SAFERUN.IMS.Web.Repositories;
using SAFERUN.IMS.Web.Extensions;


namespace SAFERUN.IMS.Web.Controllers
{
    public class PurchasePlansController : Controller
    {
        
        //Please RegisterType UnityConfig.cs
        //container.RegisterType<IRepositoryAsync<PurchasePlan>, Repository<PurchasePlan>>();
        //container.RegisterType<IPurchasePlanService, PurchasePlanService>();
        
        //private ImsDbContext db = new ImsDbContext();
        private readonly IPurchasePlanService  _purchasePlanService;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public PurchasePlansController (IPurchasePlanService  purchasePlanService, IUnitOfWorkAsync unitOfWork)
        {
            _purchasePlanService  = purchasePlanService;
            _unitOfWork = unitOfWork;
        }

        // GET: PurchasePlans/Index
        public ActionResult Index()
        {
            
            //var purchaseplans  = _purchasePlanService.Queryable().Include(p => p.Order).Include(p => p.SKU).AsQueryable();
            
             //return View(purchaseplans);
			 return View();
        }

        // Get :PurchasePlans/PageList
        // For Index View Boostrap-Table load  data 
        [HttpGet]
        public ActionResult GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
            int totalCount = 0;
            //int pagenum = offset / limit +1;
            			 
            var purchaseplans  = _purchasePlanService.Query(new PurchasePlanQuery().Withfilter(filters)).Include(p => p.Order).Include(p => p.SKU).OrderBy(n=>n.OrderBy(sort,order)).SelectPage(page, rows, out totalCount);
            
                        var datarows = purchaseplans .Select(  n => new { OrderOrderKey = (n.Order==null?"": n.Order.OrderKey) ,SKUSku = (n.SKU==null?"": n.SKU.Sku) , Id = n.Id , OrderKey = n.OrderKey , SKUId = n.SKUId , DesignName = n.DesignName , ComponentSKU = n.ComponentSKU , ALTSku = n.ALTSku , GraphSKU = n.GraphSKU , StockSKU = n.StockSKU , Status = n.Status , ConsumeQty = n.ConsumeQty , RequirementQty = n.RequirementQty , RejectRatio = n.RejectRatio , Deploy = n.Deploy , Locator = n.Locator , Brand = n.Brand , Supplier = n.Supplier , OrderPlanDate = n.OrderPlanDate , PlanedStartDate = n.PlanedStartDate , ActualStartDate = n.ActualStartDate , PlanDeliveryDate = n.PlanDeliveryDate , ActualDeliveryDate = n.ActualDeliveryDate , Remark = n.Remark , OrderId = n.OrderId }).ToList();
            var pagelist = new { total = totalCount, rows = datarows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }

		[HttpPost]
		public ActionResult SaveData(PurchasePlanChangeViewModel purchaseplans)
        {
            if (purchaseplans.updated != null)
            {
                foreach (var updated in purchaseplans.updated)
                {
                    _purchasePlanService.Update(updated);
                }
            }
            if (purchaseplans.deleted != null)
            {
                foreach (var deleted in purchaseplans.deleted)
                {
                    _purchasePlanService.Delete(deleted);
                }
            }
            if (purchaseplans.inserted != null)
            {
                foreach (var inserted in purchaseplans.inserted)
                {
                    _purchasePlanService.Insert(inserted);
                }
            }
            _unitOfWork.SaveChanges();

            return Json(new {Success=true}, JsonRequestBehavior.AllowGet);
        }

				public ActionResult GetOrders()
        {
            var orderRepository = _unitOfWork.Repository<Order>();
            var data = orderRepository.Queryable().ToList();
            var rows = data.Select(n => new { Id = n.Id, OrderKey = n.OrderKey });
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
				public ActionResult GetSKUs()
        {
            var skuRepository = _unitOfWork.Repository<SKU>();
            var data = skuRepository.Queryable().ToList();
            var rows = data.Select(n => new { Id = n.Id, Sku = n.Sku });
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
		
		
       
        // GET: PurchasePlans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchasePlan purchasePlan = _purchasePlanService.Find(id);
            if (purchasePlan == null)
            {
                return HttpNotFound();
            }
            return View(purchasePlan);
        }
        

        // GET: PurchasePlans/Create
        public ActionResult Create()
        {
            PurchasePlan purchasePlan = new PurchasePlan();
            //set default value
            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey");
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku");
            return View(purchasePlan);
        }

        // POST: PurchasePlans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Order,SKU,Id,OrderKey,SKUId,DesignName,ComponentSKU,ALTSku,GraphSKU,StockSKU,Status,ConsumeQty,RequirementQty,RejectRatio,Deploy,Locator,Brand,Supplier,OrderPlanDate,PlanedStartDate,ActualStartDate,PlanDeliveryDate,ActualDeliveryDate,Remark,OrderId,BomComponentId,ParentBomComponentId,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] PurchasePlan purchasePlan)
        {
            if (ModelState.IsValid)
            {
             				_purchasePlanService.Insert(purchasePlan);
                           _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has append a PurchasePlan record");
                return RedirectToAction("Index");
            }

            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey", purchasePlan.OrderId);
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", purchasePlan.SKUId);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(purchasePlan);
        }

        // GET: PurchasePlans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchasePlan purchasePlan = _purchasePlanService.Find(id);
            if (purchasePlan == null)
            {
                return HttpNotFound();
            }
            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey", purchasePlan.OrderId);
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", purchasePlan.SKUId);
            return View(purchasePlan);
        }

        // POST: PurchasePlans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Order,SKU,Id,OrderKey,SKUId,DesignName,ComponentSKU,ALTSku,GraphSKU,StockSKU,Status,ConsumeQty,RequirementQty,RejectRatio,Deploy,Locator,Brand,Supplier,OrderPlanDate,PlanedStartDate,ActualStartDate,PlanDeliveryDate,ActualDeliveryDate,Remark,OrderId,BomComponentId,ParentBomComponentId,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] PurchasePlan purchasePlan)
        {
            if (ModelState.IsValid)
            {
                purchasePlan.ObjectState = ObjectState.Modified;
                				_purchasePlanService.Update(purchasePlan);
                                
                _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has update a PurchasePlan record");
                return RedirectToAction("Index");
            }
            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey", purchasePlan.OrderId);
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", purchasePlan.SKUId);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(purchasePlan);
        }

        // GET: PurchasePlans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchasePlan purchasePlan = _purchasePlanService.Find(id);
            if (purchasePlan == null)
            {
                return HttpNotFound();
            }
            return View(purchasePlan);
        }

        // POST: PurchasePlans/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PurchasePlan purchasePlan =  _purchasePlanService.Find(id);
             _purchasePlanService.Delete(purchasePlan);
            _unitOfWork.SaveChanges();
           if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            DisplaySuccessMessage("Has delete a PurchasePlan record");
            return RedirectToAction("Index");
        }


       

 

        private void DisplaySuccessMessage(string msgText)
        {
            TempData["SuccessMessage"] = msgText;
        }

        private void DisplayErrorMessage()
        {
            TempData["ErrorMessage"] = "Save changes was unsuccessful.";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
