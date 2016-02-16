using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAFERUN.IMS.Web.Models
{
    [MetadataType(typeof(DataTableImportMappingMetadata))]
    public partial class DataTableImportMapping
    {
    }

    public partial class DataTableImportMappingMetadata
    {
        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 实体名称")]
        [Display(Name = "实体名称")]
        [MaxLength(50)]
        public string EntitySetName { get; set; }

        [Required(ErrorMessage = "Please enter : 字段名")]
        [Display(Name = "字段名")]
        [MaxLength(50)]
        public string FieldName { get; set; }

        [Display(Name = "类型")]
        [MaxLength(30)]
        public string TypeName { get; set; }

        [Display(Name = "是否必填")]
        public bool IsRequired { get; set; }
        [Display(Name = "默认值")]
        [MaxLength(50)]
        public string DefaultValue { get; set; }

        [Display(Name = "Excel列名")]
        [MaxLength(50)]
        public string SourceFieldName { get; set; }

        [Display(Name = "是否启用")]
        public bool IsEnabled { get; set; }

        [Display(Name = "验证表达式")]
        [MaxLength(100)]
        public string RegularExpression { get; set; }

    }




	public class DataTableImportMappingChangeViewModel
    {
        public IEnumerable<DataTableImportMapping> inserted { get; set; }
        public IEnumerable<DataTableImportMapping> deleted { get; set; }
        public IEnumerable<DataTableImportMapping> updated { get; set; }
    }

}
