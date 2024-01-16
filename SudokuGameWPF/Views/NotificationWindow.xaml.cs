using SudokuGameWPF.ViewModel;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SudokuGameWPF.Views
{
    public partial class NotificationWindow : Window
    {
        Action<int> CloseAction;
        int ID;

        DispatcherTimer Timer;

        private DoubleAnimation doubleAnim;

        public NotificationWindow(NotificationType type, string message, float durationInMS,
            int ID, Action<int> CloseAction)
        {
            InitializeComponent();

            doubleAnim = new DoubleAnimation()
            {
                Duration = TimeSpan.FromSeconds(1.5),
                EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseIn },
            };

            Loaded += NotificationWindow_Loaded;

            lblHeader.Content = type.ToString();

            switch (type)
            {
                case NotificationType.Info:
                    lblHeader.Foreground = Brushes.LightGreen;
                    break;
                case NotificationType.Warning:
                    lblHeader.Foreground = Brushes.Yellow;
                    break;
                case NotificationType.Error:
                    lblHeader.Foreground = Brushes.Red;
                    break;
                default:
                    break;
            }

            tbInfo.Text = message;
            this.CloseAction = CloseAction;
            this.ID = ID;

            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromMilliseconds(durationInMS);
            Timer.Tick += CloseSelf;
            Timer.Start();
        }

        private void NotificationWindow_Loaded(object sender, RoutedEventArgs e)
        {
            RenderTransform = new ScaleTransform(1, 1, ActualWidth, ActualHeight);
            doubleAnim.From = 0;

            RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, doubleAnim);
        }

        private void CloseSelf(object sender, EventArgs e)
        {
            doubleAnim.Completed += DoubleAnim_Completed;
            doubleAnim.To = 0;
            RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, doubleAnim);

            Timer.Stop();
            Timer.Tick -= CloseSelf;
            Timer = null;
            CloseAction?.Invoke(ID);
        }

        private void DoubleAnim_Completed(object sender, EventArgs e)
        {
            doubleAnim.Completed -= DoubleAnim_Completed;
            this.Close();
        }

        public void UpdatePosition(double top, double left)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                this.Top = top;
                this.Left = left;
            }));
        }
    }
}
