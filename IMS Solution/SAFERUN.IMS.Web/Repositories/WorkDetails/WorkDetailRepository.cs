
                    
      
    
 
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Repository.Pattern.Repositories;

using SAFERUN.IMS.Web.Models;

namespace SAFERUN.IMS.Web.Repositories
{
  public static class WorkDetailRepository  
    {
 
                 public static IEnumerable<WorkDetail> GetByWorkId(this IRepositoryAsync<WorkDetail> repository, int workid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.WorkId==workid);
             return query;

         }
             
                 public static IEnumerable<WorkDetail> GetByParentSKUId(this IRepositoryAsync<WorkDetail> repository, int parentskuid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.ParentSKUId==parentskuid);
             return query;

         }
             
                 public static IEnumerable<WorkDetail> GetByComponentSKUId(this IRepositoryAsync<WorkDetail> repository, int componentskuid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.ComponentSKUId==componentskuid);
             return query;

         }
             
        
         
	}
}



