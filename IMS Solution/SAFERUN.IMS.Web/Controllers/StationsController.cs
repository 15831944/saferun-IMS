


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
    public class StationsController : Controller
    {
        
        //Please RegisterType UnityConfig.cs
        //container.RegisterType<IRepositoryAsync<Station>, Repository<Station>>();
        //container.RegisterType<IStationService, StationService>();
        
        //private ImsDbContext db = new ImsDbContext();
        private readonly IStationService  _stationService;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public StationsController (IStationService  stationService, IUnitOfWorkAsync unitOfWork)
        {
            _stationService  = stationService;
            _unitOfWork = unitOfWork;
        }

        // GET: Stations/Index
        public ActionResult Index()
        {
            
            //var stations  = _stationService.Queryable().AsQueryable();
            //return View(stations  );
			return View();
        }

        // Get :Stations/PageList
        // For Index View Boostrap-Table load  data 
        [HttpGet]
        public ActionResult GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
            int totalCount = 0;
            //int pagenum = offset / limit +1;
                        var stations  = _stationService.Query(new StationQuery().Withfilter(filters)).OrderBy(n=>n.OrderBy(sort,order)).SelectPage(page, rows, out totalCount);
                        var datarows = stations .Select(  n => new {  Id = n.Id , StationNo = n.StationNo , Name = n.Name , Equipment = n.Equipment , Description = n.Description , EquipmentNumber = n.EquipmentNumber , StandardElapsedTime = n.StandardElapsedTime , WorkingTime = n.WorkingTime , Status = n.Status }).ToList();
            var pagelist = new { total = totalCount, rows = datarows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }

		[HttpPost]
		public ActionResult SaveData(StationChangeViewModel stations)
        {
            if (stations.updated != null)
            {
                foreach (var updated in stations.updated)
                {
                    _stationService.Update(updated);
                }
            }
            if (stations.deleted != null)
            {
                foreach (var deleted in stations.deleted)
                {
                    _stationService.Delete(deleted);
                }
            }
            if (stations.inserted != null)
            {
                foreach (var inserted in stations.inserted)
                {
                    _stationService.Insert(inserted);
                }
            }
            _unitOfWork.SaveChanges();

            return Json(new {Success=true}, JsonRequestBehavior.AllowGet);
        }

		
		
       
        // GET: Stations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Station station = _stationService.Find(id);
            if (station == null)
            {
                return HttpNotFound();
            }
            return View(station);
        }
        

        // GET: Stations/Create
        public ActionResult Create()
        {
            Station station = new Station();
            //set default value
            return View(station);
        }

        // POST: Stations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StationNo,Name,Equipment,Description,EquipmentNumber,StandardElapsedTime,WorkingTime,Status,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] Station station)
        {
            if (ModelState.IsValid)
            {
             				_stationService.Insert(station);
                           _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has append a Station record");
                return RedirectToAction("Index");
            }

            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(station);
        }

        // GET: Stations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Station station = _stationService.Find(id);
            if (station == null)
            {
                return HttpNotFound();
            }
            return View(station);
        }

        // POST: Stations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StationNo,Name,Equipment,Description,EquipmentNumber,StandardElapsedTime,WorkingTime,Status,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] Station station)
        {
            if (ModelState.IsValid)
            {
                station.ObjectState = ObjectState.Modified;
                				_stationService.Update(station);
                                
                _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has update a Station record");
                return RedirectToAction("Index");
            }
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(station);
        }

        // GET: Stations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Station station = _stationService.Find(id);
            if (station == null)
            {
                return HttpNotFound();
            }
            return View(station);
        }

        // POST: Stations/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Station station =  _stationService.Find(id);
             _stationService.Delete(station);
            _unitOfWork.SaveChanges();
           if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            DisplaySuccessMessage("Has delete a Station record");
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
