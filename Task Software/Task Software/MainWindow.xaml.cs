using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BO;
using Task_Software.SideControl;
using Task_Software.Menus;
using System.Windows.Threading;
using System.Collections;
using System.Drawing;
using System.IO;

namespace Task_Software
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MyNotifiactions notifywindow = new MyNotifiactions();
        

        Home home;
        Notify notify;
        NewTask newtask;
        Task_Software.SideControl.Task task;
        MemberView member;
        AddMember addmbr;
        ProfileView profile;

        BoClass bolayer;

        DispatcherTimer dispatcherTimer;
        public static Queue<string> queueNotify = new Queue<string>();

        public MainWindow(BoClass b)
        {
            InitializeComponent();
            bolayer = b;
            //notify = new Notify(b);
            //newtask = new NewTask(b);
            //task = new SideControl.Task(b);
            //member = new MemberView(b);
            //addmbr = new AddMember(b);
            //profile = new ProfileView(b);
            set_notify_icon();
            bolayer.load_notifications();
            notify = new Notify(bolayer);
            
            bolayer.load_task_form_server();
            bolayer.load_member_form_server();
            bolayer.load_notifications();
            bolayer.load_files_form_server();

            

            create_Home_Window();
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            

            mediaElement_new.Play();
        }

        

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (bolayer.has_side_notification())
                {
                    List<string> sl = bolayer.get_side_notifications();
                    for (int i = 0; i < sl.Count; i++)
                    {
                        queueNotify.Enqueue(sl[i]);
                    }
                }
                while (queueNotify.Count > 0)
                {
                    notifywindow.AddNotification(new Notification { Message = queueNotify.Peek() });
                    queueNotify.Dequeue();
                }
            }
            catch (Exception)
            {

                dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                dispatcherTimer.Start();
            }
        }
        private void close_button_Click(object sender, RoutedEventArgs e)
        {
            before_side_window(0);
            //this.Visibility = Visibility.Hidden;
            
            this.WindowState = WindowState.Minimized;
            this.ShowInTaskbar = false;
        }
        private void bar_border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        /// <summary>
        /// ...................Home Methods....................
        /// </summary>
        public void create_Home_Window()
        {
            home = new Home();
            //notify.................
            notification_button notifybutton = new notification_button();
            notifybutton.PreviewMouseLeftButtonDown += notificationbutton_Click;
            home.Mainstackpanel.Children.Add(notifybutton);

            //assign a task.................
            if (bolayer.isadmin)
            {

                assign_task_button assigntask = new assign_task_button();
                assigntask.PreviewMouseLeftButtonDown += NewTaskbutton_Click;
                home.Mainstackpanel.Children.Add(assigntask);
            }

            //tasks.................
            tasks_button task = new tasks_button();
            task.PreviewMouseLeftButtonDown += taskbutton_Click;
            home.Mainstackpanel.Children.Add(task);


            if (bolayer.isadmin)
            {
                //view member...........................
                Members_view_button memberview = new Members_view_button();
                memberview.PreviewMouseLeftButtonDown += membersbutton_Click;
                home.Mainstackpanel.Children.Add(memberview);

                //add member button...............................
                add_member_button addmember = new add_member_button();
                addmember.PreviewMouseLeftButtonDown += addmemberbutton_Click;
                home.Mainstackpanel.Children.Add(addmember);


            }
            else
            {

                home.Width = 324;
            }
            //profile........................
            profile_view_button profile = new profile_view_button();
            profile.PreviewMouseLeftButtonDown += profilrbutton_Click;
            home.Mainstackpanel.Children.Add(profile);


            before_side_window(0);
            home.close_button.Click += close_button_Click;
            home.bar_border.MouseDown += bar_border_MouseDown;
            home.min_button.Click += Min_button_Click;
            BigStackpanel.Children.Add(home);
        }

        private void Min_button_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void before_side_window(int n)
        {
            
            while (BigStackpanel.Children.Count > n)
            {
                BigStackpanel.Children.RemoveAt(BigStackpanel.Children.Count - 1);
            }
            
        }
        private void notificationbutton_Click(object sender, MouseButtonEventArgs e)
        {
            before_side_window(1);

            
            notify.close_button.Click += notify_close_button_Click;
            BigStackpanel.Children.Add(notify);
        }
        private void notify_close_button_Click(object sender, RoutedEventArgs e)
        {
            before_side_window(1);
        }
        //=========================================================================================================================
        private void NewTaskbutton_Click(object sender, MouseButtonEventArgs e)
        {
            before_side_window(1);
            newtask_window_create();
        }
        private void newtask_window_create()
        {
            newtask = new NewTask(bolayer);
            newtask.close_button.Click += notify_close_button_Click;

            newtask.sendbutton.Click += sendbutton_Click;


            BigStackpanel.Children.Add(newtask);
        }
        private void sendbutton_Click(object sender, RoutedEventArgs e)
        {
            string title, description, givendate, duedate, assignedmember, status;
            title = newtask.titlebox.Text;
            description = newtask.descriptionbox.Text;
            DateTime now = DateTime.Now;
            givendate = now.ToString();
            duedate = newtask.duedatebox.Text;
            
            if (title == "" || description == "" || duedate == "" || newtask.selectedmemberslist.Items.Count == 0)
            {
                MessageBox.Show("Fill every requirment");
            }
            else
            {
                int cc = 0;
                if (newtask.groupradioButton.IsChecked == true) cc = 1;


                newtask.settaskinfo(cc, title, description, givendate, duedate);

            }
            before_side_window(1);

        }
        private void taskbutton_Click(object sender, MouseButtonEventArgs e)
        {
            before_side_window(1);
            task = new Task_Software.SideControl.Task(bolayer);
            task.close_button.Click += notify_close_button_Click;
            BigStackpanel.Children.Add(task);

        }
        private void membersbutton_Click(object sender, MouseButtonEventArgs e)
        {
            before_side_window(1);
            member = new MemberView(bolayer);
            member.close_button.Click += notify_close_button_Click;
            BigStackpanel.Children.Add(member);
        }
        private void addmemberbutton_Click(object sender, MouseButtonEventArgs e)
        {
            before_side_window(1);
            addmbr = new AddMember(bolayer);
            addmbr.close_button.Click += notify_close_button_Click;
            BigStackpanel.Children.Add(addmbr);
        }
        private void profilrbutton_Click(object sender, MouseButtonEventArgs e)
        {
            before_side_window(1);
            profile = new ProfileView(bolayer);
            profile.close_button.Click += notify_close_button_Click;
            BigStackpanel.Children.Add(profile);
        }
        private void Window_Closed(object sender, EventArgs e)
        {

            notifywindow.Close();
            dispatcherTimer.Stop();
            
        }

        ///....................................................................
        // notifyicon
        System.Windows.Forms.ContextMenu contextMenu ;
        System.ComponentModel.IContainer components;
        System.Windows.Forms.NotifyIcon notifyIcon ;
        public void set_notify_icon()
        {
            
            contextMenu = new System.Windows.Forms.ContextMenu();
            components = new System.ComponentModel.Container();
            int idx = 0;
             notifyIcon = new System.Windows.Forms.NotifyIcon(components);
             /// task.......................................
             System.Windows.Forms.MenuItem taskmenu = new System.Windows.Forms.MenuItem();
             contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { taskmenu });
            taskmenu.Index = idx++;
             taskmenu.Text = "Tasks";
             taskmenu.Click += new System.EventHandler(this.Notify_taskmenu_Click);
             /////////////.............................................................

             /// assign task.......................................
             if (bolayer.isadmin)
             {
                 System.Windows.Forms.MenuItem assign = new System.Windows.Forms.MenuItem();
                 contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { assign });
                 assign.Index = idx++;
                 assign.Text = "Asign a task";
                 assign.Click += new System.EventHandler(this.Notify_assigntask_Click); 
             }
             /////////////.............................................................

             if (bolayer.isadmin)
             {
                 /// member.......................................
                 System.Windows.Forms.MenuItem membermenu = new System.Windows.Forms.MenuItem();
                 contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { membermenu });
                 membermenu.Index = idx++;
                 membermenu.Text = "Members";
                 membermenu.Click += new System.EventHandler(this.Notify_membermenu_Click);
                 /////////////.............................................................

                 /// add member.......................................
                 System.Windows.Forms.MenuItem addmembermenu = new System.Windows.Forms.MenuItem();
                 contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { addmembermenu });
                 addmembermenu.Index = idx++;
                 addmembermenu.Text = "Add Members";
                 addmembermenu.Click += new System.EventHandler(this.Notify_addmembermenu_Click);
                 /////////////............................................................. 
             }

             /// Profile.......................................
             System.Windows.Forms.MenuItem Profilemenu = new System.Windows.Forms.MenuItem();
             contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { Profilemenu });
             Profilemenu.Index = idx++;
             Profilemenu.Text = "&Profile";
             Profilemenu.Click += new System.EventHandler(this.Notify_Profile_Click);
             /////////////.............................................................
             /// Profile.......................................
             System.Windows.Forms.MenuItem notifymenu = new System.Windows.Forms.MenuItem();
             contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { notifymenu });
             notifymenu.Index = idx++;
             notifymenu.Text = "&Notification";
             notifymenu.Click += new System.EventHandler(this.Notify_notify_Click);
             /////////////.............................................................

             /// Exit menu.......................................
             

             
             /////////////.............................................................

             /// Exit menu.......................................
             System.Windows.Forms.MenuItem exitmenu = new System.Windows.Forms.MenuItem();
             contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { exitmenu });
             exitmenu.Index = idx++;
             exitmenu.Text = "&Exit";
             exitmenu.Click += new System.EventHandler(this.Notify_exitmenu_Click);
            /////////////.............................................................


            
            Stream iconStream = Application.GetResourceStream(new Uri("pack://application:,,,/Icon.ico")).Stream;
            notifyIcon.Icon = new System.Drawing.Icon(iconStream);

            // contex to notify
            notifyIcon.ContextMenu = contextMenu;

             notifyIcon.Text = "Task";
             notifyIcon.Visible = true;
             notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            
        }

        private void Notify_notify_Click(object sender, EventArgs e)
        {
            before_side_window(0);
            notify.close_button.Click += windo_by_icon_close_button_Click;
            notify.bar_border.MouseDown += bar_border_MouseDown;
            BigStackpanel.Children.Add(notify);

            this.WindowState = WindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void Notify_Profile_Click(object sender, EventArgs e)
        {
            before_side_window(0);
            profile = new ProfileView(bolayer);
            profile.close_button.Click += windo_by_icon_close_button_Click;
            profile.bar_border.MouseDown += bar_border_MouseDown;
            BigStackpanel.Children.Add(profile);

            this.WindowState = WindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void windo_by_icon_close_button_Click(object sender, RoutedEventArgs e)
        {
            before_side_window(0);
            this.WindowState = WindowState.Minimized;
            this.ShowInTaskbar = false;
        }
        private void Notify_addmembermenu_Click(object sender, EventArgs e)
        {
            before_side_window(0);
            addmbr = new AddMember(bolayer);
            addmbr.close_button.Click += windo_by_icon_close_button_Click;
            addmbr.bar_border.MouseDown += bar_border_MouseDown;
            BigStackpanel.Children.Add(addmbr);

            this.WindowState = WindowState.Normal;
            this.ShowInTaskbar = true;
        }
        private void Notify_membermenu_Click(object sender, EventArgs e)
        {
            before_side_window(0);
            member = new MemberView(bolayer);
            member.close_button.Click += windo_by_icon_close_button_Click;
            member.bar_border.MouseDown += bar_border_MouseDown;
            BigStackpanel.Children.Add(member);

            this.WindowState = WindowState.Normal;
            this.ShowInTaskbar = true;
        }
        private void Notify_taskmenu_Click(object sender, EventArgs e)
        {
            before_side_window(0);
            task = new Task_Software.SideControl.Task(bolayer);
            task.close_button.Click += windo_by_icon_close_button_Click;
            task.bar_border.MouseDown += bar_border_MouseDown;
            BigStackpanel.Children.Add(task);

            this.WindowState = WindowState.Normal;
            this.ShowInTaskbar = true;
        }
        private void Notify_assigntask_Click(object sender, EventArgs e)
        {
            before_side_window(0);
            newtask = new NewTask(bolayer);
            newtask.close_button.Click += windo_by_icon_close_button_Click;

            newtask.sendbutton.Click += sendbutton_Click;

            newtask.bar_border.MouseDown += bar_border_MouseDown;
            BigStackpanel.Children.Add(newtask);

            this.WindowState = WindowState.Normal;
            this.ShowInTaskbar = true;
        }
        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {

            create_Home_Window();

            this.WindowState = WindowState.Normal;
            this.ShowInTaskbar = true;

        }
        private void Notify_exitmenu_Click(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }



        public static void playsounds(int i)
        {
            if(i==1)
            {
                
            }
        }
    }
}
