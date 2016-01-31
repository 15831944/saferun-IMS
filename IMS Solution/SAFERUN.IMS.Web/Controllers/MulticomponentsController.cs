


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
    public class MulticomponentsController : Controller
    {
        
        //Please RegisterType UnityConfig.cs
        //container.RegisterType<IRepositoryAsync<Multicomponent>, Repository<Multicomponent>>();
        //container.RegisterType<IMulticomponentService, MulticomponentService>();
        
        //private ApplicationDbContext db = new ApplicationDbContext();
        private readonly IMulticomponentService  _multicomponentService;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public MulticomponentsController (IMulticomponentService  multicomponentService, IUnitOfWorkAsync unitOfWork)
        {
            _multicomponentService  = multicomponentService;
            _unitOfWork = unitOfWork;
        }

        // GET: Multicomponents/Index
        public ActionResult Index()
        {
            
            //var multicomponents  = _multicomponentService.Queryable().AsQueryable();
            //return View(multicomponents  );
			return View();
        }

        // Get :Multicomponents/PageList
        // For Index View Boostrap-Table load  data 
        [HttpGet]
        public ActionResult GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
            int totalCount = 0;
            //int pagenum = offset / limit +1;
                        var multicomponents  = _multicomponentService.Query(new MulticomponentQuery().Withfilter(filters)).OrderBy(n=>n.OrderBy(sort,order)).SelectPage(page, rows, out totalCount);
                        var datarows = multicomponents .Select(  n => new {  Id = n.Id , DeviceID = n.DeviceID , Order = n.Order , Name = n.Name , Description = n.Description }).ToList();
            var pagelist = new { total = totalCount, rows = datarows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }

		[HttpPost]
		public ActionResult SaveData(MulticomponentChangeViewModel multicomponents)
        {
            if (multicomponents.updated != null)
            {
                foreach (var updated in multicomponents.updated)
                {
                    _multicomponentService.Update(updated);
                }
            }
            if (multicomponents.deleted != null)
            {
                foreach (var deleted in multicomponents.deleted)
                {
                    _multicomponentService.Delete(deleted);
                }
            }
            if (multicomponents.inserted != null)
            {
                foreach (var inserted in multicomponents.inserted)
                {
                    _multicomponentService.Insert(inserted);
                }
            }
            _unitOfWork.SaveChanges();

            return Json(new {Success=true}, JsonRequestBehavior.AllowGet);
        }

		
		
       
        // GET: Multicomponents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Multicomponent multicomponent = _multicomponentService.Find(id);
            if (multicomponent == null)
            {
                return HttpNotFound();
            }
            return View(multicomponent);
        }
        

        // GET: Multicomponents/Create
        public ActionResult Create()
        {
            Multicomponent multicomponent = new Multicomponent();
            //set default value
            return View(multicomponent);
        }

        // POST: Multicomponents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DeviceID,Order,Name,Description,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] Multicomponent multicomponent)
        {
            if (ModelState.IsValid)
            {
             				_multicomponentService.Insert(multicomponent);
                           _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has append a Multicomponent record");
                return RedirectToAction("Index");
            }

            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(multicomponent);
        }

        // GET: Multicomponents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Multicomponent multicomponent = _multicomponentService.Find(id);
            if (multicomponent == null)
            {
                return HttpNotFound();
            }
            return View(multicomponent);
        }

        // POST: Multicomponents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DeviceID,Order,Name,Description,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] Multicomponent multicomponent)
        {
            if (ModelState.IsValid)
            {
                multicomponent.ObjectState = ObjectState.Modified;
                				_multicomponentService.Update(multicomponent);
                                
                _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has update a Multicomponent record");
                return RedirectToAction("Index");
            }
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(multicomponent);
        }

        // GET: Multicomponents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Multicomponent multicomponent = _multicomponentService.Find(id);
            if (multicomponent == null)
            {
                return HttpNotFound();
            }
            return View(multicomponent);
        }

        // POST: Multicomponents/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Multicomponent multicomponent =  _multicomponentService.Find(id);
             _multicomponentService.Delete(multicomponent);
            _unitOfWork.SaveChanges();
           if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            DisplaySuccessMessage("Has delete a Multicomponent record");
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
