using System.Windows;
using ExpenseTrackingSystem.Notifications;
using GalaSoft.MvvmLight.Messaging;

namespace ExpenseTrackingSystem.View
{
    public partial class StatisticsView : Window
    {
        public StatisticsView()
        {
            InitializeComponent();

            DataContext = new StaticticsViewModel();

            Loaded += (s, e) =>
            {
                Messenger.Default.Register<NotificationMessage>(this, message =>
                {

                    if (message.Notification == MessengerMessage.CLOSE_STATISTIC_FORM)
                    {
                        this.Close();
                    }
                });

                ErrorHandlerHelper.RegistrateErrorHandler(this, MessengerMessage.ERROR_MESSAGE_STATISTICS);
            };

            Closed += (s, e) =>
            {
                Messenger.Default.Unregister(this);
            };
        }
    }
}