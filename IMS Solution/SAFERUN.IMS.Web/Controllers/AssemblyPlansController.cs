


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
    public class AssemblyPlansController : Controller
    {
        
        //Please RegisterType UnityConfig.cs
        //container.RegisterType<IRepositoryAsync<AssemblyPlan>, Repository<AssemblyPlan>>();
        //container.RegisterType<IAssemblyPlanService, AssemblyPlanService>();
        
        //private ImsDbContext db = new ImsDbContext();
        private readonly IAssemblyPlanService  _assemblyPlanService;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public AssemblyPlansController (IAssemblyPlanService  assemblyPlanService, IUnitOfWorkAsync unitOfWork)
        {
            _assemblyPlanService  = assemblyPlanService;
            _unitOfWork = unitOfWork;
        }

        // GET: AssemblyPlans/Index
        public ActionResult Index()
        {
            
            //var assemblyplans  = _assemblyPlanService.Queryable().Include(a => a.Order).Include(a => a.SKU).AsQueryable();
            
             //return View(assemblyplans);
			 return View();
        }

        // Get :AssemblyPlans/PageList
        // For Index View Boostrap-Table load  data 
        [HttpGet]
        public ActionResult GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
            int totalCount = 0;
            //int pagenum = offset / limit +1;
            			 
            var assemblyplans  = _assemblyPlanService.Query(new AssemblyPlanQuery().Withfilter(filters)).Include(a => a.Order).Include(a => a.SKU).OrderBy(n=>n.OrderBy(sort,order)).SelectPage(page, rows, out totalCount);
            
                        var datarows = assemblyplans .Select(  n => new { OrderOrderKey = (n.Order==null?"": n.Order.OrderKey) ,SKUSku = (n.SKU==null?"": n.SKU.Sku) , Id = n.Id , OrderKey = n.OrderKey , SKUId = n.SKUId , DesignName = n.DesignName , ComponentSKU = n.ComponentSKU , FinishedSKU = n.FinishedSKU , OrderDate = n.OrderDate , RequirementDate = n.RequirementDate , ResolveDate1 = n.ResolveDate1 , ActualReleaseDate1 = n.ActualReleaseDate1 , SetPlanDate2 = n.SetPlanDate2 , PutawayDate2 = n.PutawayDate2 , SetPlanDate3 = n.SetPlanDate3 , PutawayDate3 = n.PutawayDate3 , SetPlanDate4 = n.SetPlanDate4 , PutawayDate4 = n.PutawayDate4 , SetPlanDate5 = n.SetPlanDate5 , DeliveryDate5 = n.DeliveryDate5 , PutawayDate5 = n.PutawayDate5 , SetPlanDate6 = n.SetPlanDate6 , ActualStartDate6 = n.ActualStartDate6 , ActualCompletedDate6 = n.ActualCompletedDate6 , Remark = n.Remark , Status = n.Status , OrderId = n.OrderId }).ToList();
            var pagelist = new { total = totalCount, rows = datarows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }

		[HttpPost]
		public ActionResult SaveData(AssemblyPlanChangeViewModel assemblyplans)
        {
            if (assemblyplans.updated != null)
            {
                foreach (var updated in assemblyplans.updated)
                {
                    _assemblyPlanService.Update(updated);
                }
            }
            if (assemblyplans.deleted != null)
            {
                foreach (var deleted in assemblyplans.deleted)
                {
                    _assemblyPlanService.Delete(deleted);
                }
            }
            if (assemblyplans.inserted != null)
            {
                foreach (var inserted in assemblyplans.inserted)
                {
                    _assemblyPlanService.Insert(inserted);
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
		
		
       
        // GET: AssemblyPlans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssemblyPlan assemblyPlan = _assemblyPlanService.Find(id);
            if (assemblyPlan == null)
            {
                return HttpNotFound();
            }
            return View(assemblyPlan);
        }
        

        // GET: AssemblyPlans/Create
        public ActionResult Create()
        {
            AssemblyPlan assemblyPlan = new AssemblyPlan();
            //set default value
            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey");
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku");
            return View(assemblyPlan);
        }

        // POST: AssemblyPlans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Order,SKU,Id,OrderKey,SKUId,DesignName,ComponentSKU,FinishedSKU,OrderDate,RequirementDate,ResolveDate1,ActualReleaseDate1,SetPlanDate2,PutawayDate2,SetPlanDate3,PutawayDate3,SetPlanDate4,PutawayDate4,SetPlanDate5,DeliveryDate5,PutawayDate5,SetPlanDate6,ActualStartDate6,ActualCompletedDate6,Remark,Status,OrderId,BomComponentId,ParentBomComponentId,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] AssemblyPlan assemblyPlan)
        {
            if (ModelState.IsValid)
            {
             				_assemblyPlanService.Insert(assemblyPlan);
                           _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has append a AssemblyPlan record");
                return RedirectToAction("Index");
            }

            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey", assemblyPlan.OrderId);
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", assemblyPlan.SKUId);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(assemblyPlan);
        }

        // GET: AssemblyPlans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssemblyPlan assemblyPlan = _assemblyPlanService.Find(id);
            if (assemblyPlan == null)
            {
                return HttpNotFound();
            }
            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey", assemblyPlan.OrderId);
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", assemblyPlan.SKUId);
            return View(assemblyPlan);
        }

        // POST: AssemblyPlans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Order,SKU,Id,OrderKey,SKUId,DesignName,ComponentSKU,FinishedSKU,OrderDate,RequirementDate,ResolveDate1,ActualReleaseDate1,SetPlanDate2,PutawayDate2,SetPlanDate3,PutawayDate3,SetPlanDate4,PutawayDate4,SetPlanDate5,DeliveryDate5,PutawayDate5,SetPlanDate6,ActualStartDate6,ActualCompletedDate6,Remark,Status,OrderId,BomComponentId,ParentBomComponentId,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] AssemblyPlan assemblyPlan)
        {
            if (ModelState.IsValid)
            {
                assemblyPlan.ObjectState = ObjectState.Modified;
                				_assemblyPlanService.Update(assemblyPlan);
                                
                _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has update a AssemblyPlan record");
                return RedirectToAction("Index");
            }
            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey", assemblyPlan.OrderId);
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", assemblyPlan.SKUId);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(assemblyPlan);
        }

        // GET: AssemblyPlans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssemblyPlan assemblyPlan = _assemblyPlanService.Find(id);
            if (assemblyPlan == null)
            {
                return HttpNotFound();
            }
            return View(assemblyPlan);
        }

        // POST: AssemblyPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssemblyPlan assemblyPlan =  _assemblyPlanService.Find(id);
             _assemblyPlanService.Delete(assemblyPlan);
            _unitOfWork.SaveChanges();
           if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            DisplaySuccessMessage("Has delete a AssemblyPlan record");
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
