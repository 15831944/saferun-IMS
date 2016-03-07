
                    
      
    
 
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Repository.Pattern.Repositories;

using SAFERUN.IMS.Web.Models;

namespace SAFERUN.IMS.Web.Repositories
{
  public static class ProductionScheduleRepository  
    {
 
                 public static IEnumerable<ProductionSchedule> GetByWorkId(this IRepositoryAsync<ProductionSchedule> repository, int workid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.WorkId==workid);
             return query;

         }
             
                 public static IEnumerable<ProductionSchedule> GetByCustomerId(this IRepositoryAsync<ProductionSchedule> repository, int customerid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.CustomerId==customerid);
             return query;

         }
             
        
                public static IEnumerable<ScheduleDetail>   GetScheduleDetailsByProductionScheduleId (this IRepositoryAsync<ProductionSchedule> repository,int productionscheduleid)
        {
			var scheduledetailRepository = repository.GetRepository<ScheduleDetail>(); 
            return scheduledetailRepository.Queryable().Include(x => x.ComponentSKU).Include(x => x.ParentSKU).Include(x => x.ProductionSchedule).Include(x => x.Shift).Include(x => x.Station).Where(n => n.ProductionScheduleId == productionscheduleid);
        }
         
	}
}



