
             
           
 

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
    public class OrderAuditPlanService : Service< OrderAuditPlan >, IOrderAuditPlanService
    {

        private readonly IRepositoryAsync<OrderAuditPlan> _repository;
        public  OrderAuditPlanService(IRepositoryAsync< OrderAuditPlan> repository)
            : base(repository)
        {
            _repository=repository;
        }
        
                 public  IEnumerable<OrderAuditPlan> GetByOrderId(int  orderid)
         {
            return _repository.GetByOrderId(orderid);
         }
                   
        
    }
}



