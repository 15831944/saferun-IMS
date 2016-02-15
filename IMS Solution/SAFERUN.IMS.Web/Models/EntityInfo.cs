using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAFERUN.IMS.Web.Models
{
    public partial class EntityInfo
    {
        public string EntitySetName { get; set; }
        public string FieldName { get; set; }
        public string FieldTypeName { get; set; }
        public bool IsRequired { get; set; }
    }
}