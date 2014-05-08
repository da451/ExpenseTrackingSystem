using System.Collections.Generic;
using DAL.Entities;
using NHibernate.Criterion;

namespace DAL.Repository.Imp
{
    public class RepositoryTag: RepositoryBase<Tag>
    {
        public RepositoryTag(UnitOfWork uow) : base(uow)
        {
        }

        public int CreateTag(string name, int userID)
        {
            User curentUser = GetUserByID(userID);

            Tag newTag = new Tag()
            {
                Name = name,
                User = curentUser
            };

            int tagID = Save(newTag);

            return tagID;
        }

        private User GetUserByID(int userID)
        {
            return new RepositoryUser(this._unitOfWork).Load(userID);
        }

        public IEnumerable<Tag> LoadUserTags(int userID)
        {
            return _unitOfWork.Session.CreateCriteria<Tag>().Add(Restrictions.Eq("User.UserID", userID)).List<Tag>();
        }


    }
}
