using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace Task_Software
{
    /// <summary>
    /// Interaction logic for TaskDescription.xaml
    /// </summary>
    public partial class TaskDescription : MetroWindow
    {
        public TaskDescription()
        {
            InitializeComponent();
        }

        private void closebutton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
