


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
    public class OrdersController : Controller
    {
        
        //Please RegisterType UnityConfig.cs
        //container.RegisterType<IRepositoryAsync<Order>, Repository<Order>>();
        //container.RegisterType<IOrderService, OrderService>();
        
        //private ImsDbContext db = new ImsDbContext();
        private readonly IOrderService  _orderService;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public OrdersController (IOrderService  orderService, IUnitOfWorkAsync unitOfWork)
        {
            _orderService  = orderService;
            _unitOfWork = unitOfWork;
        }

        // GET: Orders/Index
        public ActionResult Index()
        {
            
            //var orders  = _orderService.Queryable().Include(o => o.Customer).Include(o => o.ProjectType).AsQueryable();
            
             //return View(orders);
			 return View();
        }

        // Get :Orders/PageList
        // For Index View Boostrap-Table load  data 
        [HttpGet]
        public ActionResult GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
            int totalCount = 0;
            //int pagenum = offset / limit +1;
            			 
            var orders  = _orderService.Query(new OrderQuery().Withfilter(filters)).Include(o => o.Customer).Include(o => o.ProjectType).OrderBy(n=>n.OrderBy(sort,order)).SelectPage(page, rows, out totalCount);
            
                        var datarows = orders .Select(  n => new { CustomerAccountNumber = (n.Customer==null?"": n.Customer.AccountNumber) ,ProjectTypeTypeName = (n.ProjectType==null?"": n.ProjectType.TypeName) , Id = n.Id , OrderKey = n.OrderKey , Sales = n.Sales , OrderDate = n.OrderDate , AuditDate = n.AuditDate , CustomerId = n.CustomerId , ProjectTypeId = n.ProjectTypeId , ProjectName = n.ProjectName , Status = n.Status , AuditResult = n.AuditResult , Remark = n.Remark , PlanFinishDate = n.PlanFinishDate , ActualFinishDate = n.ActualFinishDate , ShipDate = n.ShipDate , ColseDate = n.ColseDate }).ToList();
            var pagelist = new { total = totalCount, rows = datarows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }

		[HttpPost]
		public ActionResult SaveData(OrderChangeViewModel orders)
        {
            if (orders.updated != null)
            {
                foreach (var updated in orders.updated)
                {
                    _orderService.Update(updated);
                }
            }
            if (orders.deleted != null)
            {
                foreach (var deleted in orders.deleted)
                {
                    _orderService.Delete(deleted);
                }
            }
            if (orders.inserted != null)
            {
                foreach (var inserted in orders.inserted)
                {
                    _orderService.Insert(inserted);
                }
            }
            _unitOfWork.SaveChanges();

            return Json(new {Success=true}, JsonRequestBehavior.AllowGet);
        }

				public ActionResult GetCustomers()
        {
            var customerRepository = _unitOfWork.Repository<Customer>();
            var data = customerRepository.Queryable().ToList();
            var rows = data.Select(n => new { Id = n.Id, AccountNumber = n.AccountNumber });
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
				public ActionResult GetProjectTypes()
        {
            var projecttypeRepository = _unitOfWork.Repository<ProjectType>();
            var data = projecttypeRepository.Queryable().ToList();
            var rows = data.Select(n => new { Id = n.Id, TypeName = n.TypeName });
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
		
				public ActionResult GetOrders()
        {
            var orderRepository = _unitOfWork.Repository<Order>();
            var data = orderRepository.Queryable().ToList();
            var rows = data.Select(n => new { Id = n.Id, OrderKey = n.OrderKey });
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
				public ActionResult GetSKUs()
        {
            var skuRepository = _unitOfWork.Repository<SKU>();
            var data = skuRepository.Queryable().ToList();
            var rows = data.Select(n => new { Id = n.Id, Sku = n.Sku });
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
		
       
        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = _orderService.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
        

        // GET: Orders/Create
        public ActionResult Create()
        {
            Order order = new Order();
            //set default value
            var customerRepository = _unitOfWork.Repository<Customer>();
            ViewBag.CustomerId = new SelectList(customerRepository.Queryable(), "Id", "AccountNumber");
            var projecttypeRepository = _unitOfWork.Repository<ProjectType>();
            ViewBag.ProjectTypeId = new SelectList(projecttypeRepository.Queryable(), "Id", "TypeName");
            return View(order);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Customer,OrderDetails,ProjectType,Id,OrderKey,Sales,OrderDate,AuditDate,CustomerId,ProjectTypeId,ProjectName,Status,AuditResult,Remark,PlanFinishDate,ActualFinishDate,ShipDate,ColseDate,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] Order order)
        {
            if (ModelState.IsValid)
            {
                             order.ObjectState = ObjectState.Added;   
                                foreach (var item in order.OrderDetails)
                {
					item.OrderId = order.Id ;
                    item.ObjectState = ObjectState.Added;
                }
                                _orderService.InsertOrUpdateGraph(order);
                            _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has append a Order record");
                return RedirectToAction("Index");
            }

            var customerRepository = _unitOfWork.Repository<Customer>();
            ViewBag.CustomerId = new SelectList(customerRepository.Queryable(), "Id", "AccountNumber", order.CustomerId);
            var projecttypeRepository = _unitOfWork.Repository<ProjectType>();
            ViewBag.ProjectTypeId = new SelectList(projecttypeRepository.Queryable(), "Id", "TypeName", order.ProjectTypeId);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = _orderService.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            var customerRepository = _unitOfWork.Repository<Customer>();
            ViewBag.CustomerId = new SelectList(customerRepository.Queryable(), "Id", "AccountNumber", order.CustomerId);
            var projecttypeRepository = _unitOfWork.Repository<ProjectType>();
            ViewBag.ProjectTypeId = new SelectList(projecttypeRepository.Queryable(), "Id", "TypeName", order.ProjectTypeId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Customer,OrderDetails,ProjectType,Id,OrderKey,Sales,OrderDate,AuditDate,CustomerId,ProjectTypeId,ProjectName,Status,AuditResult,Remark,PlanFinishDate,ActualFinishDate,ShipDate,ColseDate,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.ObjectState = ObjectState.Modified;
                                                foreach (var item in order.OrderDetails)
                {
					item.OrderId = order.Id ;
                    item.OrderKey = order.OrderKey;
                    //set ObjectState with conditions
                    if(item.Id <= 0)
                        item.ObjectState = ObjectState.Added;
                    else
                        item.ObjectState = ObjectState.Modified;
                }
                      
                _orderService.InsertOrUpdateGraph(order);
                                
                _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has update a Order record");
                return RedirectToAction("Index");
            }
            var customerRepository = _unitOfWork.Repository<Customer>();
            ViewBag.CustomerId = new SelectList(customerRepository.Queryable(), "Id", "AccountNumber", order.CustomerId);
            var projecttypeRepository = _unitOfWork.Repository<ProjectType>();
            ViewBag.ProjectTypeId = new SelectList(projecttypeRepository.Queryable(), "Id", "TypeName", order.ProjectTypeId);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = _orderService.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order =  _orderService.Find(id);
             _orderService.Delete(order);
            _unitOfWork.SaveChanges();
           if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            DisplaySuccessMessage("Has delete a Order record");
            return RedirectToAction("Index");
        }


        // Get Detail Row By Id For Edit
        // Get : Orders/EditOrderDetail/:id
        [HttpGet]
        public ActionResult EditOrderDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var orderdetailRepository = _unitOfWork.Repository<OrderDetail>();
            var orderdetail = orderdetailRepository.Find(id);

                        var orderRepository = _unitOfWork.Repository<Order>();             
                        var skuRepository = _unitOfWork.Repository<SKU>();             
            
            if (orderdetail == null)
            {
                            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey" );
                            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku" );
                            
                //return HttpNotFound();
                return PartialView("_OrderDetailEditForm", new OrderDetail());
            }
            else
            {
                            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey" , orderdetail.OrderId );  
                            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku" , orderdetail.SKUId );  
                             
            }
            return PartialView("_OrderDetailEditForm",  orderdetail);

        }
        
        // Get Create Row By Id For Edit
        // Get : Orders/CreateOrderDetail
        [HttpGet]
        public ActionResult CreateOrderDetail()
        {
                        var orderRepository = _unitOfWork.Repository<Order>();    
              ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey" );
                        var skuRepository = _unitOfWork.Repository<SKU>();    
              ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku" );
                      return PartialView("_OrderDetailEditForm");

        }

        // Post Delete Detail Row By Id
        // Get : Orders/DeleteOrderDetail/:id
        [HttpPost,ActionName("DeleteOrderDetail")]
        public ActionResult DeleteOrderDetailConfirmed(int  id)
        {
            var orderdetailRepository = _unitOfWork.Repository<OrderDetail>();
            orderdetailRepository.Delete(id);
            _unitOfWork.SaveChanges();
            if (Request.IsAjaxRequest())
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            DisplaySuccessMessage("Has delete a Order record");
            return RedirectToAction("Index");
        }

       

        // Get : Orders/GetOrderDetailsByOrderId/:id
        [HttpGet]
        public ActionResult GetOrderDetailsByOrderId(int id)
        {
            var orderdetails = _orderService.GetOrderDetailsByOrderId(id);
            if (Request.IsAjaxRequest())
            {
                return Json(orderdetails.Select( n => new { OrderOrderKey = (n.Order==null?"": n.Order.OrderKey) ,SKUSku = (n.SKU==null?"": n.SKU.Sku) , Id = n.Id , OrderKey = n.OrderKey , OrderId = n.OrderId , LineNumber = n.LineNumber , ContractNum = n.ContractNum , SKUId = n.SKUId , ProductionSku = n.ProductionSku , Model = n.Model , Qty = n.Qty , UOM = n.UOM , Price = n.Price , SubTotal = n.SubTotal , Remark = n.Remark , Status = n.Status }),JsonRequestBehavior.AllowGet);
            }  
            return View(orderdetails); 

        }

        [HttpPost]
        public ActionResult GenerateAuditPlan(int id)
        {
            var auditplan = _orderService.GenerateAuditPlan(id);
            _unitOfWork.SaveChanges();
            return Json(new {success = true } , JsonRequestBehavior.AllowGet);
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
