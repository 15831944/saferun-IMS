


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
    public class DefectTrackingsController : Controller
    {

        //Please RegisterType UnityConfig.cs
        //container.RegisterType<IRepositoryAsync<DefectTracking>, Repository<DefectTracking>>();
        //container.RegisterType<IDefectTrackingService, DefectTrackingService>();

        //private ImsDbContext db = new ImsDbContext();
        private readonly IDefectTrackingService _defectTrackingService;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public DefectTrackingsController(IDefectTrackingService defectTrackingService, IUnitOfWorkAsync unitOfWork)
        {
            _defectTrackingService = defectTrackingService;
            _unitOfWork = unitOfWork;
        }

        // GET: DefectTrackings/Index
        public ActionResult Index()
        {

            //var defecttrackings  = _defectTrackingService.Queryable().Include(d => d.DefectCode).Include(d => d.DefectType).Include(d => d.Order).Include(d => d.SKU).AsQueryable();

            //return View(defecttrackings);
            return View();
        }

        // Get :DefectTrackings/PageList
        // For Index View Boostrap-Table load  data 
        [HttpGet]
        public ActionResult GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {
            var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
            int totalCount = 0;
            //int pagenum = offset / limit +1;

            var defecttrackings = _defectTrackingService.Query(new DefectTrackingQuery().Withfilter(filters)).Include(d => d.DefectCode).Include(d => d.DefectType).Include(d => d.Order).Include(d => d.SKU).OrderBy(n => n.OrderBy(sort, order)).SelectPage(page, rows, out totalCount);

            var datarows = defecttrackings.Select(n => new { DefectCodeName = (n.DefectCode == null ? "" : n.DefectCode.Name), DefectTypeName = (n.DefectType == null ? "" : n.DefectType.Name), OrderOrderKey = (n.Order == null ? "" : n.Order.OrderKey), SKUSku = (n.SKU == null ? "" : n.SKU.Sku), Id = n.Id, ProjectName = n.ProjectName, OrderKey = n.OrderKey, OrderId = n.OrderId, SKUId = n.SKUId, ComponentSKU = n.ComponentSKU, Supplier = n.Supplier, GraphSKU = n.GraphSKU, QCQty = n.QCQty, CheckedQty = n.CheckedQty, NGQty = n.NGQty, DefectTypeId = n.DefectTypeId, DefectCodeId = n.DefectCodeId, Locator = n.Locator, DefectDesc = n.DefectDesc, Status = n.Status, Result = n.Result, Remark = n.Remark, QCUser = n.QCUser, TrackingDateTime = n.TrackingDateTime, CheckedDateTime = n.CheckedDateTime, CloseDateTime = n.CloseDateTime }).ToList();
            var pagelist = new { total = totalCount, rows = datarows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveData(DefectTrackingChangeViewModel defecttrackings)
        {
            if (defecttrackings.updated != null)
            {
                foreach (var updated in defecttrackings.updated)
                {
                    _defectTrackingService.Update(updated);
                }
            }
            if (defecttrackings.deleted != null)
            {
                foreach (var deleted in defecttrackings.deleted)
                {
                    _defectTrackingService.Delete(deleted);
                }
            }
            if (defecttrackings.inserted != null)
            {
                foreach (var inserted in defecttrackings.inserted)
                {
                    _defectTrackingService.Insert(inserted);
                }
            }
            _unitOfWork.SaveChanges();

            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDefectCodes()
        {
            var defectcodeRepository = _unitOfWork.Repository<DefectCode>();
            var data = defectcodeRepository.Queryable().ToList();
            var rows = data.Select(n => new { Id = n.Id, Name = n.Name });
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDefectTypes()
        {
            var defecttypeRepository = _unitOfWork.Repository<DefectType>();
            var data = defecttypeRepository.Queryable().ToList();
            var rows = data.Select(n => new { Id = n.Id, Name = n.Name });
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetOrders()
        {
            var orderRepository = _unitOfWork.Repository<Order>();
            var data = orderRepository.Queryable().ToList();
            var rows = data.Select(n => new { Id = n.Id, OrderKey = n.OrderKey, ProjectName = n.ProjectName, Status = n.Status });
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSKUs(int orderid=0)
        {
            var skuRepository = _unitOfWork.Repository<OrderDetail>();
            var data = skuRepository.Queryable().Where(x => x.OrderId == orderid).ToList();
            var rows = data.Select(n => new { Id = n.SKUId, Sku = n.ProductionSku });
            return Json(rows, JsonRequestBehavior.AllowGet);
        }



        // GET: DefectTrackings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DefectTracking defectTracking = _defectTrackingService.Find(id);
            if (defectTracking == null)
            {
                return HttpNotFound();
            }
            return View(defectTracking);
        }


        // GET: DefectTrackings/Create
        public ActionResult Create()
        {
            DefectTracking defectTracking = new DefectTracking();
            //set default value
            var defectcodeRepository = _unitOfWork.Repository<DefectCode>();
            ViewBag.DefectId = new SelectList(defectcodeRepository.Queryable(), "Id", "Name");
            var defecttypeRepository = _unitOfWork.Repository<DefectType>();
            ViewBag.DefectTypeId = new SelectList(defecttypeRepository.Queryable(), "Id", "Name");
            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey");
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku");
            return View(defectTracking);
        }

        // POST: DefectTrackings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DefectCode,DefectType,Order,SKU,Id,ProjectName,OrderKey,OrderId,SKUId,ComponentSKU,Supplier,GraphSKU,QCQty,CheckedQty,NGQty,DefectTypeId,DefectId,Locator,DefectDesc,Status,Result,Remark,QCUser,TrackingDateTime,CheckedDateTime,CloseDateTime,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] DefectTracking defectTracking)
        {
            if (ModelState.IsValid)
            {
                _defectTrackingService.Insert(defectTracking);
                _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has append a DefectTracking record");
                return RedirectToAction("Index");
            }

            var defectcodeRepository = _unitOfWork.Repository<DefectCode>();
            ViewBag.DefectId = new SelectList(defectcodeRepository.Queryable(), "Id", "Name", defectTracking.DefectCodeId);
            var defecttypeRepository = _unitOfWork.Repository<DefectType>();
            ViewBag.DefectTypeId = new SelectList(defecttypeRepository.Queryable(), "Id", "Name", defectTracking.DefectTypeId);
            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey", defectTracking.OrderId);
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", defectTracking.SKUId);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors = String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(defectTracking);
        }

        // GET: DefectTrackings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DefectTracking defectTracking = _defectTrackingService.Find(id);
            if (defectTracking == null)
            {
                return HttpNotFound();
            }
            var defectcodeRepository = _unitOfWork.Repository<DefectCode>();
            ViewBag.DefectId = new SelectList(defectcodeRepository.Queryable(), "Id", "Name", defectTracking.DefectCodeId);
            var defecttypeRepository = _unitOfWork.Repository<DefectType>();
            ViewBag.DefectTypeId = new SelectList(defecttypeRepository.Queryable(), "Id", "Name", defectTracking.DefectTypeId);
            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey", defectTracking.OrderId);
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", defectTracking.SKUId);
            return View(defectTracking);
        }

        // POST: DefectTrackings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DefectCode,DefectType,Order,SKU,Id,ProjectName,OrderKey,OrderId,SKUId,ComponentSKU,Supplier,GraphSKU,QCQty,CheckedQty,NGQty,DefectTypeId,DefectId,Locator,DefectDesc,Status,Result,Remark,QCUser,TrackingDateTime,CheckedDateTime,CloseDateTime,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] DefectTracking defectTracking)
        {
            if (ModelState.IsValid)
            {
                defectTracking.ObjectState = ObjectState.Modified;
                _defectTrackingService.Update(defectTracking);

                _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has update a DefectTracking record");
                return RedirectToAction("Index");
            }
            var defectcodeRepository = _unitOfWork.Repository<DefectCode>();
            ViewBag.DefectId = new SelectList(defectcodeRepository.Queryable(), "Id", "Name", defectTracking.DefectCodeId);
            var defecttypeRepository = _unitOfWork.Repository<DefectType>();
            ViewBag.DefectTypeId = new SelectList(defecttypeRepository.Queryable(), "Id", "Name", defectTracking.DefectTypeId);
            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey", defectTracking.OrderId);
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", defectTracking.SKUId);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors = String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(defectTracking);
        }

        // GET: DefectTrackings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DefectTracking defectTracking = _defectTrackingService.Find(id);
            if (defectTracking == null)
            {
                return HttpNotFound();
            }
            return View(defectTracking);
        }

        // POST: DefectTrackings/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DefectTracking defectTracking = _defectTrackingService.Find(id);
            _defectTrackingService.Delete(defectTracking);
            _unitOfWork.SaveChanges();
            if (Request.IsAjaxRequest())
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            DisplaySuccessMessage("Has delete a DefectTracking record");
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
