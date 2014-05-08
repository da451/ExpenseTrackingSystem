using System;

namespace DAL.Entities
{
    public class Expense: IPrivateKey
    {
        public Expense()
        {
        }

        public Expense(int expenseId, float spend, string comment, DateTime date, User user, Tag tag)
        {
            ExpenseID = expenseId;
            Date = date;
            Spend = spend;
            Comment = comment;
            User = user;
            Tag = tag;
        }

        public virtual int ExpenseID { protected set; get; }

        public virtual DateTime Date { set; get; }

        public virtual string Comment { set; get; }

        public virtual float Spend { set; get; }

        public virtual User User { set; get; }

        public virtual Tag Tag { set; get; }

        public virtual int PrivateKey
        {
            get { return ExpenseID; }
        }
    }
}
