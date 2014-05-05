using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repository
{
    public interface IRepository<T> where T : IPrivateKey
    {
        T Get(int id);

        T Load(int id);

        void Save(T value);

        void Update(T value);

        void Delete(T value);

        IQueryable<T> GetAll();

    }
}
