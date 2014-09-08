using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jibrary.Diagnostics
{
    public class TimeAnalyst
    {
        public IEnumerable<TimeTask> Tasks
        {
            get
            {
                return tasks.Values;

            }
        }

        Dictionary<String, TimeTask> tasks;

        public TimeAnalyst()
        {
            tasks = new Dictionary<String, TimeTask>();
        }

        #region CRUD Operations
        //Create
        public void CreateTask(String n)
        {
            tasks.Add(n, new TimeTask() { Name = n });
        }

        //Read
        public TimeTask GetTask(String n)
        {
            return tasks[n];
        }

        //Update
        public void StartTask(String n)
        {
            if (!tasks.ContainsKey(n))
                CreateTask(n);
            tasks[n].Start();
        }

        public void StopTask(String n)
        {
            tasks[n].Stop();
        }

        //delete
        public void RemoveTask(String n)
        {
            tasks.Remove(n);
        }
        #endregion

        public override string ToString()
        {
            var output = new StringBuilder();
            var headers = new String[] { "Name", "Ticks", "Time (ms)" };
            int h1 = headers[0].Length, h2 = headers[1].Length, h3 = headers[2].Length, n = 25;
            output.AppendFormat("{0,-" + (n) + "}{1,-" + (n ) + "}{2,-" + (n ) + "}{3}", headers[0], headers[1], headers[2], Environment.NewLine);

            foreach(var task in tasks.Values)
            {
                h1 = task.Name.Length;
                h2 = task.Duration.ToString().Length;
                h3 = task.GetMilliseconds().ToString().Length;

                output.AppendFormat("{0," + (-n) + "}{1," + (-n) + "}{2," + (-n) + "}{3}",
                    task.Name, task.Duration, task.GetMilliseconds(), Environment.NewLine);
            }
            return output.ToString();
        }
    }
}
