using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class SingleTask
    {
        public string id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public string starting_date { get; set; }
        public string due_date { get; set; }
        public string to { get; set; }
        public string folder { get; set; }
        public List<string> files { get; set; }
        public List<string> assignto { get; set; }

        public Boolean loadedfiles = false;
        public SingleTask(string task_id, string task_title, string task_description, string task_status, string task_starting_date, string task_due_date, string task_member, string task_folder)
        {
            files = new List<string>();
            assignto = new List<string>();
            id = task_id;
            title = task_title;
            description = task_description;
            status = task_status;
            starting_date = task_starting_date;
            due_date = task_due_date;
            to = task_member;
            folder = task_folder;
        }
    }
}
