using System.Linq;
using DAL.Entities;

namespace DAL.Repository
{
    public interface IRepositoryUser : IRepository<User>
    {
        bool Registration(string fio, string login, string password);
        bool IsValidLogin(string login);
        int LogIn(string login, string password);
    }
}