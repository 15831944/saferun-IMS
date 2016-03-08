


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
    public class ProductionSchedulesController : Controller
    {
        
        //Please RegisterType UnityConfig.cs
        //container.RegisterType<IRepositoryAsync<ProductionSchedule>, Repository<ProductionSchedule>>();
        //container.RegisterType<IProductionScheduleService, ProductionScheduleService>();
        
        //private ImsDbContext db = new ImsDbContext();
        private readonly IProductionScheduleService  _productionScheduleService;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public ProductionSchedulesController (IProductionScheduleService  productionScheduleService, IUnitOfWorkAsync unitOfWork)
        {
            _productionScheduleService  = productionScheduleService;
            _unitOfWork = unitOfWork;
        }

        // GET: ProductionSchedules/Index
        public ActionResult Index()
        {
            
            //var productionschedules  = _productionScheduleService.Queryable().Include(p => p.Customer).Include(p => p.Work).AsQueryable();
            
             //return View(productionschedules);
			 return View();
        }

        // Get :ProductionSchedules/PageList
        // For Index View Boostrap-Table load  data 
        [HttpGet]
        public ActionResult GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
            int totalCount = 0;
            //int pagenum = offset / limit +1;
            			 
            var productionschedules  = _productionScheduleService.Query(new ProductionScheduleQuery().Withfilter(filters)).Include(p => p.Customer).Include(p => p.Work).OrderBy(n=>n.OrderBy(sort,order)).SelectPage(page, rows, out totalCount);
            
                        var datarows = productionschedules .Select(  n => new { WorkWorkNo = (n.Work==null?"": n.Work.WorkNo) , Id = n.Id , ScheduleNo = n.ScheduleNo , WorkId = n.WorkId , OrderKey = n.OrderKey , OrderDate = n.OrderDate , BeginDate = n.BeginDate , CompletedDate = n.CompletedDate , Ower = n.Ower , ScheduleDate = n.ScheduleDate , Status = n.Status , Remark = n.Remark }).ToList();
            var pagelist = new { total = totalCount, rows = datarows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }

		[HttpPost]
		public ActionResult SaveData(ProductionScheduleChangeViewModel productionschedules)
        {
            if (productionschedules.updated != null)
            {
                foreach (var updated in productionschedules.updated)
                {
                    _productionScheduleService.Update(updated);
                }
            }
            if (productionschedules.deleted != null)
            {
                foreach (var deleted in productionschedules.deleted)
                {
                    _productionScheduleService.Delete(deleted);
                }
            }
            if (productionschedules.inserted != null)
            {
                foreach (var inserted in productionschedules.inserted)
                {
                    _productionScheduleService.Insert(inserted);
                }
            }
            _unitOfWork.SaveChanges();

            return Json(new {Success=true}, JsonRequestBehavior.AllowGet);
        }

				public ActionResult GetCustomers()
        {
            var customerRepository = _unitOfWork.Repository<Customer>();
            var data = customerRepository.Queryable().ToList();
            var rows = data.Select(n => new { Id = n.Id, AccountNumber = n.AccountNumber });
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
				public ActionResult GetWorks()
        {
            var workRepository = _unitOfWork.Repository<Work>();
            var data = workRepository.Queryable().ToList();
            var rows = data.Select(n => new { Id = n.Id, WorkNo = n.WorkNo });
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
		
		
       
        // GET: ProductionSchedules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductionSchedule productionSchedule = _productionScheduleService.Find(id);
            if (productionSchedule == null)
            {
                return HttpNotFound();
            }
            return View(productionSchedule);
        }
        

        // GET: ProductionSchedules/Create
        public ActionResult Create()
        {
            ProductionSchedule productionSchedule = new ProductionSchedule();
            //set default value
            var customerRepository = _unitOfWork.Repository<Customer>();
            ViewBag.CustomerId = new SelectList(customerRepository.Queryable(), "Id", "AccountNumber");
            var workRepository = _unitOfWork.Repository<Work>();
            ViewBag.WorkId = new SelectList(workRepository.Queryable(), "Id", "WorkNo");
            return View(productionSchedule);
        }

        // POST: ProductionSchedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Customer,ScheduleDetails,Work,Id,ScheduleNo,WorkId,OrderKey,OrderDate,BeginDate,CompletedDate,Ower,ScheduleDate,Status,Remark,CustomerId,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] ProductionSchedule productionSchedule)
        {
            if (ModelState.IsValid)
            {
             				_productionScheduleService.Insert(productionSchedule);
                           _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has append a ProductionSchedule record");
                return RedirectToAction("Index");
            }

            var customerRepository = _unitOfWork.Repository<Customer>();
            ViewBag.CustomerId = new SelectList(customerRepository.Queryable(), "Id", "AccountNumber", productionSchedule.CustomerId);
            var workRepository = _unitOfWork.Repository<Work>();
            ViewBag.WorkId = new SelectList(workRepository.Queryable(), "Id", "WorkNo", productionSchedule.WorkId);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(productionSchedule);
        }

        // GET: ProductionSchedules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductionSchedule productionSchedule = _productionScheduleService.Find(id);
            if (productionSchedule == null)
            {
                return HttpNotFound();
            }
            var customerRepository = _unitOfWork.Repository<Customer>();
            ViewBag.CustomerId = new SelectList(customerRepository.Queryable(), "Id", "AccountNumber", productionSchedule.CustomerId);
            var workRepository = _unitOfWork.Repository<Work>();
            ViewBag.WorkId = new SelectList(workRepository.Queryable(), "Id", "WorkNo", productionSchedule.WorkId);
            return View(productionSchedule);
        }

        // POST: ProductionSchedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Customer,ScheduleDetails,Work,Id,ScheduleNo,WorkId,OrderKey,OrderDate,BeginDate,CompletedDate,Ower,ScheduleDate,Status,Remark,CustomerId,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] ProductionSchedule productionSchedule)
        {
            if (ModelState.IsValid)
            {
                productionSchedule.ObjectState = ObjectState.Modified;
                				_productionScheduleService.Update(productionSchedule);
                                
                _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has update a ProductionSchedule record");
                return RedirectToAction("Index");
            }
            var customerRepository = _unitOfWork.Repository<Customer>();
            ViewBag.CustomerId = new SelectList(customerRepository.Queryable(), "Id", "AccountNumber", productionSchedule.CustomerId);
            var workRepository = _unitOfWork.Repository<Work>();
            ViewBag.WorkId = new SelectList(workRepository.Queryable(), "Id", "WorkNo", productionSchedule.WorkId);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(productionSchedule);
        }

        // GET: ProductionSchedules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductionSchedule productionSchedule = _productionScheduleService.Find(id);
            if (productionSchedule == null)
            {
                return HttpNotFound();
            }
            return View(productionSchedule);
        }

        // POST: ProductionSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductionSchedule productionSchedule =  _productionScheduleService.Find(id);
             _productionScheduleService.Delete(productionSchedule);
            _unitOfWork.SaveChanges();
           if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            DisplaySuccessMessage("Has delete a ProductionSchedule record");
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
