
                    
      
    
 
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Repository.Pattern.Repositories;

using SAFERUN.IMS.Web.Models;

namespace SAFERUN.IMS.Web.Repositories
{
  public static class OrderRepository  
    {
 
                 public static IEnumerable<Order> GetByCustomerId(this IRepositoryAsync<Order> repository, int customerid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.CustomerId==customerid);
             return query;

         }
             
                 public static IEnumerable<Order> GetByProjectTypeId(this IRepositoryAsync<Order> repository, int projecttypeid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.ProjectTypeId==projecttypeid);
             return query;

         }
             
        
                public static IEnumerable<OrderDetail>   GetOrderDetailsByOrderId (this IRepositoryAsync<Order> repository,int orderid)
        {
			var orderdetailRepository = repository.GetRepository<OrderDetail>(); 
            return orderdetailRepository.Queryable().Include(x => x.Order).Include(x => x.SKU).Where(n => n.OrderId == orderid);
        }
         
	}
}



