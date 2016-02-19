
                    
      
    
 
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Repository.Pattern.Repositories;

using SAFERUN.IMS.Web.Models;

namespace SAFERUN.IMS.Web.Repositories
{
  public static class ProcessStepRepository  
    {
 
                 public static IEnumerable<ProcessStep> GetByProductionProcessId(this IRepositoryAsync<ProcessStep> repository, int productionprocessid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.ProductionProcessId==productionprocessid);
             return query;

         }
             
        
         
	}
}



