
                    
      
     
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
   public class ProductionTaskQuery:QueryObject<ProductionTask>
    {
        public ProductionTaskQuery WithAnySearch(string search)
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.OrderKey.Contains(search) || x.DesignName.Contains(search) || x.ComponentSKU.Contains(search) || x.ALTSku.Contains(search) || x.GraphSKU.Contains(search) || x.ProcessName.Contains(search) || x.ProcessSetp.Contains(search) || x.ProductionLine.Contains(search) || x.Equipment.Contains(search) || x.OrderPlanDate.ToString().Contains(search) || x.Owner.Contains(search) || x.PlanStartDateTime.ToString().Contains(search) || x.PlanCompletedDateTime.ToString().Contains(search) || x.ActualStartDateTime.ToString().Contains(search) || x.ActualCompletedDateTime.ToString().Contains(search) || x.Issue.Contains(search) || x.Remark.Contains(search) );
            return this;
        }

		public ProductionTaskQuery WithPopupSearch(string search,string para="")
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.OrderKey.Contains(search) || x.DesignName.Contains(search) || x.ComponentSKU.Contains(search) || x.ALTSku.Contains(search) || x.GraphSKU.Contains(search) || x.ProcessName.Contains(search) || x.ProcessSetp.Contains(search) || x.ProductionLine.Contains(search) || x.Equipment.Contains(search) || x.OrderPlanDate.ToString().Contains(search) || x.Owner.Contains(search) || x.PlanStartDateTime.ToString().Contains(search) || x.PlanCompletedDateTime.ToString().Contains(search) || x.ActualStartDateTime.ToString().Contains(search) || x.ActualCompletedDateTime.ToString().Contains(search) || x.Issue.Contains(search) || x.Remark.Contains(search) );
            return this;
        }

		public ProductionTaskQuery Withfilter(IEnumerable<filterRule> filters)
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
				    
				    
					
				    				
					
				    						if (rule.field == "SKUId" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.SKUId == val);
						}
				   
					
				    				
											if (rule.field == "DesignName"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.DesignName.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "ComponentSKU"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ComponentSKU.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "ALTSku"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ALTSku.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "GraphSKU"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.GraphSKU.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "ProcessName"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ProcessName.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    						if (rule.field == "ProcessOrder" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.ProcessOrder == val);
						}
				   
					
				    				
											if (rule.field == "ProcessSetp"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ProcessSetp.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    						if (rule.field == "AltElapsedTime" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.AltElapsedTime == val);
						}
				   
					
				    				
											if (rule.field == "ProductionLine"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ProductionLine.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "Equipment"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Equipment.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    
											if (rule.field == "OrderPlanDate" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.OrderPlanDate)>0);
						}
				   
				    				
											if (rule.field == "Owner"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Owner.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    
											if (rule.field == "PlanStartDateTime" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.PlanStartDateTime)>0);
						}
				   
				    				
					
				    
											if (rule.field == "PlanCompletedDateTime" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.PlanCompletedDateTime)>0);
						}
				   
				    				
					
				    
											if (rule.field == "ActualStartDateTime" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.ActualStartDateTime)>0);
						}
				   
				    				
					
				    
											if (rule.field == "ActualCompletedDateTime" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.ActualCompletedDateTime)>0);
						}
				   
				    				
					
				    						if (rule.field == "ActualElapsedTime" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.ActualElapsedTime == val);
						}
				   
					
				    				
					
				    						if (rule.field == "Status" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.Status == val);
						}
				   
					
				    				
											if (rule.field == "Issue"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Issue.Contains(rule.value));
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



