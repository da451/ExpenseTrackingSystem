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

        public static TagModel ToModel(this Tag model)
        {
            return ConvertingUtil.ToModel(model);
        }

        public static UserModel ToModel(this User model)
        {
            return ConvertingUtil.ToModel(model);
        }


    }
}
