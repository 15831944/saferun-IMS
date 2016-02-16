using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SAFERUN.IMS.Web.Models
{
    public partial class DataTableImportMapping:Entity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Index("IX_DataTableImportMapping",IsUnique=true, Order=1)]
        [MaxLength(50)]
        public string EntitySetName { get; set; }
        [Required]
        [Index("IX_DataTableImportMapping", IsUnique = true, Order = 2)]
        [MaxLength(50)]
        public string FieldName { get; set; }
        public string DefaultValue { get; set; }
        public string TypeName { get; set; }
        public bool IsRequired { get; set; }
        public string SourceFieldName { get; set; }
        public bool IsEnabled { get; set; }
        public string RegularExpression { get; set; }

    }
}