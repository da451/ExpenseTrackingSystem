using System.Windows;
using ExpenseTrackingSystem.Notifications;
using ExpenseTrackingSystem.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace ExpenseTrackingSystem.View
{
    /// <summary>
    /// Description for TagsView.
    /// </summary>
    public partial class TagsView : Window
    {
        /// <summary>
        /// Initializes a new instance of the TagsView class.
        /// </summary>
        public TagsView()
        {
            InitializeComponent();

            DataContext = new TagsViewModel();

            Loaded += (s, e) =>
            {
                Messenger.Default.Register<NotificationMessage>(this, message =>
                {
                    if (message.Notification == MessengerMessage.CLOSE_TAGS_FORM)
                    {
                        Close();
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