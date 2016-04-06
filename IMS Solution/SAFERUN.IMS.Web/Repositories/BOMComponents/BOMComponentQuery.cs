
                    
      
     
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
   public class BOMComponentQuery:QueryObject<BOMComponent>
    {
        public BOMComponentQuery WithAnySearch(string search)
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.ComponentSKU.Contains(search) || x.DesignName.Contains(search) || x.ALTSku.Contains(search) || x.GraphSKU.Contains(search) || x.StockSKU.Contains(search) || x.MadeType.Contains(search) || x.SKUGroup.Contains(search) || x.Remark1.Contains(search) || x.Remark2.Contains(search) || x.Deploy.Contains(search) || x.Locator.Contains(search) || x.ProductionLine.Contains(search) || x.FinishedSKU.Contains(search) );
            return this;
        }
        public BOMComponentQuery WithId(int id, IEnumerable<filterRule> filters)
        {
            if (id == 0)
            {
                And(x=>x.ParentComponentId==null|| x.ParentComponentId==0);
            }else{
                And(x => x.ParentComponentId == id);
            }
            if (filters != null)
            {
                foreach (var rule in filters)
                {


                    if (rule.field == "Id" && !string.IsNullOrEmpty(rule.value))
                    {
                        int val = Convert.ToInt32(rule.value);
                        And(x => x.Id == val);
                    }



                    if (rule.field == "ComponentSKU" && !string.IsNullOrEmpty(rule.value))
                    {
                        And(x => x.ComponentSKU.Contains(rule.value));
                    }




                    if (rule.field == "DesignName" && !string.IsNullOrEmpty(rule.value))
                    {
                        And(x => x.DesignName.Contains(rule.value));
                    }




                    if (rule.field == "ALTSku" && !string.IsNullOrEmpty(rule.value))
                    {
                        And(x => x.ALTSku.Contains(rule.value));
                    }




                    if (rule.field == "GraphSKU" && !string.IsNullOrEmpty(rule.value))
                    {
                        And(x => x.GraphSKU.Contains(rule.value));
                    }




                    if (rule.field == "StockSKU" && !string.IsNullOrEmpty(rule.value))
                    {
                        And(x => x.StockSKU.Contains(rule.value));
                    }




                    if (rule.field == "MadeType" && !string.IsNullOrEmpty(rule.value))
                    {
                        And(x => x.MadeType.Contains(rule.value));
                    }




                    if (rule.field == "SKUGroup" && !string.IsNullOrEmpty(rule.value))
                    {
                        And(x => x.SKUGroup.Contains(rule.value));
                    }




                    if (rule.field == "Remark1" && !string.IsNullOrEmpty(rule.value))
                    {
                        And(x => x.Remark1.Contains(rule.value));
                    }




                    if (rule.field == "Remark2" && !string.IsNullOrEmpty(rule.value))
                    {
                        And(x => x.Remark2.Contains(rule.value));
                    }





                    if (rule.field == "ConsumeQty" && !string.IsNullOrEmpty(rule.value))
                    {
                        int val = Convert.ToInt32(rule.value);
                        And(x => x.ConsumeQty == val);
                    }




                    if (rule.field == "ConsumeTime" && !string.IsNullOrEmpty(rule.value))
                    {
                        int val = Convert.ToInt32(rule.value);
                        And(x => x.ConsumeTime == val);
                    }




                    if (rule.field == "RejectRatio" && !string.IsNullOrEmpty(rule.value))
                    {
                        int val = Convert.ToInt32(rule.value);
                        And(x => x.RejectRatio == val);
                    }



                    if (rule.field == "Deploy" && !string.IsNullOrEmpty(rule.value))
                    {
                        And(x => x.Deploy.Contains(rule.value));
                    }




                    if (rule.field == "Locator" && !string.IsNullOrEmpty(rule.value))
                    {
                        And(x => x.Locator.Contains(rule.value));
                    }




                    if (rule.field == "ProductionLine" && !string.IsNullOrEmpty(rule.value))
                    {
                        And(x => x.ProductionLine.Contains(rule.value));
                    }





                    if (rule.field == "ProductionProcessId" && !string.IsNullOrEmpty(rule.value))
                    {
                        int val = Convert.ToInt32(rule.value);
                        And(x => x.ProductionProcessId == val);
                    }




                    if (rule.field == "Version" && !string.IsNullOrEmpty(rule.value))
                    {
                        int val = Convert.ToInt32(rule.value);
                        And(x => x.Version == val);
                    }




                    if (rule.field == "Status" && !string.IsNullOrEmpty(rule.value))
                    {
                        int val = Convert.ToInt32(rule.value);
                        And(x => x.Status == val);
                    }






                    if (rule.field == "NoPur" && !string.IsNullOrEmpty(rule.value))
                    {
                        var boolval = Convert.ToBoolean(rule.value);
                        And(x => x.NoPur == boolval);
                    }

                    if (rule.field == "FinishedSKU" && !string.IsNullOrEmpty(rule.value))
                    {
                        And(x => x.FinishedSKU.Contains(rule.value));
                    }





                    if (rule.field == "SKUId" && !string.IsNullOrEmpty(rule.value))
                    {
                        int val = Convert.ToInt32(rule.value);
                        And(x => x.SKUId == val);
                    }




                    if (rule.field == "ParentComponentId" && !string.IsNullOrEmpty(rule.value))
                    {
                        int val = Convert.ToInt32(rule.value);
                        And(x => x.ParentComponentId == val);
                    }




                }
            }
            return this;
        }
		public BOMComponentQuery WithPopupSearch(string search,string para="")
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.ComponentSKU.Contains(search) || x.DesignName.Contains(search) || x.ALTSku.Contains(search) || x.GraphSKU.Contains(search) || x.StockSKU.Contains(search) || x.MadeType.Contains(search) || x.SKUGroup.Contains(search) || x.Remark1.Contains(search) || x.Remark2.Contains(search) || x.Deploy.Contains(search) || x.Locator.Contains(search) || x.ProductionLine.Contains(search) || x.FinishedSKU.Contains(search) );
            return this;
        }

		public BOMComponentQuery Withfilter(IEnumerable<filterRule> filters)
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
				   
					
				    				
											if (rule.field == "ComponentSKU"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ComponentSKU.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "DesignName"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.DesignName.Contains(rule.value));
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
				    
				    
					
				    				
											if (rule.field == "MadeType"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.MadeType.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "SKUGroup"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.SKUGroup.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "Remark1"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Remark1.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "Remark2"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Remark2.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    						if (rule.field == "ConsumeQty" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.ConsumeQty == val);
						}
				   
					
				    				
					
				    						if (rule.field == "ConsumeTime" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.ConsumeTime == val);
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
				    
				    
					
				    				
											if (rule.field == "ProductionLine"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ProductionLine.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    						if (rule.field == "ProductionProcessId" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.ProductionProcessId == val);
						}
				   
					
				    				
					
				    						if (rule.field == "Version" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.Version == val);
						}
				   
					
				    				
					
				    						if (rule.field == "Status" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.Status == val);
						}
				   
					
				    				
					
				    
					
				    						if (rule.field == "NoPur" && !string.IsNullOrEmpty(rule.value))
						{	
							 var boolval=Convert.ToBoolean(rule.value);
							 And(x => x.NoPur == boolval);
						}
				   				
											if (rule.field == "FinishedSKU"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.FinishedSKU.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    						if (rule.field == "SKUId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsNumeric())
						{
                                               
							int val = Convert.ToInt32(rule.value);
							And(x => x.SKUId == val);
						}




                                            if (rule.field == "ParentComponentId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsNumeric())
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



