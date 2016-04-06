
                    
      
     
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
   public class SKUQuery:QueryObject<SKU>
    {
        public SKUQuery WithAnySearch(string search)
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.Sku.Contains(search) || x.ALTSku.Contains(search) || x.ManufacturerSku.Contains(search) || x.Model.Contains(search) || x.CLASS.Contains(search) || x.SKUGroup.Contains(search) || x.MadeType.Contains(search) || x.STDUOM.Contains(search) || x.Description.Contains(search) || x.Brand.Contains(search) || x.PackKey.Contains(search) || x.SUSR2.Contains(search) || x.SUSR3.Contains(search) || x.SUSR4.Contains(search) || x.SUSR5.ToString().Contains(search) );
            return this;
        }

		public SKUQuery WithPopupSearch(string search,string para="")
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.Sku.Contains(search) || x.ALTSku.Contains(search) || x.ManufacturerSku.Contains(search) || x.Model.Contains(search) || x.CLASS.Contains(search) || x.SKUGroup.Contains(search) || x.MadeType.Contains(search) || x.STDUOM.Contains(search) || x.Description.Contains(search) || x.Brand.Contains(search) || x.PackKey.Contains(search) || x.SUSR2.Contains(search) || x.SUSR3.Contains(search) || x.SUSR4.Contains(search) || x.SUSR5.ToString().Contains(search) );
            return this;
        }

		public SKUQuery Withfilter(IEnumerable<filterRule> filters)
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
				   
					 				
											if (rule.field == "Sku")
						{
							And(x => x.Sku.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "ALTSku")
						{
							And(x => x.ALTSku.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "ManufacturerSku")
						{
							And(x => x.ManufacturerSku.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "Model")
						{
							And(x => x.Model.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "CLASS")
						{
							And(x => x.CLASS.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "SKUGroup")
						{
							And(x => x.SKUGroup.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "MadeType")
						{
							And(x => x.MadeType.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "STDUOM")
						{
							And(x => x.STDUOM.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "Description")
						{
							And(x => x.Description.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "Brand")
						{
							And(x => x.Brand.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "PackKey")
						{
							And(x => x.PackKey.Contains(rule.value));
						}
				    
				    
					 				
					
				    
					 				
					
				    
					 				
					
				    
					 				
					
				    						if (rule.field == "Price")
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.Price == val);
						}
				   
					 				
					
				    						if (rule.field == "Cost")
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.Cost == val);
						}
				   
					 				
					
				    						if (rule.field == "STDGrossWGT")
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.STDGrossWGT == val);
						}
				   
					 				
					
				    						if (rule.field == "STDNetWGT")
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.STDNetWGT == val);
						}
				   
					 				
					
				    						if (rule.field == "STDCube")
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.STDCube == val);
						}
				   
					 				
					
				    						if (rule.field == "SUSR1")
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.SUSR1 == val);
						}
				   
					 				
											if (rule.field == "SUSR2")
						{
							And(x => x.SUSR2.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "SUSR3")
						{
							And(x => x.SUSR3.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "SUSR4")
						{
							And(x => x.SUSR4.Contains(rule.value));
						}
				    
				    
					 				
					
				    
					 						if (rule.field == "SUSR5")
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.SUSR5)>=0);
						}
				   									
                   
               }
           }
            return this;
        }
    }
}



