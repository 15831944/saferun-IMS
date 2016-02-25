


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
    public class ProductionPlansController : Controller
    {

        //Please RegisterType UnityConfig.cs
        //container.RegisterType<IRepositoryAsync<ProductionPlan>, Repository<ProductionPlan>>();
        //container.RegisterType<IProductionPlanService, ProductionPlanService>();

        //private ImsDbContext db = new ImsDbContext();
        private readonly IProductionPlanService _productionPlanService;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public ProductionPlansController(IProductionPlanService productionPlanService, IUnitOfWorkAsync unitOfWork)
        {
            _productionPlanService = productionPlanService;
            _unitOfWork = unitOfWork;
        }

        // GET: ProductionPlans/Index
        public ActionResult Index()
        {

            //var productionplans  = _productionPlanService.Queryable().Include(p => p.Order).Include(p => p.ProductionProcess).Include(p => p.SKU).AsQueryable();

            //return View(productionplans);
            return View();
        }

        // Get :ProductionPlans/PageList
        // For Index View Boostrap-Table load  data 
        [HttpGet]
        public ActionResult GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {
            var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
            int totalCount = 0;
            //int pagenum = offset / limit +1;

            var productionplans = _productionPlanService.Query(new ProductionPlanQuery().Withfilter(filters)).Include(p => p.Order).Include(p => p.ProductionProcess).Include(p => p.SKU).OrderBy(n => n.OrderBy(sort, order)).SelectPage(page, rows, out totalCount);

            var datarows = productionplans.Select(n => new { OrderOrderKey = (n.Order == null ? "" : n.Order.OrderKey), ProductionProcessName = (n.ProductionProcess == null ? "" : n.ProductionProcess.Name), SKUSku = (n.SKU == null ? "" : n.SKU.Sku), Id = n.Id, OrderKey = n.OrderKey, SKUId = n.SKUId, DesignName = n.DesignName, ComponentSKU = n.ComponentSKU, ALTSku = n.ALTSku, GraphSKU = n.GraphSKU, StockSKU = n.StockSKU, SKUGroup = n.SKUGroup, MadeType = n.MadeType, Status = n.Status, ConsumeQty = n.ConsumeQty, RequirementQty = n.RequirementQty, RejectRatio = n.RejectRatio, Deploy = n.Deploy, ProductionProcessId = n.ProductionProcessId, Locator = n.Locator, ProductionLine = n.ProductionLine, OrderPlanDate = n.OrderPlanDate, PlanedStartDate = n.PlanedStartDate, PlanedCompletedDate = n.PlanedCompletedDate, ActualStartDate = n.ActualStartDate, ActualCompletedDate = n.ActualCompletedDate, ActualFinishDate = n.ActualFinishDate, Remark = n.Remark, OrderId = n.OrderId }).ToList();
            var pagelist = new { total = totalCount, rows = datarows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveData(ProductionPlanChangeViewModel productionplans)
        {
            if (productionplans.updated != null)
            {
                foreach (var updated in productionplans.updated)
                {
                    _productionPlanService.Update(updated);
                }
            }
            if (productionplans.deleted != null)
            {
                foreach (var deleted in productionplans.deleted)
                {
                    _productionPlanService.Delete(deleted);
                }
            }
            if (productionplans.inserted != null)
            {
                foreach (var inserted in productionplans.inserted)
                {
                    _productionPlanService.Insert(inserted);
                }
            }
            _unitOfWork.SaveChanges();

            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetOrders()
        {
            var orderRepository = _unitOfWork.Repository<Order>();
            var data = orderRepository.Queryable().ToList();
            var rows = data.Select(n => new { Id = n.Id, OrderKey = n.OrderKey });
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetProductionProcesses()
        {
            var productionprocessRepository = _unitOfWork.Repository<ProductionProcess>();
            var data = productionprocessRepository.Queryable().ToList();
            var rows = data.Select(n => new { Id = n.Id, Name = n.Name });
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSKUs()
        {
            var skuRepository = _unitOfWork.Repository<SKU>();
            var data = skuRepository.Queryable().ToList();
            var rows = data.Select(n => new { Id = n.Id, Sku = n.Sku });
            return Json(rows, JsonRequestBehavior.AllowGet);
        }



        // GET: ProductionPlans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductionPlan productionPlan = _productionPlanService.Find(id);
            if (productionPlan == null)
            {
                return HttpNotFound();
            }
            return View(productionPlan);
        }


        // GET: ProductionPlans/Create
        public ActionResult Create()
        {
            ProductionPlan productionPlan = new ProductionPlan();
            //set default value
            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey");
            var productionprocessRepository = _unitOfWork.Repository<ProductionProcess>();
            ViewBag.ProductionProcessId = new SelectList(productionprocessRepository.Queryable(), "Id", "Name");
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku");
            return View(productionPlan);
        }

        // POST: ProductionPlans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Order,ProductionProcess,SKU,Id,OrderKey,SKUId,DesignName,ComponentSKU,ALTSku,GraphSKU,StockSKU,SKUGroup,MadeType,Status,ConsumeQty,RequirementQty,RejectRatio,Deploy,ProductionProcessId,Locator,ProductionLine,OrderPlanDate,PlanedStartDate,PlanedCompletedDate,ActualStartDate,ActualCompletedDate,ActualFinishDate,Remark,OrderId,BomComponentId,ParentBomComponentId,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] ProductionPlan productionPlan)
        {
            if (ModelState.IsValid)
            {
                _productionPlanService.Insert(productionPlan);
                _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has append a ProductionPlan record");
                return RedirectToAction("Index");
            }

            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey", productionPlan.OrderId);
            var productionprocessRepository = _unitOfWork.Repository<ProductionProcess>();
            ViewBag.ProductionProcessId = new SelectList(productionprocessRepository.Queryable(), "Id", "Name", productionPlan.ProductionProcessId);
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", productionPlan.SKUId);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors = String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(productionPlan);
        }

        // GET: ProductionPlans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductionPlan productionPlan = _productionPlanService.Find(id);
            if (productionPlan == null)
            {
                return HttpNotFound();
            }
            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey", productionPlan.OrderId);
            var productionprocessRepository = _unitOfWork.Repository<ProductionProcess>();
            ViewBag.ProductionProcessId = new SelectList(productionprocessRepository.Queryable(), "Id", "Name", productionPlan.ProductionProcessId);
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", productionPlan.SKUId);
            return View(productionPlan);
        }

        // POST: ProductionPlans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Order,ProductionProcess,SKU,Id,OrderKey,SKUId,DesignName,ComponentSKU,ALTSku,GraphSKU,StockSKU,SKUGroup,MadeType,Status,ConsumeQty,RequirementQty,RejectRatio,Deploy,ProductionProcessId,Locator,ProductionLine,OrderPlanDate,PlanedStartDate,PlanedCompletedDate,ActualStartDate,ActualCompletedDate,ActualFinishDate,Remark,OrderId,BomComponentId,ParentBomComponentId,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] ProductionPlan productionPlan)
        {
            if (ModelState.IsValid)
            {
                productionPlan.ObjectState = ObjectState.Modified;
                _productionPlanService.Update(productionPlan);

                _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has update a ProductionPlan record");
                return RedirectToAction("Index");
            }
            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey", productionPlan.OrderId);
            var productionprocessRepository = _unitOfWork.Repository<ProductionProcess>();
            ViewBag.ProductionProcessId = new SelectList(productionprocessRepository.Queryable(), "Id", "Name", productionPlan.ProductionProcessId);
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", productionPlan.SKUId);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors = String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(productionPlan);
        }

        // GET: ProductionPlans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductionPlan productionPlan = _productionPlanService.Find(id);
            if (productionPlan == null)
            {
                return HttpNotFound();
            }
            return View(productionPlan);
        }

        // POST: ProductionPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductionPlan productionPlan = _productionPlanService.Find(id);
            _productionPlanService.Delete(productionPlan);
            _unitOfWork.SaveChanges();
            if (Request.IsAjaxRequest())
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            DisplaySuccessMessage("Has delete a ProductionPlan record");
            return RedirectToAction("Index");
        }



        [HttpPost]
        public ActionResult GenerateTask(int[] plansid)
        {

            var list = _productionPlanService.GenerateProductionTask(plansid);
            _unitOfWork.SaveChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
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
