
                    
      
     
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
   public class StationQuery:QueryObject<Station>
    {
        public StationQuery WithAnySearch(string search)
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.StationNo.Contains(search) || x.Name.Contains(search) || x.Equipment.Contains(search) || x.Description.Contains(search) );
            return this;
        }

		public StationQuery WithPopupSearch(string search,string para="")
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.StationNo.Contains(search) || x.Name.Contains(search) || x.Equipment.Contains(search) || x.Description.Contains(search) );
            return this;
        }

		public StationQuery Withfilter(IEnumerable<filterRule> filters)
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
				   
					
				    				
											if (rule.field == "StationNo"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.StationNo.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "Name"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Name.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "Equipment"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Equipment.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "Description"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Description.Contains(rule.value));
						}
				    
				    
					
				    				
					
				    						if (rule.field == "EquipmentNumber" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.EquipmentNumber == val);
						}
				   
					
				    				
					
				    						if (rule.field == "StandardElapsedTime" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.StandardElapsedTime == val);
						}
				   
					
				    				
					
				    						if (rule.field == "WorkingTime" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.WorkingTime == val);
						}
				   
					
				    				
					
				    						if (rule.field == "Status" && !string.IsNullOrEmpty(rule.value))
						{
							int val = Convert.ToInt32(rule.value);
							And(x => x.Status == val);
						}
				   
					
				    									
                   
               }
           }
            return this;
        }
    }
}



