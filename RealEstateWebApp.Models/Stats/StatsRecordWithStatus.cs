using RealEstateWebApp.Models.Record;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateWebApp.Models.Stats
{
    public class StatsRecordWithStatus
    {
        public RecordStatusType Status { get; set; }
        public int RecordCount { get; set; }
    }
}
