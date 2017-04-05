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
using System.Windows.Shapes;
using BO;
using System.Windows.Threading;
using System.Configuration;
using ENTITY;
using System.Media;
using System.Reflection;
using System.IO;

namespace Task_Software
{
    public partial class Signin : Window
    {
        BoClass bolayer;
        public DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        string message = "";

        public Signin()
        {
            InitializeComponent();

            var appSettings = ConfigurationSettings.AppSettings;
            string ip = appSettings["Ip"];
            string port = appSettings["Port"];
            string username = appSettings["Username"];
            string password = appSettings["Password"];
            string databaseName = appSettings["DatabaseName"];

            string ftpusername = appSettings["Ftpusername"];
            string ftppassword = appSettings["Ftppassword"];
            string ftpserver = appSettings["Ftpserver"];

            EntityClass entitylayer = new EntityClass(ip, port, username, password, databaseName, "login", "member", "task_info", ftpusername, ftppassword, ftpserver);


            


            bolayer = new BoClass(entitylayer);
            //bolayer.create_schema();
            bolayer.create_member_table();
            bolayer.create_login_table();
            bolayer.create_task_info();
            MessageBox.Show(bolayer.create_task_info());
            bolayer.create_notification();
            bolayer.dummy_login();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            
            

            mediaElement.Play();

        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {

            if (message != "")
            {
                gotohome();
                dispatcherTimer.Stop();
            }
        }

        private void close_button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Signinbutton_Click(object sender, RoutedEventArgs e)
        {
            message = "";
            message = bolayer.sign_in(usernamebox.Text, passwordbox.Password.ToString());


            //
            dispatcherTimer.Start();

        }

        private void bar_border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        public void gotohome()
        {

            if (message == "Admin")
            {
                bolayer.isadmin = true;
                bolayer.mainname = usernamebox.Text;
                

                MainWindow home = new MainWindow(bolayer);
                
                home.Show();
                this.Close();
            }
            else if (message == "Member")
            {
                bolayer.isadmin = false;
                bolayer.mainname = usernamebox.Text;

                MainWindow home = new MainWindow(bolayer);
                mediaElement.Play();
                home.Show();
                this.Close();
            }
            else if (usernamebox.Text == "" || passwordbox.Password.ToString() == "")
            {
                MessageBox.Show("Fill Every requirement");
            }
            else
            {
                MessageBox.Show(message);
            }
        }
    }
}
