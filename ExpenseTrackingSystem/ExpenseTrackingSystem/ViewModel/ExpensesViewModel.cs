using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DAL.Entities;
using DAL.Repository.Imp;
using ExpenseTrackingSystem.Extensions;
using ExpenseTrackingSystem.Model;
using ExpenseTrackingSystem.Notifications;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace ExpenseTrackingSystem.ViewModel
{
    public class ExpensesViewModel : ViewModelBase
    {
        public ExpensesViewModel()
        {
            _listOfExpenses =new ObservableCollection<ExpenseModel>();

            Load();
        }



        public const string ListOfExpensesPropertyName = "ListOfExpenses";

        private ObservableCollection<ExpenseModel> _listOfExpenses;

        public ObservableCollection<ExpenseModel> ListOfExpenses
        {
            get
            {
                return _listOfExpenses;
            }

            set
            {
                if (_listOfExpenses == value)
                {
                    return;
                }

                RaisePropertyChanging(ListOfExpensesPropertyName);
                _listOfExpenses = value;
                RaisePropertyChanged(ListOfExpensesPropertyName);
            }
        }



        private RelayCommand _loadExpensesCommand;

        public RelayCommand LoadExpensesCommand
        {
            get
            {
                return _loadExpensesCommand
                    ?? (_loadExpensesCommand = new RelayCommand(
                                          () =>
                                          {
                                          }));
            }
        }



        private void Load()
        {
            UnitOfWork uow = new UnitOfWork();

            try
            {
                RepositoryExpense repositoryExpense = new RepositoryExpense(uow);

                uow.BeginTransaction();

                int userID = AuthorizationService.GetUserID();

                IEnumerable<Expense> expenses = repositoryExpense.LoadUserExpenses(userID);

                foreach (var expense in expenses)
                {
                    _listOfExpenses.Add(expense.ToModel());
                }

                uow.Commit();
            }
            catch
            {
                uow.RollBack();
                throw;
            }


        }


        
        private RelayCommand _closeFormCommand;

        public RelayCommand CloseFormCommand
        {
            get
            {
                return _closeFormCommand
                    ?? (_closeFormCommand = new RelayCommand(
                                          () => Messenger.Default.Send<NotificationMessage>(new NotificationMessage(MessengerMessage.CLOSE_EXPENSES_FORM))));
            }
        }



        private RelayCommand _openTagsFormCommand;

        public RelayCommand OpenTagsFormCommand
        {
            get
            {
                return _openTagsFormCommand
                    ?? (_openTagsFormCommand = new RelayCommand(
                                          () => Messenger.Default.Send<NotificationMessage>(new NotificationMessage(MessengerMessage.OPEN_TAGS_FORM))));
            }
        }



        private RelayCommand _openExpenseEditFormCommand;

        public RelayCommand OpenExpenseEditFormCommand
        {
            get
            {
                return _openExpenseEditFormCommand
                    ?? (_openExpenseEditFormCommand = new RelayCommand(
                                          () =>
                                          {
                                              Messenger.Default.Send<NotificationMessage>(new NotificationMessage(MessengerMessage.OPEN_EXPENSE_EDIT_FORM));
                                          }));
            }
        }
    }
}