
                    
      
    
 
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Repository.Pattern.Repositories;

using SAFERUN.IMS.Web.Models;

namespace SAFERUN.IMS.Web.Repositories
{
  public static class DefectTypeRepository  
    {
 
        
                public static IEnumerable<DefectCode>   GetDefectCodesByDefectTypeId (this IRepositoryAsync<DefectType> repository,int defecttypeid)
        {
			var defectcodeRepository = repository.GetRepository<DefectCode>(); 
            return defectcodeRepository.Queryable().Include(x => x.DefectType).Where(n => n.DefectTypeId == defecttypeid);
        }
         
	}
}



