using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repository
{
    public interface IUoW
    {
        void BeginTransaction();

        void Commit();

        void RollBack();
    }
}
