using System.Windows;
using ExpenseTrackingSystem.Notifications;
using ExpenseTrackingSystem.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace ExpenseTrackingSystem.View
{

    public partial class ExpensesView : Window
    {

        public ExpensesView()
        {
            InitializeComponent();

            DataContext = new ExpensesViewModel();
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
                });

                Messenger.Default.Register<NotificationMessageAction>(this, message =>
                {
                    if (message.Notification == MessengerMessage.OPEN_EXPENSE_EDIT_FORM)
                    {
                        ExpenseEditView expenseEditView = new ExpenseEditView();

                        ExpenseEditViewModel expenseEditViewModel = expenseEditView.DataContext as ExpenseEditViewModel;

                        if (expenseEditViewModel != null && message.Sender is int)
                        {
                            expenseEditViewModel.ExpenseID = (int) message.Sender;
                        }

                        expenseEditView.ShowDialog();

                        message.Execute();
                    }

                    if (message.Notification == MessengerMessage.OPEN_TAGS_FORM)
                    {
                        TagsView tagsView = new TagsView();

                        tagsView.ShowDialog();

                        message.Execute();
                    }

                    if (message.Notification == MessengerMessage.DELETE_EXPENSE)
                    {
                        if (
                            MessageBox.Show(this, "Do you want to delete this expense?", "Deete?",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Information) == MessageBoxResult.Yes)
                        {
                            message.Execute();
                        }
                    }

                    ErrorHandlerHelper.RegistrateErrorHandler(this, MessengerMessage.ERROR_MESSAGE_EXPENSES);
                });

            };

            Closed += (s, e) =>
            {
                Messenger.Default.Unregister(this);
            };
        }
    }
}