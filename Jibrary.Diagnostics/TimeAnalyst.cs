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

        public void SaveResults(Stream stream)
        {
            using (var Writer = new StreamWriter(stream))
                foreach (var v in Tasks)
                    Writer.WriteLine(v.Name + " : " + v.Duration + " Ticks | " + v.GetMilliseconds() + " Milliseconds ");
        }

        public void SaveResults(TextWriter writer)
        {
            foreach (var v in Tasks)
                writer.WriteLine(v.Name + " : " + v.Duration + " Ticks | " + v.GetMilliseconds() + " Milliseconds ");
        }

        public override string ToString()
        {
            //init
            StringBuilder output = new StringBuilder(base.ToString());
            output.AppendFormat("{0, -20} {1, -20} {2, -20}  {3}", "Name", "Ticks", "Milliseconds", Environment.NewLine);

            //output list
            foreach (TimeTask task in tasks.Values)
                output.AppendFormat("{0, -20} {1, -20} {2, -20}  {3}", task.Name, task.Duration, task.GetMilliseconds(), Environment.NewLine);
            return output.ToString();
        }
    }
}
