
             
           
 

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
    public class PurchasePlanService : Service< PurchasePlan >, IPurchasePlanService
    {

        private readonly IRepositoryAsync<PurchasePlan> _repository;
        public  PurchasePlanService(IRepositoryAsync< PurchasePlan> repository)
            : base(repository)
        {
            _repository=repository;
        }
        
                 public  IEnumerable<PurchasePlan> GetBySKUId(int  skuid)
         {
            return _repository.GetBySKUId(skuid);
         }
                  public  IEnumerable<PurchasePlan> GetByOrderId(int  orderid)
         {
            return _repository.GetByOrderId(orderid);
         }
                   
        
    }
}



