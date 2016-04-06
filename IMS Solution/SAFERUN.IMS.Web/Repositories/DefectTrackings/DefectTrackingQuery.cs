
                    
      
     
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
   public class DefectTrackingQuery:QueryObject<DefectTracking>
    {
        public DefectTrackingQuery WithAnySearch(string search)
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.ProjectName.Contains(search) || x.OrderKey.Contains(search) || x.ComponentSKU.Contains(search) || x.Supplier.Contains(search) || x.GraphSKU.Contains(search) || x.Locator.Contains(search) || x.DefectDesc.Contains(search) || x.Result.Contains(search) || x.Remark.Contains(search) || x.QCUser.Contains(search) || x.TrackingDateTime.ToString().Contains(search) || x.CheckedDateTime.ToString().Contains(search) || x.CloseDateTime.ToString().Contains(search) );
            return this;
        }

		public DefectTrackingQuery WithPopupSearch(string search,string para="")
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.ProjectName.Contains(search) || x.OrderKey.Contains(search) || x.ComponentSKU.Contains(search) || x.Supplier.Contains(search) || x.GraphSKU.Contains(search) || x.Locator.Contains(search) || x.DefectDesc.Contains(search) || x.Result.Contains(search) || x.Remark.Contains(search) || x.QCUser.Contains(search) || x.TrackingDateTime.ToString().Contains(search) || x.CheckedDateTime.ToString().Contains(search) || x.CloseDateTime.ToString().Contains(search) );
            return this;
        }

		public DefectTrackingQuery Withfilter(IEnumerable<filterRule> filters)
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
				   
					
				    				
											if (rule.field == "ProjectName"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ProjectName.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "OrderKey"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.OrderKey.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    						if (rule.field == "OrderId" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.OrderId == val);
						}
				   
					
				    				
					
				    						if (rule.field == "SKUId" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.SKUId == val);
						}
				   
					
				    				
											if (rule.field == "ComponentSKU"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ComponentSKU.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "Supplier"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Supplier.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "GraphSKU"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.GraphSKU.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    						if (rule.field == "QCQty" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.QCQty == val);
						}
				   
					
				    				
					
				    						if (rule.field == "CheckedQty" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.CheckedQty == val);
						}
				   
					
				    				
					
				    						if (rule.field == "NGQty" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.NGQty == val);
						}
				   
					
				    				
					
				    						if (rule.field == "DefectTypeId" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.DefectTypeId == val);
						}
				   
					
				    				
					
				    						if (rule.field == "DefectId" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
                            And(x => x.DefectCodeId == val);
						}
				   
					
				    				
											if (rule.field == "Locator"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Locator.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "DefectDesc"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.DefectDesc.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    						if (rule.field == "Status" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.Status == val);
						}
				   
					
				    				
											if (rule.field == "Result"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Result.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "Remark"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Remark.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "QCUser"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.QCUser.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    
											if (rule.field == "TrackingDateTime" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.TrackingDateTime)>=0);
						}
				   
				    				
					
				    
											if (rule.field == "CheckedDateTime" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.CheckedDateTime)>=0);
						}
				   
				    				
					
				    
											if (rule.field == "CloseDateTime" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.CloseDateTime)>=0);
						}
				   
				    									
                   
               }
           }
            return this;
        }
    }
}



