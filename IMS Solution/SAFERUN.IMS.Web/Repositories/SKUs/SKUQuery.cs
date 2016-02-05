
                    
      
     
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
   public class SKUQuery:QueryObject<SKU>
    {
        public SKUQuery WithAnySearch(string search)
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.Sku.Contains(search) || x.ALTSku.Contains(search) || x.ManufacturerSku.Contains(search) || x.Model.Contains(search) || x.CLASS.Contains(search) || x.SKUGroup.Contains(search) || x.MadeType.Contains(search) || x.STDUOM.Contains(search) || x.Description.Contains(search) || x.Brand.Contains(search) || x.PackKey.Contains(search) || x.SUSR2.Contains(search) || x.SUSR3.Contains(search) || x.SUSR4.Contains(search) || x.SUSR5.ToString().Contains(search) );
            return this;
        }

		public SKUQuery WithPopupSearch(string search,string para="")
        {
            if (!string.IsNullOrEmpty(search))
                And( x =>  x.Sku.Contains(search) || x.ALTSku.Contains(search) || x.ManufacturerSku.Contains(search) || x.Model.Contains(search) || x.CLASS.Contains(search) || x.SKUGroup.Contains(search) || x.MadeType.Contains(search) || x.STDUOM.Contains(search) || x.Description.Contains(search) || x.Brand.Contains(search) || x.PackKey.Contains(search) || x.SUSR2.Contains(search) || x.SUSR3.Contains(search) || x.SUSR4.Contains(search) || x.SUSR5.ToString().Contains(search) );
            return this;
        }

		public SKUQuery Withfilter(IEnumerable<filterRule> filters)
        {
           if (filters != null)
           {
               foreach (var rule in filters)
               {
                  					
                   
               }
           }
            return this;
        }
    }
}



