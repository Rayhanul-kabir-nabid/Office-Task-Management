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
using MahApps.Metro.Controls;
namespace Task_Software
{
    /// <summary>
    /// Interaction logic for Memberinfo.xaml
    /// </summary>
    public partial class Memberinfo : MetroWindow
    {
        Boolean isEditing = false; 
        public Memberinfo()
        {
            InitializeComponent();
            namebox.Text = "member name";
            desigbox.Text = "designation";
            emailbox.Text = "email adress";
            phonebox.Text = "01716445081";
            homebox.Text = "khulna";
            roombox.Text = "0101";
        }

        private void closebutton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

       
        private void Editbutton_Click(object sender, RoutedEventArgs e)
        {
            if(isEditing==false)
            {
                isEditing = true;
                namebox.IsEnabled = true;
                desigbox.IsEnabled = true;
                emailbox.IsEnabled = true;
                phonebox.IsEnabled = true;
                homebox.IsEnabled = true;
                roombox.IsEnabled = true;

                Editbutton.Content = "OK";

            }
            else
            {
                isEditing = false;
                namebox.IsEnabled = false;
                desigbox.IsEnabled = false;
                emailbox.IsEnabled = false;
                phonebox.IsEnabled = false;
                homebox.IsEnabled = false;
                roombox.IsEnabled = false;

                Editbutton.Content = "Edit Member";
            }
        }
    }
}
