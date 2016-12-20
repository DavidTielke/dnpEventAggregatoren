using System.Linq;
using DataClasses;

namespace DataStoring.Contract
{
    public interface IRepository<TEntity>
        where TEntity : IIdentifyableEntity
    {
        void Insert(TEntity item);
        void Update(TEntity item);
        void Delete(int id);
        IQueryable<TEntity> Query { get; }
    }
}