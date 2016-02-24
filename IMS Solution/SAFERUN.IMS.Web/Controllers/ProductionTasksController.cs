


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
    public class ProductionTasksController : Controller
    {
        
        //Please RegisterType UnityConfig.cs
        //container.RegisterType<IRepositoryAsync<ProductionTask>, Repository<ProductionTask>>();
        //container.RegisterType<IProductionTaskService, ProductionTaskService>();
        
        //private ImsDbContext db = new ImsDbContext();
        private readonly IProductionTaskService  _productionTaskService;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public ProductionTasksController (IProductionTaskService  productionTaskService, IUnitOfWorkAsync unitOfWork)
        {
            _productionTaskService  = productionTaskService;
            _unitOfWork = unitOfWork;
        }

        // GET: ProductionTasks/Index
        public ActionResult Index()
        {
            
            //var productiontasks  = _productionTaskService.Queryable().Include(p => p.Order).Include(p => p.SKU).AsQueryable();
            
             //return View(productiontasks);
			 return View();
        }

        // Get :ProductionTasks/PageList
        // For Index View Boostrap-Table load  data 
        [HttpGet]
        public ActionResult GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
            int totalCount = 0;
            //int pagenum = offset / limit +1;
            			 
            var productiontasks  = _productionTaskService.Query(new ProductionTaskQuery().Withfilter(filters)).Include(p => p.Order).Include(p => p.SKU).OrderBy(n=>n.OrderBy(sort,order)).SelectPage(page, rows, out totalCount);
            
                        var datarows = productiontasks .Select(  n => new { OrderOrderKey = (n.Order==null?"": n.Order.OrderKey) ,SKUSku = (n.SKU==null?"": n.SKU.Sku) , Id = n.Id , OrderKey = n.OrderKey , SKUId = n.SKUId , DesignName = n.DesignName , ComponentSKU = n.ComponentSKU , ALTSku = n.ALTSku , GraphSKU = n.GraphSKU , ProcessName = n.ProcessName , ProcessOrder = n.ProcessOrder , ProcessSetp = n.ProcessSetp , AltElapsedTime = n.AltElapsedTime , ProductionLine = n.ProductionLine , Equipment = n.Equipment , OrderPlanDate = n.OrderPlanDate , Owner = n.Owner , PlanStartDateTime = n.PlanStartDateTime , PlanCompletedDateTime = n.PlanCompletedDateTime , ActualStartDateTime = n.ActualStartDateTime , ActualCompletedDateTime = n.ActualCompletedDateTime , ActualElapsedTime = n.ActualElapsedTime , Status = n.Status , Issue = n.Issue , Remark = n.Remark , OrderId = n.OrderId }).ToList();
            var pagelist = new { total = totalCount, rows = datarows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }

		[HttpPost]
		public ActionResult SaveData(ProductionTaskChangeViewModel productiontasks)
        {
            if (productiontasks.updated != null)
            {
                foreach (var updated in productiontasks.updated)
                {
                    _productionTaskService.Update(updated);
                }
            }
            if (productiontasks.deleted != null)
            {
                foreach (var deleted in productiontasks.deleted)
                {
                    _productionTaskService.Delete(deleted);
                }
            }
            if (productiontasks.inserted != null)
            {
                foreach (var inserted in productiontasks.inserted)
                {
                    _productionTaskService.Insert(inserted);
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
		
		
       
        // GET: ProductionTasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductionTask productionTask = _productionTaskService.Find(id);
            if (productionTask == null)
            {
                return HttpNotFound();
            }
            return View(productionTask);
        }
        

        // GET: ProductionTasks/Create
        public ActionResult Create()
        {
            ProductionTask productionTask = new ProductionTask();
            //set default value
            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey");
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku");
            return View(productionTask);
        }

        // POST: ProductionTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Order,SKU,Id,OrderKey,SKUId,DesignName,ComponentSKU,ALTSku,GraphSKU,ProcessName,ProcessOrder,ProcessSetp,AltElapsedTime,ProductionLine,Equipment,OrderPlanDate,Owner,PlanStartDateTime,PlanCompletedDateTime,ActualStartDateTime,ActualCompletedDateTime,ActualElapsedTime,Status,Issue,Remark,OrderId,BomComponentId,ParentBomComponentId,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] ProductionTask productionTask)
        {
            if (ModelState.IsValid)
            {
             				_productionTaskService.Insert(productionTask);
                           _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has append a ProductionTask record");
                return RedirectToAction("Index");
            }

            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey", productionTask.OrderId);
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", productionTask.SKUId);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(productionTask);
        }

        // GET: ProductionTasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductionTask productionTask = _productionTaskService.Find(id);
            if (productionTask == null)
            {
                return HttpNotFound();
            }
            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey", productionTask.OrderId);
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", productionTask.SKUId);
            return View(productionTask);
        }

        // POST: ProductionTasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Order,SKU,Id,OrderKey,SKUId,DesignName,ComponentSKU,ALTSku,GraphSKU,ProcessName,ProcessOrder,ProcessSetp,AltElapsedTime,ProductionLine,Equipment,OrderPlanDate,Owner,PlanStartDateTime,PlanCompletedDateTime,ActualStartDateTime,ActualCompletedDateTime,ActualElapsedTime,Status,Issue,Remark,OrderId,BomComponentId,ParentBomComponentId,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] ProductionTask productionTask)
        {
            if (ModelState.IsValid)
            {
                productionTask.ObjectState = ObjectState.Modified;
                				_productionTaskService.Update(productionTask);
                                
                _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has update a ProductionTask record");
                return RedirectToAction("Index");
            }
            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey", productionTask.OrderId);
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", productionTask.SKUId);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(productionTask);
        }

        // GET: ProductionTasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductionTask productionTask = _productionTaskService.Find(id);
            if (productionTask == null)
            {
                return HttpNotFound();
            }
            return View(productionTask);
        }

        // POST: ProductionTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductionTask productionTask =  _productionTaskService.Find(id);
             _productionTaskService.Delete(productionTask);
            _unitOfWork.SaveChanges();
           if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            DisplaySuccessMessage("Has delete a ProductionTask record");
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
