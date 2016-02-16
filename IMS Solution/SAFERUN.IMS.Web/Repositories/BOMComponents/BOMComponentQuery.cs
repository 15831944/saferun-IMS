
                    
      
     
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
   public class BOMComponentQuery:QueryObject<BOMComponent>
    {
        public BOMComponentQuery WithAnySearch(string search)
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.DesignName.Contains(search) || x.ComponentSKU.Contains(search) || x.ALTSku.Contains(search) || x.GraphSKU.Contains(search) || x.StockSKU.Contains(search) || x.Remark1.Contains(search) || x.Remark2.Contains(search) || x.Deploy.Contains(search) || x.Locator.Contains(search) || x.ProductionLine.Contains(search) );
            return this;
        }

		public BOMComponentQuery WithPopupSearch(string search,string para="")
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.DesignName.Contains(search) || x.ComponentSKU.Contains(search) || x.ALTSku.Contains(search) || x.GraphSKU.Contains(search) || x.StockSKU.Contains(search) || x.Remark1.Contains(search) || x.Remark2.Contains(search) || x.Deploy.Contains(search) || x.Locator.Contains(search) || x.ProductionLine.Contains(search) );
            return this;
        }

		public BOMComponentQuery Withfilter(IEnumerable<filterRule> filters)
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
				    
				    
					 				
											if (rule.field == "Remark1")
						{
							And(x => x.Remark1.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "Remark2")
						{
							And(x => x.Remark2.Contains(rule.value));
						}
				    
				    
					 				
					
				    						if (rule.field == "ConsumeQty")
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.ConsumeQty == val);
						}
				   
					 				
					
				    						if (rule.field == "ConsumeTime")
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.ConsumeTime == val);
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
				    
				    
					 				
					
				    						if (rule.field == "Status")
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.Status == val);
						}
				   
					 				
					
				    
					 				
					
				    						if (rule.field == "SKUId" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.SKUId == val);
						}
				   
					 				
					
				    						if (rule.field == "ParentComponentId")
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.ParentComponentId == val);
						}
				   
					 									
                   
               }
           }
            return this;
        }
    }
}



