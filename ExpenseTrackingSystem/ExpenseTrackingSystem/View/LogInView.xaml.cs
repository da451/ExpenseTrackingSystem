using System;
using System.Windows;
using ExpenseTrackingSystem.Notifications;
using ExpenseTrackingSystem.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace ExpenseTrackingSystem.View
{
    /// <summary>
    /// Description for LogInView.
    /// </summary>
    public partial class LogInView : Window
    {
        /// <summary>
        /// Initializes a new instance of the LogInView class.
        /// </summary>
        public LogInView()
        {
            InitializeComponent();

            DataContext = new LogInViewModel();

            Loaded += (s, e) =>
            {
                Messenger.Default.Register<NotificationMessage>(this, message =>
                {
                    if (message.Notification == MessengerMessage.OPEN_REGISTRATION_FORM)
                    {
                        RegistrationView rv = new RegistrationView();

                        rv.ShowDialog();
                    }

                    if (message.Notification == MessengerMessage.CLOSE_LOGIN_FORM)
                    {
                        this.Close();
                    }
                });

                Messenger.Default.Register<NotificationMessage<string>>(this, message =>
                {
                    if (message.Notification == MessengerMessage.SUCCESSFUL_LOGIN)
                    {
                        //MessageBox.Show(this, string.Format("Successful logged user {0}", message.Content),"Message", MessageBoxButton.OK, MessageBoxImage.Information);
                        ExpensesView view = new ExpensesView();

                        view.Show();

                        this.Close();
                    }

                    if (message.Notification == MessengerMessage.UNSUCCESSFUL_LOGIN)
                    {
                        MessageBox.Show(this, string.Format("User {0} not found", message.Content), "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    if (message.Notification == MessengerMessage.ERROR_MESSAGE_LOGIN)
                    {
                        MessageBox.Show(this, string.Format("Unhandled exception:{0}{1}",Environment.NewLine, message.Content), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                });
            };

            Closed += (s, e) =>
            {
                Messenger.Default.Unregister(this);
            };
        }
    }
}