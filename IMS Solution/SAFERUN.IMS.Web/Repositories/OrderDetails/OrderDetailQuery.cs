
                    
      
     
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
   public class OrderDetailQuery:QueryObject<OrderDetail>
    {
        public OrderDetailQuery WithAnySearch(string search)
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.OrderKey.Contains(search) || x.LineNumber.Contains(search) || x.ContractNum.Contains(search) || x.ProductionSku.Contains(search) || x.Model.Contains(search) || x.UOM.Contains(search) || x.Remark.Contains(search) );
            return this;
        }

		public OrderDetailQuery WithPopupSearch(string search,string para="")
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.OrderKey.Contains(search) || x.LineNumber.Contains(search) || x.ContractNum.Contains(search) || x.ProductionSku.Contains(search) || x.Model.Contains(search) || x.UOM.Contains(search) || x.Remark.Contains(search) );
            return this;
        }

		public OrderDetailQuery Withfilter(IEnumerable<filterRule> filters)
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
				    
				    
					 				
					
				    						if (rule.field == "OrderId")
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.OrderId == val);
						}
				   
					 				
											if (rule.field == "LineNumber")
						{
							And(x => x.LineNumber.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "ContractNum")
						{
							And(x => x.ContractNum.Contains(rule.value));
						}
				    
				    
					 				
					
				    						if (rule.field == "SKUId")
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.SKUId == val);
						}
				   
					 				
											if (rule.field == "ProductionSku")
						{
							And(x => x.ProductionSku.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "Model")
						{
							And(x => x.Model.Contains(rule.value));
						}
				    
				    
					 				
					
				    						if (rule.field == "Qty")
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.Qty == val);
						}
				   
					 				
											if (rule.field == "UOM")
						{
							And(x => x.UOM.Contains(rule.value));
						}
				    
				    
					 				
					
				    						if (rule.field == "Price")
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.Price == val);
						}
				   
					 				
					
				    						if (rule.field == "SubTotal")
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.SubTotal == val);
						}
				   
					 				
											if (rule.field == "Remark")
						{
							And(x => x.Remark.Contains(rule.value));
						}
				    
				    
					 				
					
				    						if (rule.field == "Status")
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.Status == val);
						}
				   
					 									
                   
               }
           }
            return this;
        }
    }
}



