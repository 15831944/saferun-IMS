


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
    public class WorkProcessDetailsController : Controller
    {

        //Please RegisterType UnityConfig.cs
        //container.RegisterType<IRepositoryAsync<WorkProcessDetail>, Repository<WorkProcessDetail>>();
        //container.RegisterType<IWorkProcessDetailService, WorkProcessDetailService>();

        //private ImsDbContext db = new ImsDbContext();
        private readonly IWorkProcessDetailService _workProcessDetailService;
        private readonly IWorkProcessService _workProcessService;
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly IStationService _stationService;

        public WorkProcessDetailsController(IStationService stationService,IWorkProcessService workProcessService,IWorkProcessDetailService workProcessDetailService, IUnitOfWorkAsync unitOfWork)
        {
            _workProcessDetailService = workProcessDetailService;
            _unitOfWork = unitOfWork;
            _workProcessService = workProcessService;
            _stationService = stationService;
        }

        public ActionResult StationWorking(string station="",string status="") {

            int[] initstatus ={0,1};
            if (status == "未开始")
            {
                initstatus = new int[] { 0 };
            }
            else if (status == "未完成")
            {
                initstatus = new int[] { 0, 1 };
            }
            else if (status=="已完成")
            {
                initstatus = new int[] { 2};
            }

            var stations = this._stationService.Queryable().ToList();
            ViewBag.Stations = stations;
            var working = this._workProcessDetailService.Queryable().Include(w => w.WorkProcess).Where(x => initstatus.Contains(x.Status) && x.Station.StationNo==station).Select(x => new StationWorkViewModel()
            {
                Id = x.Id,
                ProjectName = x.WorkProcess.ProjectName,
                ComponentSKU = x.ComponentSKU,
                GraphSKU = x.GraphSKU,
                Status = x.Status,
                StartingDateTime = x.StartingDateTime,
                CompletedDateTime = x.CompletedDateTime,
                ElapsedTime = x.ElapsedTime,
                StandardElapsedTime = x.StandardElapsedTime,
                Operator = x.Operator
            }).ToList();

            return View(working);
        }

        // GET: WorkProcessDetails/Index
        public ActionResult Index()
        {

            //var workprocessdetails  = _workProcessDetailService.Queryable().Include(w => w.ProcessStep).Include(w => w.SKU).Include(w => w.Station).Include(w => w.WorkProcess).AsQueryable();

            //return View(workprocessdetails);
            return View();
        }

        // Get :WorkProcessDetails/PageList
        // For Index View Boostrap-Table load  data 
        [HttpGet]
        public ActionResult GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {
            var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
            int totalCount = 0;
            //int pagenum = offset / limit +1;

            var workprocessdetails = _workProcessDetailService.Query(new WorkProcessDetailQuery().Withfilter(filters)).Include(w => w.ProcessStep).Include(w => w.SKU).Include(w => w.Station).Include(w => w.WorkProcess).OrderBy(n => n.OrderBy(sort, order)).SelectPage(page, rows, out totalCount);

            var datarows = workprocessdetails.Select(n => new { ProcessStepOrder = (n.ProcessStep == null ? 0 : n.ProcessStep.Order), ProcessStepStepName = (n.ProcessStep == null ? "" : n.ProcessStep.StepName), SKUSku = (n.SKU == null ? "" : n.SKU.Sku), StationStationNo = (n.Station == null ? "" : n.Station.StationNo), WorkProcessWorkNo = (n.WorkProcess == null ? "" : n.WorkProcess.WorkNo), WorkProcessProjectName = (n.WorkProcess == null ? "" : n.WorkProcess.ProjectName), Id = n.Id, WorkProcessId = n.WorkProcessId, SKUId = n.SKUId, ComponentSKU = n.ComponentSKU, GraphSKU = n.GraphSKU, ProcessStepId = n.ProcessStepId, StepName = n.StepName, StationId = n.StationId, StandardElapsedTime = n.StandardElapsedTime, StartingDateTime = n.StartingDateTime, CompletedDateTime = n.CompletedDateTime, ElapsedTime = n.ElapsedTime, WorkingTime = n.WorkingTime, Operator = n.Operator, QCConfirm = n.QCConfirm, QCConfirmDateTime = n.QCConfirmDateTime, CompletedConfirm = n.CompletedConfirm, Status = n.Status, Remark = n.Remark }).ToList();
            var pagelist = new { total = totalCount, rows = datarows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveData(WorkProcessDetailChangeViewModel workprocessdetails)
        {
            if (workprocessdetails.updated != null)
            {
                foreach (var updated in workprocessdetails.updated)
                {
                    _workProcessDetailService.Update(updated);
                }
            }
            if (workprocessdetails.deleted != null)
            {
                foreach (var deleted in workprocessdetails.deleted)
                {
                    _workProcessDetailService.Delete(deleted);
                }
            }
            if (workprocessdetails.inserted != null)
            {
                foreach (var inserted in workprocessdetails.inserted)
                {
                    _workProcessDetailService.Insert(inserted);
                }
            }
            _unitOfWork.SaveChanges();
            if (workprocessdetails.updated != null)
            {
                this._workProcessService.CompletedWork(workprocessdetails.updated.First().WorkProcessId);
                _unitOfWork.SaveChanges();
            }

            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProcessSteps()
        {
            var processstepRepository = _unitOfWork.Repository<ProcessStep>();
            var data = processstepRepository.Queryable().ToList();
            var rows = data.Select(n => new { Id = n.Id, StepName = n.StepName, Order = n.Order, ElapsedTime = n.ElapsedTime, StationId=n.StationId });
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSKUs()
        {
            var skuRepository = _unitOfWork.Repository<SKU>();
            var data = skuRepository.Queryable().ToList();
            var rows = data.Select(n => new { Id = n.Id, Sku = n.Sku });
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetStations()
        {
            var stationRepository = _unitOfWork.Repository<Station>();
            var data = stationRepository.Queryable().ToList();
            var rows = data.Select(n => new { Id = n.Id, StationNo = n.StationNo });
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetWorkProcesses()
        {
            var workprocessRepository = _unitOfWork.Repository<WorkProcess>();
            var data = workprocessRepository.Queryable().ToList();
            var rows = data.Select(n => new { Id = n.Id, WorkNo = n.WorkNo });
            return Json(rows, JsonRequestBehavior.AllowGet);
        }



        // GET: WorkProcessDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkProcessDetail workProcessDetail = _workProcessDetailService.Find(id);
            if (workProcessDetail == null)
            {
                return HttpNotFound();
            }
            return View(workProcessDetail);
        }


        // GET: WorkProcessDetails/Create
        public ActionResult Create()
        {
            WorkProcessDetail workProcessDetail = new WorkProcessDetail();
            //set default value
            var processstepRepository = _unitOfWork.Repository<ProcessStep>();
            ViewBag.ProcessStepId = new SelectList(processstepRepository.Queryable(), "Id", "StepName");
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku");
            var stationRepository = _unitOfWork.Repository<Station>();
            ViewBag.StationId = new SelectList(stationRepository.Queryable(), "Id", "StationNo");
            var workprocessRepository = _unitOfWork.Repository<WorkProcess>();
            ViewBag.WorkProcessId = new SelectList(workprocessRepository.Queryable(), "Id", "WorkNo");
            return View(workProcessDetail);
        }

        // POST: WorkProcessDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProcessStep,SKU,Station,WorkProcess,Id,WorkProcessId,SKUId,ComponentSKU,GraphSKU,ProcessStepId,StepName,StationId,StandardElapsedTime,StartingDateTime,CompletedDateTime,ElapsedTime,WorkingTime,Operator,QCConfirm,QCConfirmDateTime,CompletedConfirm,Status,Remark,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] WorkProcessDetail workProcessDetail)
        {
            if (ModelState.IsValid)
            {
                _workProcessDetailService.Insert(workProcessDetail);
                _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has append a WorkProcessDetail record");
                return RedirectToAction("Index");
            }

            var processstepRepository = _unitOfWork.Repository<ProcessStep>();
            ViewBag.ProcessStepId = new SelectList(processstepRepository.Queryable(), "Id", "StepName", workProcessDetail.ProcessStepId);
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", workProcessDetail.SKUId);
            var stationRepository = _unitOfWork.Repository<Station>();
            ViewBag.StationId = new SelectList(stationRepository.Queryable(), "Id", "StationNo", workProcessDetail.StationId);
            var workprocessRepository = _unitOfWork.Repository<WorkProcess>();
            ViewBag.WorkProcessId = new SelectList(workprocessRepository.Queryable(), "Id", "WorkNo", workProcessDetail.WorkProcessId);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors = String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(workProcessDetail);
        }

        // GET: WorkProcessDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkProcessDetail workProcessDetail = _workProcessDetailService.Find(id);
            if (workProcessDetail == null)
            {
                return HttpNotFound();
            }
            var processstepRepository = _unitOfWork.Repository<ProcessStep>();
            ViewBag.ProcessStepId = new SelectList(processstepRepository.Queryable(), "Id", "StepName", workProcessDetail.ProcessStepId);
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", workProcessDetail.SKUId);
            var stationRepository = _unitOfWork.Repository<Station>();
            ViewBag.StationId = new SelectList(stationRepository.Queryable(), "Id", "StationNo", workProcessDetail.StationId);
            var workprocessRepository = _unitOfWork.Repository<WorkProcess>();
            ViewBag.WorkProcessId = new SelectList(workprocessRepository.Queryable(), "Id", "WorkNo", workProcessDetail.WorkProcessId);
            return View(workProcessDetail);
        }

        // POST: WorkProcessDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProcessStep,SKU,Station,WorkProcess,Id,WorkProcessId,SKUId,ComponentSKU,GraphSKU,ProcessStepId,StepName,StationId,StandardElapsedTime,StartingDateTime,CompletedDateTime,ElapsedTime,WorkingTime,Operator,QCConfirm,QCConfirmDateTime,CompletedConfirm,Status,Remark,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] WorkProcessDetail workProcessDetail)
        {
            if (ModelState.IsValid)
            {
                workProcessDetail.ObjectState = ObjectState.Modified;
                _workProcessDetailService.Update(workProcessDetail);

                _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has update a WorkProcessDetail record");
                return RedirectToAction("Index");
            }
            var processstepRepository = _unitOfWork.Repository<ProcessStep>();
            ViewBag.ProcessStepId = new SelectList(processstepRepository.Queryable(), "Id", "StepName", workProcessDetail.ProcessStepId);
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", workProcessDetail.SKUId);
            var stationRepository = _unitOfWork.Repository<Station>();
            ViewBag.StationId = new SelectList(stationRepository.Queryable(), "Id", "StationNo", workProcessDetail.StationId);
            var workprocessRepository = _unitOfWork.Repository<WorkProcess>();
            ViewBag.WorkProcessId = new SelectList(workprocessRepository.Queryable(), "Id", "WorkNo", workProcessDetail.WorkProcessId);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors = String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(workProcessDetail);
        }

        // GET: WorkProcessDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkProcessDetail workProcessDetail = _workProcessDetailService.Find(id);
            if (workProcessDetail == null)
            {
                return HttpNotFound();
            }
            return View(workProcessDetail);
        }

        // POST: WorkProcessDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkProcessDetail workProcessDetail = _workProcessDetailService.Find(id);
            _workProcessDetailService.Delete(workProcessDetail);
            _unitOfWork.SaveChanges();
            if (Request.IsAjaxRequest())
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            DisplaySuccessMessage("Has delete a WorkProcessDetail record");
            return RedirectToAction("Index");
        }





        [HttpPost]
        public ActionResult Start(int id = 0)
        {
            this._workProcessDetailService.Start(id);
            _unitOfWork.SaveChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Completed(int id = 0)
        {
            this._workProcessDetailService.Completed(id);
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
