using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebformsCms.Domain;
using Dapper;
using DapperExtensions;
using DapperExtensions.Mapper;

namespace WebformsCms.Data
{

    public abstract class Repository<T> : IRepository<T> where T: Entity
    {
        
        protected IUnitOfWork unitOfWork = null;

        public Repository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Delete(int entityId)
        {
            var entity = GetSingle(entityId);
            Delete(entity);
        }

        public void Delete(T entity)
        {
            unitOfWork.Connection.Delete<T>(entity, unitOfWork.Transaction);
        }

        public IEnumerable<T> GetAll()
        {
            return unitOfWork.Connection.GetList<T>(null, null, unitOfWork.Transaction);
        }

        public T GetSingle(int id)
        {
            return unitOfWork.Connection.Get<T>(id, unitOfWork.Transaction);
        }

        public int Save(T entity)
        {
            if(entity.Id == 0)
            {
                return unitOfWork.Connection.Insert<T>(entity, unitOfWork.Transaction);
            }
            else
            {
                unitOfWork.Connection.Update<T>(entity, unitOfWork.Transaction);
                return entity.Id;
            }
        }
    }
}
