using System.Linq;
using DAL.Entities;

namespace DAL.Repository
{
    public interface IRepository<T> where T : IPrivateKey
    {
        T Get(int id);

        T Load(int id);

        int Save(T value);

        void Update(T value);

        void Delete(T value);

        IQueryable<T> GetAll();

    }
}
