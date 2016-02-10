
             
           
 

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
    public class OrderDetailService : Service< OrderDetail >, IOrderDetailService
    {

        private readonly IRepositoryAsync<OrderDetail> _repository;
        public  OrderDetailService(IRepositoryAsync< OrderDetail> repository)
            : base(repository)
        {
            _repository=repository;
        }
        
                 public  IEnumerable<OrderDetail> GetByOrderId(int  orderid)
         {
            return _repository.GetByOrderId(orderid);
         }
                  public  IEnumerable<OrderDetail> GetBySKUId(int  skuid)
         {
            return _repository.GetBySKUId(skuid);
         }
                   
        
    }
}



