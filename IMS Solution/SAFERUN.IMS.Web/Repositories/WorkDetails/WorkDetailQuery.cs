



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
    public class WorkDetailQuery : QueryObject<WorkDetail>
    {
        public WorkDetailQuery WithAnySearch(string search)
        {
            if (!string.IsNullOrEmpty(search))
                And(x => x.WorkNo.Contains(search) || x.GraphSKU.Contains(search) || x.GraphVer.Contains(search) || x.Brand.Contains(search) || x.Process.Contains(search) || x.Responsibility.Contains(search) || x.AltOrderProdctionDate.ToString().Contains(search) || x.AltProdctionDate1.ToString().Contains(search) || x.ActualProdctionDate1.ToString().Contains(search) || x.AltProdctionDate2.ToString().Contains(search) || x.ActualProdctionDate2.ToString().Contains(search) || x.AltProdctionDate3.ToString().Contains(search) || x.ActualProdctionDate3.ToString().Contains(search) || x.AltProdctionDate4.ToString().Contains(search) || x.ActualProdctionDate4.ToString().Contains(search) || x.AltProdctionDate5.ToString().Contains(search) || x.ActualProdctionDate5.ToString().Contains(search) || x.ConfirmUser.Contains(search) || x.Remark1.Contains(search) || x.Remark2.Contains(search));
            return this;
        }

        public WorkDetailQuery WithPopupSearch(string search, string para = "")
        {
            if (!string.IsNullOrEmpty(search))
                And(x => x.WorkNo.Contains(search) || x.GraphSKU.Contains(search) || x.GraphVer.Contains(search) || x.Brand.Contains(search) || x.Process.Contains(search) || x.Responsibility.Contains(search) || x.AltOrderProdctionDate.ToString().Contains(search) || x.AltProdctionDate1.ToString().Contains(search) || x.ActualProdctionDate1.ToString().Contains(search) || x.AltProdctionDate2.ToString().Contains(search) || x.ActualProdctionDate2.ToString().Contains(search) || x.AltProdctionDate3.ToString().Contains(search) || x.ActualProdctionDate3.ToString().Contains(search) || x.AltProdctionDate4.ToString().Contains(search) || x.ActualProdctionDate4.ToString().Contains(search) || x.AltProdctionDate5.ToString().Contains(search) || x.ActualProdctionDate5.ToString().Contains(search) || x.ConfirmUser.Contains(search) || x.Remark1.Contains(search) || x.Remark2.Contains(search));
            return this;
        }

        public WorkDetailQuery Withfilter(IEnumerable<filterRule> filters)
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



                    if (rule.field == "WorkNo" && !string.IsNullOrEmpty(rule.value))
                    {
                        And(x => x.WorkNo.Contains(rule.value));
                    }





                    if (rule.field == "WorkId" && !string.IsNullOrEmpty(rule.value))
                    {
                        int val = Convert.ToInt32(rule.value);
                        And(x => x.WorkId == val);
                    }




                    if (rule.field == "ParentSKUId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsNumeric())
                    {
                        int val = Convert.ToInt32(rule.value);
                        And(x => x.ParentSKUId == val);
                    }

                    if (rule.field == "Status" && !string.IsNullOrEmpty(rule.value) && rule.value.IsNumeric())
                    {
                        int val = Convert.ToInt32(rule.value);
                        And(x => x.Status == val);
                    }


                    if (rule.field == "ComponentSKUId" && !string.IsNullOrEmpty(rule.value) && rule.value.IsNumeric())
                    {
                        int val = Convert.ToInt32(rule.value);
                        And(x => x.ComponentSKUId == val);
                    }



                    if (rule.field == "GraphSKU" && !string.IsNullOrEmpty(rule.value))
                    {
                        And(x => x.GraphSKU.Contains(rule.value));
                    }




                    if (rule.field == "GraphVer" && !string.IsNullOrEmpty(rule.value))
                    {
                        And(x => x.GraphVer.Contains(rule.value));
                    }





                    if (rule.field == "ConsumeQty" && !string.IsNullOrEmpty(rule.value))
                    {
                        int val = Convert.ToInt32(rule.value);
                        And(x => x.ConsumeQty == val);
                    }




                    if (rule.field == "StockQty" && !string.IsNullOrEmpty(rule.value))
                    {
                        int val = Convert.ToInt32(rule.value);
                        And(x => x.StockQty == val);
                    }




                    if (rule.field == "RequirementQty" && !string.IsNullOrEmpty(rule.value))
                    {
                        int val = Convert.ToInt32(rule.value);
                        And(x => x.RequirementQty == val);
                    }



                    if (rule.field == "Brand" && !string.IsNullOrEmpty(rule.value))
                    {
                        And(x => x.Brand.Contains(rule.value));
                    }




                    if (rule.field == "Process" && !string.IsNullOrEmpty(rule.value))
                    {
                        And(x => x.Process.Contains(rule.value));
                    }




                    if (rule.field == "Responsibility" && !string.IsNullOrEmpty(rule.value))
                    {
                        And(x => x.Responsibility.Contains(rule.value));
                    }






                    if (rule.field == "AltOrderProdctionDate" && !string.IsNullOrEmpty(rule.value))
                    {
                        var date = Convert.ToDateTime(rule.value);
                        And(x => SqlFunctions.DateDiff("d", date, x.AltOrderProdctionDate) > 0);
                    }




                    if (rule.field == "AltProdctionDate1" && !string.IsNullOrEmpty(rule.value))
                    {
                        var date = Convert.ToDateTime(rule.value);
                        And(x => SqlFunctions.DateDiff("d", date, x.AltProdctionDate1) > 0);
                    }




                    if (rule.field == "ActualProdctionDate1" && !string.IsNullOrEmpty(rule.value))
                    {
                        var date = Convert.ToDateTime(rule.value);
                        And(x => SqlFunctions.DateDiff("d", date, x.ActualProdctionDate1) > 0);
                    }




                    if (rule.field == "AltProdctionDate2" && !string.IsNullOrEmpty(rule.value))
                    {
                        var date = Convert.ToDateTime(rule.value);
                        And(x => SqlFunctions.DateDiff("d", date, x.AltProdctionDate2) > 0);
                    }




                    if (rule.field == "ActualProdctionDate2" && !string.IsNullOrEmpty(rule.value))
                    {
                        var date = Convert.ToDateTime(rule.value);
                        And(x => SqlFunctions.DateDiff("d", date, x.ActualProdctionDate2) > 0);
                    }




                    if (rule.field == "AltProdctionDate3" && !string.IsNullOrEmpty(rule.value))
                    {
                        var date = Convert.ToDateTime(rule.value);
                        And(x => SqlFunctions.DateDiff("d", date, x.AltProdctionDate3) > 0);
                    }




                    if (rule.field == "ActualProdctionDate3" && !string.IsNullOrEmpty(rule.value))
                    {
                        var date = Convert.ToDateTime(rule.value);
                        And(x => SqlFunctions.DateDiff("d", date, x.ActualProdctionDate3) > 0);
                    }




                    if (rule.field == "AltProdctionDate4" && !string.IsNullOrEmpty(rule.value))
                    {
                        var date = Convert.ToDateTime(rule.value);
                        And(x => SqlFunctions.DateDiff("d", date, x.AltProdctionDate4) > 0);
                    }




                    if (rule.field == "ActualProdctionDate4" && !string.IsNullOrEmpty(rule.value))
                    {
                        var date = Convert.ToDateTime(rule.value);
                        And(x => SqlFunctions.DateDiff("d", date, x.ActualProdctionDate4) > 0);
                    }




                    if (rule.field == "AltProdctionDate5" && !string.IsNullOrEmpty(rule.value))
                    {
                        var date = Convert.ToDateTime(rule.value);
                        And(x => SqlFunctions.DateDiff("d", date, x.AltProdctionDate5) > 0);
                    }




                    if (rule.field == "ActualProdctionDate5" && !string.IsNullOrEmpty(rule.value))
                    {
                        var date = Convert.ToDateTime(rule.value);
                        And(x => SqlFunctions.DateDiff("d", date, x.ActualProdctionDate5) > 0);
                    }


                    if (rule.field == "ConfirmUser" && !string.IsNullOrEmpty(rule.value))
                    {
                        And(x => x.ConfirmUser.Contains(rule.value));
                    }




                    if (rule.field == "Remark1" && !string.IsNullOrEmpty(rule.value))
                    {
                        And(x => x.Remark1.Contains(rule.value));
                    }




                    if (rule.field == "Remark2" && !string.IsNullOrEmpty(rule.value))
                    {
                        And(x => x.Remark2.Contains(rule.value));
                    }





                }
            }
            return this;
        }
    }
}



