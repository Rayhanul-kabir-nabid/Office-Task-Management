using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace Task_Software
{
    /// <summary>
    /// Interaction logic for Flag.xaml
    /// </summary>
    public partial class Flag : Window
    {
        public Flag()
        {
            InitializeComponent();


            


   System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

    config.AppSettings.Settings.Remove("LastDateFeesChecked");

   config.AppSettings.Settings.Add("LastDateFeesChecked", DateTime.Now.ToShortDateString());

   config.Save(ConfigurationSaveMode.Modified);

    ConfigurationManager.RefreshSection("appSettings");



            //var appSettings = ConfigurationSettings.AppSettings;
            //string done = appSettings["hasvalue"];
            //if(done=="NO")
            //{
            //    flggrid.Visibility = Visibility.Visible;
            //}
            //else
            //{

            //}
        }
        
        private void close_button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void bar_border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Savebutton_Click(object sender, RoutedEventArgs e)
        {
            string ip = ipnamebox.Text,
                   port = portnamebox.Text,
                   user = usernamebox.Text,
                   pass = passnamebox.Text,
                   data = databasenamebox.Text,
                   ftpserver = ftpnamebox.Text,
                   ftpuser = ftpusernamebox.Text,
                   ftppass = ftppassnamebox.Text;
           if(ip=="" || port=="" || user=="" || pass=="" || data=="" || ftpserver=="" || ftpuser=="" || ftppass=="")
           {
               MessageBox.Show("Fill Every Requirment!");
           }
           else
           {
               Signin s = new Signin();
               s.Show();
               this.Close();
           }
        }
    }
}
