
                    
      
     
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
   public class PurchasePlanQuery:QueryObject<PurchasePlan>
    {
        public PurchasePlanQuery WithAnySearch(string search)
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.OrderKey.Contains(search) || x.DesignName.Contains(search) || x.ComponentSKU.Contains(search) || x.ALTSku.Contains(search) || x.GraphSKU.Contains(search) || x.StockSKU.Contains(search) || x.Deploy.Contains(search) || x.Locator.Contains(search) || x.Brand.Contains(search) || x.Supplier.Contains(search) || x.OrderPlanDate.ToString().Contains(search) || x.PlanedStartDate.ToString().Contains(search) || x.ActualStartDate.ToString().Contains(search) || x.PlanDeliveryDate.ToString().Contains(search) || x.ActualDeliveryDate.ToString().Contains(search) || x.Remark.Contains(search) );
            return this;
        }

		public PurchasePlanQuery WithPopupSearch(string search,string para="")
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.OrderKey.Contains(search) || x.DesignName.Contains(search) || x.ComponentSKU.Contains(search) || x.ALTSku.Contains(search) || x.GraphSKU.Contains(search) || x.StockSKU.Contains(search) || x.Deploy.Contains(search) || x.Locator.Contains(search) || x.Brand.Contains(search) || x.Supplier.Contains(search) || x.OrderPlanDate.ToString().Contains(search) || x.PlanedStartDate.ToString().Contains(search) || x.ActualStartDate.ToString().Contains(search) || x.PlanDeliveryDate.ToString().Contains(search) || x.ActualDeliveryDate.ToString().Contains(search) || x.Remark.Contains(search) );
            return this;
        }

		public PurchasePlanQuery Withfilter(IEnumerable<filterRule> filters)
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
				    
				    
					
				    				
											if (rule.field == "Locator"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Locator.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "Brand"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Brand.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "Supplier"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Supplier.Contains(rule.value));
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
				   
				    				
					
				    
											if (rule.field == "ActualStartDate" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.ActualStartDate)>=0);
						}
				   
				    				
					
				    
											if (rule.field == "PlanDeliveryDate" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.PlanDeliveryDate)>=0);
						}
				   
				    				
					
				    
											if (rule.field == "ActualDeliveryDate" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.ActualDeliveryDate)>=0);
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



