using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAFERUN.IMS.Web.Models
{
    [Serializable]
    public class WorkGeneratorViewModel
    {
        public int WorkId { get; set; }
        public string WorkNo { get; set; }
        public int OrderId { get; set; }
        public string OrderKey { get; set; }
        public int OrderDetailId { get; set; }

        public IEnumerable<int> WorkingBomComponents { get; set; }
    }
}