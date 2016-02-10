
                    
      
    
 
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Repository.Pattern.Repositories;

using SAFERUN.IMS.Web.Models;

namespace SAFERUN.IMS.Web.Repositories
{
  public static class OrderAuditPlanRepository  
    {
 
                 public static IEnumerable<OrderAuditPlan> GetByOrderId(this IRepositoryAsync<OrderAuditPlan> repository, int orderid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.OrderId==orderid);
             return query;

         }
             
        
         
	}
}



