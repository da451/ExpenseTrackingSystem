using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DAL.Entities;
using DAL.Repository.Imp;
using ExpenseTrackingSystem.Extensions;
using GalaSoft.MvvmLight.Command;

namespace ExpenseTrackingSystem.Model
{
    public class ExpenseCollection : ObservableCollection<ExpenseModel>
    {
        private RelayCommand _loadExpensesCommand;

        public RelayCommand LoadExpensesCommand
        {
            get
            {
                return _loadExpensesCommand
                    ?? (_loadExpensesCommand = new RelayCommand(LoadExpenses));
            }
        }

        private void LoadExpenses()
        {
            UnitOfWork uow = new UnitOfWork();

            Clear();

            try
            {
                RepositoryExpense repositoryExpense = new RepositoryExpense(uow);

                uow.BeginTransaction();

                int userID = AuthorizationService.GetUserID();

                IEnumerable<Expense> expenses = repositoryExpense.LoadUserExpenses(userID);

                foreach (var expense in expenses)
                {
                    Add(expense.ToModel());
                }

                uow.Commit();
            }
            catch
            {
                uow.RollBack();
                throw;
            }
        }



        public void LoadGroupByTag(DateTime dateFrom, DateTime dateTo)
        {
            UnitOfWork uow = new UnitOfWork();

            Clear();

            try
            {
                RepositoryExpense repositoryExpense = new RepositoryExpense(uow);

                uow.BeginTransaction();

                int userID = AuthorizationService.GetUserID();

                IEnumerable<Expense> expenses = repositoryExpense.GetExpensesByTag(userID, dateFrom, dateTo);

                foreach (var expense in expenses)
                {
                    Add(expense.ToModel());
                }

                uow.Commit();
            }
            catch
            {
                uow.RollBack();
                throw;
            }
        }
    }
}
