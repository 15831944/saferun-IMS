using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAFERUN.IMS.Web.Models
{
    public class StationWorkViewModel
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string ComponentSKU { get; set; }
        public string GraphSKU { get; set; }
        public int Status { get; set; }
        public DateTime? StartingDateTime { get; set; }
        public DateTime? CompletedDateTime { get; set; }
        public decimal ElapsedTime { get; set; }
        public decimal StandardElapsedTime { get; set; }
        public string Operator { get; set; }
    }
}
