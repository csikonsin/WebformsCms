using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebformsCms.Data
{
    public class UsersRepository : Repository<Domain.Users>
    {
        public UsersRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
