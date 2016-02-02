using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SAFERUN.IMS.Web.Models
{
    [MetadataType(typeof(MessageMetadata))]
    public partial class Message
    {
    }

    public partial class MessageMetadata
    {
        [Display(Name = "回复消息")]
        public Message RequestMessage { get; set; }

        [Required(ErrorMessage = "Please enter : Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : From")]
        [Display(Name = "From")]
        [MaxLength(20)]
        public string From { get; set; }

        [Required(ErrorMessage = "Please enter : To")]
        [Display(Name = "To")]
        [MaxLength(20)]
        public string To { get; set; }

        [Display(Name = "标题")]
        [MaxLength(50)]
        public string Title { get; set; }

        [Display(Name = "内容")]
        [MaxLength(500)]
        public string Content { get; set; }

        [Display(Name = "状态")]
        public int Status { get; set; }

        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }

        [Display(Name = "回复消息")]
        public int MessageId { get; set; }

    }




	public class MessageChangeViewModel
    {
        public IEnumerable<Message> inserted { get; set; }
        public IEnumerable<Message> deleted { get; set; }
        public IEnumerable<Message> updated { get; set; }
    }

}
