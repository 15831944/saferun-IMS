


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
    public class RepairJobsController : Controller
    {
        
        //Please RegisterType UnityConfig.cs
        //container.RegisterType<IRepositoryAsync<RepairJob>, Repository<RepairJob>>();
        //container.RegisterType<IRepairJobService, RepairJobService>();
        
        //private ImsDbContext db = new ImsDbContext();
        private readonly IRepairJobService  _repairJobService;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public RepairJobsController (IRepairJobService  repairJobService, IUnitOfWorkAsync unitOfWork)
        {
            _repairJobService  = repairJobService;
            _unitOfWork = unitOfWork;
        }

        // GET: RepairJobs/Index
        public ActionResult Index()
        {
            
            //var repairjobs  = _repairJobService.Queryable().Include(r => r.Order).Include(r => r.SKU).AsQueryable();
            
             //return View(repairjobs);
			 return View();
        }

        // Get :RepairJobs/PageList
        // For Index View Boostrap-Table load  data 
        [HttpGet]
        public ActionResult GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
            int totalCount = 0;
            //int pagenum = offset / limit +1;
            			 
            var repairjobs  = _repairJobService.Query(new RepairJobQuery().Withfilter(filters)).Include(r => r.Order).Include(r => r.SKU).OrderBy(n=>n.OrderBy(sort,order)).SelectPage(page, rows, out totalCount);
            
                        var datarows = repairjobs .Select(  n => new { OrderOrderKey = (n.Order==null?"": n.Order.OrderKey) ,SKUSku = (n.SKU==null?"": n.SKU.Sku) , Id = n.Id , OrderKey = n.OrderKey , ProjectName = n.ProjectName , SKUId = n.SKUId , GraphSKU = n.GraphSKU , DesignName = n.DesignName , RepairQty = n.RepairQty , Issue = n.Issue , ProcessName = n.ProcessName , ResponsibleDepartment = n.ResponsibleDepartment , Owner = n.Owner , StartDateTime = n.StartDateTime , CompletedDateTime = n.CompletedDateTime , ElapsedTime = n.ElapsedTime , Status = n.Status , Remark = n.Remark , OrderId = n.OrderId }).ToList();
            var pagelist = new { total = totalCount, rows = datarows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }

		[HttpPost]
		public ActionResult SaveData(RepairJobChangeViewModel repairjobs)
        {
            if (repairjobs.updated != null)
            {
                foreach (var updated in repairjobs.updated)
                {
                    _repairJobService.Update(updated);
                }
            }
            if (repairjobs.deleted != null)
            {
                foreach (var deleted in repairjobs.deleted)
                {
                    _repairJobService.Delete(deleted);
                }
            }
            if (repairjobs.inserted != null)
            {
                foreach (var inserted in repairjobs.inserted)
                {
                    _repairJobService.Insert(inserted);
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
		
		
       
        // GET: RepairJobs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RepairJob repairJob = _repairJobService.Find(id);
            if (repairJob == null)
            {
                return HttpNotFound();
            }
            return View(repairJob);
        }
        

        // GET: RepairJobs/Create
        public ActionResult Create()
        {
            RepairJob repairJob = new RepairJob();
            //set default value
            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey");
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku");
            return View(repairJob);
        }

        // POST: RepairJobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Order,SKU,Id,OrderKey,ProjectName,SKUId,GraphSKU,DesignName,RepairQty,Issue,ProcessName,ResponsibleDepartment,Owner,StartDateTime,CompletedDateTime,ElapsedTime,Status,Remark,OrderId,BomComponentId,ParentBomComponentId")] RepairJob repairJob)
        {
            if (ModelState.IsValid)
            {
             				_repairJobService.Insert(repairJob);
                           _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has append a RepairJob record");
                return RedirectToAction("Index");
            }

            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey", repairJob.OrderId);
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", repairJob.SKUId);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(repairJob);
        }

        // GET: RepairJobs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RepairJob repairJob = _repairJobService.Find(id);
            if (repairJob == null)
            {
                return HttpNotFound();
            }
            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey", repairJob.OrderId);
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", repairJob.SKUId);
            return View(repairJob);
        }

        // POST: RepairJobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Order,SKU,Id,OrderKey,ProjectName,SKUId,GraphSKU,DesignName,RepairQty,Issue,ProcessName,ResponsibleDepartment,Owner,StartDateTime,CompletedDateTime,ElapsedTime,Status,Remark,OrderId,BomComponentId,ParentBomComponentId")] RepairJob repairJob)
        {
            if (ModelState.IsValid)
            {
                repairJob.ObjectState = ObjectState.Modified;
                				_repairJobService.Update(repairJob);
                                
                _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has update a RepairJob record");
                return RedirectToAction("Index");
            }
            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey", repairJob.OrderId);
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", repairJob.SKUId);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(repairJob);
        }

        // GET: RepairJobs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RepairJob repairJob = _repairJobService.Find(id);
            if (repairJob == null)
            {
                return HttpNotFound();
            }
            return View(repairJob);
        }

        // POST: RepairJobs/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RepairJob repairJob =  _repairJobService.Find(id);
             _repairJobService.Delete(repairJob);
            _unitOfWork.SaveChanges();
           if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            DisplaySuccessMessage("Has delete a RepairJob record");
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
