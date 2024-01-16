using SudokuGameWPF.Views;
using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows;
using System.Windows.Threading;

namespace SudokuGameWPF.ViewModel
{
    public class NotifactionController
    {
        #region Singleton
        private static object _lock = new object();
        private static NotifactionController _instance;

        public static NotifactionController Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new NotifactionController();
                    }

                    return _instance;
                }
            }
        }

        private NotifactionController() 
        { 
            NotificationList = new Dictionary<int, NotificationData>();
            DurationOfNotifacitonWindowInMS = 5 * 1000; //2 seconds
            NotificationCount = 0;

            WorkArea = System.Windows.SystemParameters.WorkArea;
        }
        #endregion

        private Dictionary<int, NotificationData> NotificationList;
        private float DurationOfNotifacitonWindowInMS;
        private int NotificationCount;
        private readonly Rect WorkArea;

        public void ShowNotification(NotificationType notificationType, string message)
        {
            NotificationList.Add(NotificationCount, new NotificationData()
            {
                NotificationWindow = CreateAndShowNotification(notificationType, message),
                CreatedDate = DateTime.UtcNow,
            });
            NotificationCount++;
        }

        private NotificationWindow CreateAndShowNotification(NotificationType notificationType, string message)
        {
            NotificationWindow notificationWindow = new NotificationWindow(notificationType, message, 
                DurationOfNotifacitonWindowInMS,NotificationCount, OnNotifacionClose);

            notificationWindow.Top = WorkArea.Bottom - NotificationList.Count * notificationWindow.Height;
            notificationWindow.Left = WorkArea.Right - notificationWindow.Width;

            notificationWindow.Show();

            return notificationWindow;
        }

        private void UpdateNotificationPosition(NotificationWindow notificationWindow, int index)
        {
            notificationWindow.UpdatePosition(WorkArea.Bottom - index * notificationWindow.Height,
                                              WorkArea.Right - notificationWindow.Width);
        }

        private void OnNotifacionClose(int id)
        {
            NotificationList.Remove(id);

            int count = 0;

            foreach (var notification in NotificationList)
            {
                UpdateNotificationPosition(notification.Value.NotificationWindow, count++ + 1);
            }
        }

        private struct NotificationData
        {
            public NotificationWindow NotificationWindow { get; set; }

            public DateTime CreatedDate { get; set; }
        }
    }

    

    public enum NotificationType
    {
        Info,
        Warning,
        Error,
    }
}
