using System.Diagnostics;
namespace Orasi.Toolkit.Utils
{
    /// <summary>
    /// Class to use inplace of Thread.sleep()
    /// </summary>
    class Sleeper
    {
        private static Stopwatch stopwatch = new Stopwatch();

        /// <summary>
        /// Use a thread safe sleep method using the Stopwatch class</summary>
        /// <param name="millis">Number of milliseconds to pause. 3000 will cause a wait for 3 seconds</param>
        public static void sleep(long millis)
        {
            stopwatch.Start();
            do { } while (stopwatch.ElapsedMilliseconds < millis);
            stopwatch.Stop();
            stopwatch.Reset();
        }
    }
}
