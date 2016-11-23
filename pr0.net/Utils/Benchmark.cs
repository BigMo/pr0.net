using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace pr0.net.Utils
{
    public static class Benchmark
    {
        public static double Action(Action ac)
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();
            ac?.Invoke();
            sw.Stop();

            return sw.Elapsed.TotalMilliseconds;
        }
    }
}
