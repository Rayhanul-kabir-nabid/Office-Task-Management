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
    /// Interaction logic for Tasklist.xaml
    /// </summary>
    public partial class NewTask : MetroWindow
    {
        List<String> selectedlist = new List<string>();

        public NewTask()
        {
            InitializeComponent();
            memberscombobox.Items.Add("member 1");
            memberscombobox.Items.Add("member 2");
            memberscombobox.Items.Add("member 3");
            memberscombobox.Items.Add("member 4");
            
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void memberscombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String s = memberscombobox.SelectedItem.ToString();
            if (selectedlist.Contains(s)) return;
            selectedmemberslist.Items.Add(s);
            selectedlist.Add(s);
            this.Height += 22;


        }

        private void closebutton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
