using DAL.Entities;

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

    }
}
