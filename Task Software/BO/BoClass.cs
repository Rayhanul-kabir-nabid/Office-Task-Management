using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using ENTITY;
using System.Data;
using System.Threading;
using System.Collections;
using System.Windows.Threading;

namespace BO
{
    public class BoClass
    {

        public EntityClass entitylayer;
        public DalClass datalayer;
        public FTPServerConnection ftpsc;
        public string mainname = "";
        public List<string> alladmins=new List<string>();
        public Boolean isadmin;

        public List<SingleTask> TasksList = new List<SingleTask>();

        public Queue notifyqueue = new Queue();
        
        ///  flags........
        public Boolean cmp_member_load = false;
        public Boolean cmp_member_load_2 = false;
        public Boolean complete_file_load = false;

        public Boolean cmp_task_load = false;
        public Boolean cmp_notification_load = false;


        public List<Person> memberall = new List<Person>();

        public String temp;

        public int folder;
        // threed...........
        public DispatcherTimer bothreedtimer = new System.Windows.Threading.DispatcherTimer();
        Queue<Thread> threadqueue = new Queue<Thread>();
        Boolean isanythreadrunning = false;

        public BoClass(EntityClass e)
        {
            entitylayer = e;
            datalayer = new DalClass(e);
            ftpsc = new FTPServerConnection(e);


            bothreedtimer = new System.Windows.Threading.DispatcherTimer();
            bothreedtimer.Tick += new EventHandler(bothreedtimer_Tick);
            bothreedtimer.Interval = new TimeSpan(0, 0, 1);
            bothreedtimer.Start();
        }
        Thread tnow=new Thread(demomethod);
        private static void demomethod(){ }
        private void bothreedtimer_Tick(object sender, EventArgs e)
        {
            if(threadqueue.Count>0 && !tnow.IsAlive)
            {
                tnow = threadqueue.Peek();
                threadqueue.Dequeue();
                tnow.Start();
            }
        }

        public void setfolder()
        {
            int hd = TasksList.Count - 1;
            if (hd < 0)
            {
                folder = 0;
            }
            else
            {
                string f = TasksList[hd].id;

                folder = Int32.Parse(f);
            }
            

        }
        public string nextfolder()
        {
            folder++;
            return folder.ToString();
        }
        public string dbconnect()
        {
            return datalayer.dbconnect();
        }
        public string create_schema()
        {
            return datalayer.create_schema();
        }
        public string create_login_table()
        {
            return datalayer.create_login_table();
        }
        public string create_member_table()
        {
            return datalayer.create_member_table();
        }
       
        public void change_password(string username, string new_password1, string new_password2)
        {
            Thread ttask = new Thread(() => datalayer.change_password(username, new_password1, new_password2));
            threadqueue.Enqueue(ttask);
            
        }
        public string sign_in(string username, string password)
        {
            return datalayer.sign_in(username, password);
        }
        
        public void save_edit_member(string full_name, string designation, string contact, string email, string address, string room_no, string user)
        {
            Thread ttask = new Thread(() => datalayer.save_edit_member(full_name, designation, contact, email, address, room_no, user));
            threadqueue.Enqueue(ttask);
            
        }
        public void save_edit_profile(string full_name, string designation, string contact, string email, string address, string room_no, string user)
        {
            Thread ttask = new Thread(() => datalayer.save_edit_profile(full_name, designation, contact, email, address, room_no, user));
            threadqueue.Enqueue(ttask);
            
        }
        public void delete_member(string username)
        {
            Thread ttask = new Thread(() => datalayer.delete_member(username));
            threadqueue.Enqueue(ttask);
        }
        public void add_member(string name, string full_name, string designation, string phone, string email, string room, string address, string temp_password, string type)
        {
            Thread ttask = new Thread(() =>  datalayer.add_member(name, full_name, designation, phone, email, room, address, temp_password, type));
            threadqueue.Enqueue(ttask);
        }
        
        public void dummy_login()
        {
             datalayer.dummy_login();
        }

        /// <summary>
        /// customs................................
        /// </summary>
        /// <returns></returns>

        public void load_member_form_server()
        {
            Thread tmem = new Thread(loadmembertrd);
            threadqueue.Enqueue(tmem);
        }
        public void loadmembertrd()
        {
            memberall.Clear();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = datalayer.view_member();
            dt = ds.Tables["Member"];
            long count = dt.Rows.Count;


            alladmins.Clear();

            for (int i = 0; i < count; i++)
            {
                Person p = new Person(dt.Rows[i]["name"].ToString(),
                                      dt.Rows[i]["full_name"].ToString(),
                                      dt.Rows[i]["designation"].ToString(),
                                      dt.Rows[i]["contact_no"].ToString(),
                                      dt.Rows[i]["email"].ToString(),
                                      dt.Rows[i]["address"].ToString(),
                                      dt.Rows[i]["room_no"].ToString());

                p.type = dt.Rows[i]["type"].ToString();
                memberall.Add(p);

                if(p.type=="Admin")
                {
                    alladmins.Add(p.name);
                }

            }
            cmp_member_load = true;
            cmp_member_load_2 = true;
            //datalayer.notification_queue.Enqueue("member.....................");
        }

        public List<string> getmemberslist()
        {
            List<string> mlist = new List<string>();
            for (int i = 0; i < memberall.Count; i++)
            {
                mlist.Add(memberall[i].name);
            }
            return mlist;
        }
        public List<Person> getmemberslist_person()
        {

            return memberall;
        }

        public Person getpersoninfo(string nametofind)
        {
            for (int j = 0; j < memberall.Count; j++)
            {
                if (nametofind == memberall[j].name)
                {
                    return memberall[j];
                }
            }
            return null;
        }


        ///  notification..............
        List<string> allnotification = new List<string>();
        public string create_notification()
        {
            return datalayer.create_notification();
        }
        public void insert_notification(string description, string member, string time)
        {
            Thread ttask = new Thread(() => datalayer.insert_notification(description, member, time));
            threadqueue.Enqueue(ttask);
        }
        public void notification_delete(string id)
        {
            Thread ttask = new Thread(() => datalayer.notification_delete(id));
            threadqueue.Enqueue(ttask);
        }
        public void load_notifications()
        {
            Thread t = new Thread( getnotification_trd);
            threadqueue.Enqueue(t);
        }
        public List<string> getnotification()
        {
            return allnotification;
        }
        public void getnotification_trd()
        {

            List<string> notific = new List<string>();
            try
            {
                List<string> ids = new List<string>();
                DataSet ds = datalayer.notification_by_name(mainname);

                DataTable dt = new DataTable();
                dt = ds.Tables["notification"];
                int size = dt.Rows.Count;

                for (int i = size - 1; i >= 0; i--)
                {
                    string s = dt.Rows[i]["description"].ToString() + "   -    " + dt.Rows[i]["time"];
                    notific.Add(s);
                    string idd = dt.Rows[i]["id"].ToString();
                    notific.Add(idd);
                    
                }
                
            }
            catch (Exception)
            {

                
            }
            
            allnotification= notific;
            cmp_notification_load = true;
        }

        public Boolean unseen_notification_by_name()
        {
            return datalayer.unseen_notification_by_name(mainname);
        }

        public void send_notify_to_all_admins(string n, string t)
        {
            Thread tsn = new Thread(() => Send_notify_thread(n, t));
            threadqueue.Enqueue(tsn);
        }

        public void Send_notify_thread(string n, string t)
        {
            for(int i=0; i<alladmins.Count; i++)
            {
                datalayer.insert_notification(n, alladmins[i], t);
            }
        }
        ////// task...............................................................

        public string create_task_info()
        {
            return datalayer.create_task_info();
        }
    
        public void insert_task_info(string title, string description, string given_date, string submission_date, string assigned_members, string status, string folder)
        {

            Thread t=new Thread(()=> datalayer.insert_task_info(title, description, given_date, submission_date, assigned_members, status, folder));
            threadqueue.Enqueue(t);

        }
        

        public void load_task_form_server()
        {
            Thread ttask = new Thread(loadtasktrd);
            threadqueue.Enqueue(ttask);
        }
        public void load_files_form_server()
        {
            
        }
        public void loadtasktrd()
        {
            List<SingleTask> Tll = new List<SingleTask>();

            try
            {
                DataSet ds;
                ds = datalayer.tasks_info();
                DataTable dt = new DataTable();
                dt = ds.Tables["task_info"];
                int size = dt.Rows.Count;

                for (int i = 0; i < size; i++)
                {
                    SingleTask p = new SingleTask(dt.Rows[i]["id"].ToString(),
                                                  dt.Rows[i]["title"].ToString(),
                                                  dt.Rows[i]["description"].ToString(),
                                                  dt.Rows[i]["status"].ToString(),
                                                  dt.Rows[i]["given_date"].ToString(),
                                                  dt.Rows[i]["submission_date"].ToString(),
                                                  dt.Rows[i]["assign_to"].ToString(),
                                                  dt.Rows[i]["folder"].ToString());

                    //p.files = ftpsc.GetFilesListinDirectory(dt.Rows[i]["folder"].ToString());
                    p.assignto = p.to.Split(',').ToList();

                    if (isadmin)
                    {
                        Tll.Add(p);
                    }
                    else
                    {
                        for (int x = 0; x < p.assignto.Count; x++)
                        {
                            if (p.assignto[x] == mainname)
                            {
                                Tll.Add(p);
                                break;
                            }
                        }
                    }

                }
            }
            catch (Exception)
            {

                
            }
            TasksList = Tll;
            cmp_task_load = true;

            
            Thread tfile = new Thread(loadfile2);
            tfile.Start();



        }
        public void loadfile2()
        {
            int sz = TasksList.Count;
            for (int i = 0; i < sz; i++)
            {

                TasksList[i].files = ftpsc.GetFilesListinDirectory(TasksList[i].folder);
            }
            cmp_task_load = true;
            
        }
        public List<SingleTask> get_all_task_info()
        {

            return TasksList;
        }
        public SingleTask get_a_task_by_id(string aid)
        {
            for (int i = 0; i < TasksList.Count; i++)
            {
                if (TasksList[i].id == aid)
                {
                    return TasksList[i];
                }
            }
            return null;
        }
        public List<SingleTask> get_task_info_by_name(string m)
        {
            List<SingleTask> membertask = new List<SingleTask>();
            for (int i = 0; i < TasksList.Count; i++)
            {
                for (int j = 0; j < TasksList[i].assignto.Count; j++)
                {
                    if (TasksList[i].assignto[j] == m)
                    {
                        membertask.Add(TasksList[i]);
                    }
                }
            }
            return membertask;
        }
        public List<SingleTask> get_task_info_by_status(string s)
        {
            List<SingleTask> membertask = new List<SingleTask>();
            for (int i = 0; i < TasksList.Count; i++)
            {
                if (TasksList[i].status == s)
                {
                    membertask.Add(TasksList[i]);
                }
            }
            return membertask;
        }
        public void edit_task_info_date(string id, string lastdate)
        {
            Thread t = new Thread(() => datalayer.edit_task_info_date(Int32.Parse(id), lastdate));
            threadqueue.Enqueue(t);
        }
        public void edit_task_info_description(string id, string description)
        {
            Thread t = new Thread(() => datalayer.edit_task_info_description(Int32.Parse(id), description));
            threadqueue.Enqueue(t);
        }
        public void task_complete(string id, string status)
        {
            Thread t = new Thread(() => datalayer.task_complete(Int32.Parse(id), status));
            threadqueue.Enqueue(t);
        }
        public void task_delete(SingleTask id)
        {
            Thread t = new Thread(() => datalayer.task_delete(Int32.Parse(id.id)));
            threadqueue.Enqueue(t);

            Thread t2 = new Thread(() => ftpsc.deletefile(id.folder));
            threadqueue.Enqueue(t2);


        }


        public Boolean has_side_notification()
        {
            if (datalayer.notification_queue.Count > 0)
                return true;
            else
            {
                return false;
            }
        }
        public List<string> get_side_notifications()
        {
            List<string> s = new List<string>();
            while(datalayer.notification_queue.Count>0)
            {
                s.Add(datalayer.notification_queue.Peek());
                datalayer.notification_queue.Dequeue();
            }

            return s;

        }

    }
}
