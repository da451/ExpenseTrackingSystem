using System.Windows;
using ExpenseTrackingSystem.Notifications;
using GalaSoft.MvvmLight.Messaging;

namespace ExpenseTrackingSystem.View
{
    /// <summary>
    /// Description for ExpensesView.
    /// </summary>
    public partial class ExpensesView : Window
    {
        /// <summary>
        /// Initializes a new instance of the ExpensesView class.
        /// </summary>
        public ExpensesView()
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                Messenger.Default.Register<NotificationMessage>(this, message =>
                {
                    if (message.Notification == MessengerMessage.CLOSE_EXPENSES_FORM)
                    {
                        if (
                            MessageBox.Show(this, "Do you want to exit?", "Exit", MessageBoxButton.YesNo,
                                MessageBoxImage.Information) == MessageBoxResult.Yes)
                        {
                            this.Close();
                        }
                    }

                    if (message.Notification == MessengerMessage.OPEN_TAGS_FORM)
                    {
                        TagsView tagsView = new TagsView();

                        tagsView.ShowDialog();
                    }

                    if (message.Notification == MessengerMessage.OPEN_EXPENSE_EDIT_FORM)
                    {
                        ExpenseEditView expenseEditView = new ExpenseEditView();

                        expenseEditView.ShowDialog();
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