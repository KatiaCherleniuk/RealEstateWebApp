using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateWebApp.Models.Stats
{
    public class StatsAveragePrices
    {
        public string Title { get; set; }
        public DateTime Period { get; set; }
        public int AveragePrice { get; set; }
    }
}
