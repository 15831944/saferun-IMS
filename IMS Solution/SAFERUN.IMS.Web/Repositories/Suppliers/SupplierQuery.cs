
                    
      
     
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
   public class SupplierQuery:QueryObject<Supplier>
    {
        public SupplierQuery WithAnySearch(string search)
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.AccountNumber.Contains(search) || x.ShortName.Contains(search) || x.Name.Contains(search) || x.CustomerType.Contains(search) || x.AddressLine1.Contains(search) || x.AddressLine2.Contains(search) || x.City.Contains(search) || x.Province.Contains(search) || x.ContactName.Contains(search) || x.Phone1.Contains(search) || x.Phone2.Contains(search) || x.Email.Contains(search) );
            return this;
        }

		public SupplierQuery WithPopupSearch(string search,string para="")
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.AccountNumber.Contains(search) || x.ShortName.Contains(search) || x.Name.Contains(search) || x.CustomerType.Contains(search) || x.AddressLine1.Contains(search) || x.AddressLine2.Contains(search) || x.City.Contains(search) || x.Province.Contains(search) || x.ContactName.Contains(search) || x.Phone1.Contains(search) || x.Phone2.Contains(search) || x.Email.Contains(search) );
            return this;
        }

		public SupplierQuery Withfilter(IEnumerable<filterRule> filters)
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
				   
					
				    				
											if (rule.field == "AccountNumber"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.AccountNumber.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "ShortName"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ShortName.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "Name"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Name.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "CustomerType"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.CustomerType.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "AddressLine1"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.AddressLine1.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "AddressLine2"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.AddressLine2.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "City"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.City.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "Province"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Province.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "ContactName"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.ContactName.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "Phone1"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Phone1.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "Phone2"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Phone2.Contains(rule.value));
						}
				    
				    
					
				    				
											if (rule.field == "Email"  && !string.IsNullOrEmpty(rule.value))
						{
							And(x => x.Email.Contains(rule.value));
						}
				    
				    
					
				    									
                   
               }
           }
            return this;
        }
    }
}



