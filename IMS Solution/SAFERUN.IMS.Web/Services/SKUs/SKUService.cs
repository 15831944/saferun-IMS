
             
           
 

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Repository.Pattern.Repositories;
using Service.Pattern;

using SAFERUN.IMS.Web.Models;
using SAFERUN.IMS.Web.Repositories;
using System.Data;
namespace SAFERUN.IMS.Web.Services
{
    public class SKUService : Service< SKU >, ISKUService
    {

        private readonly IRepositoryAsync<SKU> _repository;
        public  SKUService(IRepositoryAsync< SKU> repository)
            : base(repository)
        {
            _repository=repository;
        }




        public void ImportSku(System.Data.DataTable datatable)
        {
            foreach (DataRow row in datatable.Rows)
            {
                if (row["存货编码"].ToString().Trim() == string.Empty)
                    continue;
                SKU item = new SKU();
                item.Sku = row["存货编码"].ToString().Trim();
                item.ManufacturerSku = row["图纸编号"].ToString().Trim();
                item.Description = row["图纸名称"].ToString().Trim();

                item.MadeType = row["生产方式"].ToString().Trim();
                if (Find(item.Sku) == null)
                {
                    this.Insert(item);
                }

            }
        }


        public SKU Find(string sku)
        {
            return this.Queryable().Where(x => x.Sku == sku).FirstOrDefault();
        }
    }
}



