using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions;
using WebformsCms.Domain;

namespace WebformsCms.Data
{
    public interface IMenuRepository
    {
         List<Domain.Menus> GetAllParentMenus();
    }

    public class MenusRepository : Repository<Domain.Menus>, IMenuRepository
    {
        public MenusRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<Menus> GetMenusByParentControlId(string id)
        {
            var p = Predicates.Field<Domain.Menus>(m => m.ParentControlId, Operator.Eq, id);
            return unitOfWork.Connection.GetList<Domain.Menus>(p, null, unitOfWork.Transaction).ToList();
        }

        public List<Domain.Menus> GetAllParentMenus()
        {
            var p = Predicates.Field<Domain.Menus>(m => m.ParentId, Operator.Eq, null);
            return unitOfWork.Connection.GetList<Domain.Menus>(p, null, unitOfWork.Transaction).ToList();
        }
    }
}
