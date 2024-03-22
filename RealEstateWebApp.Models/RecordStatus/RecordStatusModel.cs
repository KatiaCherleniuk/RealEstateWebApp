using RealEstateWebApp.Models.Record;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstateWebApp.Models.RecordStatus
{
    public class RecordStatusModel
    {
        public int Id { get; set; }
        public int RecordId { get; set; }
        public RecordStatusType Type { get; set; }
        public DateTime UpdatedAt {  get; set; }

    }
}
