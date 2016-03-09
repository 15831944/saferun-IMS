
                    
      
     
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
   public class WorkProcessDetailQuery:QueryObject<WorkProcessDetail>
    {
        public WorkProcessDetailQuery WithAnySearch(string search)
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.StepName.Contains(search) || x.StartingDateTime.ToString().Contains(search) || x.CompletedDateTime.ToString().Contains(search) || x.Operator.Contains(search) || x.QCConfirm.Contains(search) || x.QCConfirmDateTime.ToString().Contains(search) || x.CompletedConfirm.Contains(search) || x.Remark.Contains(search) );
            return this;
        }

		public WorkProcessDetailQuery WithPopupSearch(string search,string para="")
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.StepName.Contains(search) || x.StartingDateTime.ToString().Contains(search) || x.CompletedDateTime.ToString().Contains(search) || x.Operator.Contains(search) || x.QCConfirm.Contains(search) || x.QCConfirmDateTime.ToString().Contains(search) || x.CompletedConfirm.Contains(search) || x.Remark.Contains(search) );
            return this;
        }

		public WorkProcessDetailQuery Withfilter(IEnumerable<filterRule> filters)
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
				   
					
				    				
					
				    						if (rule.field == "WorkProcessId" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.WorkProcessId == val);
						}
				   
					
				    				
					
				    						if (rule.field == "ProcessStepId" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.ProcessStepId == val);
						}
				   
					
				    				
											if (rule.field == "StepName"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.StepName.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    						if (rule.field == "StationId" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.StationId == val);
						}
				   
					
				    				
					
				    						if (rule.field == "StandardElapsedTime" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.StandardElapsedTime == val);
						}
				   
					
				    				
					
				    
											if (rule.field == "StartingDateTime" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.StartingDateTime)>0);
						}
				   
				    				
					
				    
											if (rule.field == "CompletedDateTime" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.CompletedDateTime)>0);
						}
				   
				    				
					
				    						if (rule.field == "ElapsedTime" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.ElapsedTime == val);
						}
				   
					
				    				
					
				    						if (rule.field == "WorkingTime" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.WorkingTime == val);
						}
				   
					
				    				
											if (rule.field == "Operator"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Operator.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "QCConfirm"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.QCConfirm.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    
											if (rule.field == "QCConfirmDateTime" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.QCConfirmDateTime)>0);
						}
				   
				    				
											if (rule.field == "CompletedConfirm"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CompletedConfirm.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    						if (rule.field == "Status" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.Status == val);
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



