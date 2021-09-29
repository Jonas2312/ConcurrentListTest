using Net5.PerformanceMeasurement;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net5
{
    static class ConcurrentBagTest
    {
        public static void Test1()
        {
            var ConcurrentList = new ConcurrentBag<string>();

            var Setup = new Action(() =>
            {

            });

            var TestFunc = new Action(() =>
            {
                var random = new Random();
                for(int i = 0; i < 999999; i++)
                {
                    int randomInt = random.Next(0, 100);
                    string s = "ABC";
                    if (randomInt < 25)
                    {
                        if (!(ConcurrentList.Count == 0))
                        {
                            var x = ConcurrentList.ElementAt(ConcurrentList.Count - 1);
                            x = string.Empty;
                        }
                    }
                    else if (randomInt < 75)
                        ConcurrentList.Add(s);
                    else if (randomInt < 95)
                    {
                        if (!(ConcurrentList.Count == 0))
                            ConcurrentList.TryTake(out _);
                    }
                    else if (randomInt < 96)
                    {
                        var y = ConcurrentList.Where(x => x.Equals("ABC"));
                    }
                    else if (randomInt < 98)
                    {
                        if (!(ConcurrentList.Count == 0))
                            ConcurrentList.Clear();
                    }
                    else if (randomInt < 100)
                        Console.WriteLine(ConcurrentList.Count);
                }

            });

            long ticks = 0;
            var TestFunc2 = new Action(() =>
            {
                DynamicProgrammingHelper.ExecuteParallelNTimes(TestFunc, 100);
            });
            PerformanceMeasurementHelper.ExecuteMethodAverageNExecutions(Setup, TestFunc2, 10, out ticks);

        }
    }
}
