
                    
      
    
 
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Repository.Pattern.Repositories;

using SAFERUN.IMS.Web.Models;

namespace SAFERUN.IMS.Web.Repositories
{
  public static class WorkRepository  
    {
 
                 public static IEnumerable<Work> GetByWorkTypeId(this IRepositoryAsync<Work> repository, int worktypeid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.WorkTypeId==worktypeid);
             return query;

         }
             
                 public static IEnumerable<Work> GetByOrderId(this IRepositoryAsync<Work> repository, int orderid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.OrderId==orderid);
             return query;

         }
             
        
                public static IEnumerable<WorkDetail>   GetWorkDetailsByWorkId (this IRepositoryAsync<Work> repository,int workid)
        {
			var workdetailRepository = repository.GetRepository<WorkDetail>(); 
            return workdetailRepository.Queryable().Include(x => x.ComponentSKU).Include(x => x.ParentSKU).Include(x => x.Work).Where(n => n.WorkId == workid);
        }
         
	}
}



