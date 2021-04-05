using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer
{
    interface IRelationshipRepo<TEntity> where TEntity : class
    {
        int Attach(Guid idOfParent, Guid idOfentityToBeAdded);

        int Detach(Guid idOfParent, Guid idOfentityToBeRemoved);

        ICollection<TEntity> GetAllbyParent(Guid idOfParent);

        
    }
}
