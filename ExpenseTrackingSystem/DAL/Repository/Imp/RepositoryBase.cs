using System;
using System.Linq;
using DAL.Entities;
using NHibernate.Linq;

namespace DAL.Repository.Imp
{
    public class RepositoryBase<T> : IRepository<T> where T : class, IPrivateKey 
    {
        protected UnitOfWork _unitOfWork;

        public RepositoryBase(IUoW uow)
        { 
            _unitOfWork = uow  as UnitOfWork;

            if (_unitOfWork == null)
            {
                throw new ArgumentException("Not correct implimitation of IUoW");
            }
        }

        #region IRepository<T> Members
        
        public T Get(int id)
        {
            return _unitOfWork.Session.Get<T>(id);
        }

        public T Load(int id)
        {
            return _unitOfWork.Session.Load<T>(id);
        }

        public int Save(T value)
        {
            return (int)_unitOfWork.Session.Save(value);
        }

        public void Update(T value)
        {
            _unitOfWork.Session.Update(value);
        }

        public void Delete(T value)
        {
            _unitOfWork.Session.Delete(value);
        }

        public IQueryable<T> GetAll() 
        {

            return _unitOfWork.Session.Query<T>();
        }

        #endregion
    }
}
