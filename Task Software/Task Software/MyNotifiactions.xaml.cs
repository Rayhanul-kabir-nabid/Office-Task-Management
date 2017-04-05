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

namespace Task_Software
{

    public partial class MyNotifiactions
    {
        private const byte MAX_NOTIFICATIONS = 4;
        private int count;
        public Notifications Notifications = new Notifications();
        private readonly Notifications buffer = new Notifications();
        private const double topOffset = 20;
        private const double leftOffset = 380;
        public MyNotifiactions()
        {
            InitializeComponent();
            NotificationsControl.DataContext = Notifications;
            this.Top = SystemParameters.WorkArea.Top + topOffset;
            this.Left = SystemParameters.WorkArea.Left + SystemParameters.WorkArea.Width - leftOffset;
        }

        public void AddNotification(Notification notification)
        {

            notification.Id = count++;
            if (Notifications.Count + 1 > MAX_NOTIFICATIONS)
            {
                buffer.Add(notification);
            }
            else
            {
                Notifications.Add(notification);
                //MessageBox.Show("sound 1");
                mediaElement_done.Stop();
                mediaElement_done.Play();
                
            }
                

            //Show window if there're notifications
            if (Notifications.Count > 0 && !IsActive)
                Show();
        }

        public void RemoveNotification(Notification notification)
        {
            if (Notifications.Contains(notification))
                Notifications.Remove(notification);

            if (buffer.Count > 0)
            {
                Notifications.Add(buffer[0]);
                buffer.RemoveAt(0);
                //MessageBox.Show("sound 1");
                mediaElement_done.Stop();
                mediaElement_done.Play();
                
                
            }

            //Close window if there's nothing to show
            if (Notifications.Count < 1)
                Hide();
        }

        private void NotificationWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Height != 0.0)
                return;
            var element = sender as Grid;
            RemoveNotification(Notifications.First(n => n.Id == Int32.Parse(element.Tag.ToString())));
        }
    }
}
