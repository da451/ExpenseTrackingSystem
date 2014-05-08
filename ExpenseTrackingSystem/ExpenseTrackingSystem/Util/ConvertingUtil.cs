using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Entities;
using ExpenseTrackingSystem.Model;

namespace ExpenseTrackingSystem.Util
{
    public static class ConvertingUtil
    {
        #region Entities

        public static Expense ToEntities(ExpenseModel model)
        {
            User user = ToEntities(model.User);

            Tag tag = ToEntities(model.Tag);

            Expense resualt = new Expense(model.ExpenseID, model.Spend, model.Comment, model.Date, user, tag);

            return resualt;
        }

        public static Tag ToEntities(TagModel model)
        {
            User user = ToEntities(model.User);

            Tag resualt = new Tag(model.TagID, model.Name, user);
            
            return resualt;
        }

        public static User ToEntities(UserModel model)
        {
            User resualt = new User(model.UserID,model.FIO,model.Login, model.Password);

            resualt.Tags = ToEntities(model.Tags);

            return resualt;
        }

        public static IList<Tag> ToEntities(IList<TagModel> model)
        {
            IList<Tag> resualt = new List<Tag>();

            if (model != null)
            {
                resualt = model.Select(o => new Tag(o.TagID, o.Name, ToEntities(o.User))).ToList<Tag>();
            }

            return resualt;
        }

        #endregion


        #region Models

        public static ExpenseModel ToModel(Expense entity)
        {
            ExpenseModel resualt = new ExpenseModel()
            {
                Comment = entity.Comment,
                Date = entity.Date,
                ExpenseID = entity.ExpenseID,
                Spend = entity.Spend,
                Tag = ToModel(entity.Tag),
                User = ToModel(entity.User)
            };

            return resualt;
        }

        public static TagModel ToModel(Tag entity)
        {
            TagModel resualt = new TagModel()
            {
                Name = entity.Name,
                TagID = entity.TagID,
                User = ToModel(entity.User)
            };



            return resualt;
        }

        public static UserModel ToModel(User entity)
        {
            UserModel resualt = new UserModel()
            {
                FIO = entity.FIO,
                Login = entity.Login,
                Password = entity.Password,
                UserID = entity.UserID
            };

            return resualt;
        }

        #endregion

    }
}
