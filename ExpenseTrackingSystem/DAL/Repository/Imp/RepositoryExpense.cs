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



    }
}
