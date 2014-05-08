using DAL.Entities;
using FluentNHibernate.Mapping;

namespace DAL.Mapping
{
    public class ExpenseMap : ClassMap<Expense>
    {
        public ExpenseMap()
        {
            Table("EXPENSES");

            Id(x => x.ExpenseID).Column("EXPENSE_ID");

            Map(x => x.Date).Column("DATE");

            Map(x => x.Spend).Column("SPEND");

            Map(x => x.Comment).Column("COMMENT");

            References(x => x.User).Column("USER_ID");

            References(x => x.Tag).Column("TAG_ID");
        }
    }
}
