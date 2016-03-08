
                    
      
     
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
   public class ScheduleDetailQuery:QueryObject<ScheduleDetail>
    {
        public ScheduleDetailQuery WithAnySearch(string search)
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.ScheduleNo.Contains(search) || x.WorkNo.Contains(search) || x.GraphSKU.Contains(search) || x.GraphVer.Contains(search) || x.Operator.Contains(search) || x.AltProdctionDate1.ToString().Contains(search) || x.ActualProdctionDate1.ToString().Contains(search) || x.AltProdctionDate2.ToString().Contains(search) || x.ActualProdctionDate2.ToString().Contains(search) || x.AltProdctionDate3.ToString().Contains(search) || x.ActualProdctionDate3.ToString().Contains(search) || x.Remark1.Contains(search) || x.Remark2.Contains(search) );
            return this;
        }

		public ScheduleDetailQuery WithPopupSearch(string search,string para="")
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.ScheduleNo.Contains(search) || x.WorkNo.Contains(search) || x.GraphSKU.Contains(search) || x.GraphVer.Contains(search) || x.Operator.Contains(search) || x.AltProdctionDate1.ToString().Contains(search) || x.ActualProdctionDate1.ToString().Contains(search) || x.AltProdctionDate2.ToString().Contains(search) || x.ActualProdctionDate2.ToString().Contains(search) || x.AltProdctionDate3.ToString().Contains(search) || x.ActualProdctionDate3.ToString().Contains(search) || x.Remark1.Contains(search) || x.Remark2.Contains(search) );
            return this;
        }

		public ScheduleDetailQuery Withfilter(IEnumerable<filterRule> filters)
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
				    
				    
					
				    				
											if (rule.field == "WorkNo"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.WorkNo.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    						if (rule.field == "ParentSKUId" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.ParentSKUId == val);
						}
				   
					
				    				
					
				    						if (rule.field == "ComponentSKUId" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.ComponentSKUId == val);
						}
				   
					
				    				
											if (rule.field == "GraphSKU"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.GraphSKU.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "GraphVer"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.GraphVer.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    						if (rule.field == "ConsumeQty" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.ConsumeQty == val);
						}
				   
					
				    				
					
				    						if (rule.field == "StockQty" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.StockQty == val);
						}
				   
					
				    				
					
				    						if (rule.field == "RequirementQty" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.RequirementQty == val);
						}
				   
					
				    				
					
				    						if (rule.field == "ScheduleProductionQty" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.ScheduleProductionQty == val);
						}
				   
					
				    				
					
				    						if (rule.field == "ActualProductionQty" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.ActualProductionQty == val);
						}
				   
					
				    				
					
				    						if (rule.field == "StationId" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.StationId == val);
						}
				   
					
				    				
					
				    						if (rule.field == "ShiftId" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.ShiftId == val);
						}
				   
					
				    				
											if (rule.field == "Operator"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Operator.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    						if (rule.field == "Status" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.Status == val);
						}
				   
					
				    				
					
				    
											if (rule.field == "AltProdctionDate1" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.AltProdctionDate1)>0);
						}
				   
				    				
					
				    
											if (rule.field == "ActualProdctionDate1" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.ActualProdctionDate1)>0);
						}
				   
				    				
					
				    
											if (rule.field == "AltProdctionDate2" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.AltProdctionDate2)>0);
						}
				   
				    				
					
				    
											if (rule.field == "ActualProdctionDate2" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.ActualProdctionDate2)>0);
						}
				   
				    				
					
				    
											if (rule.field == "AltProdctionDate3" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.AltProdctionDate3)>0);
						}
				   
				    				
					
				    
											if (rule.field == "ActualProdctionDate3" && !string.IsNullOrEmpty(rule.value))
						{	
							var date = Convert.ToDateTime(rule.value) ;
							And(x => SqlFunctions.DateDiff("d", date, x.ActualProdctionDate3)>0);
						}
				   
				    				
					
				    						if (rule.field == "AltConsumeTime" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.AltConsumeTime == val);
						}
				   
					
				    				
					
				    						if (rule.field == "ActualConsumeTime" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.ActualConsumeTime == val);
						}
				   
					
				    				
											if (rule.field == "Remark1"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Remark1.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "Remark2"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Remark2.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    						if (rule.field == "ProductionScheduleId" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.ProductionScheduleId == val);
						}
				   
					
				    									
                   
               }
           }
            return this;
        }
    }
}



