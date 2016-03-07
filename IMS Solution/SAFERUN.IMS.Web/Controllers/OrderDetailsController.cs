


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
    public class OrderDetailsController : Controller
    {
        
        //Please RegisterType UnityConfig.cs
        //container.RegisterType<IRepositoryAsync<OrderDetail>, Repository<OrderDetail>>();
        //container.RegisterType<IOrderDetailService, OrderDetailService>();
        
        //private ImsDbContext db = new ImsDbContext();
        private readonly IOrderDetailService  _orderDetailService;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public OrderDetailsController (IOrderDetailService  orderDetailService, IUnitOfWorkAsync unitOfWork)
        {
            _orderDetailService  = orderDetailService;
            _unitOfWork = unitOfWork;
        }

        // GET: OrderDetails/Index
        public ActionResult Index()
        {
            
            //var orderdetails  = _orderDetailService.Queryable().Include(o => o.Order).Include(o => o.SKU).AsQueryable();
            
             //return View(orderdetails);
			 return View();
        }

        // Get :OrderDetails/PageList
        // For Index View Boostrap-Table load  data 
        [HttpGet]
        public ActionResult GetData(int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {
			var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
            int totalCount = 0;
            //int pagenum = offset / limit +1;
            			 
            var orderdetails  = _orderDetailService.Query(new OrderDetailQuery().Withfilter(filters)).Include(o => o.Order).Include(o => o.SKU).OrderBy(n=>n.OrderBy(sort,order)).SelectPage(page, rows, out totalCount);
            
                        var datarows = orderdetails .Select(  n => new { OrderOrderKey = (n.Order==null?"": n.Order.OrderKey) ,SKUSku = (n.SKU==null?"": n.SKU.Sku) , Id = n.Id , OrderKey = n.OrderKey , OrderId = n.OrderId , LineNumber = n.LineNumber , ContractNum = n.ContractNum , SKUId = n.SKUId , ProductionSku = n.ProductionSku , Model = n.Model , Qty = n.Qty , UOM = n.UOM , Price = n.Price , SubTotal = n.SubTotal , Remark = n.Remark , Status = n.Status }).ToList();
            var pagelist = new { total = totalCount, rows = datarows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetDataWithOrderId(int orderid=0,int page = 1, int rows = 10, string sort = "Id", string order = "asc", string filterRules = "")
        {
            var filters = JsonConvert.DeserializeObject<IEnumerable<filterRule>>(filterRules);
            int totalCount = 0;
            //int pagenum = offset / limit +1;

            var orderdetails = _orderDetailService.Query(new OrderDetailQuery().WithOrderId(orderid)).Include(o => o.Order).Include(o => o.SKU).OrderBy(n => n.OrderBy(sort, order)).SelectPage(page, rows, out totalCount);

            var datarows = orderdetails.Select(n => new { OrderOrderKey = (n.Order == null ? "" : n.Order.OrderKey), SKUSku = (n.SKU == null ? "" : n.SKU.Sku), Id = n.Id, OrderKey = n.OrderKey, OrderId = n.OrderId, LineNumber = n.LineNumber, ContractNum = n.ContractNum, SKUId = n.SKUId, ProductionSku = n.ProductionSku, Model = n.Model, Qty = n.Qty, UOM = n.UOM, Price = n.Price, SubTotal = n.SubTotal, Remark = n.Remark, Status = n.Status }).ToList();
            var pagelist = new { total = totalCount, rows = datarows };
            return Json(pagelist, JsonRequestBehavior.AllowGet);
        }

		[HttpPost]
		public ActionResult SaveData(OrderDetailChangeViewModel orderdetails)
        {
            if (orderdetails.updated != null)
            {
                foreach (var updated in orderdetails.updated)
                {
                    _orderDetailService.Update(updated);
                }
            }
            if (orderdetails.deleted != null)
            {
                foreach (var deleted in orderdetails.deleted)
                {
                    _orderDetailService.Delete(deleted);
                }
            }
            if (orderdetails.inserted != null)
            {
                foreach (var inserted in orderdetails.inserted)
                {
                    _orderDetailService.Insert(inserted);
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
				public ActionResult GetSKUs()
        {
            var skuRepository = _unitOfWork.Repository<SKU>();
            var data = skuRepository.Queryable().ToList();
            var rows = data.Select(n => new { Id = n.Id, Sku = n.Sku });
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
		
		
       
        // GET: OrderDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = _orderDetailService.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }
        

        // GET: OrderDetails/Create
        public ActionResult Create()
        {
            OrderDetail orderDetail = new OrderDetail();
            //set default value
            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey");
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku");
            return View(orderDetail);
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Order,SKU,Id,OrderKey,OrderId,LineNumber,ContractNum,SKUId,ProductionSku,Model,Qty,UOM,Price,SubTotal,Remark,Status,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
             				_orderDetailService.Insert(orderDetail);
                           _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has append a OrderDetail record");
                return RedirectToAction("Index");
            }

            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey", orderDetail.OrderId);
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", orderDetail.SKUId);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(orderDetail);
        }

        // GET: OrderDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = _orderDetailService.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey", orderDetail.OrderId);
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", orderDetail.SKUId);
            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Order,SKU,Id,OrderKey,OrderId,LineNumber,ContractNum,SKUId,ProductionSku,Model,Qty,UOM,Price,SubTotal,Remark,Status,CreatedUserId,CreatedDateTime,LastEditUserId,LastEditDateTime")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                orderDetail.ObjectState = ObjectState.Modified;
                				_orderDetailService.Update(orderDetail);
                                
                _unitOfWork.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                DisplaySuccessMessage("Has update a OrderDetail record");
                return RedirectToAction("Index");
            }
            var orderRepository = _unitOfWork.Repository<Order>();
            ViewBag.OrderId = new SelectList(orderRepository.Queryable(), "Id", "OrderKey", orderDetail.OrderId);
            var skuRepository = _unitOfWork.Repository<SKU>();
            ViewBag.SKUId = new SelectList(skuRepository.Queryable(), "Id", "Sku", orderDetail.SKUId);
            if (Request.IsAjaxRequest())
            {
                var modelStateErrors =String.Join("", this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(n=>n.ErrorMessage)));
                return Json(new { success = false, err = modelStateErrors }, JsonRequestBehavior.AllowGet);
            }
            DisplayErrorMessage();
            return View(orderDetail);
        }

        // GET: OrderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = _orderDetailService.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDetail orderDetail =  _orderDetailService.Find(id);
             _orderDetailService.Delete(orderDetail);
            _unitOfWork.SaveChanges();
           if (Request.IsAjaxRequest())
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            DisplaySuccessMessage("Has delete a OrderDetail record");
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
