using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MSIAB2OSCAPP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 100000; i++)
            {
                MSIABVisitor afterburner = new MSIABVisitor();

                List<ABReportDataGroup> abReport = new List<ABReportDataGroup>(afterburner.GetReportArray());
                
                Console.WriteLine(i);
                DateTime time = new DateTime(1970,1,1, 0, 0, 0, DateTimeKind.Utc);
                time = time.AddSeconds(afterburner.GetDataTime());
                Console.WriteLine("UTC:" + time.ToString());
                foreach (ABReportDataGroup abReportDataGroup in abReport)
                {
                    Console.WriteLine(abReportDataGroup.dataName + abReportDataGroup.dataValue + abReportDataGroup.dataUnit);
                }
                Thread.Sleep(1000);
                Console.Clear();
            }
        }
    }
}
