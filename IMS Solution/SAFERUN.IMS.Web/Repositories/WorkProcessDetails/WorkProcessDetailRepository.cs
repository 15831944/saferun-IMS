
                    
      
    
 
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Repository.Pattern.Repositories;

using SAFERUN.IMS.Web.Models;

namespace SAFERUN.IMS.Web.Repositories
{
  public static class WorkProcessDetailRepository  
    {
 
                 public static IEnumerable<WorkProcessDetail> GetByWorkProcessId(this IRepositoryAsync<WorkProcessDetail> repository, int workprocessid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.WorkProcessId==workprocessid);
             return query;

         }
             
                 public static IEnumerable<WorkProcessDetail> GetByProcessStepId(this IRepositoryAsync<WorkProcessDetail> repository, int processstepid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.ProcessStepId==processstepid);
             return query;

         }
             
                 public static IEnumerable<WorkProcessDetail> GetByStationId(this IRepositoryAsync<WorkProcessDetail> repository, int stationid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.StationId==stationid);
             return query;

         }
             
        
         
	}
}



