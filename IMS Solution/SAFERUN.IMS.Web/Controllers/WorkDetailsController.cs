


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
    public class WorkDetailsController : Controller
    {

        //Please RegisterType UnityConfig.cs
        //container.RegisterType<IRepositoryAsync<WorkDetail>, Repository<WorkDetail>>();
        //container.RegisterType<IWorkDetailService, WorkDetailService>();

        //private ImsDbContext db = new ImsDbContext();
        private readonly IWorkDetailService _workDetailService;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public WorkDetailsController(IWorkDetailService workDetailService, IUnitOfWorkAsync unitOfWork)
        {
            _workDetailService = workDetailService;
            _unitOfWork = unitOfWork;
        }

        // GET: WorkDetails/Index
        public ActionResult Index()
        {

            //var workdetails  = _workDetailService.Queryable().Include(w => w.ComponentSKU).Include(w => w.ParentSKU).Include(w => w.Work).AsQueryable();

            //return View(workdetails);
            return View();
        }

        // Get :WorkDetails/PageList
        // For Index View Boostrap-Table load  data 
        [HttpGet]
        public ActionResult GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {
            var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
            int totalCount = 0;
            //int pagenum = offset / limit +1;

            var workdetails = _workDetailService.Query(new WorkDetailQuery().Withfilter(filters)).Include(w => w.ComponentSKU).Include(w => w.ParentSKU).Include(w => w.Work).OrderBy(n => n.OrderBy(sort, order)).SelectPage(page, rows, out totalCount);

            var datarows = workdetails.Select(n => new
            {
                WorkDate=n.Work.WorkDate,
                ProductionQty=(n.ProductionQty==null?0:n.ProductionQty),
                DoingQty = (n.DoingQty==null?0:n.DoingQty),
                FinishedQty=(n.FinishedQty==null?0:n.FinishedQty),
                Status = n.Status,
                ComponentSKUProductName = (n.ComponentSKU == null ? "" : n.ComponentSKU.ProductName),
                ComponentSKUSku = (n.ComponentSKU == null ? "" : n.ComponentSKU.Sku),
                ParentSKUProductName = (n.ParentSKU == null ? "" : n.ParentSKU.ProductName),
                ParentSKUSku = (n.ParentSKU == null ? "" : n.ParentSKU.Sku),
                WorkWorkNo = (n.Work == null ? "" : n.Work.WorkNo),
                Id = n.Id,
                WorkNo = n.WorkNo,
                WorkId = n.WorkId,
                ParentSKUId = n.ParentSKUId,
                ComponentSKUId = n.ComponentSKUId,
                GraphSKU = n.GraphSKU,
                GraphVer = n.GraphVer,
                ConsumeQty = n.ConsumeQty,
                StockQty = n.StockQty,
                RequirementQty = n.RequirementQty,
                Brand = n.Brand,
                Process = n.Process,
                Responsibility = n.Responsibility,
                AltOrderProdctionDate = n.AltOrderProdctionDate,
                AltProdctionDate1 = n.AltProdctionDate1,
                ActualProdctionDate1 = n.ActualProdctionDate1,
                AltProdctionDate2 = n.AltProdctionDate2,
                ActualProdctionDate2 = n.ActualProdctionDate2,
                AltProdctionDate3 = n.AltProdctionDate3,
                ActualProdctionDate3 = n.ActualProdctionDate3,
                AltProdctionDate4 = n.AltProdctionDate4,
                ActualProdctionDate4 = n.ActualProdctionDate4,
                AltProdctionDate5 = n.AltProdctionDate5,
                ActualProdctionDate5 = n.ActualProdctionDate5,
                ConfirmUser = n.ConfirmUser,
                Remark1 = n.Remark1,
                Remark2 = n.Remark2
            }).ToList();
            var pagelist = new { total = totalCount, rows = datarows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveData(WorkDetailChangeViewModel workdetails)
        {
            if (workdetails.updated != null)
            {
                foreach (var updated in workdetails.updated)
                {
                    _workDetailService.Update(updated);
                }
            }
            if (workdetails.deleted != null)
            {
                foreach (var deleted in workdetails.deleted)
                {
                    _workDetailService.Delete(deleted);
                }
            }
            if (workdetails.inserted != null)
            {
                foreach (var inserted in workdetails.inserted)
                {
                    _workDetailService.Insert(inserted);
                }
            }
            _unitOfWork.SaveChanges();

            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetParentSKUs()
        {
            var skuRepository = _unitOfWork.Repository<SKU>();
            var data = skuRepository.Queryable().Where(x=>x.SKUGroup=="部套").ToList();
            var rows = data.Select(n => new { Id = n.Id, Sku = n.Sku });
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSKUs()
        {
            var skuRepository = _unitOfWork.Repository<SKU>();
            var data = skuRepository.Queryable().Where(x=>x.SKUGroup=="零件").ToList();
            var rows = data.Select(n => new { Id = n.Id, Sku = n.Sku });
            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetWorks()
        {
            var workRepository = _unitOfWork.Repository<Work>();
            var data = workRepository.Queryable().ToList();
            var rows = data.Select(n => new { Id = n.Id, WorkNo = n.WorkNo });
            return Json(rows, JsonRequestBehavior.AllowGet);
        }



        // GET: WorkDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkDetail workDetail = _workDetailService.Find(id);
            if (workDetail == null)
            {
                return HttpNotFound();
            }
            return View(workDetail);
        }


        // GET: WorkDetails/Create
        public ActionResult Create()
        {
            WorkDetail workDetail = new WorkDetail();
            //set default value
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.ComponentSKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku");
            //var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.ParentSKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku");
            var workRepository = _unitOfWork.Repository<Work>();
            ViewBag.WorkId = new SelectList(workRepository.Queryable(), "Id", "WorkNo");
            return View(workDetail);
        }

        // POST: WorkDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ComponentSKU,ParentSKU,Work,Id,WorkNo,WorkId,ParentSKUId,ComponentSKUId,GraphSKU,GraphVer,ConsumeQty,StockQty,RequirementQty,Brand,Process,Responsibility,AltOrderProdctionDate,AltProdctionDate1,ActualProdctionDate1,AltProdctionDate2,ActualProdctionDate2,AltProdctionDate3,ActualProdctionDate3,AltProdctionDate4,ActualProdctionDate4,AltProdctionDate5,ActualProdctionDate5,ConfirmUser,Remark1,Remark2,BomComponentId,ParentBomComponentId,OrderKey,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] WorkDetail workDetail)
        {
            if (ModelState.IsValid)
            {
                _workDetailService.Insert(workDetail);
                _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has append a WorkDetail record");
                return RedirectToAction("Index");
            }

            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.ComponentSKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", workDetail.ComponentSKUId);

            ViewBag.ParentSKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", workDetail.ParentSKUId);
            var workRepository = _unitOfWork.Repository<Work>();
            ViewBag.WorkId = new SelectList(workRepository.Queryable(), "Id", "WorkNo", workDetail.WorkId);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors = String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(workDetail);
        }

        // GET: WorkDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkDetail workDetail = _workDetailService.Find(id);
            if (workDetail == null)
            {
                return HttpNotFound();
            }
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.ComponentSKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", workDetail.ComponentSKUId);
            //var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.ParentSKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", workDetail.ParentSKUId);
            var workRepository = _unitOfWork.Repository<Work>();
            ViewBag.WorkId = new SelectList(workRepository.Queryable(), "Id", "WorkNo", workDetail.WorkId);
            return View(workDetail);
        }

        // POST: WorkDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ComponentSKU,ParentSKU,Work,Id,WorkNo,WorkId,ParentSKUId,ComponentSKUId,GraphSKU,GraphVer,ConsumeQty,StockQty,RequirementQty,Brand,Process,Responsibility,AltOrderProdctionDate,AltProdctionDate1,ActualProdctionDate1,AltProdctionDate2,ActualProdctionDate2,AltProdctionDate3,ActualProdctionDate3,AltProdctionDate4,ActualProdctionDate4,AltProdctionDate5,ActualProdctionDate5,ConfirmUser,Remark1,Remark2,BomComponentId,ParentBomComponentId,OrderKey,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] WorkDetail workDetail)
        {
            if (ModelState.IsValid)
            {
                workDetail.ObjectState = ObjectState.Modified;
                _workDetailService.Update(workDetail);

                _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has update a WorkDetail record");
                return RedirectToAction("Index");
            }
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.ComponentSKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", workDetail.ComponentSKUId);
            //var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.ParentSKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", workDetail.ParentSKUId);
            var workRepository = _unitOfWork.Repository<Work>();
            ViewBag.WorkId = new SelectList(workRepository.Queryable(), "Id", "WorkNo", workDetail.WorkId);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors = String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(workDetail);
        }

        // GET: WorkDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkDetail workDetail = _workDetailService.Find(id);
            if (workDetail == null)
            {
                return HttpNotFound();
            }
            return View(workDetail);
        }

        // POST: WorkDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkDetail workDetail = _workDetailService.Find(id);
            _workDetailService.Delete(workDetail);
            _unitOfWork.SaveChanges();
            if (Request.IsAjaxRequest())
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            DisplaySuccessMessage("Has delete a WorkDetail record");
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
