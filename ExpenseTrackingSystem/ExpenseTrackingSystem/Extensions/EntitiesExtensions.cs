using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Entities;
using ExpenseTrackingSystem.Model;
using ExpenseTrackingSystem.Util;

namespace ExpenseTrackingSystem.Extensions
{
    public static class EntitiesExtensions
    {
        public static ExpenseModel ToModel(this Expense model)
        {
            return ConvertingUtil.ToModel(model);
        }
    }
}
