




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
    public class BOMComponentService : Service<BOMComponent>, IBOMComponentService
    {
        private readonly ISKUService _iskuservice;
        private readonly IRepositoryAsync<BOMComponent> _repository;
        public BOMComponentService(IRepositoryAsync<BOMComponent> repository, ISKUService iskuservice)
            : base(repository)
        {
            _repository = repository;
            _iskuservice = iskuservice;
        }

        public IEnumerable<BOMComponent> GetBySKUId(int skuid)
        {
            return _repository.GetBySKUId(skuid);
        }
        public IEnumerable<BOMComponent> GetByParentComponentId(int parentcomponentid)
        {
            return _repository.GetByParentComponentId(parentcomponentid);
        }
        public IEnumerable<BOMComponent> GetComponentsByParentComponentId(int parentcomponentid)
        {
            return _repository.GetComponentsByParentComponentId(parentcomponentid);
        }




        public void ImportFormExcel(System.Data.DataTable datatable)
        {
            List<BOMComponent> list = new List<BOMComponent>();
            int i = 0;
            foreach (DataRow dr in datatable.Rows)
            {
                string strsku = dr["组件"].ToString().Trim();
                if (string.IsNullOrEmpty(strsku))
                {
                    continue;
                }
             
                SKU sku = _iskuservice.Queryable().Where(x => x.Sku == strsku).FirstOrDefault();
                if (sku == null)
                {
                    throw new Exception(string.Format("{0}没有维护SKU主数据", strsku));
                }
                string pstrsku = dr["父件"].ToString().Trim();
                if (!string.IsNullOrEmpty(pstrsku))
                {
                    SKU psku = _iskuservice.Queryable().Where(x => x.Sku == pstrsku).FirstOrDefault();
                    if (psku == null)
                    {
                        throw new Exception(string.Format("{0}没有维护SKU主数据", pstrsku));
                    }
                }
                //if ((i++) <= 5)
                //{
                    BOMComponent item = new BOMComponent();
                    item.SKUId = sku.Id;
                    item.ComponentSKU = sku.Sku;
                    item.DesignName = dr["名称"].ToString().Trim();
                    item.ConsumeTime = Convert.ToDecimal(dr["单位用时"].ToString().Trim());
                    item.StockSKU = sku.Sku;
                    item.ConsumeQty = Convert.ToDecimal(dr["用量"].ToString().Trim());
                    item.FinishedSKU = dr["成品组件"].ToString().Trim();
                    item.GraphSKU = dr["图纸编号"].ToString().Trim();
                    item.Locator = dr["工序"].ToString().Trim();
                    item.Remark1 = dr["备注"].ToString().Trim();
                    item.RejectRatio = Convert.ToDecimal(dr["不良率"].ToString().Trim());
               
                    var parentitem = list.Where(x => x.ComponentSKU == pstrsku).FirstOrDefault();
                    item.ParentComponent = parentitem;
                    item.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added;

                    list.Add(item);
                //}

            }
            this.InsertRange(list);
        }
    }
}



