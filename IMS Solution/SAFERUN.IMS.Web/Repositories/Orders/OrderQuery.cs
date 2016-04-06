
                    
      
     
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
   public class OrderQuery:QueryObject<Order>
    {
        public OrderQuery WithAnySearch(string search)
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.OrderKey.Contains(search) || x.Sales.Contains(search) || x.OrderDate.ToString().Contains(search) || x.AuditDate.ToString().Contains(search) || x.ProjectName.Contains(search) || x.AuditResult.Contains(search) || x.Remark.Contains(search) || x.PlanFinishDate.ToString().Contains(search) || x.ActualFinishDate.ToString().Contains(search) || x.ShipDate.ToString().Contains(search) || x.ColseDate.ToString().Contains(search) );
            return this;
        }

		public OrderQuery WithPopupSearch(string search,string para="")
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.OrderKey.Contains(search) || x.Sales.Contains(search) || x.OrderDate.ToString().Contains(search) || x.AuditDate.ToString().Contains(search) || x.ProjectName.Contains(search) || x.AuditResult.Contains(search) || x.Remark.Contains(search) || x.PlanFinishDate.ToString().Contains(search) || x.ActualFinishDate.ToString().Contains(search) || x.ShipDate.ToString().Contains(search) || x.ColseDate.ToString().Contains(search) );
            return this;
        }

		public OrderQuery Withfilter(IEnumerable<filterRule> filters)
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
				    
				    
					 				
											if (rule.field == "Sales")
						{
							And(x => x.Sales.Contains(rule.value));
						}
				    
				    
					 				
					
				    
					 						if (rule.field == "OrderDate")
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.OrderDate)>=0);
						}
				   				
					
				    
					 						if (rule.field == "AuditDate")
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.AuditDate)>=0);
						}
				   				
					
				    						if (rule.field == "CustomerId")
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.CustomerId == val);
						}
				   
					 				
					
				    						if (rule.field == "ProjectTypeId")
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.ProjectTypeId == val);
						}
				   
					 				
											if (rule.field == "ProjectName")
						{
							And(x => x.ProjectName.Contains(rule.value));
						}
				    
				    
					 				
					
				    						if (rule.field == "Status")
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.Status == val);
						}
				   
					 				
											if (rule.field == "AuditResult")
						{
							And(x => x.AuditResult.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "Remark")
						{
							And(x => x.Remark.Contains(rule.value));
						}
				    
				    
					 				
					
				    
					 						if (rule.field == "PlanFinishDate")
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.PlanFinishDate)>=0);
						}
				   				
					
				    
					 						if (rule.field == "ActualFinishDate")
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.ActualFinishDate)>=0);
						}
				   				
					
				    
					 						if (rule.field == "ShipDate")
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.ShipDate)>=0);
						}
				   				
					
				    
					 						if (rule.field == "ColseDate")
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.ColseDate)>=0);
						}
				   									
                   
               }
           }
            return this;
        }
    }
}



