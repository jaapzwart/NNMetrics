using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NNMetrics.Data;

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
   
    public static class ModelHelper
    {
        
        public static List<object> MultiLineData()
        {
            List<object> objs = new List<object>();
           
            if (SharedData.db_mttr.Count().Equals(4))
            {
                objs.Add(new[] { "", SharedData.db_title[0], SharedData.db_title[1], SharedData.db_title[2], SharedData.db_title[3] });
                objs.Add(new[] { 0, SharedData.db_mttr[0], SharedData.db_mttr[1], SharedData.db_mttr[2], SharedData.db_mttr[3] });
            }
            return objs;
        }
    }
}
