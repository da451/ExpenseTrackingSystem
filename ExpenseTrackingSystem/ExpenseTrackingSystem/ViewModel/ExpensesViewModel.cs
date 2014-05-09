using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
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
            _listOfExpenses =new ExpenseCollection();

            _listOfExpenses.LoadExpensesCommand.Execute(null);
        }



        public const string ListOfExpensesPropertyName = "ListOfExpenses";

        private ExpenseCollection _listOfExpenses;

        public ExpenseCollection ListOfExpenses
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




        public const string SelectedExpensePropertyName = "SelectedExpense";

        private ExpenseModel _selectedExpense;

        public ExpenseModel SelectedExpense
        {
            get
            {
                return _selectedExpense;
            }

            set
            {
                if (_selectedExpense == value)
                {
                    return;
                }

                RaisePropertyChanging(SelectedExpensePropertyName);
                _selectedExpense = value;
                RaisePropertyChanged(SelectedExpensePropertyName);
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
                                          () => Messenger.Default.Send<NotificationMessageAction>(
                                              new NotificationMessageAction(MessengerMessage.OPEN_TAGS_FORM, ReloadExpenses))));
            }
        }



        private RelayCommand _createNewExpenseCommand;

        public RelayCommand CreateNewExpenseCommand
        {
            get
            {
                return _createNewExpenseCommand
                    ?? (_createNewExpenseCommand = new RelayCommand(
                                          () =>
                                          {
                                              Messenger.Default.Send<NotificationMessageAction>(
                                                  new NotificationMessageAction(MessengerMessage.OPEN_EXPENSE_EDIT_FORM, ReloadExpenses));
                                          }));
            }
        }


        private RelayCommand _updateExpenseCommand;

        public RelayCommand UpdateExpenseCommand
        {
            get
            {
                return _updateExpenseCommand
                    ?? (_updateExpenseCommand = new RelayCommand(
                                          () =>
                                          {
                                              Messenger.Default.Send<NotificationMessageAction>(
                                                  new NotificationMessageAction(SelectedExpense.ExpenseID, MessengerMessage.OPEN_EXPENSE_EDIT_FORM, ReloadExpenses));
                                          }));
            }
        }

        private RelayCommand _deleteExpenseCommand;

        /// <summary>
        /// Gets the DeleteExpenseCommand.
        /// </summary>
        public RelayCommand DeleteExpenseCommand
        {
            get
            {
                return _deleteExpenseCommand
                    ?? (_deleteExpenseCommand = new RelayCommand(
                                          () =>
                                          {
                                              SelectedExpense.Delete();

                                              _listOfExpenses.Remove(SelectedExpense);

                                              SelectedExpense = null;
                                          }));
            }
        }

        private void ReloadExpenses()
        {
            int selectedExpenseID = -1;

            if (SelectedExpense != null)
            {
                selectedExpenseID = SelectedExpense.ExpenseID;
            }
            _listOfExpenses.LoadExpensesCommand.Execute(null);

            if (selectedExpenseID > 0)
            {
                SelectedExpense = _listOfExpenses.Where(o => o.ExpenseID == selectedExpenseID).FirstOrDefault();
            }

        }
    }
}