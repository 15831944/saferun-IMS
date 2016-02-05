
                    
      
     
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Repository.Pattern.Repositories;
using Repository.Pattern.Ef6;
using SAFERUN.IMS.Web.Models;
using SAFERUN.IMS.Web.Extensions;
using System.Data.Entity.SqlServer;

namespace SAFERUN.IMS.Web.Repositories
{
   public class ProjectTypeQuery:QueryObject<ProjectType>
    {
        public ProjectTypeQuery WithAnySearch(string search)
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.TypeName.Contains(search) || x.Model.Contains(search) || x.Version.Contains(search) || x.Description.Contains(search) || x.StartDate.ToString().Contains(search) || x.ExpiryDate.ToString().Contains(search) );
            return this;
        }

		public ProjectTypeQuery WithPopupSearch(string search,string para="")
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.TypeName.Contains(search) || x.Model.Contains(search) || x.Version.Contains(search) || x.Description.Contains(search) || x.StartDate.ToString().Contains(search) || x.ExpiryDate.ToString().Contains(search) );
            return this;
        }

		public ProjectTypeQuery Withfilter(IEnumerable<filterRule> filters)
        {
           if (filters != null)
           {
               foreach (var rule in filters)
               {
                  
					
				    						if (rule.field == "Id")
						{
							And(x => x.Id == Convert.ToInt32(rule.value));
						}
				   
					 				
											if (rule.field == "TypeName")
						{
							And(x => x.TypeName.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "Model")
						{
							And(x => x.Model.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "Version")
						{
							And(x => x.Version.Contains(rule.value));
						}
				    
				    
					 				
					
				    						if (rule.field == "Status")
						{
							And(x => x.Status == Convert.ToInt32(rule.value));
						}
				   
					 				
											if (rule.field == "Description")
						{
							And(x => x.Description.Contains(rule.value));
						}
				    
				    
					 				
					
				    
					 						if (rule.field == "StartDate")
						{
                            var date = Convert.ToDateTime(rule.value) ;
                            And(x => SqlFunctions.DateDiff("d", date, x.StartDate)>0);
						}
				   				
					
				    
					 						if (rule.field == "ExpiryDate")
						{
                            var date = Convert.ToDateTime(rule.value).Date;
                            And(x => SqlFunctions.DateDiff("d", date, x.ExpiryDate) > 0);
						}
				   									
                   
               }
           }
            return this;
        }
    }
}



