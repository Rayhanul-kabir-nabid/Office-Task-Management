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
using BO;
using System.Threading;

namespace Task_Software.SideWindowCollection
{
   
    public partial class EditDescription : Window
    {
        BoClass bo;
        string id;
        public EditDescription(BoClass b, string i, string tx)
        {
            InitializeComponent();
            bo = b;
            id = i;
            destextBox.Text = tx;
        }

        private void canclebutton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void savebutton_Click(object sender, RoutedEventArgs e)
        {
            string nd = destextBox.Text;
            if(nd=="")
            {
                MessageBox.Show("Please enter description");

            }
            else
            {
                
                bo.edit_task_info_description(id, nd);
                this.Close();
            }
            
        }
        

        private void Window_Closed(object sender, EventArgs e)
        {
            
        }

        private void draggrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
