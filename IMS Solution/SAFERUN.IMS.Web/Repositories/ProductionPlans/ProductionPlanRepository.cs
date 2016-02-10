
                    
      
    
 
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Repository.Pattern.Repositories;

using SAFERUN.IMS.Web.Models;

namespace SAFERUN.IMS.Web.Repositories
{
  public static class ProductionPlanRepository  
    {
 
                 public static IEnumerable<ProductionPlan> GetBySKUId(this IRepositoryAsync<ProductionPlan> repository, int skuid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.SKUId==skuid);
             return query;

         }
             
                 public static IEnumerable<ProductionPlan> GetByOrderId(this IRepositoryAsync<ProductionPlan> repository, int orderid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.OrderId==orderid);
             return query;

         }
             
        
         
	}
}



