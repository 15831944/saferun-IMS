﻿




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
    public interface IOrderService : IService<Order>
    {

        IEnumerable<Order> GetByCustomerId(int customerid);
        IEnumerable<Order> GetByProjectTypeId(int projecttypeid);

        IEnumerable<OrderDetail> GetOrderDetailsByOrderId(int orderid);

        IEnumerable<OrderAuditPlan> GenerateAuditPlan(int orderId);
        IEnumerable<ProductionPlan> GenerateProductionPlan(int orderId);
        IEnumerable<PurchasePlan> GeneratePurchasePlan(int orderId);

        IEnumerable<AssemblyPlan> GenerateAssemblyPlan(int orderId);

    }
}