
             
           
 

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
    public class OrderService : Service< Order >, IOrderService
    {

        private readonly IRepositoryAsync<Order> _repository;
        public  OrderService(IRepositoryAsync< Order> repository)
            : base(repository)
        {
            _repository=repository;
        }
        
                 public  IEnumerable<Order> GetByCustomerId(int  customerid)
         {
            return _repository.GetByCustomerId(customerid);
         }
                  public  IEnumerable<Order> GetByProjectTypeId(int  projecttypeid)
         {
            return _repository.GetByProjectTypeId(projecttypeid);
         }
                          public IEnumerable<OrderDetail>   GetOrderDetailsByOrderId (int orderid)
        {
            return _repository.GetOrderDetailsByOrderId(orderid);
        }
         
        
    }
}



