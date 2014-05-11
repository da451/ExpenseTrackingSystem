using System.Collections.Generic;
using System.Linq;
using DAL.Entities;

namespace DAL.Repository
{
    public interface IRepositoryTag : IRepository<Tag>
    {
        int CreateTag(string name, int userID);
        IEnumerable<Tag> LoadUserTags(int userID);
    }
}