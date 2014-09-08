using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
namespace Jibrary.Diagnostics
{
    public class TimeTask
    {
        private Stopwatch timer;

        public string Name { get; set; }
        public long Duration { get; private set; }

        public TimeTask()
        {
            timer = new Stopwatch();
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
            Duration = timer.ElapsedTicks;
        }

        public long GetMilliseconds()
        {
            return timer.ElapsedMilliseconds;
        }

        public override string ToString()
        {
            return timer.ElapsedTicks.ToString();
        }
    }
}
