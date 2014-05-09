using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using ExpenseTrackingSystem.Notifications;
using GalaSoft.MvvmLight.Messaging;

namespace ExpenseTrackingSystem
{
    public static class ErrorHandlerHelper
    {
        public static void RegistrateErrorHandler(Window sender, string notification)
        {
            Messenger.Default.Register<NotificationMessage<string>>(sender, message =>
            {
                if (message.Notification == notification)
                {
                    MessageBox.Show(sender, string.Format("Unhandled exception:{0}{1}", Environment.NewLine, message.Content), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }
    }
}
