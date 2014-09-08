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

        public virtual string Name { get; set; }
        public virtual long Duration { get; private set; }

        public TimeTask()
        {
            timer = new Stopwatch();
        }

        public virtual void Start()
        {
            timer.Start();
        }

        public virtual void Stop()
        {
            timer.Stop();
            Duration = timer.ElapsedTicks;
        }

        public virtual long GetMilliseconds()
        {
            return timer.ElapsedMilliseconds;
        }

        public override string ToString()
        {
            return timer.ElapsedTicks.ToString();
        }

    }
}
