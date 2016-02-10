
                    
      
     
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
   public class ProductionPlanQuery:QueryObject<ProductionPlan>
    {
        public ProductionPlanQuery WithAnySearch(string search)
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.OrderKey.Contains(search) || x.DesignName.Contains(search) || x.ComponentSKU.Contains(search) || x.ALTSku.Contains(search) || x.GraphSKU.Contains(search) || x.StockSKU.Contains(search) || x.Deploy.Contains(search) || x.Locator.Contains(search) || x.ProductionLine.Contains(search) || x.OrderPlanDate.ToString().Contains(search) || x.ActualFinishDate.ToString().Contains(search) || x.Remark.Contains(search) );
            return this;
        }

		public ProductionPlanQuery WithPopupSearch(string search,string para="")
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.OrderKey.Contains(search) || x.DesignName.Contains(search) || x.ComponentSKU.Contains(search) || x.ALTSku.Contains(search) || x.GraphSKU.Contains(search) || x.StockSKU.Contains(search) || x.Deploy.Contains(search) || x.Locator.Contains(search) || x.ProductionLine.Contains(search) || x.OrderPlanDate.ToString().Contains(search) || x.ActualFinishDate.ToString().Contains(search) || x.Remark.Contains(search) );
            return this;
        }

		public ProductionPlanQuery Withfilter(IEnumerable<filterRule> filters)
        {
           if (filters != null)
           {
               foreach (var rule in filters)
               {
                  
					
				    						if (rule.field == "Id")
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.Id == val);
						}
				   
					 				
											if (rule.field == "OrderKey")
						{
							And(x => x.OrderKey.Contains(rule.value));
						}
				    
				    
					 				
					
				    						if (rule.field == "SKUId")
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.SKUId == val);
						}
				   
					 				
											if (rule.field == "DesignName")
						{
							And(x => x.DesignName.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "ComponentSKU")
						{
							And(x => x.ComponentSKU.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "ALTSku")
						{
							And(x => x.ALTSku.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "GraphSKU")
						{
							And(x => x.GraphSKU.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "StockSKU")
						{
							And(x => x.StockSKU.Contains(rule.value));
						}
				    
				    
					 				
					
				    						if (rule.field == "Status")
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.Status == val);
						}
				   
					 				
					
				    						if (rule.field == "ConsumeQty")
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.ConsumeQty == val);
						}
				   
					 				
					
				    						if (rule.field == "RequirementQty")
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.RequirementQty == val);
						}
				   
					 				
					
				    						if (rule.field == "RejectRatio")
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.RejectRatio == val);
						}
				   
					 				
											if (rule.field == "Deploy")
						{
							And(x => x.Deploy.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "Locator")
						{
							And(x => x.Locator.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "ProductionLine")
						{
							And(x => x.ProductionLine.Contains(rule.value));
						}
				    
				    
					 				
					
				    
					 						if (rule.field == "OrderPlanDate")
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.OrderPlanDate)>0);
						}
				   				
					
				    
					 						if (rule.field == "ActualFinishDate")
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.ActualFinishDate)>0);
						}
				   				
											if (rule.field == "Remark")
						{
							And(x => x.Remark.Contains(rule.value));
						}
				    
				    
					 				
					
				    						if (rule.field == "OrderId")
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



