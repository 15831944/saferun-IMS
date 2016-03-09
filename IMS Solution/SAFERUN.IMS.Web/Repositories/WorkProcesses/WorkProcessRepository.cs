
                    
      
    
 
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Repository.Pattern.Repositories;

using SAFERUN.IMS.Web.Models;

namespace SAFERUN.IMS.Web.Repositories
{
  public static class WorkProcessRepository  
    {
 
                 public static IEnumerable<WorkProcess> GetByWorkId(this IRepositoryAsync<WorkProcess> repository, int workid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.WorkId==workid);
             return query;

         }
             
                 public static IEnumerable<WorkProcess> GetByOrderId(this IRepositoryAsync<WorkProcess> repository, int orderid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.OrderId==orderid);
             return query;

         }
             
                 public static IEnumerable<WorkProcess> GetBySKUId(this IRepositoryAsync<WorkProcess> repository, int skuid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.SKUId==skuid);
             return query;

         }
             
                 public static IEnumerable<WorkProcess> GetByProductionProcessId(this IRepositoryAsync<WorkProcess> repository, int productionprocessid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.ProductionProcessId==productionprocessid);
             return query;

         }
             
                 public static IEnumerable<WorkProcess> GetByWorkDetailId(this IRepositoryAsync<WorkProcess> repository, int workdetailid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.WorkDetailId==workdetailid);
             return query;

         }
             
                 public static IEnumerable<WorkProcess> GetByCustomerId(this IRepositoryAsync<WorkProcess> repository, int customerid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.CustomerId==customerid);
             return query;

         }
             
        
                public static IEnumerable<WorkProcessDetail>   GetWorkProcessDetailsByWorkProcessId (this IRepositoryAsync<WorkProcess> repository,int workprocessid)
        {
			var workprocessdetailRepository = repository.GetRepository<WorkProcessDetail>(); 
            return workprocessdetailRepository.Queryable().Include(x => x.ProcessStep).Include(x => x.Station).Include(x => x.WorkProcess).Where(n => n.WorkProcessId == workprocessid);
        }
         
	}
}



