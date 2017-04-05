using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BO;
using System.Data;
using ENTITY;
using System.Threading;
using System.Windows.Threading;
using Task_Software.SideWindowCollection;
namespace Task_Software.SideControl
{
    /// <summary>
    /// Interaction logic for Task.xaml
    /// </summary>
    public partial class Task : UserControl
    {

        public List<SingleTask> alltasks { get; set; }

        BoClass bolayer;
        BOftp ftp;
        public string currentTaskid;
        WaitWindow wait = new WaitWindow();

        public DispatcherTimer tasktimer;

        SingleTask tasknow=null;

        public Task(BoClass b)
        {
            InitializeComponent();
            alltasks = new List<SingleTask>();
            bolayer = b;
            ftp = new BOftp(bolayer.entitylayer);

            if(bolayer.isadmin)
            {
                membercombo.Visibility = Visibility.Visible;
                membercombo.Items.Add("All Members");
                List<string> lst = bolayer.getmemberslist();
                for (int i = 0; i < lst.Count; i++)
                {
                    membercombo.Items.Add(lst[i]);
                }
                membercombo.SelectedIndex = 0;

                editdatebutton.Visibility = Visibility.Visible;
                editdesbutton.Visibility = Visibility.Visible;
            }
            stutuscombo.Items.Add("All Tasks");
            stutuscombo.Items.Add("Pending Tasks");
            stutuscombo.Items.Add("Done Tasks");
            stutuscombo.SelectedIndex = 0;
            
            alltasks = bolayer.get_all_task_info();
            taskviewgrid.ItemsSource = alltasks;

            if(!bolayer.isadmin)
            {
                deleteButton.Visibility = Visibility.Hidden;
            }

            tasktimer = new System.Windows.Threading.DispatcherTimer();
            tasktimer.Tick += new EventHandler(task_timer_loop);
            tasktimer.Interval = new TimeSpan(0, 0, 1);
            tasktimer.Start();

        }

        private void task_timer_loop(object sender, EventArgs e)
        {
            try
            {
                if (bolayer.cmp_task_load)
                {

                    alltasks = bolayer.get_all_task_info();
                    taskviewgrid.ItemsSource = alltasks;

                    bolayer.cmp_task_load = false;
                }
                if (bolayer.complete_file_load)
                {

                    alltasks = bolayer.get_all_task_info();
                    taskviewgrid.ItemsSource = alltasks;

                    bolayer.complete_file_load = false;
                }
            }
            catch (Exception)
            {

                tasktimer = new System.Windows.Threading.DispatcherTimer();
                tasktimer.Tick += new EventHandler(task_timer_loop);
                tasktimer.Interval = new TimeSpan(0, 0, 1);
                tasktimer.Start();
            }
        }

        private void taskviewgrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (taskviewgrid.SelectedIndex < 0)
            {
                description_window.Visibility = Visibility.Hidden;
                currentTaskid = null;

                return;

            }
            description_window.Visibility = Visibility.Visible;
            object item = taskviewgrid.SelectedItem;
            string id = (taskviewgrid.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text;
            currentTaskid = id;
            tasknow = bolayer.get_a_task_by_id(id);

            if(bolayer.isadmin)
            {
                donetaskbutton.Visibility = Visibility.Hidden;
                for (int i = 0; i < tasknow.assignto.Count; i++)
                {
                    if (bolayer.mainname == tasknow.assignto[i])
                    {
                        donetaskbutton.Visibility = Visibility.Visible;
                        break;
                    }
                }
            }
            if(tasknow.status=="Done")
            {
                donetaskbutton.Content = "Pending";
            }
            else donetaskbutton.Content = "Done";
        }
        //side looks
        
        //end...........

        // down combos.......
        private void stutuscombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(stutuscombo.SelectedIndex==0)
            {
                alltasks = bolayer.get_all_task_info();
                taskviewgrid.ItemsSource = alltasks;
            }
            else if(stutuscombo.SelectedIndex ==1)
            {
                alltasks = bolayer.get_task_info_by_status("Pending");
                taskviewgrid.ItemsSource = alltasks;
            }
            else
            {
                alltasks = bolayer.get_task_info_by_status("Done");
                taskviewgrid.ItemsSource = alltasks;
            }
            
        }
        private void membercombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (membercombo.SelectedIndex == 0)
            {
                alltasks = bolayer.get_all_task_info();
                taskviewgrid.ItemsSource = alltasks;
            }
            else
            {
                string filtername = membercombo.SelectedItem.ToString();

                alltasks = bolayer.get_task_info_by_name(filtername);
                taskviewgrid.ItemsSource = alltasks;    
            }
            
        }

        private void editdatebutton_Click(object sender, RoutedEventArgs e)
        {
            EditDate ed = new EditDate(bolayer, currentTaskid);
            ed.ShowDialog();

            bolayer.load_task_form_server();
        }

        private void donetaskbutton_Click(object sender, RoutedEventArgs e)
        {
            object item = taskviewgrid.SelectedItem;
            int i = Int32.Parse(currentTaskid);

            string s = (taskviewgrid.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text;

            string dat = DateTime.Now.ToString();

            if (tasknow.status=="Done")
            {
                bolayer.task_complete(currentTaskid, "Pending");
                string nn = bolayer.getpersoninfo(bolayer.mainname).full_name + " has updated the task " + tasknow.title + " status to pending.";
                bolayer.send_notify_to_all_admins(nn,  dat);
                if(tasknow.assignto.Count>1)
                {
                    for(int x=0; x<tasknow.assignto.Count; x++)
                    {
                        if (tasknow.assignto[x] != bolayer.mainname)
                            bolayer.insert_notification(nn, tasknow.assignto[x], dat);
                    }
                }
                
            }
            else
            {
                bolayer.task_complete(currentTaskid, "Done");

                string nn= bolayer.getpersoninfo(bolayer.mainname).full_name + " has updated the task " + tasknow.title + " status to done.";
                bolayer.send_notify_to_all_admins(nn,  dat);
                if (tasknow.assignto.Count > 1)
                {
                    for (int x = 0; x < tasknow.assignto.Count; x++)
                    {
                        if (tasknow.assignto[x] != bolayer.mainname)
                            bolayer.insert_notification(nn, tasknow.assignto[x],dat);
                    }
                }
            }
            //MainWindow.queueNotify.Enqueue("Task status has updated.");
            bolayer.load_task_form_server();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            YesNoWindow yy = new YesNoWindow("Do you want to delete this task ?");
            yy.ShowDialog();
            if(yy.ans==true)
            {
                
                bolayer.task_delete( tasknow);
                ftp.deletefile(tasknow.folder);
                bolayer.load_task_form_server();

            }
        }

        string selectedfilename;
        
       
        
        
        
        private void downloadbutton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            string path = dialog.SelectedPath;
            Thread t1 = new Thread(() => downloadthread(path));
            t1.Start();
        }
        public void downloadthread(string path)
        {


            if (ftp.DownloadFileFTP(path, tasknow.folder, selectedfilename))
            {
                MainWindow.queueNotify.Enqueue("'" + selectedfilename + "' for '" + bolayer.get_a_task_by_id(currentTaskid).title + "' has been downloaded successfully!");
            }
            else
            {
                MainWindow.queueNotify.Enqueue("Download Failed");
            }
        }
        private void filesListbox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (filesListbox.SelectedIndex < 0)
            {
                downloadbutton.Visibility = Visibility.Hidden;
                return;
            }
            downloadbutton.Visibility = Visibility.Visible;
            selectedfilename = filesListbox.SelectedItem.ToString();
        }
        private void downloadallbutton_Click_1(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            string path = dialog.SelectedPath;

            Thread t2 = new Thread(() => downloadallthread(path));
            t2.Start();
        }
        public void downloadallthread(string path)
        {

            if (ftp.DownloadFileFTP_All(path, tasknow.folder))
            {

                MainWindow.queueNotify.Enqueue("All files for '" + bolayer.get_a_task_by_id(currentTaskid).title + "' has been downloaded successfully!");
            }
            else
            {
                MainWindow.queueNotify.Enqueue("Download Failed");
            }
        }

        private void editdesbutton_Click(object sender, RoutedEventArgs e)
        {
            EditDescription edes = new EditDescription(bolayer, currentTaskid, tasknow.description);
            edes.ShowDialog();
            bolayer.load_task_form_server();
        }

        private void close_button_Click(object sender, RoutedEventArgs e)
        {
            description_window.Visibility = Visibility.Hidden;
        }
    }
}

