using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
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
using Microsoft.Win32;
using System.Windows.Threading;

namespace Task_Software.SideControl
{
    /// <summary>
    /// Interaction logic for NewTask.xaml
    /// </summary>
    public partial class NewTask : UserControl
    {
        public List<string> uplodfilelist = new List<string>();
        BoClass bolayer;

        BOftp boupoad;

        public NewTask(BoClass b)
        {
            InitializeComponent();
            bolayer = b;
            boupoad = new BOftp(b.entitylayer);
            fileGrid.Visibility = Visibility.Hidden;
            membersgrid.Visibility = Visibility.Hidden;
            memberRemove.Visibility = Visibility.Hidden;
            fileremove.Visibility = Visibility.Hidden;
            List<string> memberlst = bolayer.getmemberslist();
            sendtocombobox.ItemsSource = memberlst;



        }




        public void settaskinfo(int c, string title, string description, string givendate, string duedate)
        {
            if (c == 1)
            {
                bolayer.setfolder();
                string am = "";
                for (int i = 0; i < selectedmemberslist.Items.Count; i++)
                {
                    am += selectedmemberslist.Items[i].ToString() + ",";
                }
                string folder = bolayer.nextfolder();
                bolayer.insert_task_info(title, description, givendate, duedate, am, "Pending", folder);
                if (uplodfilelist.Count > 0)
                {
                    try
                    {
                        boupoad.Uploadfile(folder, uplodfilelist);
                        MainWindow.queueNotify.Enqueue("Upload successfull");

                    }
                    catch (Exception)
                    {
                        MainWindow.queueNotify.Enqueue("Upload unsuccessfull");
                    }
                }

                for (int i = 0; i < selectedmemberslist.Items.Count; i++)
                {
                    bolayer.insert_notification(bolayer.getpersoninfo(bolayer.mainname).full_name + " Assigned you a new task.", selectedmemberslist.Items[i].ToString(), givendate);
                }

                bolayer.load_task_form_server();
            }
            else
            {
                bolayer.setfolder();
                for (int i = 0; i < selectedmemberslist.Items.Count; i++)
                {
                    string folder = bolayer.nextfolder();

                    bolayer.insert_task_info(title, description, givendate, duedate, selectedmemberslist.Items[i].ToString(), "Pending", folder);
                    if (uplodfilelist.Count > 0)
                    {
                        try
                        {
                            boupoad.Uploadfile(folder, uplodfilelist);
                            MainWindow.queueNotify.Enqueue("Upload Complete");
                        }
                        catch (Exception)
                        {
                            MainWindow.queueNotify.Enqueue("Upload unsuccessfull");
                        }
                    }
                    bolayer.insert_notification(bolayer.getpersoninfo(bolayer.mainname).full_name + " Assigned you a new task.", selectedmemberslist.Items[i].ToString(), givendate);


                }

                bolayer.load_task_form_server();

            }


        }

        private void browsbutton_Click(object sender, RoutedEventArgs e)
        {


            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Multiselect = true;
            if (openfile.ShowDialog() == true)
            {
                string[] fl1 = openfile.SafeFileNames;
                for (int i = 0; i < fl1.Length; i++)
                    selectedFiles.Items.Add(fl1[i]);

                string[] fl = openfile.FileNames;
                for (int i = 0; i < fl.Length; i++)
                    uplodfilelist.Add(fl[i]);
                if (fileGrid.Visibility == Visibility.Hidden)
                    fileGrid.Visibility = Visibility.Visible;
            }
        }

        private void sendtocombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sendtocombobox.SelectedIndex == -1) return;

            if (membersgrid.Visibility == Visibility.Hidden)
                membersgrid.Visibility = Visibility.Visible;

            if (selectedmemberslist.Items.Contains(sendtocombobox.SelectedItem.ToString()) == true) return;

            selectedmemberslist.Items.Add(sendtocombobox.SelectedItem.ToString());
            sendtocombobox.SelectedIndex = -1;
        }

        private void fileremove_Click(object sender, RoutedEventArgs e)
        {
            int i = selectedFiles.SelectedIndex;
            selectedFiles.Items.RemoveAt(i);
            uplodfilelist.RemoveAt(i);

        }

        private void memberRemove_Click(object sender, RoutedEventArgs e)
        {
            int i = selectedmemberslist.SelectedIndex;
            selectedmemberslist.Items.RemoveAt(i);
        }

        private void selectedFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectedFiles.SelectedIndex >= 0) fileremove.Visibility = Visibility.Visible;
            else fileremove.Visibility = Visibility.Hidden;

            if (selectedFiles.Items.Count == 0) fileGrid.Visibility = Visibility.Hidden;
        }

        private void selectedmemberslist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectedmemberslist.SelectedIndex >= 0) memberRemove.Visibility = Visibility.Visible;
            else memberRemove.Visibility = Visibility.Hidden;

            if (selectedmemberslist.Items.Count == 0) membersgrid.Visibility = Visibility.Hidden;

        }

        private void clearbutton_Click(object sender, RoutedEventArgs e)
        {
            titlebox.Clear();
            descriptionbox.Clear();
            selectedmemberslist.Items.Clear();
            uplodfilelist.Clear();
            selectedFiles.Items.Clear();
            membersgrid.Visibility = Visibility.Hidden;
            fileGrid.Visibility = Visibility.Hidden;
        }


    }
}