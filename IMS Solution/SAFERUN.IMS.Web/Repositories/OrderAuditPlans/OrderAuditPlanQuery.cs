
                    
      
     
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
   public class OrderAuditPlanQuery:QueryObject<OrderAuditPlan>
    {
        public OrderAuditPlanQuery WithAnySearch(string search)
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.OrderKey.Contains(search) || x.AuditContent.Contains(search) || x.Department.Contains(search) || x.AuditResult.Contains(search) || x.PlanAuditDate.ToString().Contains(search) || x.AuditDate.ToString().Contains(search) || x.AuditUser.Contains(search) || x.Remark.Contains(search) );
            return this;
        }

		public OrderAuditPlanQuery WithPopupSearch(string search,string para="")
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.OrderKey.Contains(search) || x.AuditContent.Contains(search) || x.Department.Contains(search) || x.AuditResult.Contains(search) || x.PlanAuditDate.ToString().Contains(search) || x.AuditDate.ToString().Contains(search) || x.AuditUser.Contains(search) || x.Remark.Contains(search) );
            return this;
        }

		public OrderAuditPlanQuery Withfilter(IEnumerable<filterRule> filters)
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
				    
				    
					 				
											if (rule.field == "AuditContent")
						{
							And(x => x.AuditContent.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "Department")
						{
							And(x => x.Department.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "AuditResult")
						{
							And(x => x.AuditResult.Contains(rule.value));
						}
				    
				    
					 				
					
				    						if (rule.field == "Status")
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.Status == val);
						}
				   
					 				
					
				    
					 						if (rule.field == "PlanAuditDate")
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.PlanAuditDate)>=0);
						}
				   				
					
				    
					 						if (rule.field == "AuditDate")
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.AuditDate)>=0);
						}
				   				
											if (rule.field == "AuditUser")
						{
							And(x => x.AuditUser.Contains(rule.value));
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



