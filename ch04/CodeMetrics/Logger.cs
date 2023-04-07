using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeMetricsBadCode
{
    class Logger
    {
        internal static void GenerateLog(Exception err)
        {
            Console.WriteLine(err.ToString());
        }
    }
}
