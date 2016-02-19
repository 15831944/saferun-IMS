




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
    public class ProcessStepService : Service<ProcessStep>, IProcessStepService
    {

        private readonly IRepositoryAsync<ProcessStep> _repository;
        private readonly IProductionProcessService _processservice;
        private readonly IDataTableImportMappingService _mappingservice;
        public ProcessStepService(IProductionProcessService processservice, IRepositoryAsync<ProcessStep> repository, IDataTableImportMappingService mappingservice)
            : base(repository)
        {
            _repository = repository;
            _mappingservice = mappingservice;
            _processservice = processservice;
        }

        public IEnumerable<ProcessStep> GetByProductionProcessId(int productionprocessid)
        {
            return _repository.GetByProductionProcessId(productionprocessid);
        }



        public void ImportDataTable(System.Data.DataTable datatable)
        {
        
            Dictionary<string, ProductionProcess> processlist = new Dictionary<string, ProductionProcess>();
            foreach (DataRow row in datatable.Rows)
            {
               
                ProcessStep item = new ProcessStep();
                item.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added;
                ProductionProcess process = null;
                foreach (DataColumn col in datatable.Columns)
                {
                    var sourcefieldname = col.ColumnName;
                    var mapping = _mappingservice.FindMapping("ProcessStep", sourcefieldname);
                    if (mapping != null && row[sourcefieldname] != DBNull.Value)
                    {
                        if (col.ColumnName == "工序名称")
                        {
                            string name = row[sourcefieldname].ToString();
                            process = this._processservice.Queryable().Where(x => x.Name == name).FirstOrDefault();
                            if (process == null)
                            {
                                if (!processlist.ContainsKey(name))
                                {
                                    process = new ProductionProcess() { Name = name};
                                    process.ObjectState = Repository.Pattern.Infrastructure.ObjectState.Added;
                                    processlist.Add(process.Name, process);
                                }
                                else
                                {
                                    process = processlist[name];
                                }
                               
                            }
                            item.ProductionProcessId = process.Id;
                            item.ProductionProcess = process;

                            process.ProcessSteps.Add(item);
                        }
                        else
                        {
                            Type processsteptype = item.GetType();
                            PropertyInfo propertyInfo = processsteptype.GetProperty(mapping.FieldName);
                            propertyInfo.SetValue(item, Convert.ChangeType(row[sourcefieldname], propertyInfo.PropertyType), null);
                        }
                    }

                }
               
            }
            this._processservice.InsertGraphRange(processlist.Values);

            //foreach (var item in processlist.Values)
            //    this._processservice.Insert(new ProductionProcess() { Name = item.Name });
            //this._processservice.InsertOrUpdateGraph(item);
        }
    }
}



