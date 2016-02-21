
                    
      
     
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity.SqlServer;
using Repository.Pattern.Repositories;
using Repository.Pattern.Ef6;
using SAFERUN.IMS.Web.Models;
using SAFERUN.IMS.Web.Extensions;

namespace SAFERUN.IMS.Web.Repositories
{
   public class DepartmentQuery:QueryObject<Department>
    {
        public DepartmentQuery WithAnySearch(string search)
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.Name.Contains(search) || x.Description.Contains(search) );
            return this;
        }

		public DepartmentQuery WithPopupSearch(string search,string para="")
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.Name.Contains(search) || x.Description.Contains(search) );
            return this;
        }

		public DepartmentQuery Withfilter(IEnumerable<filterRule> filters)
        {
           if (filters != null)
           {
               foreach (var rule in filters)
               {
                  
					
				    						if (rule.field == "Id" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.Id == val);
						}
				   
					
				    				
											if (rule.field == "Name"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Name.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "Description"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Description.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    						if (rule.field == "ParentDepartmentId" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.ParentDepartmentId == val);
						}
				   
					
				    									
                   
               }
           }
            return this;
        }
    }
}



