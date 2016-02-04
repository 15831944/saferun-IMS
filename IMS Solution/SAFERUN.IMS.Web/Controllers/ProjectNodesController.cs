


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
    public class ProjectNodesController : Controller
    {
        
        //Please RegisterType UnityConfig.cs
        //container.RegisterType<IRepositoryAsync<ProjectNode>, Repository<ProjectNode>>();
        //container.RegisterType<IProjectNodeService, ProjectNodeService>();
        
        //private ImsDbContext db = new ImsDbContext();
        private readonly IProjectNodeService  _projectNodeService;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public ProjectNodesController (IProjectNodeService  projectNodeService, IUnitOfWorkAsync unitOfWork)
        {
            _projectNodeService  = projectNodeService;
            _unitOfWork = unitOfWork;
        }

        // GET: ProjectNodes/Index
        public ActionResult Index()
        {
            
            //var projectnodes  = _projectNodeService.Queryable().Include(p => p.Department).Include(p => p.ProjectType).AsQueryable();
            
             //return View(projectnodes);
			 return View();
        }

        // Get :ProjectNodes/PageList
        // For Index View Boostrap-Table load  data 
        [HttpGet]
        public ActionResult GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
            int totalCount = 0;
            //int pagenum = offset / limit +1;
            			 
            var projectnodes  = _projectNodeService.Query(new ProjectNodeQuery().Withfilter(filters)).Include(p => p.Department).Include(p => p.ProjectType).OrderBy(n=>n.OrderBy(sort,order)).SelectPage(page, rows, out totalCount);
            
                        var datarows = projectnodes .Select(  n => new { DepartmentName = (n.Department==null?"": n.Department.Name) ,ProjectTypeTypeName = (n.ProjectType==null?"": n.ProjectType.TypeName) , Id = n.Id , Name = n.Name , Order = n.Order , ElapsedDay = n.ElapsedDay , DepartmentId = n.DepartmentId , Description = n.Description , ProjectTypeId = n.ProjectTypeId }).ToList();
            var pagelist = new { total = totalCount, rows = datarows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }

		[HttpPost]
		public ActionResult SaveData(ProjectNodeChangeViewModel projectnodes)
        {
            if (projectnodes.updated != null)
            {
                foreach (var updated in projectnodes.updated)
                {
                    _projectNodeService.Update(updated);
                }
            }
            if (projectnodes.deleted != null)
            {
                foreach (var deleted in projectnodes.deleted)
                {
                    _projectNodeService.Delete(deleted);
                }
            }
            if (projectnodes.inserted != null)
            {
                foreach (var inserted in projectnodes.inserted)
                {
                    _projectNodeService.Insert(inserted);
                }
            }
            _unitOfWork.SaveChanges();

            return Json(new {Success=true}, JsonRequestBehavior.AllowGet);
        }

				public ActionResult GetDepartments()
        {
            var departmentRepository = _unitOfWork.Repository<Department>();
            var data = departmentRepository.Queryable().ToList();
            var rows = data.Select(n => new { Id = n.Id, Name = n.Name });
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
				public ActionResult GetProjectTypes()
        {
            var projecttypeRepository = _unitOfWork.Repository<ProjectType>();
            var data = projecttypeRepository.Queryable().ToList();
            var rows = data.Select(n => new { Id = n.Id, TypeName = n.TypeName });
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
		
		
       
        // GET: ProjectNodes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectNode projectNode = _projectNodeService.Find(id);
            if (projectNode == null)
            {
                return HttpNotFound();
            }
            return View(projectNode);
        }
        

        // GET: ProjectNodes/Create
        public ActionResult Create()
        {
            ProjectNode projectNode = new ProjectNode();
            //set default value
            var departmentRepository = _unitOfWork.Repository<Department>();
            ViewBag.DepartmentId = new SelectList(departmentRepository.Queryable(), "Id", "Name");
            var projecttypeRepository = _unitOfWork.Repository<ProjectType>();
            ViewBag.ProjectTypeId = new SelectList(projecttypeRepository.Queryable(), "Id", "TypeName");
            return View(projectNode);
        }

        // POST: ProjectNodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Department,ProjectType,Id,Name,Order,ElapsedDay,DepartmentId,Description,ProjectTypeId,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] ProjectNode projectNode)
        {
            if (ModelState.IsValid)
            {
             				_projectNodeService.Insert(projectNode);
                           _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has append a ProjectNode record");
                return RedirectToAction("Index");
            }

            var departmentRepository = _unitOfWork.Repository<Department>();
            ViewBag.DepartmentId = new SelectList(departmentRepository.Queryable(), "Id", "Name", projectNode.DepartmentId);
            var projecttypeRepository = _unitOfWork.Repository<ProjectType>();
            ViewBag.ProjectTypeId = new SelectList(projecttypeRepository.Queryable(), "Id", "TypeName", projectNode.ProjectTypeId);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(projectNode);
        }

        // GET: ProjectNodes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectNode projectNode = _projectNodeService.Find(id);
            if (projectNode == null)
            {
                return HttpNotFound();
            }
            var departmentRepository = _unitOfWork.Repository<Department>();
            ViewBag.DepartmentId = new SelectList(departmentRepository.Queryable(), "Id", "Name", projectNode.DepartmentId);
            var projecttypeRepository = _unitOfWork.Repository<ProjectType>();
            ViewBag.ProjectTypeId = new SelectList(projecttypeRepository.Queryable(), "Id", "TypeName", projectNode.ProjectTypeId);
            return View(projectNode);
        }

        // POST: ProjectNodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Department,ProjectType,Id,Name,Order,ElapsedDay,DepartmentId,Description,ProjectTypeId,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] ProjectNode projectNode)
        {
            if (ModelState.IsValid)
            {
                projectNode.ObjectState = ObjectState.Modified;
                				_projectNodeService.Update(projectNode);
                                
                _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has update a ProjectNode record");
                return RedirectToAction("Index");
            }
            var departmentRepository = _unitOfWork.Repository<Department>();
            ViewBag.DepartmentId = new SelectList(departmentRepository.Queryable(), "Id", "Name", projectNode.DepartmentId);
            var projecttypeRepository = _unitOfWork.Repository<ProjectType>();
            ViewBag.ProjectTypeId = new SelectList(projecttypeRepository.Queryable(), "Id", "TypeName", projectNode.ProjectTypeId);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(projectNode);
        }

        // GET: ProjectNodes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectNode projectNode = _projectNodeService.Find(id);
            if (projectNode == null)
            {
                return HttpNotFound();
            }
            return View(projectNode);
        }

        // POST: ProjectNodes/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectNode projectNode =  _projectNodeService.Find(id);
             _projectNodeService.Delete(projectNode);
            _unitOfWork.SaveChanges();
           if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            DisplaySuccessMessage("Has delete a ProjectNode record");
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
