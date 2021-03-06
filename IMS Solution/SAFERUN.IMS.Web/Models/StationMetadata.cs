﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAFERUN.IMS.Web.Models
{
    [MetadataType(typeof(StationMetadata))]
    public partial class Station
    {
    }

    public partial class StationMetadata
    {
        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 工位代码")]
        [Display(Name = "工位代码")]
        [MaxLength(20)]
        public string StationNo { get; set; }

        [Required(ErrorMessage = "Please enter : 工位名称")]
        [Display(Name = "工位名称")]
        [MaxLength(20)]
        public string Name { get; set; }
        [Display(Name = "设备名称")]
        [MaxLength(50)]
        public string Equipment { get; set; }

        [Display(Name = "描述")]
        [MaxLength(200)]
        public string Description { get; set; }

        [Display(Name = "机台数量")]
        public int EquipmentNumber { get; set; }

        [Display(Name = "标准单位耗时(H)")]
        public decimal StandardElapsedTime { get; set; }
        [Display(Name = "工作时长(H)")]
        public decimal WorkingTime { get; set; }
         [Display(Name = "状态")]
        public int Status { get; set; }


        [Display(Name = "CreatedUserId")]
        public string CreatedUserId { get; set; }

        [Display(Name = "CreatedDateTime")]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "LastEditUserId")]
        public string LastEditUserId { get; set; }

        [Display(Name = "LastEditDateTime")]
        public DateTime LastEditDateTime { get; set; }

    }




	public class StationChangeViewModel
    {
        public IEnumerable<Station> inserted { get; set; }
        public IEnumerable<Station> deleted { get; set; }
        public IEnumerable<Station> updated { get; set; }
    }

}
