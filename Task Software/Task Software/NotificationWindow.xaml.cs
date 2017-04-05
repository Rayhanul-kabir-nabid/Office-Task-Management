using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Task_Software
{

    public partial class NotificationWindow : Window
    {
        DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        public NotificationWindow(string s): base()
        {
            this.InitializeComponent();
            notifybox.Text = s;
            Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
            {
                var workingArea = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
                var transform = PresentationSource.FromVisual(this).CompositionTarget.TransformFromDevice;
                var corner = transform.Transform(new Point(workingArea.Right, workingArea.Bottom));

                this.Left = corner.X - this.ActualWidth;
                this.Top = corner.Y - this.ActualHeight;
            }));
            timer.Interval = TimeSpan.FromSeconds(6d);
            timer.Tick += new EventHandler(timer_Tick);
        }
        public new void Show()
        {
            base.Show();
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            //set default result if necessary

            timer.Stop();
            this.Close();
        }

    }
}