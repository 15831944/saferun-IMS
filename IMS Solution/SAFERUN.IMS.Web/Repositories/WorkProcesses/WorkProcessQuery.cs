
                    
      
     
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
   public class WorkProcessQuery:QueryObject<WorkProcess>
    {
        public WorkProcessQuery WithAnySearch(string search)
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.WorkNo.Contains(search) || x.OrderKey.Contains(search) || x.ProjectName.Contains(search) || x.GraphSKU.Contains(search) || x.Operator.Contains(search) || x.WorkDate.ToString().Contains(search) || x.Remark.Contains(search) );
            return this;
        }

		public WorkProcessQuery WithPopupSearch(string search,string para="")
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.WorkNo.Contains(search) || x.OrderKey.Contains(search) || x.ProjectName.Contains(search) || x.GraphSKU.Contains(search) || x.Operator.Contains(search) || x.WorkDate.ToString().Contains(search) || x.Remark.Contains(search) );
            return this;
        }

		public WorkProcessQuery Withfilter(IEnumerable<filterRule> filters)
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
				   
					
				    				
											if (rule.field == "WorkNo"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.WorkNo.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    						if (rule.field == "WorkId" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.WorkId == val);
						}
				   
					
				    				
					
				    						if (rule.field == "OrderId" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.OrderId == val);
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
				    
				    
					
				    				
					
				    						if (rule.field == "RequirementQty" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.RequirementQty == val);
						}
				   
					
				    				
					
				    						if (rule.field == "ProductionQty" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.ProductionQty == val);
						}
				   
					
				    				
					
				    						if (rule.field == "FinishedQty" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.FinishedQty == val);
						}
				   
					
				    				
					
				    						if (rule.field == "WorkItems" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.WorkItems == val);
						}
				   
					
				    				
					
				    						if (rule.field == "ProductionProcessId" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.ProductionProcessId == val);
						}
				   
					
				    				
					
				    						if (rule.field == "Status" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.Status == val);
						}
				   
					
				    				
											if (rule.field == "Operator"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Operator.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    
											if (rule.field == "WorkDate" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.WorkDate)>0);
						}
				   
				    				
											if (rule.field == "Remark"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Remark.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    						if (rule.field == "WorkDetailId" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.WorkDetailId == val);
						}
				   
					
				    				
					
				    						if (rule.field == "CustomerId" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.CustomerId == val);
						}
				   
					
				    									
                   
               }
           }
            return this;
        }
    }
}



