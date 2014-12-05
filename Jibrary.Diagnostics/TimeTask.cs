using System.Diagnostics;

namespace Jibrary.Diagnostics
{
    public class TimeTask
    { 
        private Stopwatch timer;

        public virtual string Name { get; set; }
        public virtual long Duration { get { return timer.ElapsedTicks; } }
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
