﻿using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SAFERUN.IMS.Web.Models
{
    public partial class Station:Entity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Index("IX_Station",IsUnique=true)]
        public string StationNo { get; set; }
        [Required]
        public string Name { get; set; }
        public string Equipment { get; set; }
        public string Description { get; set; }
        public int EquipmentNumber { get; set; }

        public decimal StandardElapsedTime { get; set; }

        public decimal WorkingTime { get; set; }

        public int Status { get; set; }

        





        [ScaffoldColumn(false)]
        [Display(Name = "新增用户", Description = "新增用户")]
        [StringLength(20)]
        public string CreatedUserId { get; set; }
        [ScaffoldColumn(false)]
        [Display(Name = "新增时间", Description = "新增时间")]
        public DateTime? CreatedDateTime { get; set; }
        [ScaffoldColumn(false)]
        [Display(Name = "最后修改用户", Description = "最后修改用户")]
        [StringLength(20)]
        public string LastEditUserId { get; set; }
        [ScaffoldColumn(false)]
        [Display(Name = "最后修改时间", Description = "最后修改时间")]
        public DateTime? LastEditDateTime { get; set; }


    }
}