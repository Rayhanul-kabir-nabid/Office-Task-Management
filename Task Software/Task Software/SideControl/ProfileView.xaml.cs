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

namespace Task_Software.SideControl
{
    /// <summary>
    /// Interaction logic for ProfileView.xaml
    /// </summary>
    public partial class ProfileView : UserControl
    {
        BoClass bolayer;
        Person properson;
        private DispatcherTimer profileTimer;

        public ProfileView(BoClass bo)
        {
            InitializeComponent();
            bolayer = bo;

            try
            {
                properson = bolayer.getpersoninfo(bolayer.mainname);

                namebox.Text = properson.name;
                desigbox.Text = properson.designation;
                emailbox.Text = properson.email;
                phonebox.Text = properson.contact_no;
                homebox.Text = properson.adress;
                roombox.Text = properson.room_no;
            }
            catch (Exception)
            {

                
            }


            profileTimer = new System.Windows.Threading.DispatcherTimer();
            profileTimer.Tick += new EventHandler(profileTimer_Tick);
            profileTimer.Interval = new TimeSpan(0, 0, 1);
            profileTimer.Start();
        }

        private void profileTimer_Tick(object sender, EventArgs e)
        {
            if(bolayer.cmp_member_load_2)
            {
                bolayer.cmp_member_load_2 = false;
                properson = bolayer.getpersoninfo(bolayer.mainname);
            
                namebox.Text = properson.name;
                desigbox.Text = properson.designation;
                emailbox.Text = properson.email;
                phonebox.Text = properson.contact_no;
                homebox.Text = properson.adress;
                roombox.Text = properson.room_no;

                profileTimer.Stop();
            
            }
        }
        

        private void Editbutton_Click(object sender, RoutedEventArgs e)
        {


            savebutton.Visibility = Visibility.Visible;
            canclebutton.Visibility = Visibility.Visible;
            Editbutton.Visibility = Visibility.Hidden;
            passbutton.Visibility = Visibility.Hidden;

            desigbox.Visibility = Visibility.Hidden;
            desigcombo.Visibility = Visibility.Visible;

            namebox.IsEnabled = true;
            emailbox.IsEnabled = true;
            phonebox.IsEnabled = true;
            homebox.IsEnabled = true;
            roombox.IsEnabled = true;
        }

        private void savebutton_Click(object sender, RoutedEventArgs e)
        {
            
            if (namebox.Text == "" || desigcombo.Text == "" || phonebox.Text == "" || emailbox.Text == "" || homebox.Text == "" || roombox.Text == "" )
            {
                MessageBox.Show("Fill every requirement");
                
            }
            else
            {
                string n = namebox.Text, d = desigcombo.Text, p = phonebox.Text, em = emailbox.Text, h = homebox.Text, r = roombox.Text;
                bolayer.save_edit_profile(n, d, p, em, h, r, bolayer.mainname);
                bolayer.load_member_form_server();

                savebutton.Visibility = Visibility.Hidden;
                canclebutton.Visibility = Visibility.Hidden;
                Editbutton.Visibility = Visibility.Visible;
                passbutton.Visibility = Visibility.Visible;

                desigbox.Visibility = Visibility.Visible;
                desigcombo.Visibility = Visibility.Hidden;

                namebox.IsEnabled = false;
                emailbox.IsEnabled = false;
                phonebox.IsEnabled = false;
                homebox.IsEnabled = false;
                roombox.IsEnabled = false;
            }
            
            
        }

        private void canclebutton_Click(object sender, RoutedEventArgs e)
        {
            savebutton.Visibility = Visibility.Hidden;
            canclebutton.Visibility = Visibility.Hidden;
            Editbutton.Visibility = Visibility.Visible;
            passbutton.Visibility = Visibility.Visible;

            desigbox.Visibility = Visibility.Visible;
            desigcombo.Visibility = Visibility.Hidden;

            namebox.IsEnabled = false;
            emailbox.IsEnabled = false;
            phonebox.IsEnabled = false;
            homebox.IsEnabled = false;
            roombox.IsEnabled = false;
            bolayer.cmp_member_load_2 = true;
        }

        private void passbutton_Click(object sender, RoutedEventArgs e)
        {
            normalpart.Visibility = Visibility.Hidden;
            passchangepart.Visibility = Visibility.Visible;
        }

        private void savepassbutton_Click(object sender, RoutedEventArgs e)
        {
            string p1= passwordbox.Password.ToString(), p2= repasswordbox.Password.ToString();
            string message;
            if (p1!=p2)
            {
                MessageBox.Show("Password don't match");
            }
            if (p1 == "" || p2 == "")
            {
                MessageBox.Show("Fill every requirement");
            }
            else if(p1.Length<4 || p2.Length<4)
            {
                MessageBox.Show("Password is too sort. Enter at least 4 character.");
            }
            else
            {
                bolayer.change_password(bolayer.mainname, p1, p2);
                
            }
            passwordbox.Clear();
            repasswordbox.Clear();
            normalpart.Visibility = Visibility.Visible;
            passchangepart.Visibility = Visibility.Hidden;
        }
        

        private void backbutton_Click(object sender, RoutedEventArgs e)
        {
            normalpart.Visibility = Visibility.Visible;
            passchangepart.Visibility = Visibility.Hidden;
        }

    }
}
