using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebformsCms.Data
{
    public class WebsitesRepository : Repository<Domain.Websites>
    {
        public WebsitesRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
