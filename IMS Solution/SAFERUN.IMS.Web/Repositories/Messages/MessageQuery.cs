
                    
      
     
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
   public class MessageQuery:QueryObject<Message>
    {
        public MessageQuery WithAnySearch(string search)
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.From.Contains(search) || x.To.Contains(search) || x.Title.Contains(search) || x.Content.Contains(search) || x.CreateTime.ToString().Contains(search) );
            return this;
        }

		public MessageQuery WithPopupSearch(string search,string para="")
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.From.Contains(search) || x.To.Contains(search) || x.Title.Contains(search) || x.Content.Contains(search) || x.CreateTime.ToString().Contains(search) );
            return this;
        }

		public MessageQuery Withfilter(IEnumerable<filterRule> filters)
        {
           if (filters != null)
           {
               foreach (var rule in filters)
               {
                  
					
				    
					 				
					
				    						if (rule.field == "Id")
						{
							And(x => x.Id == Convert.ToInt32(rule.value));
						}
				   
					 				
											if (rule.field == "From")
						{
							And(x => x.From.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "To")
						{
							And(x => x.To.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "Title")
						{
							And(x => x.Title.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "Content")
						{
							And(x => x.Content.Contains(rule.value));
						}
				    
				    
					 				
					
				    						if (rule.field == "Status")
						{
							And(x => x.Status == Convert.ToInt32(rule.value));
						}
				   
					 				
					
				    
					 						if (rule.field == "CreateTime")
						{
							And(x => x.CreateTime .Date == Convert.ToDateTime(rule.value).Date);
						}
				   									
                   
               }
           }
            return this;
        }
    }
}



