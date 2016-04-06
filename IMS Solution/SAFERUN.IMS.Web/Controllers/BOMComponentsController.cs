


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
    public class BOMComponentsController : Controller
    {

        //Please RegisterType UnityConfig.cs
        //container.RegisterType<IRepositoryAsync<BOMComponent>, Repository<BOMComponent>>();
        //container.RegisterType<IBOMComponentService, BOMComponentService>();

        //private ImsDbContext db = new ImsDbContext();
        private readonly IBOMComponentService _bOMComponentService;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public BOMComponentsController(IBOMComponentService bOMComponentService, IUnitOfWorkAsync unitOfWork)
        {
            _bOMComponentService = bOMComponentService;
            _unitOfWork = unitOfWork;
        }

        // GET: BOMComponents/Index
        public ActionResult Index()
        {

            //var bomcomponents  = _bOMComponentService.Queryable().Include(b => b.ParentComponent).Include(b => b.ProductionProcess).Include(b => b.SKU).AsQueryable();

            //return View(bomcomponents);
            return View();
        }

        // Get :BOMComponents/PageList
        // For Index View Boostrap-Table load  data 
        [HttpGet]
        public ActionResult GetData(int page = 1, int rows = 10, int id=0,string sort = "Id", string order = "asc", string filterRules = "")
        {
            var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
            int totalCount = 0;
            //int pagenum = offset / limit +1;
           
            if (id == 0) {
                var bomcomponents = _bOMComponentService.Query(new BOMComponentQuery().WithId(id,filters)).Include(b => b.ParentComponent).Include(b => b.ProductionProcess).Include(b => b.SKU).OrderBy(n => n.OrderBy(sort, order)).SelectPage(page, rows, out totalCount); ;
            
                var datarows = bomcomponents.Select(n => new { state = "closed", _parentId = n.ParentComponentId, ParentComponentComponentSKU = (n.ParentComponent == null ? "" : n.ParentComponent.ComponentSKU), ProductionProcessName = (n.ProductionProcess == null ? "" : n.ProductionProcess.Name), SKUSku = (n.SKU == null ? "" : n.SKU.Sku), Id = n.Id, ComponentSKU = n.ComponentSKU, DesignName = n.DesignName, ALTSku = n.ALTSku, GraphSKU = n.GraphSKU, StockSKU = n.StockSKU, MadeType = n.MadeType, SKUGroup = n.SKUGroup, Remark1 = n.Remark1, Remark2 = n.Remark2, ConsumeQty = n.ConsumeQty, ConsumeTime = n.ConsumeTime, RejectRatio = n.RejectRatio, Deploy = n.Deploy, Locator = n.Locator, ProductionLine = n.ProductionLine, ProductionProcessId = n.ProductionProcessId, Version = n.Version, Status = n.Status, NoPur = n.NoPur, FinishedSKU = n.FinishedSKU, SKUId = n.SKUId, ParentComponentId = n.ParentComponentId }).ToList();
            var pagelist = new { total = totalCount, rows = datarows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var bomcomponents = _bOMComponentService.Query(new BOMComponentQuery().WithId(id, filters)).Include(b => b.ParentComponent).Include(b => b.ProductionProcess).Include(b => b.SKU).Include(b => b.Components).OrderBy(n => n.OrderBy(sort, order)).Select().ToList();
                var datarows = bomcomponents.Select(n => new { state = (n.Components.Any()==true?"closed":"open"), _parentId = n.ParentComponentId, ParentComponentComponentSKU = (n.ParentComponent == null ? "" : n.ParentComponent.ComponentSKU), ProductionProcessName = (n.ProductionProcess == null ? "" : n.ProductionProcess.Name), SKUSku = (n.SKU == null ? "" : n.SKU.Sku), Id = n.Id, ComponentSKU = n.ComponentSKU, DesignName = n.DesignName, ALTSku = n.ALTSku, GraphSKU = n.GraphSKU, StockSKU = n.StockSKU, MadeType = n.MadeType, SKUGroup = n.SKUGroup, Remark1 = n.Remark1, Remark2 = n.Remark2, ConsumeQty = n.ConsumeQty, ConsumeTime = n.ConsumeTime, RejectRatio = n.RejectRatio, Deploy = n.Deploy, Locator = n.Locator, ProductionLine = n.ProductionLine, ProductionProcessId = n.ProductionProcessId, Version = n.Version, Status = n.Status, NoPur = n.NoPur, FinishedSKU = n.FinishedSKU, SKUId = n.SKUId, ParentComponentId = n.ParentComponentId }).ToList();
                //var pagelist = new { total = totalCount, rows = datarows };
                return Json(datarows, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GridData(int page = 1, int rows = 10, int id = 0, string sort = "Id", string order = "asc", string filterRules = "")
        {
            var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
            int totalCount = 0;
            //int pagenum = offset / limit +1;
          
                var bomcomponents = _bOMComponentService.Query(new BOMComponentQuery().Withfilter(filters)).Include(b => b.ParentComponent).Include(b => b.ProductionProcess).Include(b => b.SKU).OrderBy(n => n.OrderBy(sort, order)).SelectPage(page, rows, out totalCount); 
                var datarows = bomcomponents.Select(n => new {   _parentId = n.ParentComponentId, ParentComponentComponentSKU = (n.ParentComponent == null ? "" : n.ParentComponent.ComponentSKU), ProductionProcessName = (n.ProductionProcess == null ? "" : n.ProductionProcess.Name), SKUSku = (n.SKU == null ? "" : n.SKU.Sku), Id = n.Id, ComponentSKU = n.ComponentSKU, DesignName = n.DesignName, ALTSku = n.ALTSku, GraphSKU = n.GraphSKU, StockSKU = n.StockSKU, MadeType = n.MadeType, SKUGroup = n.SKUGroup, Remark1 = n.Remark1, Remark2 = n.Remark2, ConsumeQty = n.ConsumeQty, ConsumeTime = n.ConsumeTime, RejectRatio = n.RejectRatio, Deploy = n.Deploy, Locator = n.Locator, ProductionLine = n.ProductionLine, ProductionProcessId = n.ProductionProcessId, Version = n.Version, Status = n.Status, NoPur = n.NoPur, FinishedSKU = n.FinishedSKU, SKUId = n.SKUId, ParentComponentId = n.ParentComponentId }).ToList();
                var pagelist = new { total = totalCount, rows = datarows };
                return Json(pagelist, JsonRequestBehavior.AllowGet);
            
        }

        [HttpPost]
        public ActionResult SaveData(BOMComponentChangeViewModel bomcomponents)
        {
            if (bomcomponents.updated != null)
            {
                foreach (var updated in bomcomponents.updated)
                {
                    _bOMComponentService.Update(updated);
                }
            }
            if (bomcomponents.deleted != null)
            {
                foreach (var deleted in bomcomponents.deleted)
                {
                    _bOMComponentService.Delete(deleted);
                }
            }
            if (bomcomponents.inserted != null)
            {
                foreach (var inserted in bomcomponents.inserted)
                {
                    _bOMComponentService.Insert(inserted);
                }
            }
            _unitOfWork.SaveChanges();

            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetBOMComponents(string productionsku="")
        {
            var bomcomponentRepository = _unitOfWork.Repository<BOMComponent>();
            var data = bomcomponentRepository.Queryable().Where(x => x.SKUGroup == "部套" ).ToList();
            var rows = data.Select(n => new { Id = n.Id, ComponentSKU = n.ComponentSKU });
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
            var data = skuRepository.Queryable().Where(x=>x.SKUGroup=="零件").ToList();
            var rows = data.Select(n => new { Id = n.Id, Sku = n.Sku });
            return Json(rows, JsonRequestBehavior.AllowGet);
        }



        // GET: BOMComponents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOMComponent bOMComponent = _bOMComponentService.Find(id);
            if (bOMComponent == null)
            {
                return HttpNotFound();
            }
            return View(bOMComponent);
        }


        // GET: BOMComponents/Create
        public ActionResult Create()
        {
            BOMComponent bOMComponent = new BOMComponent();
            //set default value
            var bomcomponentRepository = _unitOfWork.Repository<BOMComponent>();
            ViewBag.ParentComponentId = new SelectList(bomcomponentRepository.Queryable(), "Id", "ComponentSKU");
            var productionprocessRepository = _unitOfWork.Repository<ProductionProcess>();
            ViewBag.ProductionProcessId = new SelectList(productionprocessRepository.Queryable(), "Id", "Name");
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku");
            return View(bOMComponent);
        }

        // POST: BOMComponents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Components,ParentComponent,ProductionProcess,SKU,Id,ComponentSKU,DesignName,ALTSku,GraphSKU,StockSKU,MadeType,SKUGroup,Remark1,Remark2,ConsumeQty,ConsumeTime,RejectRatio,Deploy,Locator,ProductionLine,ProductionProcessId,Version,Status,NoPur,FinishedSKU,DesignPricture1,DesignPrictureURL1,DesignPricture2,DesignPrictureURL2,SKUId,ParentComponentId,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] BOMComponent bOMComponent)
        {
            if (ModelState.IsValid)
            {
                _bOMComponentService.Insert(bOMComponent);
                _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has append a BOMComponent record");
                return RedirectToAction("Index");
            }

            var bomcomponentRepository = _unitOfWork.Repository<BOMComponent>();
            ViewBag.ParentComponentId = new SelectList(bomcomponentRepository.Queryable(), "Id", "ComponentSKU", bOMComponent.ParentComponentId);
            var productionprocessRepository = _unitOfWork.Repository<ProductionProcess>();
            ViewBag.ProductionProcessId = new SelectList(productionprocessRepository.Queryable(), "Id", "Name", bOMComponent.ProductionProcessId);
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", bOMComponent.SKUId);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors = String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(bOMComponent);
        }

        // GET: BOMComponents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOMComponent bOMComponent = _bOMComponentService.Find(id);
            if (bOMComponent == null)
            {
                return HttpNotFound();
            }
            var bomcomponentRepository = _unitOfWork.Repository<BOMComponent>();
            ViewBag.ParentComponentId = new SelectList(bomcomponentRepository.Queryable(), "Id", "ComponentSKU", bOMComponent.ParentComponentId);
            var productionprocessRepository = _unitOfWork.Repository<ProductionProcess>();
            ViewBag.ProductionProcessId = new SelectList(productionprocessRepository.Queryable(), "Id", "Name", bOMComponent.ProductionProcessId);
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", bOMComponent.SKUId);
            return View(bOMComponent);
        }

        // POST: BOMComponents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Components,ParentComponent,ProductionProcess,SKU,Id,ComponentSKU,DesignName,ALTSku,GraphSKU,StockSKU,MadeType,SKUGroup,Remark1,Remark2,ConsumeQty,ConsumeTime,RejectRatio,Deploy,Locator,ProductionLine,ProductionProcessId,Version,Status,NoPur,FinishedSKU,DesignPricture1,DesignPrictureURL1,DesignPricture2,DesignPrictureURL2,SKUId,ParentComponentId,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] BOMComponent bOMComponent)
        {
            if (ModelState.IsValid)
            {
                bOMComponent.ObjectState = ObjectState.Modified;
                _bOMComponentService.Update(bOMComponent);

                _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has update a BOMComponent record");
                return RedirectToAction("Index");
            }
            var bomcomponentRepository = _unitOfWork.Repository<BOMComponent>();
            ViewBag.ParentComponentId = new SelectList(bomcomponentRepository.Queryable(), "Id", "ComponentSKU", bOMComponent.ParentComponentId);
            var productionprocessRepository = _unitOfWork.Repository<ProductionProcess>();
            ViewBag.ProductionProcessId = new SelectList(productionprocessRepository.Queryable(), "Id", "Name", bOMComponent.ProductionProcessId);
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", bOMComponent.SKUId);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors = String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(bOMComponent);
        }

        // GET: BOMComponents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOMComponent bOMComponent = _bOMComponentService.Find(id);
            if (bOMComponent == null)
            {
                return HttpNotFound();
            }
            return View(bOMComponent);
        }

        // POST: BOMComponents/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BOMComponent bOMComponent = _bOMComponentService.Find(id);
            _bOMComponentService.Delete(bOMComponent);
            _unitOfWork.SaveChanges();
            if (Request.IsAjaxRequest())
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            DisplaySuccessMessage("Has delete a BOMComponent record");
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
