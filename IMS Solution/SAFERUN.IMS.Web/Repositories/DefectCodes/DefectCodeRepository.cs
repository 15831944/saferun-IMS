
                    
      
    
 
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Repository.Pattern.Repositories;

using SAFERUN.IMS.Web.Models;

namespace SAFERUN.IMS.Web.Repositories
{
  public static class DefectCodeRepository  
    {
 
                 public static IEnumerable<DefectCode> GetByDefectTypeId(this IRepositoryAsync<DefectCode> repository, int defecttypeid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.DefectTypeId==defecttypeid);
             return query;

         }
             
        
         
	}
}



