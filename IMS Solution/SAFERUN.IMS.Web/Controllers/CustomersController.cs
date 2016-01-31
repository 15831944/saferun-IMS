


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
    public class CustomersController : Controller
    {
        
        //Please RegisterType UnityConfig.cs
        //container.RegisterType<IRepositoryAsync<Customer>, Repository<Customer>>();
        //container.RegisterType<ICustomerService, CustomerService>();
        
        //private ImsDbContext db = new ImsDbContext();
        private readonly ICustomerService  _customerService;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public CustomersController (ICustomerService  customerService, IUnitOfWorkAsync unitOfWork)
        {
            _customerService  = customerService;
            _unitOfWork = unitOfWork;
        }

        // GET: Customers/Index
        public ActionResult Index()
        {
            
            //var customers  = _customerService.Queryable().AsQueryable();
            //return View(customers  );
			return View();
        }

        // Get :Customers/PageList
        // For Index View Boostrap-Table load  data 
        [HttpGet]
        public ActionResult GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
            int totalCount = 0;
            //int pagenum = offset / limit +1;
                        var customers  = _customerService.Query(new CustomerQuery().Withfilter(filters)).OrderBy(n=>n.OrderBy(sort,order)).SelectPage(page, rows, out totalCount);
                        var datarows = customers .Select(  n => new {  Id = n.Id , AccountNumber = n.AccountNumber , ShortName = n.ShortName , Name = n.Name , CustomerType = n.CustomerType , AddressLine1 = n.AddressLine1 , AddressLine2 = n.AddressLine2 , City = n.City , Province = n.Province , ContactName = n.ContactName , Phone1 = n.Phone1 , Phone2 = n.Phone2 , Email = n.Email }).ToList();
            var pagelist = new { total = totalCount, rows = datarows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }

		[HttpPost]
		public ActionResult SaveData(CustomerChangeViewModel customers)
        {
            if (customers.updated != null)
            {
                foreach (var updated in customers.updated)
                {
                    _customerService.Update(updated);
                }
            }
            if (customers.deleted != null)
            {
                foreach (var deleted in customers.deleted)
                {
                    _customerService.Delete(deleted);
                }
            }
            if (customers.inserted != null)
            {
                foreach (var inserted in customers.inserted)
                {
                    _customerService.Insert(inserted);
                }
            }
            _unitOfWork.SaveChanges();

            return Json(new {Success=true}, JsonRequestBehavior.AllowGet);
        }

		
		
       
        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = _customerService.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
        

        // GET: Customers/Create
        public ActionResult Create()
        {
            Customer customer = new Customer();
            //set default value
            return View(customer);
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AccountNumber,ShortName,Name,CustomerType,AddressLine1,AddressLine2,City,Province,ContactName,Phone1,Phone2,Email,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] Customer customer)
        {
            if (ModelState.IsValid)
            {
             				_customerService.Insert(customer);
                           _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has append a Customer record");
                return RedirectToAction("Index");
            }

            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = _customerService.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AccountNumber,ShortName,Name,CustomerType,AddressLine1,AddressLine2,City,Province,ContactName,Phone1,Phone2,Email,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.ObjectState = ObjectState.Modified;
                				_customerService.Update(customer);
                                
                _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has update a Customer record");
                return RedirectToAction("Index");
            }
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = _customerService.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer =  _customerService.Find(id);
             _customerService.Delete(customer);
            _unitOfWork.SaveChanges();
           if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            DisplaySuccessMessage("Has delete a Customer record");
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
