using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNMetrics.Data
{
    public static class SharedData
    {
        public static ApplicationDbContext _contextGlobal;
        public static string userName = "";
        public static List<string> db_title = new List<string>();
        public static List<int> db_mttr = new List<int>();
        public static List<int> db_PO = new List<int>();
        public static List<int> db_Completed = new List<int>();
        public static int MetricsRecordCount = 0;
    }
}
