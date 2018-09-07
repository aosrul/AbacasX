using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbacasXData.Contracts
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
