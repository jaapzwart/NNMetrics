using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNMetrics.Models
{
    /// <summary>
    /// What are the metrics that needs to be measured and when are they measured?
    /// </summary>
    public class Metrics
    {
        public int ID { get; set; }
        public string userName { get; set; }
        public string Title { get; set; }
        public DateTime MeasureDate { get; set; }
        public int POSatisfaction { get; set; }
        public int CompletedForecast { get; set; }
        public int MTTR { get; set; }
        public int NumberOfDeployments { get; set; }
    }
}
