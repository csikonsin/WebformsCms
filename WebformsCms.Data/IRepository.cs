using System.Collections.Generic;
using WebformsCms.Domain;

namespace WebformsCms.Data
{
    public interface IRepository<T> where T:Domain.Entity
    {
        T GetSingle(int id);

        IEnumerable<T> GetAll();

        int Save(T entity);

        void Delete(T entity);
        void Delete(int entityId);
    }
}