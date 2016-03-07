


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
    public class ScheduleDetailsController : Controller
    {
        
        //Please RegisterType UnityConfig.cs
        //container.RegisterType<IRepositoryAsync<ScheduleDetail>, Repository<ScheduleDetail>>();
        //container.RegisterType<IScheduleDetailService, ScheduleDetailService>();
        
        //private ImsDbContext db = new ImsDbContext();
        private readonly IScheduleDetailService  _scheduleDetailService;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public ScheduleDetailsController (IScheduleDetailService  scheduleDetailService, IUnitOfWorkAsync unitOfWork)
        {
            _scheduleDetailService  = scheduleDetailService;
            _unitOfWork = unitOfWork;
        }

        // GET: ScheduleDetails/Index
        public ActionResult Index()
        {
            
            //var scheduledetails  = _scheduleDetailService.Queryable().Include(s => s.ComponentSKU).Include(s => s.ParentSKU).Include(s => s.ProductionSchedule).Include(s => s.Shift).Include(s => s.Station).AsQueryable();
            
             //return View(scheduledetails);
			 return View();
        }

        // Get :ScheduleDetails/PageList
        // For Index View Boostrap-Table load  data 
        [HttpGet]
        public ActionResult GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
            int totalCount = 0;
            //int pagenum = offset / limit +1;
            			 
            var scheduledetails  = _scheduleDetailService.Query(new ScheduleDetailQuery().Withfilter(filters)).Include(s => s.ComponentSKU).Include(s => s.ParentSKU).Include(s => s.ProductionSchedule).Include(s => s.Shift).Include(s => s.Station).OrderBy(n=>n.OrderBy(sort,order)).SelectPage(page, rows, out totalCount);
            
                        var datarows = scheduledetails .Select(  n => new { ComponentSKUSku = (n.ComponentSKU==null?"": n.ComponentSKU.Sku) ,ParentSKUSku = (n.ParentSKU==null?"": n.ParentSKU.Sku) ,ProductionScheduleScheduleNo = (n.ProductionSchedule==null?"": n.ProductionSchedule.ScheduleNo) ,ShiftName = (n.Shift==null?"": n.Shift.Name) ,StationStationNo = (n.Station==null?"": n.Station.StationNo) , Id = n.Id , ScheduleNo = n.ScheduleNo , WorkNo = n.WorkNo , ParentSKUId = n.ParentSKUId , ComponentSKUId = n.ComponentSKUId , GraphSKU = n.GraphSKU , GraphVer = n.GraphVer , ConsumeQty = n.ConsumeQty , StockQty = n.StockQty , RequirementQty = n.RequirementQty , ScheduleProductionQty = n.ScheduleProductionQty , ActualProductionQty = n.ActualProductionQty , StationId = n.StationId , ShiftId = n.ShiftId , Operator = n.Operator , AltProdctionDate1 = n.AltProdctionDate1 , ActualProdctionDate1 = n.ActualProdctionDate1 , AltProdctionDate2 = n.AltProdctionDate2 , ActualProdctionDate2 = n.ActualProdctionDate2 , AltProdctionDate3 = n.AltProdctionDate3 , ActualProdctionDate3 = n.ActualProdctionDate3 , AltConsumeTime = n.AltConsumeTime , ActualConsumeTime = n.ActualConsumeTime , Remark1 = n.Remark1 , Remark2 = n.Remark2 , ProductionScheduleId = n.ProductionScheduleId }).ToList();
            var pagelist = new { total = totalCount, rows = datarows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }

		[HttpPost]
		public ActionResult SaveData(ScheduleDetailChangeViewModel scheduledetails)
        {
            if (scheduledetails.updated != null)
            {
                foreach (var updated in scheduledetails.updated)
                {
                    _scheduleDetailService.Update(updated);
                }
            }
            if (scheduledetails.deleted != null)
            {
                foreach (var deleted in scheduledetails.deleted)
                {
                    _scheduleDetailService.Delete(deleted);
                }
            }
            if (scheduledetails.inserted != null)
            {
                foreach (var inserted in scheduledetails.inserted)
                {
                    _scheduleDetailService.Insert(inserted);
                }
            }
            _unitOfWork.SaveChanges();

            return Json(new {Success=true}, JsonRequestBehavior.AllowGet);
        }

				public ActionResult GetSKUs()
        {
            var skuRepository = _unitOfWork.Repository<SKU>();
            var data = skuRepository.Queryable().ToList();
            var rows = data.Select(n => new { Id = n.Id, Sku = n.Sku });
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
				 
				public ActionResult GetProductionSchedules()
        {
            var productionscheduleRepository = _unitOfWork.Repository<ProductionSchedule>();
            var data = productionscheduleRepository.Queryable().ToList();
            var rows = data.Select(n => new { Id = n.Id, ScheduleNo = n.ScheduleNo });
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
				public ActionResult GetShifts()
        {
            var shiftRepository = _unitOfWork.Repository<Shift>();
            var data = shiftRepository.Queryable().ToList();
            var rows = data.Select(n => new { Id = n.Id, Name = n.Name });
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
				public ActionResult GetStations()
        {
            var stationRepository = _unitOfWork.Repository<Station>();
            var data = stationRepository.Queryable().ToList();
            var rows = data.Select(n => new { Id = n.Id, StationNo = n.StationNo });
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
		
		
       
        // GET: ScheduleDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduleDetail scheduleDetail = _scheduleDetailService.Find(id);
            if (scheduleDetail == null)
            {
                return HttpNotFound();
            }
            return View(scheduleDetail);
        }
        

        // GET: ScheduleDetails/Create
        public ActionResult Create()
        {
            ScheduleDetail scheduleDetail = new ScheduleDetail();
            //set default value
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.ComponentSKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku");
            //var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.ParentSKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku");
            var productionscheduleRepository = _unitOfWork.Repository<ProductionSchedule>();
            ViewBag.ProductionScheduleId = new SelectList(productionscheduleRepository.Queryable(), "Id", "ScheduleNo");
            var shiftRepository = _unitOfWork.Repository<Shift>();
            ViewBag.ShiftId = new SelectList(shiftRepository.Queryable(), "Id", "Name");
            var stationRepository = _unitOfWork.Repository<Station>();
            ViewBag.StationId = new SelectList(stationRepository.Queryable(), "Id", "StationNo");
            return View(scheduleDetail);
        }

        // POST: ScheduleDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ComponentSKU,ParentSKU,ProductionSchedule,Shift,Station,Id,ScheduleNo,WorkNo,ParentSKUId,ComponentSKUId,GraphSKU,GraphVer,ConsumeQty,StockQty,RequirementQty,ScheduleProductionQty,ActualProductionQty,StationId,ShiftId,Operator,AltProdctionDate1,ActualProdctionDate1,AltProdctionDate2,ActualProdctionDate2,AltProdctionDate3,ActualProdctionDate3,AltConsumeTime,ActualConsumeTime,Remark1,Remark2,ProductionScheduleId,BomComponentId,ParentBomComponentId,OrderKey,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] ScheduleDetail scheduleDetail)
        {
            if (ModelState.IsValid)
            {
             				_scheduleDetailService.Insert(scheduleDetail);
                           _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has append a ScheduleDetail record");
                return RedirectToAction("Index");
            }

            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.ComponentSKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", scheduleDetail.ComponentSKUId);
            //var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.ParentSKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", scheduleDetail.ParentSKUId);
            var productionscheduleRepository = _unitOfWork.Repository<ProductionSchedule>();
            ViewBag.ProductionScheduleId = new SelectList(productionscheduleRepository.Queryable(), "Id", "ScheduleNo", scheduleDetail.ProductionScheduleId);
            var shiftRepository = _unitOfWork.Repository<Shift>();
            ViewBag.ShiftId = new SelectList(shiftRepository.Queryable(), "Id", "Name", scheduleDetail.ShiftId);
            var stationRepository = _unitOfWork.Repository<Station>();
            ViewBag.StationId = new SelectList(stationRepository.Queryable(), "Id", "StationNo", scheduleDetail.StationId);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(scheduleDetail);
        }

        // GET: ScheduleDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduleDetail scheduleDetail = _scheduleDetailService.Find(id);
            if (scheduleDetail == null)
            {
                return HttpNotFound();
            }
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.ComponentSKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", scheduleDetail.ComponentSKUId);
            //var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.ParentSKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", scheduleDetail.ParentSKUId);
            var productionscheduleRepository = _unitOfWork.Repository<ProductionSchedule>();
            ViewBag.ProductionScheduleId = new SelectList(productionscheduleRepository.Queryable(), "Id", "ScheduleNo", scheduleDetail.ProductionScheduleId);
            var shiftRepository = _unitOfWork.Repository<Shift>();
            ViewBag.ShiftId = new SelectList(shiftRepository.Queryable(), "Id", "Name", scheduleDetail.ShiftId);
            var stationRepository = _unitOfWork.Repository<Station>();
            ViewBag.StationId = new SelectList(stationRepository.Queryable(), "Id", "StationNo", scheduleDetail.StationId);
            return View(scheduleDetail);
        }

        // POST: ScheduleDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ComponentSKU,ParentSKU,ProductionSchedule,Shift,Station,Id,ScheduleNo,WorkNo,ParentSKUId,ComponentSKUId,GraphSKU,GraphVer,ConsumeQty,StockQty,RequirementQty,ScheduleProductionQty,ActualProductionQty,StationId,ShiftId,Operator,AltProdctionDate1,ActualProdctionDate1,AltProdctionDate2,ActualProdctionDate2,AltProdctionDate3,ActualProdctionDate3,AltConsumeTime,ActualConsumeTime,Remark1,Remark2,ProductionScheduleId,BomComponentId,ParentBomComponentId,OrderKey,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] ScheduleDetail scheduleDetail)
        {
            if (ModelState.IsValid)
            {
                scheduleDetail.ObjectState = ObjectState.Modified;
                				_scheduleDetailService.Update(scheduleDetail);
                                
                _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has update a ScheduleDetail record");
                return RedirectToAction("Index");
            }
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.ComponentSKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", scheduleDetail.ComponentSKUId);
            //var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.ParentSKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", scheduleDetail.ParentSKUId);
            var productionscheduleRepository = _unitOfWork.Repository<ProductionSchedule>();
            ViewBag.ProductionScheduleId = new SelectList(productionscheduleRepository.Queryable(), "Id", "ScheduleNo", scheduleDetail.ProductionScheduleId);
            var shiftRepository = _unitOfWork.Repository<Shift>();
            ViewBag.ShiftId = new SelectList(shiftRepository.Queryable(), "Id", "Name", scheduleDetail.ShiftId);
            var stationRepository = _unitOfWork.Repository<Station>();
            ViewBag.StationId = new SelectList(stationRepository.Queryable(), "Id", "StationNo", scheduleDetail.StationId);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(scheduleDetail);
        }

        // GET: ScheduleDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduleDetail scheduleDetail = _scheduleDetailService.Find(id);
            if (scheduleDetail == null)
            {
                return HttpNotFound();
            }
            return View(scheduleDetail);
        }

        // POST: ScheduleDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ScheduleDetail scheduleDetail =  _scheduleDetailService.Find(id);
             _scheduleDetailService.Delete(scheduleDetail);
            _unitOfWork.SaveChanges();
           if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            DisplaySuccessMessage("Has delete a ScheduleDetail record");
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
