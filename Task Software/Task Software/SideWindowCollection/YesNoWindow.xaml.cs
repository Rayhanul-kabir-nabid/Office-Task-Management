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

namespace Task_Software.SideWindowCollection
{
    /// <summary>
    /// Interaction logic for YesNoWindow.xaml
    /// </summary>
    public partial class YesNoWindow : Window
    {
        public Boolean ans;

        public YesNoWindow( string dilogs)
        {
            InitializeComponent();
            showcausetextblock.Text = dilogs;
            ans = false;
        }

        private void yesbutton_Click(object sender, RoutedEventArgs e)
        {
            ans = true;
            this.Close();
        }

        private void canclebutton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void draggrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
