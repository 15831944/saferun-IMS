
                    
      
    
 
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Repository.Pattern.Repositories;

using SAFERUN.IMS.Web.Models;

namespace SAFERUN.IMS.Web.Repositories
{
  public static class ProjectNodeRepository  
    {
 
                 public static IEnumerable<ProjectNode> GetByDepartmentId(this IRepositoryAsync<ProjectNode> repository, int departmentid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.DepartmentId==departmentid);
             return query;

         }
             
                 public static IEnumerable<ProjectNode> GetByProjectTypeId(this IRepositoryAsync<ProjectNode> repository, int projecttypeid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.ProjectTypeId==projecttypeid);
             return query;

         }
             
        
         
	}
}



