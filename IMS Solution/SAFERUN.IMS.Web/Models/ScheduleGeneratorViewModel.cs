using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAFERUN.IMS.Web.Models
{
    [Serializable]
    public class ScheduleGeneratorViewModel
    {
        public int ProductionScheduleId { get; set; }
        public string ScheduleNo { get; set; }
        public int WorkId { get; set; }
        public string WorkNo { get; set; }
        public int OrderId { get; set; }
        public string OrderKey { get; set; }


        public IEnumerable<int> WorkDetails { get; set; }
    }
}