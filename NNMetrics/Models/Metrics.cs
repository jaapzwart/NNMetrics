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
        /// <summary>
        /// Get data for the MTTR
        /// </summary>
        /// <returns>Json object with MTTR data</returns>
        public static List<object> MultiLineDataMTTR()
        {
            List<object> objs = new List<object>();
           
            if (SharedData.db_mttr.Count().Equals(1))
            {
                objs.Add(new[] { "", SharedData.db_title[0] });
                objs.Add(new[] { 0, SharedData.db_mttr[0] });
            }
            if (SharedData.db_mttr.Count().Equals(2))
            {
                objs.Add(new[] { "", SharedData.db_title[0], SharedData.db_title[1] });
                objs.Add(new[] { 0, SharedData.db_mttr[0], SharedData.db_mttr[1] });
            }
            if (SharedData.db_mttr.Count().Equals(3))
            {
                objs.Add(new[] { "", SharedData.db_title[0], SharedData.db_title[1], SharedData.db_title[2] });
                objs.Add(new[] { 0, SharedData.db_mttr[0], SharedData.db_mttr[1], SharedData.db_mttr[2] });
            }
            if (SharedData.db_mttr.Count().Equals(4))
            {
                objs.Add(new[] { "", SharedData.db_title[0], SharedData.db_title[1], SharedData.db_title[2], SharedData.db_title[3] });
                objs.Add(new[] { 0, SharedData.db_mttr[0], SharedData.db_mttr[1], SharedData.db_mttr[2], SharedData.db_mttr[3] });
            }
            if (SharedData.db_mttr.Count().Equals(5))
            {
                objs.Add(new[] { "", SharedData.db_title[0], SharedData.db_title[1], SharedData.db_title[2], SharedData.db_title[3],
                SharedData.db_title[4] });
                objs.Add(new[] { 0, SharedData.db_mttr[0], SharedData.db_mttr[1], SharedData.db_mttr[2], SharedData.db_mttr[3],
                SharedData.db_mttr[4] });
            }
            if (SharedData.db_mttr.Count().Equals(6))
            {
                objs.Add(new[] { "", SharedData.db_title[0], SharedData.db_title[1], SharedData.db_title[2], SharedData.db_title[3],
                SharedData.db_title[4], SharedData.db_title[5] });
                objs.Add(new[] { 0, SharedData.db_mttr[0], SharedData.db_mttr[1], SharedData.db_mttr[2], SharedData.db_mttr[3],
                SharedData.db_mttr[4], SharedData.db_mttr[5] });
            }
            if (SharedData.db_mttr.Count().Equals(7))
            {
                objs.Add(new[] { "", SharedData.db_title[0], SharedData.db_title[1], SharedData.db_title[2], SharedData.db_title[3],
                SharedData.db_title[4], SharedData.db_title[5], SharedData.db_title[6] });
                objs.Add(new[] { 0, SharedData.db_mttr[0], SharedData.db_mttr[1], SharedData.db_mttr[2], SharedData.db_mttr[3],
                SharedData.db_mttr[4], SharedData.db_mttr[5], SharedData.db_mttr[6] });
            }
            if (SharedData.db_mttr.Count().Equals(8))
            {
                objs.Add(new[] { "", SharedData.db_title[0], SharedData.db_title[1], SharedData.db_title[2], SharedData.db_title[3],
                SharedData.db_title[4], SharedData.db_title[5], SharedData.db_title[6], SharedData.db_title[7] });
                objs.Add(new[] { 0, SharedData.db_mttr[0], SharedData.db_mttr[1], SharedData.db_mttr[2], SharedData.db_mttr[3],
                SharedData.db_mttr[4], SharedData.db_mttr[5], SharedData.db_mttr[6], SharedData.db_mttr[7] });
            }
            if (SharedData.db_mttr.Count().Equals(9))
            {
                objs.Add(new[] { "", SharedData.db_title[0], SharedData.db_title[1], SharedData.db_title[2], SharedData.db_title[3],
                SharedData.db_title[4], SharedData.db_title[5], SharedData.db_title[6], SharedData.db_title[7], SharedData.db_title[8] });
                objs.Add(new[] { 0, SharedData.db_mttr[0], SharedData.db_mttr[1], SharedData.db_mttr[2], SharedData.db_mttr[3],
                SharedData.db_mttr[4], SharedData.db_mttr[5], SharedData.db_mttr[6], SharedData.db_mttr[7], SharedData.db_mttr[8] });
            }
            return objs;
        }

        /// <summary>
        /// Get the data for the PO satisfaction
        /// </summary>
        /// <returns>Json object with PO satisfaction data</returns>
        public static List<object> MultiLineDataPOSatisfaction()
        {
            List<object> objs = new List<object>();

            if (SharedData.db_PO.Count().Equals(1))
            {
                objs.Add(new[] { "", SharedData.db_title[0] });
                objs.Add(new[] { 0, SharedData.db_PO[0] });
            }
            if (SharedData.db_PO.Count().Equals(2))
            {
                objs.Add(new[] { "", SharedData.db_title[0], SharedData.db_title[1] });
                objs.Add(new[] { 0, SharedData.db_PO[0], SharedData.db_PO[1] });
            }
            if (SharedData.db_PO.Count().Equals(3))
            {
                objs.Add(new[] { "", SharedData.db_title[0], SharedData.db_title[1], SharedData.db_title[2] });
                objs.Add(new[] { 0, SharedData.db_PO[0], SharedData.db_PO[1], SharedData.db_PO[2] });
            }
            if (SharedData.db_PO.Count().Equals(4))
            {
                objs.Add(new[] { "", SharedData.db_title[0], SharedData.db_title[1], SharedData.db_title[2], SharedData.db_title[3] });
                objs.Add(new[] { 0, SharedData.db_PO[0], SharedData.db_PO[1], SharedData.db_PO[2], SharedData.db_PO[3] });
            }
            if (SharedData.db_PO.Count().Equals(5))
            {
                objs.Add(new[] { "", SharedData.db_title[0], SharedData.db_title[1], SharedData.db_title[2], SharedData.db_title[3],
                SharedData.db_title[4] });
                objs.Add(new[] { 0, SharedData.db_PO[0], SharedData.db_PO[1], SharedData.db_PO[2], SharedData.db_PO[3],
                SharedData.db_PO[4] });
            }
            if (SharedData.db_PO.Count().Equals(6))
            {
                objs.Add(new[] { "", SharedData.db_title[0], SharedData.db_title[1], SharedData.db_title[2], SharedData.db_title[3],
                SharedData.db_title[4], SharedData.db_title[5] });
                objs.Add(new[] { 0, SharedData.db_PO[0], SharedData.db_PO[1], SharedData.db_PO[2], SharedData.db_PO[3],
                SharedData.db_PO[4], SharedData.db_PO[5] });
            }
            if (SharedData.db_PO.Count().Equals(7))
            {
                objs.Add(new[] { "", SharedData.db_title[0], SharedData.db_title[1], SharedData.db_title[2], SharedData.db_title[3],
                SharedData.db_title[4], SharedData.db_title[5], SharedData.db_title[6] });
                objs.Add(new[] { 0, SharedData.db_PO[0], SharedData.db_PO[1], SharedData.db_PO[2], SharedData.db_PO[3],
                SharedData.db_PO[4], SharedData.db_PO[5], SharedData.db_PO[6] });
            }
            if (SharedData.db_PO.Count().Equals(8))
            {
                objs.Add(new[] { "", SharedData.db_title[0], SharedData.db_title[1], SharedData.db_title[2], SharedData.db_title[3],
                SharedData.db_title[4], SharedData.db_title[5], SharedData.db_title[6], SharedData.db_title[7] });
                objs.Add(new[] { 0, SharedData.db_PO[0], SharedData.db_PO[1], SharedData.db_PO[2], SharedData.db_PO[3],
                SharedData.db_PO[4], SharedData.db_PO[5], SharedData.db_PO[6], SharedData.db_PO[7] });
            }
            if (SharedData.db_PO.Count().Equals(9))
            {
                objs.Add(new[] { "", SharedData.db_title[0], SharedData.db_title[1], SharedData.db_title[2], SharedData.db_title[3],
                SharedData.db_title[4], SharedData.db_title[5], SharedData.db_title[6], SharedData.db_title[7], SharedData.db_title[8] });
                objs.Add(new[] { 0, SharedData.db_PO[0], SharedData.db_PO[1], SharedData.db_PO[2], SharedData.db_PO[3],
                SharedData.db_PO[4], SharedData.db_PO[5], SharedData.db_PO[6], SharedData.db_PO[7], SharedData.db_PO[8] });
            }
            return objs;
        }

        /// <summary>
        /// Whats the percentage complete from the planned stories?
        /// </summary>
        /// <returns>Chart with an overview of the metric what is completed from planned.</returns>
        public static List<object> MultiLineDataCompletedFromPlanned()
        {
            List<object> objs = new List<object>();

            if (SharedData.db_Completed.Count().Equals(1))
            {
                objs.Add(new[] { "", SharedData.db_title[0] });
                objs.Add(new[] { 0, SharedData.db_Completed[0] });
            }
            if (SharedData.db_Completed.Count().Equals(2))
            {
                objs.Add(new[] { "", SharedData.db_title[0], SharedData.db_title[1] });
                objs.Add(new[] { 0, SharedData.db_Completed[0], SharedData.db_Completed[1] });
            }
            if (SharedData.db_Completed.Count().Equals(3))
            {
                objs.Add(new[] { "", SharedData.db_title[0], SharedData.db_title[1], SharedData.db_title[2] });
                objs.Add(new[] { 0, SharedData.db_Completed[0], SharedData.db_Completed[1], SharedData.db_Completed[2] });
            }
            if (SharedData.db_Completed.Count().Equals(4))
            {
                objs.Add(new[] { "", SharedData.db_title[0], SharedData.db_title[1], SharedData.db_title[2], SharedData.db_title[3] });
                objs.Add(new[] { 0, SharedData.db_Completed[0], SharedData.db_Completed[1], SharedData.db_Completed[2], SharedData.db_Completed[3] });
            }
            if (SharedData.db_Completed.Count().Equals(5))
            {
                objs.Add(new[] { "", SharedData.db_title[0], SharedData.db_title[1], SharedData.db_title[2], SharedData.db_title[3],
                SharedData.db_title[4] });
                objs.Add(new[] { 0, SharedData.db_Completed[0], SharedData.db_Completed[1], SharedData.db_Completed[2], SharedData.db_Completed[3],
                SharedData.db_Completed[4] });
            }
            if (SharedData.db_Completed.Count().Equals(6))
            {
                objs.Add(new[] { "", SharedData.db_title[0], SharedData.db_title[1], SharedData.db_title[2], SharedData.db_title[3],
                SharedData.db_title[4], SharedData.db_title[5] });
                objs.Add(new[] { 0, SharedData.db_Completed[0], SharedData.db_Completed[1], SharedData.db_Completed[2], SharedData.db_Completed[3],
                SharedData.db_Completed[4], SharedData.db_Completed[5] });
            }
            if (SharedData.db_Completed.Count().Equals(7))
            {
                objs.Add(new[] { "", SharedData.db_title[0], SharedData.db_title[1], SharedData.db_title[2], SharedData.db_title[3],
                SharedData.db_title[4], SharedData.db_title[5], SharedData.db_title[6] });
                objs.Add(new[] { 0, SharedData.db_Completed[0], SharedData.db_Completed[1], SharedData.db_Completed[2], SharedData.db_Completed[3],
                SharedData.db_Completed[4], SharedData.db_Completed[5], SharedData.db_Completed[6] });
            }
            if (SharedData.db_Completed.Count().Equals(8))
            {
                objs.Add(new[] { "", SharedData.db_title[0], SharedData.db_title[1], SharedData.db_title[2], SharedData.db_title[3],
                SharedData.db_title[4], SharedData.db_title[5], SharedData.db_title[6], SharedData.db_title[7] });
                objs.Add(new[] { 0, SharedData.db_Completed[0], SharedData.db_Completed[1], SharedData.db_Completed[2], SharedData.db_Completed[3],
                SharedData.db_Completed[4], SharedData.db_Completed[5], SharedData.db_Completed[6], SharedData.db_Completed[7] });
            }
            if (SharedData.db_Completed.Count().Equals(9))
            {
                objs.Add(new[] { "", SharedData.db_title[0], SharedData.db_title[1], SharedData.db_title[2], SharedData.db_title[3],
                SharedData.db_title[4], SharedData.db_title[5], SharedData.db_title[6], SharedData.db_title[7], SharedData.db_title[8] });
                objs.Add(new[] { 0, SharedData.db_Completed[0], SharedData.db_Completed[1], SharedData.db_Completed[2], SharedData.db_Completed[3],
                SharedData.db_Completed[4], SharedData.db_Completed[5], SharedData.db_Completed[6], SharedData.db_Completed[7], SharedData.db_Completed[8] });
            }
            return objs;
        }
    }
}
