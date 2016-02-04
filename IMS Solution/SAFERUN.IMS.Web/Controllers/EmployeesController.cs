


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
    public class EmployeesController : Controller
    {
        
        //Please RegisterType UnityConfig.cs
        //container.RegisterType<IRepositoryAsync<Employee>, Repository<Employee>>();
        //container.RegisterType<IEmployeeService, EmployeeService>();
        
        //private ImsDbContext db = new ImsDbContext();
        private readonly IEmployeeService  _employeeService;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public EmployeesController (IEmployeeService  employeeService, IUnitOfWorkAsync unitOfWork)
        {
            _employeeService  = employeeService;
            _unitOfWork = unitOfWork;
        }

        // GET: Employees/Index
        public ActionResult Index()
        {
            
            //var employees  = _employeeService.Queryable().Include(e => e.Department).Include(e => e.Manager).AsQueryable();
            
             //return View(employees);
			 return View();
        }

        // Get :Employees/PageList
        // For Index View Boostrap-Table load  data 
        [HttpGet]
        public ActionResult GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
            int totalCount = 0;
            //int pagenum = offset / limit +1;
            			 
            var employees  = _employeeService.Query(new EmployeeQuery().Withfilter(filters)).Include(e => e.Department).Include(e => e.Manager).OrderBy(n=>n.OrderBy(sort,order)).SelectPage(page, rows, out totalCount);
            
                        var datarows = employees .Select(  n => new { DepartmentName = (n.Department==null?"": n.Department.Name) ,ManagerName = (n.Manager==null?"": n.Manager.Name) , Id = n.Id , Name = n.Name , WorkNo = n.WorkNo , Title = n.Title , BirthDate = n.BirthDate , MaritalStatus = n.MaritalStatus , Gender = n.Gender , HireDate = n.HireDate , DepartmentId = n.DepartmentId , ManagerID = n.ManagerID }).ToList();
            var pagelist = new { total = totalCount, rows = datarows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }

		[HttpPost]
		public ActionResult SaveData(EmployeeChangeViewModel employees)
        {
            if (employees.updated != null)
            {
                foreach (var updated in employees.updated)
                {
                    _employeeService.Update(updated);
                }
            }
            if (employees.deleted != null)
            {
                foreach (var deleted in employees.deleted)
                {
                    _employeeService.Delete(deleted);
                }
            }
            if (employees.inserted != null)
            {
                foreach (var inserted in employees.inserted)
                {
                    _employeeService.Insert(inserted);
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
				public ActionResult GetEmployees()
        {
            var employeeRepository = _unitOfWork.Repository<Employee>();
            var data = employeeRepository.Queryable().ToList();
            var rows = data.Select(n => new { Id = n.Id, Name = n.Name });
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
		
		
       
        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _employeeService.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }
        

        // GET: Employees/Create
        public ActionResult Create()
        {
            Employee employee = new Employee();
            //set default value
            var departmentRepository = _unitOfWork.Repository<Department>();
            ViewBag.DepartmentId = new SelectList(departmentRepository.Queryable(), "Id", "Name");
            var employeeRepository = _unitOfWork.Repository<Employee>();
            ViewBag.ManagerID = new SelectList(employeeRepository.Queryable(), "Id", "Name");
            return View(employee);
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Department,Manager,Id,Name,WorkNo,Title,BirthDate,MaritalStatus,Gender,HireDate,DepartmentId,ManagerID,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] Employee employee)
        {
            if (ModelState.IsValid)
            {
             				_employeeService.Insert(employee);
                           _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has append a Employee record");
                return RedirectToAction("Index");
            }

            var departmentRepository = _unitOfWork.Repository<Department>();
            ViewBag.DepartmentId = new SelectList(departmentRepository.Queryable(), "Id", "Name", employee.DepartmentId);
            var employeeRepository = _unitOfWork.Repository<Employee>();
            ViewBag.ManagerID = new SelectList(employeeRepository.Queryable(), "Id", "Name", employee.ManagerID);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _employeeService.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            var departmentRepository = _unitOfWork.Repository<Department>();
            ViewBag.DepartmentId = new SelectList(departmentRepository.Queryable(), "Id", "Name", employee.DepartmentId);
            var employeeRepository = _unitOfWork.Repository<Employee>();
            ViewBag.ManagerID = new SelectList(employeeRepository.Queryable(), "Id", "Name", employee.ManagerID);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Department,Manager,Id,Name,WorkNo,Title,BirthDate,MaritalStatus,Gender,HireDate,DepartmentId,ManagerID,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.ObjectState = ObjectState.Modified;
                				_employeeService.Update(employee);
                                
                _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has update a Employee record");
                return RedirectToAction("Index");
            }
            var departmentRepository = _unitOfWork.Repository<Department>();
            ViewBag.DepartmentId = new SelectList(departmentRepository.Queryable(), "Id", "Name", employee.DepartmentId);
            var employeeRepository = _unitOfWork.Repository<Employee>();
            ViewBag.ManagerID = new SelectList(employeeRepository.Queryable(), "Id", "Name", employee.ManagerID);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _employeeService.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee =  _employeeService.Find(id);
             _employeeService.Delete(employee);
            _unitOfWork.SaveChanges();
           if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            DisplaySuccessMessage("Has delete a Employee record");
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
