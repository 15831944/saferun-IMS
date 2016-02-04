


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
    public class ProjectTypesController : Controller
    {
        
        //Please RegisterType UnityConfig.cs
        //container.RegisterType<IRepositoryAsync<ProjectType>, Repository<ProjectType>>();
        //container.RegisterType<IProjectTypeService, ProjectTypeService>();
        
        //private ImsDbContext db = new ImsDbContext();
        private readonly IProjectTypeService  _projectTypeService;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public ProjectTypesController (IProjectTypeService  projectTypeService, IUnitOfWorkAsync unitOfWork)
        {
            _projectTypeService  = projectTypeService;
            _unitOfWork = unitOfWork;
        }

        // GET: ProjectTypes/Index
        public ActionResult Index()
        {
            
            //var projecttypes  = _projectTypeService.Queryable().AsQueryable();
            //return View(projecttypes  );
			return View();
        }

        // Get :ProjectTypes/PageList
        // For Index View Boostrap-Table load  data 
        [HttpGet]
        public ActionResult GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
            int totalCount = 0;
            //int pagenum = offset / limit +1;
                        var projecttypes  = _projectTypeService.Query(new ProjectTypeQuery().Withfilter(filters)).OrderBy(n=>n.OrderBy(sort,order)).SelectPage(page, rows, out totalCount);
                        var datarows = projecttypes .Select(  n => new {  Id = n.Id , TypeName = n.TypeName , Version = n.Version , Status = n.Status , Description = n.Description , StartDate = n.StartDate , ExpiryDate = n.ExpiryDate }).ToList();
            var pagelist = new { total = totalCount, rows = datarows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }

		[HttpPost]
		public ActionResult SaveData(ProjectTypeChangeViewModel projecttypes)
        {
            if (projecttypes.updated != null)
            {
                foreach (var updated in projecttypes.updated)
                {
                    _projectTypeService.Update(updated);
                }
            }
            if (projecttypes.deleted != null)
            {
                foreach (var deleted in projecttypes.deleted)
                {
                    _projectTypeService.Delete(deleted);
                }
            }
            if (projecttypes.inserted != null)
            {
                foreach (var inserted in projecttypes.inserted)
                {
                    _projectTypeService.Insert(inserted);
                }
            }
            _unitOfWork.SaveChanges();

            return Json(new {Success=true}, JsonRequestBehavior.AllowGet);
        }

		
		
       
        // GET: ProjectTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectType projectType = _projectTypeService.Find(id);
            if (projectType == null)
            {
                return HttpNotFound();
            }
            return View(projectType);
        }
        

        // GET: ProjectTypes/Create
        public ActionResult Create()
        {
            ProjectType projectType = new ProjectType();
            //set default value
            return View(projectType);
        }

        // POST: ProjectTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TypeName,Version,Status,Description,StartDate,ExpiryDate,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] ProjectType projectType)
        {
            if (ModelState.IsValid)
            {
             				_projectTypeService.Insert(projectType);
                           _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has append a ProjectType record");
                return RedirectToAction("Index");
            }

            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(projectType);
        }

        // GET: ProjectTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectType projectType = _projectTypeService.Find(id);
            if (projectType == null)
            {
                return HttpNotFound();
            }
            return View(projectType);
        }

        // POST: ProjectTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TypeName,Version,Status,Description,StartDate,ExpiryDate,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] ProjectType projectType)
        {
            if (ModelState.IsValid)
            {
                projectType.ObjectState = ObjectState.Modified;
                				_projectTypeService.Update(projectType);
                                
                _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has update a ProjectType record");
                return RedirectToAction("Index");
            }
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(projectType);
        }

        // GET: ProjectTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectType projectType = _projectTypeService.Find(id);
            if (projectType == null)
            {
                return HttpNotFound();
            }
            return View(projectType);
        }

        // POST: ProjectTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectType projectType =  _projectTypeService.Find(id);
             _projectTypeService.Delete(projectType);
            _unitOfWork.SaveChanges();
           if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            DisplaySuccessMessage("Has delete a ProjectType record");
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
