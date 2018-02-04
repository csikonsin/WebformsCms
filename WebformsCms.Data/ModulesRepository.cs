using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions;
using Dapper;

namespace WebformsCms.Data
{
    public interface IModulesRepository
    {
        List<Domain.Modules> GetMenuModules(int menuId);
    }
    public class ModulesRepository : Repository<Domain.Modules>, IModulesRepository
    {
        public ModulesRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<Domain.Modules> GetMenuModules(int menuId)
        {
            var p = Predicates.Field<Domain.Modules>(m => m.MenuId, Operator.Eq, menuId);
            return unitOfWork.Connection.GetList<Domain.Modules>(p, null, unitOfWork.Transaction).ToList();
        }

        public int GetNextPosition(int menuId, int parentId)
        {

            object res = unitOfWork.Connection.Query<int?>("SELECT MAX(Position) FROM Modules WHERE MenuId=@MenuId AND ISNULL(ParentId,0) = @ParentId", new { MenuId = menuId, ParentId = parentId }, unitOfWork.Transaction).FirstOrDefault();
                  
            if(res == null)
            {
                return 100;
            }


            return (int)res + 100;

        }

        public int GetMenuIdFromModuleId(int moduleId)
        {            
            var module = GetSingle(moduleId);
            if (module == null) return 0;
            return module.MenuId;
        }
    }
}
