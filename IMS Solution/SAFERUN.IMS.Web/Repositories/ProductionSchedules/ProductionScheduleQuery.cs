
                    
      
     
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
   public class ProductionScheduleQuery:QueryObject<ProductionSchedule>
    {
        public ProductionScheduleQuery WithAnySearch(string search)
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.ScheduleNo.Contains(search) || x.OrderKey.Contains(search) || x.OrderDate.ToString().Contains(search) || x.BeginDate.ToString().Contains(search) || x.CompletedDate.ToString().Contains(search) || x.Ower.Contains(search) || x.ScheduleDate.ToString().Contains(search) || x.Remark.Contains(search) );
            return this;
        }

		public ProductionScheduleQuery WithPopupSearch(string search,string para="")
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.ScheduleNo.Contains(search) || x.OrderKey.Contains(search) || x.OrderDate.ToString().Contains(search) || x.BeginDate.ToString().Contains(search) || x.CompletedDate.ToString().Contains(search) || x.Ower.Contains(search) || x.ScheduleDate.ToString().Contains(search) || x.Remark.Contains(search) );
            return this;
        }

		public ProductionScheduleQuery Withfilter(IEnumerable<filterRule> filters)
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
				   
					
				    				
											if (rule.field == "ScheduleNo"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ScheduleNo.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    						if (rule.field == "WorkId" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.WorkId == val);
						}
				   
					
				    				
											if (rule.field == "OrderKey"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.OrderKey.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    
											if (rule.field == "OrderDate" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.OrderDate)>0);
						}
				   
				    				
					
				    
											if (rule.field == "BeginDate" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.BeginDate)>0);
						}
				   
				    				
					
				    
											if (rule.field == "CompletedDate" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.CompletedDate)>0);
						}
				   
				    				
											if (rule.field == "Ower"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Ower.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    
											if (rule.field == "ScheduleDate" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.ScheduleDate)>0);
						}
				   
				    				
											if (rule.field == "Remark"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Remark.Contains(rule.value));
						}
				    
				    
					
				    									
                   
               }
           }
            return this;
        }
    }
}



