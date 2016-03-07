
                    
      
     
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
   public class ShiftQuery:QueryObject<Shift>
    {
        public ShiftQuery WithAnySearch(string search)
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.Name.Contains(search) || x.OnTime.Contains(search) || x.OffTime.Contains(search) || x.Remark.Contains(search) );
            return this;
        }

		public ShiftQuery WithPopupSearch(string search,string para="")
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.Name.Contains(search) || x.OnTime.Contains(search) || x.OffTime.Contains(search) || x.Remark.Contains(search) );
            return this;
        }

		public ShiftQuery Withfilter(IEnumerable<filterRule> filters)
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
				   
					
				    				
											if (rule.field == "Name"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Name.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "OnTime"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.OnTime.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "OffTime"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.OffTime.Contains(rule.value));
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



