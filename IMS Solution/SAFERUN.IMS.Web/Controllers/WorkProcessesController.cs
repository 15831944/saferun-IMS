


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
    public class WorkProcessesController : Controller
    {

        //Please RegisterType UnityConfig.cs
        //container.RegisterType<IRepositoryAsync<WorkProcess>, Repository<WorkProcess>>();
        //container.RegisterType<IWorkProcessService, WorkProcessService>();

        //private ImsDbContext db = new ImsDbContext();
        private readonly IWorkProcessService _workProcessService;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public WorkProcessesController(IWorkProcessService workProcessService, IUnitOfWorkAsync unitOfWork)
        {
            _workProcessService = workProcessService;
            _unitOfWork = unitOfWork;
        }

        // GET: WorkProcesses/Index
        public ActionResult Index()
        {

            //var workprocesses  = _workProcessService.Queryable().Include(w => w.Customer).Include(w => w.Order).Include(w => w.ProductionProcess).Include(w => w.SKU).Include(w => w.Work).Include(w => w.WorkDetail).AsQueryable();

            //return View(workprocesses);
            return View();
        }

        // Get :WorkProcesses/PageList
        // For Index View Boostrap-Table load  data 
        [HttpGet]
        public ActionResult GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {
            var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
            int totalCount = 0;
            //int pagenum = offset / limit +1;

            var workprocesses = _workProcessService.Query(new WorkProcessQuery().Withfilter(filters)).Include(w => w.Customer).Include(w => w.Order).Include(w => w.ProductionProcess).Include(w => w.SKU).Include(w => w.Work).Include(w => w.WorkDetail).OrderBy(n => n.OrderBy(sort, order)).SelectPage(page, rows, out totalCount);

            var datarows = workprocesses.Select(n => new { CustomerAccountNumber = (n.Customer == null ? "" : n.Customer.AccountNumber), OrderOrderKey = (n.Order == null ? "" : n.Order.OrderKey), ProductionProcessName = (n.ProductionProcess == null ? "" : n.ProductionProcess.Name), SKUSku = (n.SKU == null ? "" : n.SKU.Sku), WorkWorkNo = (n.Work == null ? "" : n.Work.WorkNo), WorkDetailWorkNo = (n.WorkDetail == null ? "" : n.WorkDetail.WorkNo), Id = n.Id, WorkNo = n.WorkNo, WorkId = n.WorkId, OrderId = n.OrderId, OrderKey = n.OrderKey, ProjectName = n.ProjectName, SKUId = n.SKUId, GraphSKU = n.GraphSKU, RequirementQty = n.RequirementQty, ProductionQty = n.ProductionQty, FinishedQty = n.FinishedQty, WorkItems = n.WorkItems, ProductionProcessId = n.ProductionProcessId, Status = n.Status, Operator = n.Operator, WorkDate = n.WorkDate, Remark = n.Remark, WorkDetailId = n.WorkDetailId, CustomerId = n.CustomerId }).ToList();
            var pagelist = new { total = totalCount, rows = datarows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveData(WorkProcessChangeViewModel workprocesses)
        {
            if (workprocesses.updated != null)
            {
                foreach (var updated in workprocesses.updated)
                {
                    _workProcessService.Update(updated);
                }
            }
            if (workprocesses.deleted != null)
            {
                foreach (var deleted in workprocesses.deleted)
                {
                    _workProcessService.Delete(deleted);
                }
            }
            if (workprocesses.inserted != null)
            {
                foreach (var inserted in workprocesses.inserted)
                {
                    _workProcessService.Insert(inserted);
                }
            }
            _unitOfWork.SaveChanges();

            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCustomers()
        {
            var customerRepository = _unitOfWork.Repository<Customer>();
            var data = customerRepository.Queryable().ToList();
            var rows = data.Select(n => new { Id = n.Id, AccountNumber = n.AccountNumber });
            return Json(rows, JsonRequestBehavior.AllowGet);
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
        public ActionResult GetWorks()
        {
            var workRepository = _unitOfWork.Repository<Work>();
            var data = workRepository.Queryable().ToList();
            var rows = data.Select(n => new { Id = n.Id, WorkNo = n.WorkNo });
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetWorkDetails()
        {
            var workdetailRepository = _unitOfWork.Repository<WorkDetail>();
            var data = workdetailRepository.Queryable().ToList();
            var rows = data.Select(n => new { Id = n.Id, WorkNo = n.WorkNo });
            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProcessSteps()
        {
            var processstepRepository = _unitOfWork.Repository<ProcessStep>();
            var data = processstepRepository.Queryable().ToList();
            var rows = data.Select(n => new { Id = n.Id, StepName = n.StepName });
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


        // GET: WorkProcesses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkProcess workProcess = _workProcessService.Find(id);
            if (workProcess == null)
            {
                return HttpNotFound();
            }
            return View(workProcess);
        }


        // GET: WorkProcesses/Create
        public ActionResult Create()
        {
            WorkProcess workProcess = new WorkProcess();
            //set default value
            var customerRepository = _unitOfWork.Repository<Customer>();
            ViewBag.CustomerId = new SelectList(customerRepository.Queryable(), "Id", "AccountNumber");
            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey");
            var productionprocessRepository = _unitOfWork.Repository<ProductionProcess>();
            ViewBag.ProductionProcessId = new SelectList(productionprocessRepository.Queryable(), "Id", "Name");
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku");
            var workRepository = _unitOfWork.Repository<Work>();
            ViewBag.WorkId = new SelectList(workRepository.Queryable(), "Id", "WorkNo");
            var workdetailRepository = _unitOfWork.Repository<WorkDetail>();
            ViewBag.WorkDetailId = new SelectList(workdetailRepository.Queryable(), "Id", "WorkNo");
            return View(workProcess);
        }

        // POST: WorkProcesses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Customer,Order,ProductionProcess,SKU,Work,WorkDetail,WorkProcessDetails,Id,WorkNo,WorkId,OrderId,OrderKey,ProjectName,SKUId,GraphSKU,RequirementQty,ProductionQty,FinishedQty,WorkItems,ProductionProcessId,Status,Operator,WorkDate,Remark,WorkDetailId,CustomerId,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] WorkProcess workProcess)
        {
            if (ModelState.IsValid)
            {
                workProcess.ObjectState = ObjectState.Added;
                foreach (var item in workProcess.WorkProcessDetails)
                {
                    item.WorkProcessId = workProcess.Id;
                    item.ObjectState = ObjectState.Added;
                }
                _workProcessService.InsertOrUpdateGraph(workProcess);
                _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has append a WorkProcess record");
                return RedirectToAction("Index");
            }

            var customerRepository = _unitOfWork.Repository<Customer>();
            ViewBag.CustomerId = new SelectList(customerRepository.Queryable(), "Id", "AccountNumber", workProcess.CustomerId);
            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey", workProcess.OrderId);
            var productionprocessRepository = _unitOfWork.Repository<ProductionProcess>();
            ViewBag.ProductionProcessId = new SelectList(productionprocessRepository.Queryable(), "Id", "Name", workProcess.ProductionProcessId);
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", workProcess.SKUId);
            var workRepository = _unitOfWork.Repository<Work>();
            ViewBag.WorkId = new SelectList(workRepository.Queryable(), "Id", "WorkNo", workProcess.WorkId);
            var workdetailRepository = _unitOfWork.Repository<WorkDetail>();
            ViewBag.WorkDetailId = new SelectList(workdetailRepository.Queryable(), "Id", "WorkNo", workProcess.WorkDetailId);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors = String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(workProcess);
        }

        // GET: WorkProcesses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkProcess workProcess = _workProcessService.Find(id);
            if (workProcess == null)
            {
                return HttpNotFound();
            }
            var customerRepository = _unitOfWork.Repository<Customer>();
            ViewBag.CustomerId = new SelectList(customerRepository.Queryable(), "Id", "AccountNumber", workProcess.CustomerId);
            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey", workProcess.OrderId);
            var productionprocessRepository = _unitOfWork.Repository<ProductionProcess>();
            ViewBag.ProductionProcessId = new SelectList(productionprocessRepository.Queryable(), "Id", "Name", workProcess.ProductionProcessId);
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", workProcess.SKUId);
            var workRepository = _unitOfWork.Repository<Work>();
            ViewBag.WorkId = new SelectList(workRepository.Queryable(), "Id", "WorkNo", workProcess.WorkId);
            var workdetailRepository = _unitOfWork.Repository<WorkDetail>();
            ViewBag.WorkDetailId = new SelectList(workdetailRepository.Queryable(), "Id", "WorkNo", workProcess.WorkDetailId);
            return View(workProcess);
        }

        // POST: WorkProcesses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Customer,Order,ProductionProcess,SKU,Work,WorkDetail,WorkProcessDetails,Id,WorkNo,WorkId,OrderId,OrderKey,ProjectName,SKUId,GraphSKU,RequirementQty,ProductionQty,FinishedQty,WorkItems,ProductionProcessId,Status,Operator,WorkDate,Remark,WorkDetailId,CustomerId,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] WorkProcess workProcess)
        {
            if (ModelState.IsValid)
            {
                workProcess.ObjectState = ObjectState.Modified;
                foreach (var item in workProcess.WorkProcessDetails)
                {
                    item.WorkProcessId = workProcess.Id;
                    //set ObjectState with conditions
                    if (item.Id <= 0)
                        item.ObjectState = ObjectState.Added;
                    else
                        item.ObjectState = ObjectState.Modified;
                }

                _workProcessService.InsertOrUpdateGraph(workProcess);

                _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has update a WorkProcess record");
                return RedirectToAction("Index");
            }
            var customerRepository = _unitOfWork.Repository<Customer>();
            ViewBag.CustomerId = new SelectList(customerRepository.Queryable(), "Id", "AccountNumber", workProcess.CustomerId);
            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey", workProcess.OrderId);
            var productionprocessRepository = _unitOfWork.Repository<ProductionProcess>();
            ViewBag.ProductionProcessId = new SelectList(productionprocessRepository.Queryable(), "Id", "Name", workProcess.ProductionProcessId);
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", workProcess.SKUId);
            var workRepository = _unitOfWork.Repository<Work>();
            ViewBag.WorkId = new SelectList(workRepository.Queryable(), "Id", "WorkNo", workProcess.WorkId);
            var workdetailRepository = _unitOfWork.Repository<WorkDetail>();
            ViewBag.WorkDetailId = new SelectList(workdetailRepository.Queryable(), "Id", "WorkNo", workProcess.WorkDetailId);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors = String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(workProcess);
        }

        // GET: WorkProcesses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkProcess workProcess = _workProcessService.Find(id);
            if (workProcess == null)
            {
                return HttpNotFound();
            }
            return View(workProcess);
        }

        // POST: WorkProcesses/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkProcess workProcess = _workProcessService.Find(id);
            _workProcessService.Delete(workProcess);
            _unitOfWork.SaveChanges();
            if (Request.IsAjaxRequest())
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            DisplaySuccessMessage("Has delete a WorkProcess record");
            return RedirectToAction("Index");
        }


        // Get Detail Row By Id For Edit
        // Get : WorkProcesses/EditWorkProcessDetail/:id
        [HttpGet]
        public ActionResult EditWorkProcessDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var workprocessdetailRepository = _unitOfWork.Repository<WorkProcessDetail>();
            var workprocessdetail = workprocessdetailRepository.Find(id);

            var processstepRepository = _unitOfWork.Repository<ProcessStep>();
            var stationRepository = _unitOfWork.Repository<Station>();
            var workprocessRepository = _unitOfWork.Repository<WorkProcess>();

            if (workprocessdetail == null)
            {
                ViewBag.ProcessStepId = new SelectList(processstepRepository.Queryable(), "Id", "StepName");
                ViewBag.StationId = new SelectList(stationRepository.Queryable(), "Id", "StationNo");
                ViewBag.WorkProcessId = new SelectList(workprocessRepository.Queryable(), "Id", "WorkNo");

                //return HttpNotFound();
                return PartialView("_WorkProcessDetailEditForm", new WorkProcessDetail());
            }
            else
            {
                ViewBag.ProcessStepId = new SelectList(processstepRepository.Queryable(), "Id", "StepName", workprocessdetail.ProcessStepId);
                ViewBag.StationId = new SelectList(stationRepository.Queryable(), "Id", "StationNo", workprocessdetail.StationId);
                ViewBag.WorkProcessId = new SelectList(workprocessRepository.Queryable(), "Id", "WorkNo", workprocessdetail.WorkProcessId);

            }
            return PartialView("_WorkProcessDetailEditForm", workprocessdetail);

        }

        // Get Create Row By Id For Edit
        // Get : WorkProcesses/CreateWorkProcessDetail
        [HttpGet]
        public ActionResult CreateWorkProcessDetail()
        {
            var processstepRepository = _unitOfWork.Repository<ProcessStep>();
            ViewBag.ProcessStepId = new SelectList(processstepRepository.Queryable(), "Id", "StepName");
            var stationRepository = _unitOfWork.Repository<Station>();
            ViewBag.StationId = new SelectList(stationRepository.Queryable(), "Id", "StationNo");
            var workprocessRepository = _unitOfWork.Repository<WorkProcess>();
            ViewBag.WorkProcessId = new SelectList(workprocessRepository.Queryable(), "Id", "WorkNo");
            return PartialView("_WorkProcessDetailEditForm");

        }

        // Post Delete Detail Row By Id
        // Get : WorkProcesses/DeleteWorkProcessDetail/:id
        [HttpPost, ActionName("DeleteWorkProcessDetail")]
        public ActionResult DeleteWorkProcessDetailConfirmed(int id)
        {
            var workprocessdetailRepository = _unitOfWork.Repository<WorkProcessDetail>();
            workprocessdetailRepository.Delete(id);
            _unitOfWork.SaveChanges();
            if (Request.IsAjaxRequest())
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            DisplaySuccessMessage("Has delete a Order record");
            return RedirectToAction("Index");
        }



        // Get : WorkProcesses/GetWorkProcessDetailsByWorkProcessId/:id
        [HttpGet]
        public ActionResult GetWorkProcessDetailsByWorkProcessId(int id)
        {
            var workprocessdetails = _workProcessService.GetWorkProcessDetailsByWorkProcessId(id);
            if (Request.IsAjaxRequest())
            {
                return Json(workprocessdetails.Select(n => new { ProcessStepStepName = (n.ProcessStep == null ? "" : n.ProcessStep.StepName), StationStationNo = (n.Station == null ? "" : n.Station.StationNo), WorkProcessWorkNo = (n.WorkProcess == null ? "" : n.WorkProcess.WorkNo), Id = n.Id, WorkProcessId = n.WorkProcessId, ProcessStepId = n.ProcessStepId, StepName = n.StepName, StationId = n.StationId, StandardElapsedTime = n.StandardElapsedTime, StartingDateTime = n.StartingDateTime, CompletedDateTime = n.CompletedDateTime, ElapsedTime = n.ElapsedTime, WorkingTime = n.WorkingTime, Operator = n.Operator, QCConfirm = n.QCConfirm, QCConfirmDateTime = n.QCConfirmDateTime, CompletedConfirm = n.CompletedConfirm, Status = n.Status, Remark = n.Remark }), JsonRequestBehavior.AllowGet);
            }
            return View(workprocessdetails);

        }

        [HttpPost]
        public ActionResult GenerateWorkProceesses(IEnumerable<WorkDetail> workdetails)
        {
            _workProcessService.GenerateWorkProcesses(workdetails);
            _unitOfWork.SaveChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GenerateProcessDetails(WorkProcess process) {

            _workProcessService.GenerateWorkProcesses( process);
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
