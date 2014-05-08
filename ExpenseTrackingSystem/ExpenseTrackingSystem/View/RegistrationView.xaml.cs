using System;
using System.Windows;
using ExpenseTrackingSystem.Notifications;
using ExpenseTrackingSystem.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace ExpenseTrackingSystem.View
{
    /// <summary>
    /// Description for RegistrationView.
    /// </summary>
    public partial class RegistrationView : Window
    {
        /// <summary>
        /// Initializes a new instance of the RegistrationView class.6
        /// </summary>
        public RegistrationView()
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                this.DataContext = new RegistrationViewModel();

                Messenger.Default.Register<NotificationMessage>(this, message =>
                {
                    if (message.Notification == MessengerMessage.CLOSE_REGISTRATION_FORM)
                    {
                        this.Close();
                    }
                });

                Messenger.Default.Register<NotificationMessage<string>>(this, message =>
                {
                    if (message.Notification == MessengerMessage.SUCCESSFUL_REGISTRATION)
                    {
                        MessageBox.Show(this, string.Format("User {0} successfully registered", message.Content), "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }

                    if (message.Notification == MessengerMessage.UNSUCCESSFUL_REGISTRATION)
                    {
                        MessageBox.Show(this, string.Format("User {0} not registered", message.Content), "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    if (message.Notification == MessengerMessage.ERROR_MESSAGE_REGISTRATION)
                    {
                        MessageBox.Show(this, string.Format("Unhandled exception:{0}{1}", Environment.NewLine, message.Content), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                });


            };

            Closed+= (s, e) =>
            {
                Messenger.Default.Unregister(this);
            };
        }
    }
}