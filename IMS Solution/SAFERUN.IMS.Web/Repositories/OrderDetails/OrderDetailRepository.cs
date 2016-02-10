
                    
      
    
 
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Repository.Pattern.Repositories;

using SAFERUN.IMS.Web.Models;

namespace SAFERUN.IMS.Web.Repositories
{
  public static class OrderDetailRepository  
    {
 
                 public static IEnumerable<OrderDetail> GetByOrderId(this IRepositoryAsync<OrderDetail> repository, int orderid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.OrderId==orderid);
             return query;

         }
             
                 public static IEnumerable<OrderDetail> GetBySKUId(this IRepositoryAsync<OrderDetail> repository, int skuid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.SKUId==skuid);
             return query;

         }
             
        
         
	}
}



