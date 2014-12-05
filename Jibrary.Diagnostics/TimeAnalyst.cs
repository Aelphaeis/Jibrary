using System;
using System.Collections.Generic;
using System.Text;
namespace Jibrary.Diagnostics
{
    public class TimeAnalyst
    {
        public IEnumerable<TimeTask> Tasks
        {
            get { return tasks.Values; }
        }

        Dictionary<String, TimeTask> tasks;

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
            Int32 spacing = 25;
            String[] headers = new String[] { "Name", "Ticks", "Milliseconds" };
            StringBuilder output = new StringBuilder(base.ToString() + Environment.NewLine);
            String outputFormat = "{0, -" + spacing + "}{1, -" + spacing + "}{2, -" + spacing +"}{3}";
            output.AppendFormat(outputFormat, headers[0], headers[1], headers[2], Environment.NewLine);
            
            //output list
            foreach (TimeTask task in tasks.Values)
                output.AppendFormat(outputFormat, task.Name, task.Duration, task.GetMilliseconds(), Environment.NewLine);

            //return output 
            return output.ToString();
        }
    }
}
