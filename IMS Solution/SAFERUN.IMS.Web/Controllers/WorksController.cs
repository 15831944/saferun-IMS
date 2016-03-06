


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
    public class WorksController : Controller
    {

        //Please RegisterType UnityConfig.cs
        //container.RegisterType<IRepositoryAsync<Work>, Repository<Work>>();
        //container.RegisterType<IWorkService, WorkService>();

        //private ImsDbContext db = new ImsDbContext();
        private readonly IWorkService _workService;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public WorksController(IWorkService workService, IUnitOfWorkAsync unitOfWork)
        {
            _workService = workService;
            _unitOfWork = unitOfWork;
        }

        // GET: Works/Index
        public ActionResult Index()
        {

            //var works  = _workService.Queryable().Include(w => w.Order).Include(w => w.WorkType).AsQueryable();

            //return View(works);
            return View();
        }

        // Get :Works/PageList
        // For Index View Boostrap-Table load  data 
        [HttpGet]
        public ActionResult GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {
            var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
            int totalCount = 0;
            //int pagenum = offset / limit +1;

            var works = _workService.Query(new WorkQuery().Withfilter(filters)).Include(w => w.Order).Include(w => w.WorkType).OrderBy(n => n.OrderBy(sort, order)).SelectPage(page, rows, out totalCount);

            var datarows = works.Select(n => new { OrderProjectName = (n.Order == null ? "" : n.Order.ProjectName), OrderOrderKey = (n.Order == null ? "" : n.Order.OrderKey), WorkTypeName = (n.WorkType == null ? "" : n.WorkType.Name), Id = n.Id, WorkNo = n.WorkNo, WorkTypeId = n.WorkTypeId, OrderKey = n.OrderKey, OrderId = n.OrderId, PO = n.PO, User = n.User, WorkDate = n.WorkDate, Status = n.Status, Review = n.Review, ProductionConfirm = n.ProductionConfirm, OutsourceConfirm = n.OutsourceConfirm, AssembleConfirm = n.AssembleConfirm, PurchaseConfirm = n.PurchaseConfirm, ReviewDate = n.ReviewDate, Remark = n.Remark }).ToList();
            var pagelist = new { total = totalCount, rows = datarows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveData(WorkChangeViewModel works)
        {
            if (works.updated != null)
            {
                foreach (var updated in works.updated)
                {
                    _workService.Update(updated);
                }
            }
            if (works.deleted != null)
            {
                foreach (var deleted in works.deleted)
                {
                    _workService.Delete(deleted);
                }
            }
            if (works.inserted != null)
            {
                foreach (var inserted in works.inserted)
                {
                    _workService.Insert(inserted);
                }
            }
            _unitOfWork.SaveChanges();

            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetOrders()
        {
            var orderRepository = _unitOfWork.Repository<Order>();
            var data = orderRepository.Queryable().ToList();
            var rows = data.Select(n => new { Id = n.Id, OrderKey = n.OrderKey, ProjectName = n.ProjectName, Status = n.Status });
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetWorkTypes()
        {
            var worktypeRepository = _unitOfWork.Repository<WorkType>();
            var data = worktypeRepository.Queryable().ToList();
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


        // GET: Works/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Work work = _workService.Find(id);
            if (work == null)
            {
                return HttpNotFound();
            }
            return View(work);
        }


        // GET: Works/Create
        public ActionResult Create()
        {
            Work work = new Work();
            //set default value
            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey");
            var worktypeRepository = _unitOfWork.Repository<WorkType>();
            ViewBag.WorkTypeId = new SelectList(worktypeRepository.Queryable(), "Id", "Name");
            return View(work);
        }

        // POST: Works/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Order,WorkDetails,WorkType,Id,WorkNo,WorkTypeId,OrderKey,OrderId,PO,User,WorkDate,Status,Review,ProductionConfirm,OutsourceConfirm,AssembleConfirm,PurchaseConfirm,ReviewDate,ProductionConfirmDate,OutsourceConfirmDate,AssembleConfirmDate,PurchaseConfirmDate,Remark,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] Work work)
        {
            if (ModelState.IsValid)
            {
                work.ObjectState = ObjectState.Added;
                foreach (var item in work.WorkDetails)
                {
                    item.WorkId = work.Id;
                    item.ObjectState = ObjectState.Added;
                }
                _workService.InsertOrUpdateGraph(work);
                _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has append a Work record");
                return RedirectToAction("Index");
            }

            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey", work.OrderId);
            var worktypeRepository = _unitOfWork.Repository<WorkType>();
            ViewBag.WorkTypeId = new SelectList(worktypeRepository.Queryable(), "Id", "Name", work.WorkTypeId);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors = String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(work);
        }

        // GET: Works/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Work work = _workService.Find(id);
            if (work == null)
            {
                return HttpNotFound();
            }
            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey", work.OrderId);
            var worktypeRepository = _unitOfWork.Repository<WorkType>();
            ViewBag.WorkTypeId = new SelectList(worktypeRepository.Queryable(), "Id", "Name", work.WorkTypeId);
            return View(work);
        }

        // POST: Works/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Order,WorkDetails,WorkType,Id,WorkNo,WorkTypeId,OrderKey,OrderId,PO,User,WorkDate,Status,Review,ProductionConfirm,OutsourceConfirm,AssembleConfirm,PurchaseConfirm,ReviewDate,ProductionConfirmDate,OutsourceConfirmDate,AssembleConfirmDate,PurchaseConfirmDate,Remark,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] Work work)
        {
            if (ModelState.IsValid)
            {
                work.ObjectState = ObjectState.Modified;
                foreach (var item in work.WorkDetails)
                {
                    item.WorkId = work.Id;
                    //set ObjectState with conditions
                    if (item.Id <= 0)
                        item.ObjectState = ObjectState.Added;
                    else
                        item.ObjectState = ObjectState.Modified;
                }

                _workService.InsertOrUpdateGraph(work);

                _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has update a Work record");
                return RedirectToAction("Index");
            }
            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey", work.OrderId);
            var worktypeRepository = _unitOfWork.Repository<WorkType>();
            ViewBag.WorkTypeId = new SelectList(worktypeRepository.Queryable(), "Id", "Name", work.WorkTypeId);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors = String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n => n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(work);
        }

        // GET: Works/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Work work = _workService.Find(id);
            if (work == null)
            {
                return HttpNotFound();
            }
            return View(work);
        }

        // POST: Works/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Work work = _workService.Find(id);
            _workService.Delete(work);
            _unitOfWork.SaveChanges();
            if (Request.IsAjaxRequest())
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            DisplaySuccessMessage("Has delete a Work record");
            return RedirectToAction("Index");
        }


        // Get Detail Row By Id For Edit
        // Get : Works/EditWorkDetail/:id
        [HttpGet]
        public ActionResult EditWorkDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var workdetailRepository = _unitOfWork.Repository<WorkDetail>();
            var workdetail = workdetailRepository.Find(id);

            var skuRepository = _unitOfWork.Repository<SKU>();
            //var skuRepository = _unitOfWork.Repository<SKU>();             
            var workRepository = _unitOfWork.Repository<Work>();

            if (workdetail == null)
            {
                ViewBag.ComponentSKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku");
                ViewBag.ParentSKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku");
                ViewBag.WorkId = new SelectList(workRepository.Queryable(), "Id", "WorkNo");

                //return HttpNotFound();
                return PartialView("_WorkDetailEditForm", new WorkDetail());
            }
            else
            {
                ViewBag.ComponentSKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", workdetail.ComponentSKUId);
                ViewBag.ParentSKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", workdetail.ParentSKUId);
                ViewBag.WorkId = new SelectList(workRepository.Queryable(), "Id", "WorkNo", workdetail.WorkId);

            }
            return PartialView("_WorkDetailEditForm", workdetail);

        }

        // Get Create Row By Id For Edit
        // Get : Works/CreateWorkDetail
        [HttpGet]
        public ActionResult CreateWorkDetail()
        {
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.ComponentSKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku");
            //var skuRepository = _unitOfWork.Repository<SKU>();    
            ViewBag.ParentSKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku");
            var workRepository = _unitOfWork.Repository<Work>();
            ViewBag.WorkId = new SelectList(workRepository.Queryable(), "Id", "WorkNo");
            return PartialView("_WorkDetailEditForm");

        }

        // Post Delete Detail Row By Id
        // Get : Works/DeleteWorkDetail/:id
        [HttpPost, ActionName("DeleteWorkDetail")]
        public ActionResult DeleteWorkDetailConfirmed(int id)
        {
            var workdetailRepository = _unitOfWork.Repository<WorkDetail>();
            workdetailRepository.Delete(id);
            _unitOfWork.SaveChanges();
            if (Request.IsAjaxRequest())
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            DisplaySuccessMessage("Has delete a Order record");
            return RedirectToAction("Index");
        }



        // Get : Works/GetWorkDetailsByWorkId/:id
        [HttpGet]
        public ActionResult GetWorkDetailsByWorkId(int id)
        {
            var workdetails = _workService.GetWorkDetailsByWorkId(id);
            if (Request.IsAjaxRequest())
            {
                return Json(workdetails.Select(n => new { ComponentSKUSku = (n.ComponentSKU == null ? "" : n.ComponentSKU.Sku), ParentSKUSku = (n.ParentSKU == null ? "" : n.ParentSKU.Sku), WorkWorkNo = (n.Work == null ? "" : n.Work.WorkNo), Id = n.Id, WorkNo = n.WorkNo, WorkId = n.WorkId, ParentSKUId = n.ParentSKUId, ComponentSKUId = n.ComponentSKUId, GraphSKU = n.GraphSKU, GraphVer = n.GraphVer, ConsumeQty = n.ConsumeQty, StockQty = n.StockQty, RequirementQty = n.RequirementQty, Brand = n.Brand, Process = n.Process, Responsibility = n.Responsibility, AltOrderProdctionDate = n.AltOrderProdctionDate, AltProdctionDate1 = n.AltProdctionDate1, ActualProdctionDate1 = n.ActualProdctionDate1, AltProdctionDate2 = n.AltProdctionDate2, ActualProdctionDate2 = n.ActualProdctionDate2, AltProdctionDate3 = n.AltProdctionDate3, ActualProdctionDate3 = n.ActualProdctionDate3, AltProdctionDate4 = n.AltProdctionDate4, ActualProdctionDate4 = n.ActualProdctionDate4, AltProdctionDate5 = n.AltProdctionDate5, ActualProdctionDate5 = n.ActualProdctionDate5, ConfirmUser = n.ConfirmUser, Remark1 = n.Remark1, Remark2 = n.Remark2 }), JsonRequestBehavior.AllowGet);
            }
            return View(workdetails);

        }
        
        [HttpPost]
        public ActionResult GenerateWorkDetail(WorkGeneratorViewModel viewmodel)
        {
            _workService.GenerateWorkDetail(viewmodel);
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
