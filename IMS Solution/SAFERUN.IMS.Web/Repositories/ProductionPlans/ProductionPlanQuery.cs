
                    
      
     
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
   public class ProductionPlanQuery:QueryObject<ProductionPlan>
    {
        public ProductionPlanQuery WithAnySearch(string search)
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.OrderKey.Contains(search) || x.DesignName.Contains(search) || x.ComponentSKU.Contains(search) || x.ALTSku.Contains(search) || x.GraphSKU.Contains(search) || x.StockSKU.Contains(search) || x.SKUGroup.Contains(search) || x.MadeType.Contains(search) || x.Deploy.Contains(search) || x.Locator.Contains(search) || x.ProductionLine.Contains(search) || x.OrderPlanDate.ToString().Contains(search) || x.PlanedStartDate.ToString().Contains(search) || x.PlanedCompletedDate.ToString().Contains(search) || x.ActualStartDate.ToString().Contains(search) || x.ActualCompletedDate.ToString().Contains(search) || x.ActualFinishDate.ToString().Contains(search) || x.Remark.Contains(search) );
            return this;
        }

		public ProductionPlanQuery WithPopupSearch(string search,string para="")
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.OrderKey.Contains(search) || x.DesignName.Contains(search) || x.ComponentSKU.Contains(search) || x.ALTSku.Contains(search) || x.GraphSKU.Contains(search) || x.StockSKU.Contains(search) || x.SKUGroup.Contains(search) || x.MadeType.Contains(search) || x.Deploy.Contains(search) || x.Locator.Contains(search) || x.ProductionLine.Contains(search) || x.OrderPlanDate.ToString().Contains(search) || x.PlanedStartDate.ToString().Contains(search) || x.PlanedCompletedDate.ToString().Contains(search) || x.ActualStartDate.ToString().Contains(search) || x.ActualCompletedDate.ToString().Contains(search) || x.ActualFinishDate.ToString().Contains(search) || x.Remark.Contains(search) );
            return this;
        }
        public ProductionPlanQuery WithOrderIdSku(int orderid = 0, string p = "")
        {
            if (orderid > 0)
                And(x => x.OrderId == orderid);
            if (!string.IsNullOrEmpty(p))
            {
                int skuid = 0;
                if (int.TryParse(p, out skuid))
                {
                    And(x => x.SKUId == skuid);
                }
                else
                {
                    And(x => x.ComponentSKU.Contains(p));
                }
            }
            return this;
        }
		public ProductionPlanQuery Withfilter(IEnumerable<filterRule> filters)
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
				    
				    
					
				    				
											if (rule.field == "StockSKU"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.StockSKU.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "SKUGroup"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.SKUGroup.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "MadeType"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.MadeType.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    						if (rule.field == "Status" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.Status == val);
						}
				   
					
				    				
					
				    						if (rule.field == "ConsumeQty" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.ConsumeQty == val);
						}
				   
					
				    				
					
				    						if (rule.field == "RequirementQty" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.RequirementQty == val);
						}
				   
					
				    				
					
				    						if (rule.field == "RejectRatio" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.RejectRatio == val);
						}
				   
					
				    				
											if (rule.field == "Deploy"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Deploy.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    						if (rule.field == "ProductionProcessId" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.ProductionProcessId == val);
						}
				   
					
				    				
											if (rule.field == "Locator"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Locator.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "ProductionLine"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ProductionLine.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    
											if (rule.field == "OrderPlanDate" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.OrderPlanDate)>=0);
						}
				   
				    				
					
				    
											if (rule.field == "PlanedStartDate" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.PlanedStartDate)>=0);
						}
				   
				    				
					
				    
											if (rule.field == "PlanedCompletedDate" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.PlanedCompletedDate)>=0);
						}
				   
				    				
					
				    
											if (rule.field == "ActualStartDate" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.ActualStartDate)>=0);
						}
				   
				    				
					
				    
											if (rule.field == "ActualCompletedDate" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.ActualCompletedDate)>=0);
						}
				   
				    				
					
				    
											if (rule.field == "ActualFinishDate" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.ActualFinishDate)>=0);
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



