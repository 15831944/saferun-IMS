


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
    public class ProcessStepsController : Controller
    {
        
        //Please RegisterType UnityConfig.cs
        //container.RegisterType<IRepositoryAsync<ProcessStep>, Repository<ProcessStep>>();
        //container.RegisterType<IProcessStepService, ProcessStepService>();
        
        //private ImsDbContext db = new ImsDbContext();
        private readonly IProcessStepService  _processStepService;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public ProcessStepsController (IProcessStepService  processStepService, IUnitOfWorkAsync unitOfWork)
        {
            _processStepService  = processStepService;
            _unitOfWork = unitOfWork;
        }

        // GET: ProcessSteps/Index
        public ActionResult Index()
        {
            
            //var processsteps  = _processStepService.Queryable().Include(p => p.ProductionProcess).AsQueryable();
            
             //return View(processsteps);
			 return View();
        }

        // Get :ProcessSteps/PageList
        // For Index View Boostrap-Table load  data 
        [HttpGet]
        public ActionResult GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
            int totalCount = 0;
            //int pagenum = offset / limit +1;
            			 
            var processsteps  = _processStepService.Query(new ProcessStepQuery().Withfilter(filters)).Include(p => p.ProductionProcess).OrderBy(n=>n.OrderBy(sort,order)).SelectPage(page, rows, out totalCount);
            
                        var datarows = processsteps .Select(  n => new { ProductionProcessName = (n.ProductionProcess==null?"": n.ProductionProcess.Name) , Id = n.Id , StepName = n.StepName , Order = n.Order , ElapsedTime = n.ElapsedTime , Equipment = n.Equipment , Status = n.Status , Description = n.Description , ProductionProcessId = n.ProductionProcessId }).ToList();
            var pagelist = new { total = totalCount, rows = datarows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }

		[HttpPost]
		public ActionResult SaveData(ProcessStepChangeViewModel processsteps)
        {
            if (processsteps.updated != null)
            {
                foreach (var updated in processsteps.updated)
                {
                    _processStepService.Update(updated);
                }
            }
            if (processsteps.deleted != null)
            {
                foreach (var deleted in processsteps.deleted)
                {
                    _processStepService.Delete(deleted);
                }
            }
            if (processsteps.inserted != null)
            {
                foreach (var inserted in processsteps.inserted)
                {
                    _processStepService.Insert(inserted);
                }
            }
            _unitOfWork.SaveChanges();

            return Json(new {Success=true}, JsonRequestBehavior.AllowGet);
        }

				public ActionResult GetProductionProcesses()
        {
            var productionprocessRepository = _unitOfWork.Repository<ProductionProcess>();
            var data = productionprocessRepository.Queryable().ToList();
            var rows = data.Select(n => new { Id = n.Id, Name = n.Name });
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
		
		
       
        // GET: ProcessSteps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessStep processStep = _processStepService.Find(id);
            if (processStep == null)
            {
                return HttpNotFound();
            }
            return View(processStep);
        }
        

        // GET: ProcessSteps/Create
        public ActionResult Create()
        {
            ProcessStep processStep = new ProcessStep();
            //set default value
            var productionprocessRepository = _unitOfWork.Repository<ProductionProcess>();
            ViewBag.ProductionProcessId = new SelectList(productionprocessRepository.Queryable(), "Id", "Name");
            return View(processStep);
        }

        // POST: ProcessSteps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductionProcess,Id,StepName,Order,ElapsedTime,Equipment,Status,Description,ProductionProcessId,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] ProcessStep processStep)
        {
            if (ModelState.IsValid)
            {
             				_processStepService.Insert(processStep);
                           _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has append a ProcessStep record");
                return RedirectToAction("Index");
            }

            var productionprocessRepository = _unitOfWork.Repository<ProductionProcess>();
            ViewBag.ProductionProcessId = new SelectList(productionprocessRepository.Queryable(), "Id", "Name", processStep.ProductionProcessId);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(processStep);
        }

        // GET: ProcessSteps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessStep processStep = _processStepService.Find(id);
            if (processStep == null)
            {
                return HttpNotFound();
            }
            var productionprocessRepository = _unitOfWork.Repository<ProductionProcess>();
            ViewBag.ProductionProcessId = new SelectList(productionprocessRepository.Queryable(), "Id", "Name", processStep.ProductionProcessId);
            return View(processStep);
        }

        // POST: ProcessSteps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductionProcess,Id,StepName,Order,ElapsedTime,Equipment,Status,Description,ProductionProcessId,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] ProcessStep processStep)
        {
            if (ModelState.IsValid)
            {
                processStep.ObjectState = ObjectState.Modified;
                				_processStepService.Update(processStep);
                                
                _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has update a ProcessStep record");
                return RedirectToAction("Index");
            }
            var productionprocessRepository = _unitOfWork.Repository<ProductionProcess>();
            ViewBag.ProductionProcessId = new SelectList(productionprocessRepository.Queryable(), "Id", "Name", processStep.ProductionProcessId);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(processStep);
        }

        // GET: ProcessSteps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessStep processStep = _processStepService.Find(id);
            if (processStep == null)
            {
                return HttpNotFound();
            }
            return View(processStep);
        }

        // POST: ProcessSteps/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProcessStep processStep =  _processStepService.Find(id);
             _processStepService.Delete(processStep);
            _unitOfWork.SaveChanges();
           if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            DisplaySuccessMessage("Has delete a ProcessStep record");
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
