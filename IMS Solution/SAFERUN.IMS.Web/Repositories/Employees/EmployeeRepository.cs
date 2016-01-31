
                    
      
    
 
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Repository.Pattern.Repositories;

using SAFERUN.IMS.Web.Models;

namespace SAFERUN.IMS.Web.Repositories
{
  public static class EmployeeRepository  
    {
 
                 public static IEnumerable<Employee> GetByManagerID(this IRepositoryAsync<Employee> repository, int managerid)
         {
             var query= repository
                .Queryable()
                .Where(x => x.ManagerID==managerid);
             return query;

         }
             
        
         
	}
}



