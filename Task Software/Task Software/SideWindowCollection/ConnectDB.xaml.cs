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
namespace Task_Software.SideWindowCollection
{
    /// <summary>
    /// Interaction logic for ConnectDB.xaml
    /// </summary>
    public partial class ConnectDB : MetroWindow
    {
       
        public ConnectDB()
        {
            InitializeComponent();
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            Createbutton.IsEnabled = true;
        }

        private void Createbutton_Click(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
