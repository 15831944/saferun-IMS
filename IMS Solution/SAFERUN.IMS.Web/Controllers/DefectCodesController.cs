


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
    public class DefectCodesController : Controller
    {
        
        //Please RegisterType UnityConfig.cs
        //container.RegisterType<IRepositoryAsync<DefectCode>, Repository<DefectCode>>();
        //container.RegisterType<IDefectCodeService, DefectCodeService>();
        
        //private ImsDbContext db = new ImsDbContext();
        private readonly IDefectCodeService  _defectCodeService;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public DefectCodesController (IDefectCodeService  defectCodeService, IUnitOfWorkAsync unitOfWork)
        {
            _defectCodeService  = defectCodeService;
            _unitOfWork = unitOfWork;
        }

        // GET: DefectCodes/Index
        public ActionResult Index()
        {
            
            //var defectcodes  = _defectCodeService.Queryable().Include(d => d.DefectType).AsQueryable();
            
             //return View(defectcodes);
			 return View();
        }

        // Get :DefectCodes/PageList
        // For Index View Boostrap-Table load  data 
        [HttpGet]
        public ActionResult GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
            int totalCount = 0;
            //int pagenum = offset / limit +1;
            			 
            var defectcodes  = _defectCodeService.Query(new DefectCodeQuery().Withfilter(filters)).Include(d => d.DefectType).OrderBy(n=>n.OrderBy(sort,order)).SelectPage(page, rows, out totalCount);
            
                        var datarows = defectcodes .Select(  n => new { DefectTypeName = (n.DefectType==null?"": n.DefectType.Name) , Id = n.Id , Name = n.Name , Code = n.Code , Description = n.Description , DefectTypeId = n.DefectTypeId }).ToList();
            var pagelist = new { total = totalCount, rows = datarows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }

		[HttpPost]
		public ActionResult SaveData(DefectCodeChangeViewModel defectcodes)
        {
            if (defectcodes.updated != null)
            {
                foreach (var updated in defectcodes.updated)
                {
                    _defectCodeService.Update(updated);
                }
            }
            if (defectcodes.deleted != null)
            {
                foreach (var deleted in defectcodes.deleted)
                {
                    _defectCodeService.Delete(deleted);
                }
            }
            if (defectcodes.inserted != null)
            {
                foreach (var inserted in defectcodes.inserted)
                {
                    _defectCodeService.Insert(inserted);
                }
            }
            _unitOfWork.SaveChanges();

            return Json(new {Success=true}, JsonRequestBehavior.AllowGet);
        }

				public ActionResult GetDefectTypes()
        {
            var defecttypeRepository = _unitOfWork.Repository<DefectType>();
            var data = defecttypeRepository.Queryable().ToList();
            var rows = data.Select(n => new { Id = n.Id, Name = n.Name });
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
		
		
       
        // GET: DefectCodes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DefectCode defectCode = _defectCodeService.Find(id);
            if (defectCode == null)
            {
                return HttpNotFound();
            }
            return View(defectCode);
        }
        

        // GET: DefectCodes/Create
        public ActionResult Create()
        {
            DefectCode defectCode = new DefectCode();
            //set default value
            var defecttypeRepository = _unitOfWork.Repository<DefectType>();
            ViewBag.DefectTypeId = new SelectList(defecttypeRepository.Queryable(), "Id", "Name");
            return View(defectCode);
        }

        // POST: DefectCodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DefectType,Id,Name,Code,Description,DefectTypeId,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] DefectCode defectCode)
        {
            if (ModelState.IsValid)
            {
             				_defectCodeService.Insert(defectCode);
                           _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has append a DefectCode record");
                return RedirectToAction("Index");
            }

            var defecttypeRepository = _unitOfWork.Repository<DefectType>();
            ViewBag.DefectTypeId = new SelectList(defecttypeRepository.Queryable(), "Id", "Name", defectCode.DefectTypeId);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(defectCode);
        }

        // GET: DefectCodes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DefectCode defectCode = _defectCodeService.Find(id);
            if (defectCode == null)
            {
                return HttpNotFound();
            }
            var defecttypeRepository = _unitOfWork.Repository<DefectType>();
            ViewBag.DefectTypeId = new SelectList(defecttypeRepository.Queryable(), "Id", "Name", defectCode.DefectTypeId);
            return View(defectCode);
        }

        // POST: DefectCodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DefectType,Id,Name,Code,Description,DefectTypeId,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] DefectCode defectCode)
        {
            if (ModelState.IsValid)
            {
                defectCode.ObjectState = ObjectState.Modified;
                				_defectCodeService.Update(defectCode);
                                
                _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has update a DefectCode record");
                return RedirectToAction("Index");
            }
            var defecttypeRepository = _unitOfWork.Repository<DefectType>();
            ViewBag.DefectTypeId = new SelectList(defecttypeRepository.Queryable(), "Id", "Name", defectCode.DefectTypeId);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(defectCode);
        }

        // GET: DefectCodes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DefectCode defectCode = _defectCodeService.Find(id);
            if (defectCode == null)
            {
                return HttpNotFound();
            }
            return View(defectCode);
        }

        // POST: DefectCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DefectCode defectCode =  _defectCodeService.Find(id);
             _defectCodeService.Delete(defectCode);
            _unitOfWork.SaveChanges();
           if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            DisplaySuccessMessage("Has delete a DefectCode record");
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
