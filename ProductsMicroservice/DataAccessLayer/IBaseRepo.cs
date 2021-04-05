using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer
{
    interface IBaseRepo<TEntity> where TEntity : class
    {
        TEntity GetById(Guid id);

        int Insert(TEntity entity);

        int Delete(Guid id);

        ICollection<TEntity> GetAll();
    }
}
