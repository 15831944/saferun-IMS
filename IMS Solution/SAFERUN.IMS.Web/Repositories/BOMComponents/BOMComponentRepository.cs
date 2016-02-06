
                    
      
    
 
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Repository.Pattern.Repositories;

using SAFERUN.IMS.Web.Models;

namespace SAFERUN.IMS.Web.Repositories
{
  public static class BOMComponentRepository  
    {
 
                 public static IEnumerable<BOMComponent> GetBySKUId(this IRepositoryAsync<BOMComponent> repository, int skuid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.SKUId==skuid);
             return query;

         }
             
                 public static IEnumerable<BOMComponent> GetByParentComponentId(this IRepositoryAsync<BOMComponent> repository, int parentcomponentid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.ParentComponentId==parentcomponentid);
             return query;

         }
             
        
                public static IEnumerable<BOMComponent>   GetComponentsByParentComponentId (this IRepositoryAsync<BOMComponent> repository,int parentcomponentid)
        {
			var bomcomponentRepository = repository.GetRepository<BOMComponent>(); 
            return bomcomponentRepository.Queryable().Include(x => x.ParentComponent).Include(x => x.SKU).Where(n => n.ParentComponentId == parentcomponentid);
        }
         
	}
}



