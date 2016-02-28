
                    
      
    
 
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Repository.Pattern.Repositories;

using SAFERUN.IMS.Web.Models;

namespace SAFERUN.IMS.Web.Repositories
{
  public static class AssemblyPlanRepository  
    {
 
                 public static IEnumerable<AssemblyPlan> GetBySKUId(this IRepositoryAsync<AssemblyPlan> repository, int skuid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.SKUId==skuid);
             return query;

         }
             
                 public static IEnumerable<AssemblyPlan> GetByOrderId(this IRepositoryAsync<AssemblyPlan> repository, int orderid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.OrderId==orderid);
             return query;

         }
             
        
         
	}
}



