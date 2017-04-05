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
using ENTITY;
using System.Windows.Threading;
using System.Threading;
namespace Task_Software.SideControl
{
    /// <summary>
    /// Interaction logic for MemberView.xaml
    /// </summary>
    public partial class MemberView : UserControl
    {
        BoClass bo;
        public DispatcherTimer member_timer;
        Boolean iscurrentmemberchanged = false;
        Person personNow;
        int currentindex=-1;
        List<Person> memberlist = new List<Person>();
        
        string currentmember;
        public MemberView(BoClass b)
        {
            InitializeComponent();
            member_Info_window.Visibility = Visibility.Hidden;
            bo = b;
            memberlist = bo.getmemberslist_person();
            memberslistbox.ItemsSource = memberlist;

            member_timer = new System.Windows.Threading.DispatcherTimer();
            member_timer.Tick += new EventHandler(member_timer_loop);
            member_timer.Interval = new TimeSpan(0, 0, 1);
            member_timer.Start();
        }

        private void member_timer_loop(object sender, EventArgs e)
        {
            try
            {
                if (bo.cmp_member_load)
                {
                    bo.cmp_member_load = false;
                    memberlist = bo.getmemberslist_person();
                    memberslistbox.ItemsSource = memberlist;
                    if (currentindex >= 0)
                    {
                        memberslistbox.SelectedIndex = currentindex;
                    }

                }
            }
            catch (Exception)
            {

                member_timer = new System.Windows.Threading.DispatcherTimer();
                member_timer.Tick += new EventHandler(member_timer_loop);
                member_timer.Interval = new TimeSpan(0, 0, 1);
                member_timer.Start();
            }
        }
        
        private void memberslistbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           // MessageBox.Show(memberslistbox.SelectedIndex.ToString());
            if(memberslistbox.SelectedIndex<0)
            {
                member_Info_window.Visibility = Visibility.Hidden;
                return;
            }
            member_Info_window.Visibility = Visibility.Visible;
            currentmember = memberlist[memberslistbox.SelectedIndex].name;
            personNow = bo.getpersoninfo(currentmember);
            currentindex = memberslistbox.SelectedIndex;
            

        }
        private void Editbutton_Click(object sender, RoutedEventArgs e)
        {


            savebutton.Visibility = Visibility.Visible;
            canclebutton.Visibility = Visibility.Visible;
            Editbutton.Visibility = Visibility.Hidden;
            deletebutton.Visibility = Visibility.Hidden;

            namebox.Visibility = Visibility.Hidden;
            desigbox.Visibility = Visibility.Hidden;
            emailbox.Visibility = Visibility.Hidden;
            phonebox.Visibility = Visibility.Hidden;
            homebox.Visibility = Visibility.Hidden;
            roombox.Visibility = Visibility.Hidden;

            editpanel.Visibility = Visibility.Visible;





        }
        private void savebutton_Click(object sender, RoutedEventArgs e)
        {
            if (fullnameedit.Text == "" || desigcombo.Text == "" || phoneedit.Text == "" || emailedit.Text == "" || homeedit.Text == "" || roomedit.Text == "")
            {
                MessageBox.Show("Fill every requirements");
            }
            else
            {
                bo.save_edit_member(namebox.Text, desigcombo.Text, phonebox.Text, emailbox.Text, homebox.Text, roombox.Text, currentmember);
                bo.load_member_form_server();
                
            }
            savebutton.Visibility = Visibility.Hidden;
            canclebutton.Visibility = Visibility.Hidden;
            Editbutton.Visibility = Visibility.Visible;
            deletebutton.Visibility = Visibility.Visible;

            namebox.Visibility = Visibility.Visible;
            desigbox.Visibility = Visibility.Visible;
            emailbox.Visibility = Visibility.Visible;
            phonebox.Visibility = Visibility.Visible;
            homebox.Visibility = Visibility.Visible;
            roombox.Visibility = Visibility.Visible;

            editpanel.Visibility = Visibility.Hidden;



        }
        

        private void canclebutton_Click(object sender, RoutedEventArgs e)
        {
            savebutton.Visibility = Visibility.Hidden;
            canclebutton.Visibility = Visibility.Hidden;
            Editbutton.Visibility = Visibility.Visible;
            deletebutton.Visibility = Visibility.Visible;

            namebox.Visibility = Visibility.Visible;
            desigbox.Visibility = Visibility.Visible;
            emailbox.Visibility = Visibility.Visible;
            phonebox.Visibility = Visibility.Visible;
            homebox.Visibility = Visibility.Visible;
            roombox.Visibility = Visibility.Visible;

            editpanel.Visibility = Visibility.Hidden;
            iscurrentmemberchanged = true;
        }
        

        private void closeinfo_button_Click(object sender, RoutedEventArgs e)
        {
            member_Info_window.Visibility = Visibility.Hidden;
            
        }

        private void deletebutton_Click(object sender, RoutedEventArgs e)
        {
            bo.delete_member(currentmember);
            bo.load_member_form_server();
        }
        
        
    }
}
