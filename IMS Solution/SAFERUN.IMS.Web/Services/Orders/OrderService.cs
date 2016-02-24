




using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Repository.Pattern.Repositories;
using Service.Pattern;

using SAFERUN.IMS.Web.Models;
using SAFERUN.IMS.Web.Repositories;
namespace SAFERUN.IMS.Web.Services
{
    public class OrderService : Service<Order>, IOrderService
    {

        private readonly IRepositoryAsync<Order> _repository;
        private readonly IRepositoryAsync<ProjectNode> _projectnoderepository;
        private readonly IOrderAuditPlanService _auditplanService;
        private readonly IRepositoryAsync<BOMComponent> _bomrepository;
        private readonly IProductionPlanService _productionplanservice;
        private readonly IRepositoryAsync<OrderDetail> _orderdetailrepository;
        private readonly IPurchasePlanService _purchaseservice;
        public OrderService(IPurchasePlanService purchaseservice,IRepositoryAsync<OrderDetail> orderdetailrepository,IRepositoryAsync<Order> repository, IRepositoryAsync<ProjectNode> projectnoderepository, IOrderAuditPlanService auditplanService, IRepositoryAsync<BOMComponent> bomrepository, IProductionPlanService productionplanservice)
            : base(repository)
        {
            _repository = repository;
            _projectnoderepository = projectnoderepository;
            _auditplanService = auditplanService;
            _bomrepository = bomrepository;
            _productionplanservice = productionplanservice;
            _orderdetailrepository = orderdetailrepository;
            _purchaseservice = purchaseservice;
        }

        public IEnumerable<Order> GetByCustomerId(int customerid)
        {
            return _repository.GetByCustomerId(customerid);
        }
        public IEnumerable<Order> GetByProjectTypeId(int projecttypeid)
        {
            return _repository.GetByProjectTypeId(projecttypeid);
        }
        public IEnumerable<OrderDetail> GetOrderDetailsByOrderId(int orderid)
        {
            return _repository.GetOrderDetailsByOrderId(orderid);
        }




        public IEnumerable<OrderAuditPlan> GenerateAuditPlan(int orderId)
        {
            List<OrderAuditPlan> list = new List<OrderAuditPlan>();
            list = _auditplanService.Queryable().Where(x => x.OrderId == orderId).ToList();
            if (list.Count > 0)
            {
                return list;
            }
            else
            {
                var order = this.Find(orderId);
                var projectNodelist = _projectnoderepository.Queryable().Include(x => x.Department).Where(x => x.ProjectTypeId == order.ProjectTypeId).OrderBy(x => x.Order);
                foreach (var item in projectNodelist)
                {
                    OrderAuditPlan plan = new OrderAuditPlan();
                    plan.AuditContent = item.Name;
                    plan.Department = item.Department.Name;
                    plan.OrderId = order.Id;
                    plan.OrderKey = order.OrderKey;
                    plan.PlanAuditDate = order.OrderDate.AddDays(item.ElapsedDay);
                    plan.Status = 0;
                    _auditplanService.Insert(plan);
                    list.Add(plan);
                }
                return list;
            }
        }


        public IEnumerable<ProductionPlan> GenerateProductionPlan(int orderId)
        {
            List<ProductionPlan> list = new List<ProductionPlan>();
            var order = this.Find(orderId);
            list = _productionplanservice.Queryable().Where(x => x.OrderId == orderId).ToList();
            if (list.Count > 0)
            {
                return list;
            }
            else
            {
                var orderdetails = _orderdetailrepository.Queryable().Where(x => x.OrderId == orderId).ToList();
                foreach (var item in orderdetails)
                {
                    string[] madeTypes=new string[]{"自制","外协"};

                    var bomlist = _bomrepository.Queryable().Where(x => x.FinishedSKU == item.ProductionSku &&  madeTypes.Contains( x.MadeType)).ToList();
                    foreach (var component in bomlist)
                    {
                        ProductionPlan plan = new ProductionPlan();
                        plan.OrderId = orderId;
                        plan.OrderKey = order.OrderKey;
                        plan.BomComponentId = component.Id;
                        plan.ComponentSKU = component.ComponentSKU;
                        plan.ConsumeQty = component.ConsumeQty;
                        plan.Deploy = component.Deploy;
                        plan.DesignName = component.DesignName;
                        plan.GraphSKU = component.GraphSKU;
                        plan.Locator = component.Locator;
                        plan.OrderPlanDate = order.OrderDate;
                        plan.ParentBomComponentId = component.ParentComponentId;
                        plan.ProductionLine = component.ProductionLine;
                        plan.RejectRatio = component.RejectRatio;
                        plan.Remark = component.Remark1;
                        plan.RequirementQty = item.Qty * component.ConsumeQty;
                        plan.SKUId = component.SKUId;
                        plan.StockSKU = component.StockSKU;
                        list.Add(plan);
                        _productionplanservice.Insert(plan);
                    }
                }


                return list;
            }
        }

        public IEnumerable<PurchasePlan> GeneratePurchasePlan(int orderId)
        {
            List<PurchasePlan> list = new List<PurchasePlan>();
            var order = this.Find(orderId);
            list = _purchaseservice.Queryable().Where(x => x.OrderId == orderId).ToList();
            if (list.Count > 0)
            {
                return list;
            }
            else
            {
                var orderdetails = _orderdetailrepository.Queryable().Where(x => x.OrderId == orderId).ToList();
                foreach (var item in orderdetails)
                {
                    var bomlist = _bomrepository.Queryable().Include(x=>x.ParentComponent).Include(s=>s.SKU).Where(x => x.FinishedSKU == item.ProductionSku && x.MadeType=="外购").ToList();
                    foreach (var component in bomlist)
                    {
                        PurchasePlan purchase = new PurchasePlan();
                        purchase.OrderId = orderId;
                        purchase.OrderKey = order.OrderKey;
                        purchase.BomComponentId = component.Id;
                        purchase.ComponentSKU = component.ComponentSKU;
                        purchase.ConsumeQty = component.ConsumeQty;
                        purchase.Deploy = component.Deploy;
                        purchase.DesignName = component.DesignName;
                        purchase.GraphSKU = component.GraphSKU;
                        purchase.Locator = component.Locator;
                        purchase.OrderPlanDate = order.OrderDate;
                        purchase.ParentBomComponentId = component.ParentComponentId;
                        //purchase.ProductionLine = component.ProductionLine;
                        purchase.Brand = component.SKU.Brand;

                        purchase.RejectRatio = component.RejectRatio;
                        purchase.Remark = component.Remark1;
                        purchase.RequirementQty = item.Qty * component.ConsumeQty;
                        purchase.SKUId = component.SKUId;
                        purchase.StockSKU = component.StockSKU;
                        list.Add(purchase);
                        this._purchaseservice.Insert(purchase);
                    }
                }


                return list;
            }
        }
    }
}



