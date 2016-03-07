
                    
      
    
 
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Repository.Pattern.Repositories;

using SAFERUN.IMS.Web.Models;

namespace SAFERUN.IMS.Web.Repositories
{
  public static class ScheduleDetailRepository  
    {
 
                 public static IEnumerable<ScheduleDetail> GetByParentSKUId(this IRepositoryAsync<ScheduleDetail> repository, int parentskuid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.ParentSKUId==parentskuid);
             return query;

         }
             
                 public static IEnumerable<ScheduleDetail> GetByComponentSKUId(this IRepositoryAsync<ScheduleDetail> repository, int componentskuid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.ComponentSKUId==componentskuid);
             return query;

         }
             
                 public static IEnumerable<ScheduleDetail> GetByStationId(this IRepositoryAsync<ScheduleDetail> repository, int stationid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.StationId==stationid);
             return query;

         }
             
                 public static IEnumerable<ScheduleDetail> GetByShiftId(this IRepositoryAsync<ScheduleDetail> repository, int shiftid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.ShiftId==shiftid);
             return query;

         }
             
                 public static IEnumerable<ScheduleDetail> GetByProductionScheduleId(this IRepositoryAsync<ScheduleDetail> repository, int productionscheduleid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.ProductionScheduleId==productionscheduleid);
             return query;

         }
             
        
         
	}
}



