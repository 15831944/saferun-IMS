
                    
      
     
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
   public class ProcessStepQuery:QueryObject<ProcessStep>
    {
        public ProcessStepQuery WithAnySearch(string search)
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.StepName.Contains(search) || x.Equipment.Contains(search) || x.Description.Contains(search) );
            return this;
        }

		public ProcessStepQuery WithPopupSearch(string search,string para="")
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.StepName.Contains(search) || x.Equipment.Contains(search) || x.Description.Contains(search) );
            return this;
        }

		public ProcessStepQuery Withfilter(IEnumerable<filterRule> filters)
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
				   
					
				    				
											if (rule.field == "StepName"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.StepName.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    						if (rule.field == "Order" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.Order == val);
						}
				   
					
				    				
					
				    						if (rule.field == "ElapsedTime" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.ElapsedTime == val);
						}
				   
					
				    				
											if (rule.field == "Equipment"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Equipment.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    						if (rule.field == "Status" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.Status == val);
						}
				   
					
				    				
											if (rule.field == "Description"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Description.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    						if (rule.field == "ProductionProcessId" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.ProductionProcessId == val);
						}
				   
					
				    									
                   
               }
           }
            return this;
        }
    }
}



