
                    
      
     
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Repository.Pattern.Repositories;
using Repository.Pattern.Ef6;
using SAFERUN.IMS.Web.Models;
using SAFERUN.IMS.Web.Extensions;

namespace SAFERUN.IMS.Web.Repositories
{
   public class EmployeeQuery:QueryObject<Employee>
    {
        public EmployeeQuery WithAnySearch(string search)
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.Name.Contains(search) || x.WorkNo.Contains(search) || x.Title.Contains(search) || x.BirthDate.ToString().Contains(search) || x.MaritalStatus.Contains(search) || x.Gender.Contains(search) || x.HireDate.ToString().Contains(search) );
            return this;
        }

		public EmployeeQuery WithPopupSearch(string search,string para="")
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.Name.Contains(search) || x.WorkNo.Contains(search) || x.Title.Contains(search) || x.BirthDate.ToString().Contains(search) || x.MaritalStatus.Contains(search) || x.Gender.Contains(search) || x.HireDate.ToString().Contains(search) );
            return this;
        }

		public EmployeeQuery Withfilter(IEnumerable<filterRule> filters)
        {
           if (filters != null)
           {
               foreach (var rule in filters)
               {
                  
					
				    
					 				
					
				    						if (rule.field == "Id")
						{
							And(x => x.Id == Convert.ToInt32(rule.value));
						}
				   
					 				
											if (rule.field == "Name")
						{
							And(x => x.Name.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "WorkNo")
						{
							And(x => x.WorkNo.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "Title")
						{
							And(x => x.Title.Contains(rule.value));
						}
				    
				    
					 				
					
				    
					 						if (rule.field == "BirthDate")
						{
							And(x => x.BirthDate .Date == Convert.ToDateTime(rule.value).Date);
						}
				   				
											if (rule.field == "MaritalStatus")
						{
							And(x => x.MaritalStatus.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "Gender")
						{
							And(x => x.Gender.Contains(rule.value));
						}
				    
				    
					 				
					
				    
					 						if (rule.field == "HireDate")
						{
							And(x => x.HireDate .Date == Convert.ToDateTime(rule.value).Date);
						}
				   									
                   
               }
           }
            return this;
        }
    }
}



