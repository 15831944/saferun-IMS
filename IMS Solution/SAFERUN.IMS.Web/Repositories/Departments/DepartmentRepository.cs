
                    
      
    
 
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Repository.Pattern.Repositories;

using SAFERUN.IMS.Web.Models;

namespace SAFERUN.IMS.Web.Repositories
{
  public static class DepartmentRepository  
    {
 
                 public static IEnumerable<Department> GetByParentDepartmentId(this IRepositoryAsync<Department> repository, int parentdepartmentid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.ParentDepartmentId==parentdepartmentid);
             return query;

         }
             
        
                public static IEnumerable<Department>   GetDepartmentsByParentDepartmentId (this IRepositoryAsync<Department> repository,int parentdepartmentid)
        {
			var departmentRepository = repository.GetRepository<Department>(); 
            return departmentRepository.Queryable().Include(x => x.ParentDepartment).Where(n => n.ParentDepartmentId == parentdepartmentid);
        }
         
	}
}



