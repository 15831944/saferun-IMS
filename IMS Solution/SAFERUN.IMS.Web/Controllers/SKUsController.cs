


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
    public class SKUsController : Controller
    {
        
        //Please RegisterType UnityConfig.cs
        //container.RegisterType<IRepositoryAsync<SKU>, Repository<SKU>>();
        //container.RegisterType<ISKUService, SKUService>();
        
        //private ImsDbContext db = new ImsDbContext();
        private readonly ISKUService  _sKUService;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public SKUsController (ISKUService  sKUService, IUnitOfWorkAsync unitOfWork)
        {
            _sKUService  = sKUService;
            _unitOfWork = unitOfWork;
        }

        // GET: SKUs/Index
        public ActionResult Index()
        {
            
            //var skus  = _sKUService.Queryable().AsQueryable();
            //return View(skus  );
			return View();
        }

        // Get :SKUs/PageList
        // For Index View Boostrap-Table load  data 
        [HttpGet]
        public ActionResult GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
            int totalCount = 0;
            //int pagenum = offset / limit +1;
                        var skus  = _sKUService.Query(new SKUQuery().Withfilter(filters)).OrderBy(n=>n.OrderBy(sort,order)).SelectPage(page, rows, out totalCount);
                        var datarows = skus .Select(  n => new {  Id = n.Id , Sku = n.Sku , ProductName=n.ProductName, ALTSku = n.ALTSku , ManufacturerSku = n.ManufacturerSku , Model = n.Model , CLASS = n.CLASS , SKUGroup = n.SKUGroup , MadeType = n.MadeType , STDUOM = n.STDUOM , Description = n.Description , Brand = n.Brand , PackKey = n.PackKey , QCLOC = n.QCLOC , QCLOCOUT = n.QCLOCOUT , Active = n.Active , Price = n.Price , Cost = n.Cost , STDGrossWGT = n.STDGrossWGT , STDNetWGT = n.STDNetWGT , STDCube = n.STDCube , SUSR1 = n.SUSR1 , SUSR2 = n.SUSR2 , SUSR3 = n.SUSR3 , SUSR4 = n.SUSR4 , SUSR5 = n.SUSR5 }).ToList();
            var pagelist = new { total = totalCount, rows = datarows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }

		[HttpPost]
		public ActionResult SaveData(SKUChangeViewModel skus)
        {
            if (skus.updated != null)
            {
                foreach (var updated in skus.updated)
                {
                    _sKUService.Update(updated);
                }
            }
            if (skus.deleted != null)
            {
                foreach (var deleted in skus.deleted)
                {
                    _sKUService.Delete(deleted);
                }
            }
            if (skus.inserted != null)
            {
                foreach (var inserted in skus.inserted)
                {
                    _sKUService.Insert(inserted);
                }
            }
            _unitOfWork.SaveChanges();

            return Json(new {Success=true}, JsonRequestBehavior.AllowGet);
        }

		
		
       
        // GET: SKUs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SKU sKU = _sKUService.Find(id);
            if (sKU == null)
            {
                return HttpNotFound();
            }
            return View(sKU);
        }
        

        // GET: SKUs/Create
        public ActionResult Create()
        {
            SKU sKU = new SKU();
            //set default value
            return View(sKU);
        }

        // POST: SKUs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Sku,ALTSku,ManufacturerSku,Model,CLASS,SKUGroup,MadeType,STDUOM,Description,Brand,PackKey,QCLOC,QCLOCOUT,Active,Price,Cost,STDGrossWGT,STDNetWGT,STDCube,SUSR1,SUSR2,SUSR3,SUSR4,SUSR5,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] SKU sKU)
        {
            if (ModelState.IsValid)
            {
             				_sKUService.Insert(sKU);
                           _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has append a SKU record");
                return RedirectToAction("Index");
            }

            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(sKU);
        }

        // GET: SKUs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SKU sKU = _sKUService.Find(id);
            if (sKU == null)
            {
                return HttpNotFound();
            }
            return View(sKU);
        }

        // POST: SKUs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Sku,ALTSku,ManufacturerSku,Model,CLASS,SKUGroup,MadeType,STDUOM,Description,Brand,PackKey,QCLOC,QCLOCOUT,Active,Price,Cost,STDGrossWGT,STDNetWGT,STDCube,SUSR1,SUSR2,SUSR3,SUSR4,SUSR5,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] SKU sKU)
        {
            if (ModelState.IsValid)
            {
                sKU.ObjectState = ObjectState.Modified;
                				_sKUService.Update(sKU);
                                
                _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has update a SKU record");
                return RedirectToAction("Index");
            }
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(sKU);
        }

        // GET: SKUs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SKU sKU = _sKUService.Find(id);
            if (sKU == null)
            {
                return HttpNotFound();
            }
            return View(sKU);
        }

        // POST: SKUs/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SKU sKU =  _sKUService.Find(id);
             _sKUService.Delete(sKU);
            _unitOfWork.SaveChanges();
           if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            DisplaySuccessMessage("Has delete a SKU record");
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
