




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
using System.Reflection;
namespace SAFERUN.IMS.Web.Services
{
    public class BOMComponentService : Service<BOMComponent>, IBOMComponentService
    {
        private readonly ISKUService _iskuservice;
        private readonly IRepositoryAsync<BOMComponent> _repository;
        private readonly IDataTableImportMappingService _mappingservice;
        private readonly IProductionProcessService _processservice;
        public BOMComponentService(IProductionProcessService processservice,ISKUService iskuservice, IRepositoryAsync<BOMComponent> repository, IDataTableImportMappingService mappingservice)
            : base(repository)
        {
            _repository = repository;
            _mappingservice = mappingservice;
            _iskuservice = iskuservice;
            _processservice = processservice;
        }

        public IEnumerable<BOMComponent> GetByProductionProcessId(int productionprocessid)
        {
            return _repository.GetByProductionProcessId(productionprocessid);
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



        public void ImportDataTable(System.Data.DataTable datatable)
        {
            List<BOMComponent> list = new List<BOMComponent>();
            SKU sku = null;
            string parentsku = "";
            BOMComponent parentbomComponent = null;
            foreach (DataRow row in datatable.Rows)
            {

                BOMComponent item = new BOMComponent();
                item.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added;
                foreach (DataColumn col in datatable.Columns)
                {

                    var sourcefieldname = col.ColumnName;
                    var colval = row[sourcefieldname].ToString();
                    if (sourcefieldname == "组件")
                    {
                        string strsku = row["组件"].ToString().Trim();
                        sku = _iskuservice.Queryable().Where(x => x.Sku == strsku).FirstOrDefault();
                        if (sku == null)
                        {
                            throw new Exception(string.Format("{0}没有维护SKU主数据", strsku));
                        }
                        item.ComponentSKU = sku.Sku;
                        item.SKUId = sku.Id;
                        item.StockSKU = sku.Sku;
                        item.SKUGroup = sku.SKUGroup;

                    }
                    else if (sourcefieldname == "父件")
                    {
                        parentsku = row["父件"].ToString().Trim();
                        if (!string.IsNullOrEmpty(parentsku))
                        {
                            SKU psku = _iskuservice.Queryable().Where(x => x.Sku == parentsku).FirstOrDefault();
                            if (psku == null)
                            {
                                throw new Exception(string.Format("{0}没有维护SKU主数据", parentsku));
                            }
                            parentbomComponent = list.Where(x => x.ComponentSKU == parentsku).FirstOrDefault();
                            item.ParentComponent = parentbomComponent;
                            item.ParentComponentId = parentbomComponent.Id;
                        }
                    }
                    else if (sourcefieldname == "生产工序")
                    {
                        var process = _processservice.Queryable().Where(x => x.Name == colval).FirstOrDefault();
                        if (process != null)
                        {
                            item.ProductionProcessId = process.Id;
                        }
                    }
                    else
                    {
                        var mapping = _mappingservice.FindMapping("BOMComponent", sourcefieldname);
                        if (mapping != null && row[sourcefieldname] != DBNull.Value)
                        {

                            Type bomcomponenttype = item.GetType();
                            PropertyInfo propertyInfo = bomcomponenttype.GetProperty(mapping.FieldName);
                            propertyInfo.SetValue(item, Convert.ChangeType(row[sourcefieldname], propertyInfo.PropertyType), null);
                            //bomcomponenttype.GetProperty(mapping.FieldName).SetValue(item, row[sourcefieldname]);
                        }
                    }


                }

                list.Add(item);



            }
            this.InsertRange(list);
        }
    }
}



