
             
           
 

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
    public class ProductionPlanService : Service< ProductionPlan >, IProductionPlanService
    {

        private readonly IRepositoryAsync<ProductionPlan> _repository;
        public  ProductionPlanService(IRepositoryAsync< ProductionPlan> repository)
            : base(repository)
        {
            _repository=repository;
        }
        
                 public  IEnumerable<ProductionPlan> GetBySKUId(int  skuid)
         {
            return _repository.GetBySKUId(skuid);
         }
                  public  IEnumerable<ProductionPlan> GetByOrderId(int  orderid)
         {
            return _repository.GetByOrderId(orderid);
         }
                   
        
    }
}



