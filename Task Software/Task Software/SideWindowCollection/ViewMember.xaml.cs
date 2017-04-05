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
    /// Interaction logic for ViewMember.xaml
    /// </summary>
    public partial class ViewMember : MetroWindow
    {
        Memberinfo mi = new Memberinfo();
        public ViewMember()
        {
            InitializeComponent();


            memberlistbox.Items.Add("mahin");
            memberlistbox.Items.Add("mahin");
            memberlistbox.Items.Add("mahin");
            memberlistbox.Items.Add("mahin");
            memberlistbox.Items.Add("mahin");
            memberlistbox.Items.Add("mahin");
            memberlistbox.Items.Add("mahin");
            memberlistbox.Items.Add("mahin");
            memberlistbox.Items.Add("mahin");
            memberlistbox.Items.Add("mahin");
            memberlistbox.Items.Add("mahin");
            memberlistbox.Items.Add("mahin");
            memberlistbox.Items.Add("mahin");
            memberlistbox.Items.Add("mahin");
            memberlistbox.Items.Add("mahin");
            memberlistbox.Items.Add("mahin");
            memberlistbox.Items.Add("mahin");
            memberlistbox.Items.Add("mahin");
            memberlistbox.Items.Add("mahin");
            memberlistbox.Items.Add("mahin");
            
        }

        private void mousedown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void memberlistbox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void MetroWindow_LocationChanged(object sender, EventArgs e)
        {
            mi.Left = this.Left + this.Width;
            mi.Top = this.Top;
        }

        private void cloasebutton_Click(object sender, RoutedEventArgs e)
        {
            mi.Close();
            this.Close();
        }

        private void memberlistbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            if (mi != null) mi.Close();
            mi = null;
            mi = new Memberinfo();
            mi.Left = this.Left + this.Width;
            mi.Top = this.Top;
            mi.Show();
        }

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            mi.Close();
        }
    }
}
