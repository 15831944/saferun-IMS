
                    
      
    
 
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Repository.Pattern.Repositories;

using SAFERUN.IMS.Web.Models;

namespace SAFERUN.IMS.Web.Repositories
{
  public static class DefectTrackingRepository  
    {
 
                 public static IEnumerable<DefectTracking> GetByOrderId(this IRepositoryAsync<DefectTracking> repository, int orderid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.OrderId==orderid);
             return query;

         }
             
                 public static IEnumerable<DefectTracking> GetBySKUId(this IRepositoryAsync<DefectTracking> repository, int skuid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.SKUId==skuid);
             return query;

         }
             
                 public static IEnumerable<DefectTracking> GetByDefectTypeId(this IRepositoryAsync<DefectTracking> repository, int defecttypeid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.DefectTypeId==defecttypeid);
             return query;

         }
             
                 public static IEnumerable<DefectTracking> GetByDefectId(this IRepositoryAsync<DefectTracking> repository, int defectid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.DefectCodeId==defectid);
             return query;

         }
             
        
         
	}
}



