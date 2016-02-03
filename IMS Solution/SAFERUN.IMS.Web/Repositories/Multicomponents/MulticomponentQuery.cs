﻿
                    
      
     
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Repository.Pattern.Repositories;
using Repository.Pattern.Ef6;
using SAFERUN.IMS.Web.Models;
using SAFERUN.IMS.Web.Extensions;

namespace SAFERUN.IMS.Web.Repositories
{
   public class MulticomponentQuery:QueryObject<Multicomponent>
    {
        public MulticomponentQuery WithAnySearch(string search)
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.DeviceID.Contains(search) || x.Name.Contains(search) || x.Description.Contains(search) );
            return this;
        }

		public MulticomponentQuery WithPopupSearch(string search,string para="")
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.DeviceID.Contains(search) || x.Name.Contains(search) || x.Description.Contains(search) );
            return this;
        }

		public MulticomponentQuery Withfilter(IEnumerable<filterRule> filters)
        {
           if (filters != null)
           {
               foreach (var rule in filters)
               {
                  
					
				    						if (rule.field == "Id")
						{
							And(x => x.Id == Convert.ToInt32(rule.value));
						}
				   
					 				
											if (rule.field == "DeviceID")
						{
							And(x => x.DeviceID.Contains(rule.value));
						}
				    
				    
					 				
					
				    						if (rule.field == "Order")
						{
							And(x => x.Order == Convert.ToInt32(rule.value));
						}
				   
					 				
											if (rule.field == "Name")
						{
							And(x => x.Name.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "Description")
						{
							And(x => x.Description.Contains(rule.value));
						}
				    
				    
					 									
                   
               }
           }
            return this;
        }
    }
}


