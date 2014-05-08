using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Entities;
using DAL.Mapping;
using ExpenseTrackingSystem.Model;
using ExpenseTrackingSystem.Util;

namespace ExpenseTrackingSystem.Extensions
{
    public static class ModelsExtensions
    {
        public static Tag ToEntity(this TagModel model)
        {
            return ConvertingUtil.ToEntities(model);
        }

    }
}
