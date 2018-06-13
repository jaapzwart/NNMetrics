﻿////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Data\SharedData.cs
//
// summary:	Implements the shared data class
////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;


////////////////////////////////////////////////////////////////////////////////////////////////////
// namespace: NNMetrics.Data
//
// summary:	.
////////////////////////////////////////////////////////////////////////////////////////////////////

namespace NNMetrics.Data
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A shared data. </summary>
    ///
    /// <remarks>   Administrator, 12/06/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public static class SharedData
    {
        /// <summary>   The context global. </summary>
        public static ApplicationDbContext _contextGlobal;
        /// <summary>   Name of the user. </summary>
        public static string userName = "";
        /// <summary>   The database title. </summary>
        public static List<string> db_title = new List<string>();
        /// <summary>   The database mttr. </summary>
        public static List<int> db_mttr = new List<int>();
        /// <summary>   The database po. </summary>
        public static List<int> db_PO = new List<int>();
        /// <summary>   The database completed. </summary>
        public static List<int> db_Completed = new List<int>();
        /// <summary>   The database dp. </summary>
        public static List<int> db_dp = new List<int>();
        /// <summary>   Number of metrics records. </summary>
        public static int MetricsRecordCount = 0;
    }
}