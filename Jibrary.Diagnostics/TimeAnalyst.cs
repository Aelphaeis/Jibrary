using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace Jibrary.Diagnostics
{
    public class TimeAnalyst
    {
        Dictionary<String, TimeTask> tasks;

        public IEnumerable<TimeTask> Tasks
        {
            get { return tasks.Values; }
        }

        public TimeAnalyst()
        {
            tasks = new Dictionary<String, TimeTask>();
        }

        #region CRUD Operations
        //Create
        public void CreateTask(String name)
        {
            tasks.Add(name, new TimeTask() { Name = name });
        }

        //Read
        public TimeTask GetTask(String name)
        {
            return tasks[name];
        }

        //Update
        public void StartTask(String name)
        {
            if (!tasks.ContainsKey(name))
                CreateTask(name);
            tasks[name].Start();
        }

        public void StopTask(String name)
        {
            tasks[name].Stop();
        }

        //delete
        public void RemoveTask(String name)
        {
            tasks.Remove(name);
        }
        #endregion

        public override string ToString()
        {
            //init
            String[] headers = new String[] { "Name", "Ticks", "Milliseconds" };
            StringBuilder output = new StringBuilder(base.ToString() + Environment.NewLine);
            output.AppendFormat("{0, -" + (25 - headers[0].Length) + "}{1, -" + (25 - headers[1].Length) + "}{2, -" + (25 - headers[2].Length)+"}{3}", headers[0], headers[1], headers[2], Environment.NewLine);
            //output list
            foreach (TimeTask task in tasks.Values)
                output.AppendFormat("{0, -" + (25 - task.Name.Length) + "}{1, -" + (25 - task.Duration.ToString().Length) + "}{2, -" + (25 - task.GetMilliseconds().ToString().Length) + "}{3}", 
                    task.Name, 
                    task.Duration, 
                    task.GetMilliseconds(), 
                    Environment.NewLine);

            //return output
            return output.ToString();
        }
    }
}
