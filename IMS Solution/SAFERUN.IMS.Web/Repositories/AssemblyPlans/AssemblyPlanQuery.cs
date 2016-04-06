
                    
      
     
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
   public class AssemblyPlanQuery:QueryObject<AssemblyPlan>
    {
        public AssemblyPlanQuery WithAnySearch(string search)
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.OrderKey.Contains(search) || x.DesignName.Contains(search) || x.ComponentSKU.Contains(search) || x.FinishedSKU.Contains(search) || x.OrderDate.ToString().Contains(search) || x.RequirementDate.ToString().Contains(search) || x.ResolveDate1.ToString().Contains(search) || x.ActualReleaseDate1.ToString().Contains(search) || x.SetPlanDate2.ToString().Contains(search) || x.PutawayDate2.ToString().Contains(search) || x.SetPlanDate3.ToString().Contains(search) || x.PutawayDate3.ToString().Contains(search) || x.SetPlanDate4.ToString().Contains(search) || x.PutawayDate4.ToString().Contains(search) || x.SetPlanDate5.ToString().Contains(search) || x.DeliveryDate5.ToString().Contains(search) || x.PutawayDate5.ToString().Contains(search) || x.SetPlanDate6.ToString().Contains(search) || x.ActualStartDate6.ToString().Contains(search) || x.ActualCompletedDate6.ToString().Contains(search) || x.Remark.Contains(search) );
            return this;
        }

		public AssemblyPlanQuery WithPopupSearch(string search,string para="")
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.OrderKey.Contains(search) || x.DesignName.Contains(search) || x.ComponentSKU.Contains(search) || x.FinishedSKU.Contains(search) || x.OrderDate.ToString().Contains(search) || x.RequirementDate.ToString().Contains(search) || x.ResolveDate1.ToString().Contains(search) || x.ActualReleaseDate1.ToString().Contains(search) || x.SetPlanDate2.ToString().Contains(search) || x.PutawayDate2.ToString().Contains(search) || x.SetPlanDate3.ToString().Contains(search) || x.PutawayDate3.ToString().Contains(search) || x.SetPlanDate4.ToString().Contains(search) || x.PutawayDate4.ToString().Contains(search) || x.SetPlanDate5.ToString().Contains(search) || x.DeliveryDate5.ToString().Contains(search) || x.PutawayDate5.ToString().Contains(search) || x.SetPlanDate6.ToString().Contains(search) || x.ActualStartDate6.ToString().Contains(search) || x.ActualCompletedDate6.ToString().Contains(search) || x.Remark.Contains(search) );
            return this;
        }

		public AssemblyPlanQuery Withfilter(IEnumerable<filterRule> filters)
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
				    
				    
					
				    				
											if (rule.field == "FinishedSKU"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.FinishedSKU.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    
											if (rule.field == "OrderDate" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.OrderDate)>=0);
						}
				   
				    				
					
				    
											if (rule.field == "RequirementDate" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.RequirementDate)>=0);
						}
				   
				    				
					
				    
											if (rule.field == "ResolveDate1" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.ResolveDate1)>=0);
						}
				   
				    				
					
				    
											if (rule.field == "ActualReleaseDate1" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.ActualReleaseDate1)>=0);
						}
				   
				    				
					
				    
											if (rule.field == "SetPlanDate2" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.SetPlanDate2)>=0);
						}
				   
				    				
					
				    
											if (rule.field == "PutawayDate2" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.PutawayDate2)>=0);
						}
				   
				    				
					
				    
											if (rule.field == "SetPlanDate3" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.SetPlanDate3)>=0);
						}
				   
				    				
					
				    
											if (rule.field == "PutawayDate3" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.PutawayDate3)>=0);
						}
				   
				    				
					
				    
											if (rule.field == "SetPlanDate4" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.SetPlanDate4)>=0);
						}
				   
				    				
					
				    
											if (rule.field == "PutawayDate4" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.PutawayDate4)>=0);
						}
				   
				    				
					
				    
											if (rule.field == "SetPlanDate5" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.SetPlanDate5)>=0);
						}
				   
				    				
					
				    
											if (rule.field == "DeliveryDate5" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.DeliveryDate5)>=0);
						}
				   
				    				
					
				    
											if (rule.field == "PutawayDate5" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.PutawayDate5)>=0);
						}
				   
				    				
					
				    
											if (rule.field == "SetPlanDate6" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.SetPlanDate6)>=0);
						}
				   
				    				
					
				    
											if (rule.field == "ActualStartDate6" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.ActualStartDate6)>=0);
						}
				   
				    				
					
				    
											if (rule.field == "ActualCompletedDate6" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.ActualCompletedDate6)>=0);
						}
				   
				    				
											if (rule.field == "Remark"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Remark.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    						if (rule.field == "Status" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.Status == val);
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



