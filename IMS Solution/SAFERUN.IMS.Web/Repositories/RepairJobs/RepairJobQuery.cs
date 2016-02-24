
                    
      
     
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
   public class RepairJobQuery:QueryObject<RepairJob>
    {
        public RepairJobQuery WithAnySearch(string search)
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.OrderKey.Contains(search) || x.ProjectName.Contains(search) || x.GraphSKU.Contains(search) || x.DesignName.Contains(search) || x.Issue.Contains(search) || x.ProcessName.Contains(search) || x.ResponsibleDepartment.Contains(search) || x.Owner.Contains(search) || x.StartDateTime.ToString().Contains(search) || x.CompletedDateTime.ToString().Contains(search) || x.Remark.Contains(search) );
            return this;
        }

		public RepairJobQuery WithPopupSearch(string search,string para="")
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.OrderKey.Contains(search) || x.ProjectName.Contains(search) || x.GraphSKU.Contains(search) || x.DesignName.Contains(search) || x.Issue.Contains(search) || x.ProcessName.Contains(search) || x.ResponsibleDepartment.Contains(search) || x.Owner.Contains(search) || x.StartDateTime.ToString().Contains(search) || x.CompletedDateTime.ToString().Contains(search) || x.Remark.Contains(search) );
            return this;
        }

		public RepairJobQuery Withfilter(IEnumerable<filterRule> filters)
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
				   
					
				    				
											if (rule.field == "OrderKey"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.OrderKey.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "ProjectName"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ProjectName.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    						if (rule.field == "SKUId" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.SKUId == val);
						}
				   
					
				    				
											if (rule.field == "GraphSKU"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.GraphSKU.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "DesignName"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.DesignName.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    						if (rule.field == "RepairQty" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.RepairQty == val);
						}
				   
					
				    				
											if (rule.field == "Issue"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Issue.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "ProcessName"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ProcessName.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "ResponsibleDepartment"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ResponsibleDepartment.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "Owner"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Owner.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    
											if (rule.field == "StartDateTime" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.StartDateTime)>0);
						}
				   
				    				
					
				    
											if (rule.field == "CompletedDateTime" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.CompletedDateTime)>0);
						}
				   
				    				
					
				    						if (rule.field == "ElapsedTime" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.ElapsedTime == val);
						}
				   
					
				    				
					
				    						if (rule.field == "Status" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.Status == val);
						}
				   
					
				    				
											if (rule.field == "Remark"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Remark.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    						if (rule.field == "OrderId" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.OrderId == val);
						}
				   
					
				    									
                   
               }
           }
            return this;
        }
    }
}



