using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Entities;

namespace DAL.Repository
{
    public interface IRepositoryExpense :IRepository<Expense>
    {
        IEnumerable<Expense> LoadUserExpenses(int userID);
        IEnumerable<Expense> GetExpensesByTag(int userID, DateTime dateFrom, DateTime dateTo);
    }
}