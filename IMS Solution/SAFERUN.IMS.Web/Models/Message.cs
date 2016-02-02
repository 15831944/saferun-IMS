using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SAFERUN.IMS.Web.Models
{
    public partial class Message:Entity
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        [Required]
        public string From { get; set; }
        [Required]
        public string To { get; set; }
       
        public string Content { get; set; }
        public int Status { get; set; }

        public DateTime CreateTime { get; set; }


        public int? MessageId { get; set; }
        [ForeignKey("MessageId")]
        public virtual Message RequestMessage { get; set; }
    }
}