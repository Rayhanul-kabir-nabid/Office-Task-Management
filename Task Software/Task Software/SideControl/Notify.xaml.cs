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
using System.Windows.Threading;

namespace Task_Software.SideControl
{
    /// <summary>
    /// Interaction logic for Notify.xaml
    /// </summary>
    public partial class Notify : UserControl
    {
        BoClass bolayer;
        List<string> idses = new List<string>();
        public DispatcherTimer notifytrd;

        public Notify(BoClass b)
        {
            InitializeComponent();
            bolayer = b;
            clearbutton.Visibility = Visibility.Hidden;

            notifytrd = new System.Windows.Threading.DispatcherTimer();
            notifytrd.Tick += new EventHandler(notifyTimer_Tick);
            notifytrd.Interval = new TimeSpan(0, 0, 10);
            notifytrd.Start();

        }

        private void notifyTimer_Tick(object sender, EventArgs e)
        {
            try
            {

                if (bolayer.unseen_notification_by_name())
                {
                    bolayer.load_task_form_server();

                    bolayer.load_member_form_server();
                    bolayer.load_notifications();
                    bolayer.load_files_form_server();
                }
                if (bolayer.cmp_notification_load)
                {
                    bolayer.cmp_notification_load = false;
                    loadnotify();
                }
            }
            catch (Exception)
            {

                notifytrd = new System.Windows.Threading.DispatcherTimer();
                notifytrd.Tick += new EventHandler(notifyTimer_Tick);
                notifytrd.Interval = new TimeSpan(0, 0, 10);
                notifytrd.Start();
            }
            
        }

        public void loadnotify()
        {
            List<string> sl = bolayer.getnotification();
            notifylistbox.Items.Clear();
            idses.Clear();
            for (int i = 0; i < sl.Count; i += 2)
            {

                notifylistbox.Items.Add(sl[i]);
                idses.Add(sl[i + 1]);
            }
        }

        private void clearbutton_Click(object sender, RoutedEventArgs e)
        {
            int i = notifylistbox.SelectedIndex;
            
            notifylistbox.Items.RemoveAt(i);
            
            deletenotifytrd(i);
        }

        private void deletenotifytrd(int i)
        {
            
            
            
            bolayer.notification_delete(idses[i]);
            idses.RemoveAt(i);
        }

        private void notifylistbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(notifylistbox.SelectedIndex>=0)
            {
                clearbutton.Visibility = Visibility.Visible;
            }
            else
            {
                clearbutton.Visibility = Visibility.Hidden;
            }
        }

        private void clearallbutton_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < idses.Count; i++)
            {
                bolayer.notification_delete(idses[i]);
            }
            notifylistbox.Items.Clear();
            idses.Clear();
        }

        private void deleteallnotifytrd()
        {

            
                

        }
    }
}
