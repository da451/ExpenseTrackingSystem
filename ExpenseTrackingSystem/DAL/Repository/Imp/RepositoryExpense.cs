using System;
using System.Collections;
using System.Collections.Generic;
using DAL.Entities;
using NHibernate.Criterion;

namespace DAL.Repository.Imp
{
    public class RepositoryExpense: RepositoryBase<Expense>
    {
        public RepositoryExpense(UnitOfWork uow) : base(uow)
        {
        }

        public IEnumerable<Expense> LoadUserExpenses(int userID)
        {
            return _unitOfWork.Session.CreateCriteria<Expense>().Add(Restrictions.Eq("User.UserID", userID)).List<Expense>();
        }

        public IEnumerable<Expense> GetExpensesByTag(int userID,DateTime dateFrom, DateTime dateTo)
        {
            IList expensesByTag = _unitOfWork.Session.CreateCriteria<Expense>()
                .SetProjection(Projections.ProjectionList()
                    .Add(Projections.GroupProperty("Tag"))
                    .Add(Projections.GroupProperty("User"))
                    .Add(Projections.Sum("Spend")))
                .Add(Restrictions.And(Restrictions.Eq("User.UserID", userID),
                                      Restrictions.Between("Date",dateFrom,dateTo))).List();

            List<Expense> expenses = new List<Expense>();

            foreach (var expenseByTag in expensesByTag)
            {
                var elements = expenseByTag as object[];

                expenses.Add(new Expense()
                {
                    Tag = elements[0] as Tag,
                    User = elements[1] as User,
                    Spend = (float)elements[2]
                });
            }

            return expenses;
        }



    }
}
