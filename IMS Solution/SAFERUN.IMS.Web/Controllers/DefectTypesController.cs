


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
    public class DefectTypesController : Controller
    {
        
        //Please RegisterType UnityConfig.cs
        //container.RegisterType<IRepositoryAsync<DefectType>, Repository<DefectType>>();
        //container.RegisterType<IDefectTypeService, DefectTypeService>();
        
        //private ImsDbContext db = new ImsDbContext();
        private readonly IDefectTypeService  _defectTypeService;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public DefectTypesController (IDefectTypeService  defectTypeService, IUnitOfWorkAsync unitOfWork)
        {
            _defectTypeService  = defectTypeService;
            _unitOfWork = unitOfWork;
        }

        // GET: DefectTypes/Index
        public ActionResult Index()
        {
            
            //var defecttypes  = _defectTypeService.Queryable().AsQueryable();
            //return View(defecttypes  );
			return View();
        }

        // Get :DefectTypes/PageList
        // For Index View Boostrap-Table load  data 
        [HttpGet]
        public ActionResult GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
            int totalCount = 0;
            //int pagenum = offset / limit +1;
                        var defecttypes  = _defectTypeService.Query(new DefectTypeQuery().Withfilter(filters)).OrderBy(n=>n.OrderBy(sort,order)).SelectPage(page, rows, out totalCount);
                        var datarows = defecttypes .Select(  n => new {  Id = n.Id , Name = n.Name , Code = n.Code , Description = n.Description }).ToList();
            var pagelist = new { total = totalCount, rows = datarows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }

		[HttpPost]
		public ActionResult SaveData(DefectTypeChangeViewModel defecttypes)
        {
            if (defecttypes.updated != null)
            {
                foreach (var updated in defecttypes.updated)
                {
                    _defectTypeService.Update(updated);
                }
            }
            if (defecttypes.deleted != null)
            {
                foreach (var deleted in defecttypes.deleted)
                {
                    _defectTypeService.Delete(deleted);
                }
            }
            if (defecttypes.inserted != null)
            {
                foreach (var inserted in defecttypes.inserted)
                {
                    _defectTypeService.Insert(inserted);
                }
            }
            _unitOfWork.SaveChanges();

            return Json(new {Success=true}, JsonRequestBehavior.AllowGet);
        }

		
		
       
        // GET: DefectTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DefectType defectType = _defectTypeService.Find(id);
            if (defectType == null)
            {
                return HttpNotFound();
            }
            return View(defectType);
        }
        

        // GET: DefectTypes/Create
        public ActionResult Create()
        {
            DefectType defectType = new DefectType();
            //set default value
            return View(defectType);
        }

        // POST: DefectTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DefectCodes,Id,Name,Code,Description,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] DefectType defectType)
        {
            if (ModelState.IsValid)
            {
             				_defectTypeService.Insert(defectType);
                           _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has append a DefectType record");
                return RedirectToAction("Index");
            }

            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(defectType);
        }

        // GET: DefectTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DefectType defectType = _defectTypeService.Find(id);
            if (defectType == null)
            {
                return HttpNotFound();
            }
            return View(defectType);
        }

        // POST: DefectTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DefectCodes,Id,Name,Code,Description,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] DefectType defectType)
        {
            if (ModelState.IsValid)
            {
                defectType.ObjectState = ObjectState.Modified;
                				_defectTypeService.Update(defectType);
                                
                _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has update a DefectType record");
                return RedirectToAction("Index");
            }
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(defectType);
        }

        // GET: DefectTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DefectType defectType = _defectTypeService.Find(id);
            if (defectType == null)
            {
                return HttpNotFound();
            }
            return View(defectType);
        }

        // POST: DefectTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DefectType defectType =  _defectTypeService.Find(id);
             _defectTypeService.Delete(defectType);
            _unitOfWork.SaveChanges();
           if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            DisplaySuccessMessage("Has delete a DefectType record");
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
