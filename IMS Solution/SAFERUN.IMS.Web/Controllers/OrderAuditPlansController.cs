


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
    public class OrderAuditPlansController : Controller
    {
        
        //Please RegisterType UnityConfig.cs
        //container.RegisterType<IRepositoryAsync<OrderAuditPlan>, Repository<OrderAuditPlan>>();
        //container.RegisterType<IOrderAuditPlanService, OrderAuditPlanService>();
        
        //private ImsDbContext db = new ImsDbContext();
        private readonly IOrderAuditPlanService  _orderAuditPlanService;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public OrderAuditPlansController (IOrderAuditPlanService  orderAuditPlanService, IUnitOfWorkAsync unitOfWork)
        {
            _orderAuditPlanService  = orderAuditPlanService;
            _unitOfWork = unitOfWork;
        }

        // GET: OrderAuditPlans/Index
        public ActionResult Index()
        {
            
            //var orderauditplans  = _orderAuditPlanService.Queryable().Include(o => o.Order).AsQueryable();
            
             //return View(orderauditplans);
			 return View();
        }

        // Get :OrderAuditPlans/PageList
        // For Index View Boostrap-Table load  data 
        [HttpGet]
        public ActionResult GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
            int totalCount = 0;
            //int pagenum = offset / limit +1;
            			 
            var orderauditplans  = _orderAuditPlanService.Query(new OrderAuditPlanQuery().Withfilter(filters)).Include(o => o.Order).OrderBy(n=>n.OrderBy(sort,order)).SelectPage(page, rows, out totalCount);
            
                        var datarows = orderauditplans .Select(  n => new { OrderOrderKey = (n.Order==null?"": n.Order.OrderKey) , Id = n.Id , OrderKey = n.OrderKey , AuditContent = n.AuditContent , Department = n.Department , AuditResult = n.AuditResult , Status = n.Status , PlanAuditDate = n.PlanAuditDate , AuditDate = n.AuditDate , AuditUser = n.AuditUser , Remark = n.Remark , OrderId = n.OrderId }).ToList();
            var pagelist = new { total = totalCount, rows = datarows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }

		[HttpPost]
		public ActionResult SaveData(OrderAuditPlanChangeViewModel orderauditplans)
        {
            if (orderauditplans.updated != null)
            {
                foreach (var updated in orderauditplans.updated)
                {
                    _orderAuditPlanService.Update(updated);
                }
            }
            if (orderauditplans.deleted != null)
            {
                foreach (var deleted in orderauditplans.deleted)
                {
                    _orderAuditPlanService.Delete(deleted);
                }
            }
            if (orderauditplans.inserted != null)
            {
                foreach (var inserted in orderauditplans.inserted)
                {
                    _orderAuditPlanService.Insert(inserted);
                }
            }
            _unitOfWork.SaveChanges();

            return Json(new {Success=true}, JsonRequestBehavior.AllowGet);
        }

				public ActionResult GetOrders()
        {
            var orderRepository = _unitOfWork.Repository<Order>();
            var data = orderRepository.Queryable().ToList();
            var rows = data.Select(n => new { Id = n.Id, OrderKey = n.OrderKey });
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
		
		
       
        // GET: OrderAuditPlans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderAuditPlan orderAuditPlan = _orderAuditPlanService.Find(id);
            if (orderAuditPlan == null)
            {
                return HttpNotFound();
            }
            return View(orderAuditPlan);
        }
        

        // GET: OrderAuditPlans/Create
        public ActionResult Create()
        {
            OrderAuditPlan orderAuditPlan = new OrderAuditPlan();
            //set default value
            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey");
            return View(orderAuditPlan);
        }

        // POST: OrderAuditPlans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Order,Id,OrderKey,AuditContent,Department,AuditResult,Status,PlanAuditDate,AuditDate,AuditUser,Remark,OrderId,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] OrderAuditPlan orderAuditPlan)
        {
            if (ModelState.IsValid)
            {
             				_orderAuditPlanService.Insert(orderAuditPlan);
                           _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has append a OrderAuditPlan record");
                return RedirectToAction("Index");
            }

            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey", orderAuditPlan.OrderId);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(orderAuditPlan);
        }

        // GET: OrderAuditPlans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderAuditPlan orderAuditPlan = _orderAuditPlanService.Find(id);
            if (orderAuditPlan == null)
            {
                return HttpNotFound();
            }
            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey", orderAuditPlan.OrderId);
            return View(orderAuditPlan);
        }

        // POST: OrderAuditPlans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Order,Id,OrderKey,AuditContent,Department,AuditResult,Status,PlanAuditDate,AuditDate,AuditUser,Remark,OrderId,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] OrderAuditPlan orderAuditPlan)
        {
            if (ModelState.IsValid)
            {
                orderAuditPlan.ObjectState = ObjectState.Modified;
                				_orderAuditPlanService.Update(orderAuditPlan);
                                
                _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has update a OrderAuditPlan record");
                return RedirectToAction("Index");
            }
            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey", orderAuditPlan.OrderId);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(orderAuditPlan);
        }

        // GET: OrderAuditPlans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderAuditPlan orderAuditPlan = _orderAuditPlanService.Find(id);
            if (orderAuditPlan == null)
            {
                return HttpNotFound();
            }
            return View(orderAuditPlan);
        }

        // POST: OrderAuditPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderAuditPlan orderAuditPlan =  _orderAuditPlanService.Find(id);
             _orderAuditPlanService.Delete(orderAuditPlan);
            _unitOfWork.SaveChanges();
           if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            DisplaySuccessMessage("Has delete a OrderAuditPlan record");
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
