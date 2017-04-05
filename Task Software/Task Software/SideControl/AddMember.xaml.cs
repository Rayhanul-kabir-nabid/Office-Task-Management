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
using System.Threading;
namespace Task_Software.SideControl
{
    /// <summary>
    /// Interaction logic for AddMember.xaml
    /// </summary>
    public partial class AddMember : UserControl
    {
        BoClass bolayer;
        public AddMember(BoClass b)
        {
            InitializeComponent();
            bolayer = b;
        }
        private void addbutton_Click(object sender, RoutedEventArgs e)
        {
            if (usernamebox.Text == null || fullnamebox.Text == null || designationcombo.Text == null || phonenumber.Text == "" || emailbox.Text == "" || roombox.Text == "" || homebox.Text == "" || temppassbox.Text == "" || typebox.Text == "")
            {
                MessageBox.Show("Fill every requirement");
            }
            else if(bolayer.getpersoninfo(usernamebox.Text)!=null)
            {
                MessageBox.Show("Same user name exist");
            }
            else
            {
                string u = usernamebox.Text, f = fullnamebox.Text, d = designationcombo.Text, p = phonenumber.Text, em = emailbox.Text, r = roombox.Text, h = homebox.Text, t = temppassbox.Text, tp = typebox.Text;
                Thread tad = new Thread(() => addmembertrd(u, f, d, p, em, r, h, t, tp));
                tad.Start();
                    this.Visibility = Visibility.Hidden;
                
            }

        }

        private void addmembertrd(string u, string f, string d, string p, string e, string r, string h, string t, string tp) 
        {
             bolayer.add_member(u, f, d, p, e, r, h, t, tp);
            bolayer.load_member_form_server();
        }

        private void clearbutton_Click(object sender, RoutedEventArgs e)
        {
            usernamebox.Clear();
            fullnamebox.Clear();

            //designationcombo.Items.Clear(); 
            phonenumber.Clear();
            emailbox.Clear();
            roombox.Clear();
            homebox.Clear();
            temppassbox.Clear();
            //typebox.Items.Clear();
        }
    }
}
