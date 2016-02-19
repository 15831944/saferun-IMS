


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
    public class ProductionProcessesController : Controller
    {
        
        //Please RegisterType UnityConfig.cs
        //container.RegisterType<IRepositoryAsync<ProductionProcess>, Repository<ProductionProcess>>();
        //container.RegisterType<IProductionProcessService, ProductionProcessService>();
        
        //private ImsDbContext db = new ImsDbContext();
        private readonly IProductionProcessService  _productionProcessService;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public ProductionProcessesController (IProductionProcessService  productionProcessService, IUnitOfWorkAsync unitOfWork)
        {
            _productionProcessService  = productionProcessService;
            _unitOfWork = unitOfWork;
        }

        // GET: ProductionProcesses/Index
        public ActionResult Index()
        {
            
            //var productionprocesses  = _productionProcessService.Queryable().AsQueryable();
            //return View(productionprocesses  );
			return View();
        }

        // Get :ProductionProcesses/PageList
        // For Index View Boostrap-Table load  data 
        [HttpGet]
        public ActionResult GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
            int totalCount = 0;
            //int pagenum = offset / limit +1;
                        var productionprocesses  = _productionProcessService.Query(new ProductionProcessQuery().Withfilter(filters)).OrderBy(n=>n.OrderBy(sort,order)).SelectPage(page, rows, out totalCount);
                        var datarows = productionprocesses .Select(  n => new {  Id = n.Id , Name = n.Name , Description = n.Description , ElapsedTime = n.ElapsedTime , ProductionLine = n.ProductionLine , Status = n.Status }).ToList();
            var pagelist = new { total = totalCount, rows = datarows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }

		[HttpPost]
		public ActionResult SaveData(ProductionProcessChangeViewModel productionprocesses)
        {
            if (productionprocesses.updated != null)
            {
                foreach (var updated in productionprocesses.updated)
                {
                    _productionProcessService.Update(updated);
                }
            }
            if (productionprocesses.deleted != null)
            {
                foreach (var deleted in productionprocesses.deleted)
                {
                    _productionProcessService.Delete(deleted);
                }
            }
            if (productionprocesses.inserted != null)
            {
                foreach (var inserted in productionprocesses.inserted)
                {
                    _productionProcessService.Insert(inserted);
                }
            }
            _unitOfWork.SaveChanges();

            return Json(new {Success=true}, JsonRequestBehavior.AllowGet);
        }

		
		
       
        // GET: ProductionProcesses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductionProcess productionProcess = _productionProcessService.Find(id);
            if (productionProcess == null)
            {
                return HttpNotFound();
            }
            return View(productionProcess);
        }
        

        // GET: ProductionProcesses/Create
        public ActionResult Create()
        {
            ProductionProcess productionProcess = new ProductionProcess();
            //set default value
            return View(productionProcess);
        }

        // POST: ProductionProcesses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,ElapsedTime,ProductionLine,Status,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] ProductionProcess productionProcess)
        {
            if (ModelState.IsValid)
            {
             				_productionProcessService.Insert(productionProcess);
                           _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has append a ProductionProcess record");
                return RedirectToAction("Index");
            }

            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(productionProcess);
        }

        // GET: ProductionProcesses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductionProcess productionProcess = _productionProcessService.Find(id);
            if (productionProcess == null)
            {
                return HttpNotFound();
            }
            return View(productionProcess);
        }

        // POST: ProductionProcesses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,ElapsedTime,ProductionLine,Status,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] ProductionProcess productionProcess)
        {
            if (ModelState.IsValid)
            {
                productionProcess.ObjectState = ObjectState.Modified;
                				_productionProcessService.Update(productionProcess);
                                
                _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has update a ProductionProcess record");
                return RedirectToAction("Index");
            }
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(productionProcess);
        }

        // GET: ProductionProcesses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductionProcess productionProcess = _productionProcessService.Find(id);
            if (productionProcess == null)
            {
                return HttpNotFound();
            }
            return View(productionProcess);
        }

        // POST: ProductionProcesses/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductionProcess productionProcess =  _productionProcessService.Find(id);
             _productionProcessService.Delete(productionProcess);
            _unitOfWork.SaveChanges();
           if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            DisplaySuccessMessage("Has delete a ProductionProcess record");
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
