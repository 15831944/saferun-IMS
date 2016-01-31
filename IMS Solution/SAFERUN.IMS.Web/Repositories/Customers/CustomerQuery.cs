
                    
      
     
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
   public class CustomerQuery:QueryObject<Customer>
    {
        public CustomerQuery WithAnySearch(string search)
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.AccountNumber.Contains(search) || x.ShortName.Contains(search) || x.Name.Contains(search) || x.CustomerType.Contains(search) || x.AddressLine1.Contains(search) || x.AddressLine2.Contains(search) || x.City.Contains(search) || x.Province.Contains(search) || x.ContactName.Contains(search) || x.Phone1.Contains(search) || x.Phone2.Contains(search) || x.Email.Contains(search) );
            return this;
        }

		public CustomerQuery WithPopupSearch(string search,string para="")
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.AccountNumber.Contains(search) || x.ShortName.Contains(search) || x.Name.Contains(search) || x.CustomerType.Contains(search) || x.AddressLine1.Contains(search) || x.AddressLine2.Contains(search) || x.City.Contains(search) || x.Province.Contains(search) || x.ContactName.Contains(search) || x.Phone1.Contains(search) || x.Phone2.Contains(search) || x.Email.Contains(search) );
            return this;
        }

		public CustomerQuery Withfilter(IEnumerable<filterRule> filters)
        {
           if (filters != null)
           {
               foreach (var rule in filters)
               {
                  
					
				    						if (rule.field == "Id")
						{
							And(x => x.Id == Convert.ToInt32(rule.value));
						}
				   
					 				
											if (rule.field == "AccountNumber")
						{
							And(x => x.AccountNumber.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "ShortName")
						{
							And(x => x.ShortName.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "Name")
						{
							And(x => x.Name.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "CustomerType")
						{
							And(x => x.CustomerType.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "AddressLine1")
						{
							And(x => x.AddressLine1.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "AddressLine2")
						{
							And(x => x.AddressLine2.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "City")
						{
							And(x => x.City.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "Province")
						{
							And(x => x.Province.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "ContactName")
						{
							And(x => x.ContactName.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "Phone1")
						{
							And(x => x.Phone1.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "Phone2")
						{
							And(x => x.Phone2.Contains(rule.value));
						}
				    
				    
					 				
											if (rule.field == "Email")
						{
							And(x => x.Email.Contains(rule.value));
						}
				    
				    
					 									
                   
               }
           }
            return this;
        }
    }
}



