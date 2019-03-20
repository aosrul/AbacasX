using System.Linq;

namespace AbacasX.DataContracts
{
    public interface IRepository<T,I> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(I Id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(I Id);
    }
}
