////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Models\Metrics.cs
//
// summary:	Implements the metrics class
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NNMetrics.Data;

namespace NNMetrics.Models
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A metrics. </summary>
    ///
    /// <remarks>   Administrator, 12/06/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class Metrics
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the identifier. </summary>
        ///
        /// <value> The identifier. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int ID { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the name of the user. </summary>
        ///
        /// <value> The name of the user. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string userName { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the title. </summary>
        ///
        /// <value> The title. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string Title { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the measure date. </summary>
        ///
        /// <value> The measure date. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public DateTime MeasureDate { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the po satisfaction. </summary>
        ///
        /// <value> The po satisfaction. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int POSatisfaction { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the completed forecast. </summary>
        ///
        /// <value> The completed forecast. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int CompletedForecast { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the mttr. </summary>
        ///
        /// <value> The mttr. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int MTTR { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the number of deployments. </summary>
        ///
        /// <value> The total number of deployments. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int NumberOfDeployments { get; set; }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A model helper. </summary>
    ///
    /// <remarks>   Administrator, 12/06/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public static class ModelHelper
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Get data for the MTTR. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <returns>   Json object with MTTR data. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

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

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Multi line data po satisfaction. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <returns>   A List&lt;object&gt; </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

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

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Multi line data completed from planned. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <returns>   A List&lt;object&gt; </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

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

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Multi line data deployments. </summary>
        ///
        /// <remarks>   Administrator, 12/06/2018. </remarks>
        ///
        /// <returns>   A List&lt;object&gt; </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static List<object> MultiLineDataDeployments()
        {
            List<object> objs = new List<object>();

            if (SharedData.db_dp.Count().Equals(1))
            {
                objs.Add(new[] { "", SharedData.db_title[0] });
                objs.Add(new[] { 0, SharedData.db_dp[0] });
            }
            if (SharedData.db_dp.Count().Equals(2))
            {
                objs.Add(new[] { "", SharedData.db_title[0], SharedData.db_title[1] });
                objs.Add(new[] { 0, SharedData.db_dp[0], SharedData.db_dp[1] });
            }
            if (SharedData.db_dp.Count().Equals(3))
            {
                objs.Add(new[] { "", SharedData.db_title[0], SharedData.db_title[1], SharedData.db_title[2] });
                objs.Add(new[] { 0, SharedData.db_dp[0], SharedData.db_dp[1], SharedData.db_dp[2] });
            }
            if (SharedData.db_dp.Count().Equals(4))
            {
                objs.Add(new[] { "", SharedData.db_title[0], SharedData.db_title[1], SharedData.db_title[2], SharedData.db_title[3] });
                objs.Add(new[] { 0, SharedData.db_dp[0], SharedData.db_dp[1], SharedData.db_dp[2], SharedData.db_dp[3] });
            }
            if (SharedData.db_dp.Count().Equals(5))
            {
                objs.Add(new[] { "", SharedData.db_title[0], SharedData.db_title[1], SharedData.db_title[2], SharedData.db_title[3],
                SharedData.db_title[4] });
                objs.Add(new[] { 0, SharedData.db_dp[0], SharedData.db_dp[1], SharedData.db_dp[2], SharedData.db_dp[3],
                SharedData.db_dp[4] });
            }
            if (SharedData.db_dp.Count().Equals(6))
            {
                objs.Add(new[] { "", SharedData.db_title[0], SharedData.db_title[1], SharedData.db_title[2], SharedData.db_title[3],
                SharedData.db_title[4], SharedData.db_title[5] });
                objs.Add(new[] { 0, SharedData.db_dp[0], SharedData.db_dp[1], SharedData.db_dp[2], SharedData.db_dp[3],
                SharedData.db_dp[4], SharedData.db_dp[5] });
            }
            if (SharedData.db_dp.Count().Equals(7))
            {
                objs.Add(new[] { "", SharedData.db_title[0], SharedData.db_title[1], SharedData.db_title[2], SharedData.db_title[3],
                SharedData.db_title[4], SharedData.db_title[5], SharedData.db_title[6] });
                objs.Add(new[] { 0, SharedData.db_dp[0], SharedData.db_dp[1], SharedData.db_dp[2], SharedData.db_dp[3],
                SharedData.db_dp[4], SharedData.db_dp[5], SharedData.db_dp[6] });
            }
            if (SharedData.db_dp.Count().Equals(8))
            {
                objs.Add(new[] { "", SharedData.db_title[0], SharedData.db_title[1], SharedData.db_title[2], SharedData.db_title[3],
                SharedData.db_title[4], SharedData.db_title[5], SharedData.db_title[6], SharedData.db_title[7] });
                objs.Add(new[] { 0, SharedData.db_dp[0], SharedData.db_dp[1], SharedData.db_dp[2], SharedData.db_dp[3],
                SharedData.db_dp[4], SharedData.db_dp[5], SharedData.db_dp[6], SharedData.db_dp[7] });
            }
            if (SharedData.db_dp.Count().Equals(9))
            {
                objs.Add(new[] { "", SharedData.db_title[0], SharedData.db_title[1], SharedData.db_title[2], SharedData.db_title[3],
                SharedData.db_title[4], SharedData.db_title[5], SharedData.db_title[6], SharedData.db_title[7], SharedData.db_title[8] });
                objs.Add(new[] { 0, SharedData.db_dp[0], SharedData.db_dp[1], SharedData.db_dp[2], SharedData.db_dp[3],
                SharedData.db_dp[4], SharedData.db_dp[5], SharedData.db_dp[6], SharedData.db_dp[7], SharedData.db_dp[8] });
            }
            return objs;
        }
    }
}
