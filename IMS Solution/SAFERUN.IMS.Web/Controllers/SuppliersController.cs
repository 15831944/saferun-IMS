


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
    public class SuppliersController : Controller
    {
        
        //Please RegisterType UnityConfig.cs
        //container.RegisterType<IRepositoryAsync<Supplier>, Repository<Supplier>>();
        //container.RegisterType<ISupplierService, SupplierService>();
        
        //private ImsDbContext db = new ImsDbContext();
        private readonly ISupplierService  _supplierService;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public SuppliersController (ISupplierService  supplierService, IUnitOfWorkAsync unitOfWork)
        {
            _supplierService  = supplierService;
            _unitOfWork = unitOfWork;
        }

        // GET: Suppliers/Index
        public ActionResult Index()
        {
            
            //var suppliers  = _supplierService.Queryable().AsQueryable();
            //return View(suppliers  );
			return View();
        }

        // Get :Suppliers/PageList
        // For Index View Boostrap-Table load  data 
        [HttpGet]
        public ActionResult GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
            int totalCount = 0;
            //int pagenum = offset / limit +1;
                        var suppliers  = _supplierService.Query(new SupplierQuery().Withfilter(filters)).OrderBy(n=>n.OrderBy(sort,order)).SelectPage(page, rows, out totalCount);
                        var datarows = suppliers .Select(  n => new {  Id = n.Id , AccountNumber = n.AccountNumber , ShortName = n.ShortName , Name = n.Name , CustomerType = n.CustomerType , AddressLine1 = n.AddressLine1 , AddressLine2 = n.AddressLine2 , City = n.City , Province = n.Province , ContactName = n.ContactName , Phone1 = n.Phone1 , Phone2 = n.Phone2 , Email = n.Email }).ToList();
            var pagelist = new { total = totalCount, rows = datarows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }

		[HttpPost]
		public ActionResult SaveData(SupplierChangeViewModel suppliers)
        {
            if (suppliers.updated != null)
            {
                foreach (var updated in suppliers.updated)
                {
                    _supplierService.Update(updated);
                }
            }
            if (suppliers.deleted != null)
            {
                foreach (var deleted in suppliers.deleted)
                {
                    _supplierService.Delete(deleted);
                }
            }
            if (suppliers.inserted != null)
            {
                foreach (var inserted in suppliers.inserted)
                {
                    _supplierService.Insert(inserted);
                }
            }
            _unitOfWork.SaveChanges();

            return Json(new {Success=true}, JsonRequestBehavior.AllowGet);
        }

		
		
       
        // GET: Suppliers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = _supplierService.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }
        

        // GET: Suppliers/Create
        public ActionResult Create()
        {
            Supplier supplier = new Supplier();
            //set default value
            return View(supplier);
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AccountNumber,ShortName,Name,CustomerType,AddressLine1,AddressLine2,City,Province,ContactName,Phone1,Phone2,Email,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
             				_supplierService.Insert(supplier);
                           _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has append a Supplier record");
                return RedirectToAction("Index");
            }

            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(supplier);
        }

        // GET: Suppliers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = _supplierService.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AccountNumber,ShortName,Name,CustomerType,AddressLine1,AddressLine2,City,Province,ContactName,Phone1,Phone2,Email,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                supplier.ObjectState = ObjectState.Modified;
                				_supplierService.Update(supplier);
                                
                _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has update a Supplier record");
                return RedirectToAction("Index");
            }
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(supplier);
        }

        // GET: Suppliers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = _supplierService.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supplier supplier =  _supplierService.Find(id);
             _supplierService.Delete(supplier);
            _unitOfWork.SaveChanges();
           if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            DisplaySuccessMessage("Has delete a Supplier record");
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
