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
   public class MenuItemQuery:QueryObject<MenuItem>
    {
        public MenuItemQuery WithAnySearch(string search)
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.Title.Contains(search) || x.Description.Contains(search) || x.Code.Contains(search) || x.Url.Contains(search) );
            return this;
        }

		public MenuItemQuery WithPopupSearch(string search,string para="")
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.Title.Contains(search) || x.Description.Contains(search) || x.Code.Contains(search) || x.Url.Contains(search) );
            return this;
        }

		public MenuItemQuery Withfilter(IEnumerable<filterRule> filters)
        {
           if (filters != null)
           {
               foreach (var rule in filters)
               {
                  
					
				    
					 				
					
				    
					 				
					
				    						if (rule.field == "Id")
						{
							And(x => x.Id == Convert.ToInt32(rule.value));
						}
				   
					 				
											if (rule.field == "Title")
						{
							And(x => x.Title.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "Description")
						{
							And(x => x.Description.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "Code")
						{
							And(x => x.Code.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "Url")
						{
							And(x => x.Url.Contains(rule.value));
						}
				    
				    
					 				
					
				    
					 									
                   
               }
           }
            return this;
        }
    }
}



