
                    
      
     
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
   public class WorkQuery:QueryObject<Work>
    {
        public WorkQuery WithAnySearch(string search)
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.WorkNo.Contains(search) || x.OrderKey.Contains(search) || x.PO.Contains(search) || x.User.Contains(search) || x.WorkDate.ToString().Contains(search) || x.Review.Contains(search) || x.ProductionConfirm.Contains(search) || x.OutsourceConfirm.Contains(search) || x.AssembleConfirm.Contains(search) || x.PurchaseConfirm.Contains(search) || x.ReviewDate.ToString().Contains(search) || x.Remark.Contains(search) );
            return this;
        }

		public WorkQuery WithPopupSearch(string search,string para="")
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.WorkNo.Contains(search) || x.OrderKey.Contains(search) || x.PO.Contains(search) || x.User.Contains(search) || x.WorkDate.ToString().Contains(search) || x.Review.Contains(search) || x.ProductionConfirm.Contains(search) || x.OutsourceConfirm.Contains(search) || x.AssembleConfirm.Contains(search) || x.PurchaseConfirm.Contains(search) || x.ReviewDate.ToString().Contains(search) || x.Remark.Contains(search) );
            return this;
        }

		public WorkQuery Withfilter(IEnumerable<filterRule> filters)
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
				   
					
				    				
											if (rule.field == "WorkNo"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.WorkNo.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    						if (rule.field == "WorkTypeId" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.WorkTypeId == val);
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
				   
					
				    				
											if (rule.field == "PO"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.PO.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "User"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.User.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    
											if (rule.field == "WorkDate" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.WorkDate)>=0);
						}
				   
				    				
					
				    						if (rule.field == "Status" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.Status == val);
						}
				   
					
				    				
											if (rule.field == "Review"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Review.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "ProductionConfirm"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ProductionConfirm.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "OutsourceConfirm"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.OutsourceConfirm.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "AssembleConfirm"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.AssembleConfirm.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "PurchaseConfirm"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.PurchaseConfirm.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    
											if (rule.field == "ReviewDate" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.ReviewDate)>=0);
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



