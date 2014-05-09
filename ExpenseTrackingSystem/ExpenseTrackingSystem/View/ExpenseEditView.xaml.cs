using System;
using System.Windows;
using ExpenseTrackingSystem.Notifications;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace ExpenseTrackingSystem.View
{
    /// <summary>
    /// Description for ExpenseEditView.
    /// </summary>
    public partial class ExpenseEditView : Window
    {
        /// <summary>
        /// Initializes a new instance of the ExpenseEditView class.
        /// </summary>
        public ExpenseEditView()
        {
            InitializeComponent();

            DataContext = new ExpenseEditViewModel();

            Loaded += (s, e) =>
            {
                Messenger.Default.Register<NotificationMessage>(this, message =>
                {

                    if (message.Notification == MessengerMessage.CLOSE_EXPENSE_EDIT_FORM)
                    {
                        this.Close();
                    }
                });

                ErrorHandlerHelper.RegistrateErrorHandler(this, MessengerMessage.ERROR_MESSAGE_EXPENSE_EDIT);
            };

            Closed += (s, e) =>
            {
                Messenger.Default.Unregister(this);
            };
        }
    }
}