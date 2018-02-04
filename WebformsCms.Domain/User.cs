using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebformsCms.Domain
{
    public class Users : AuditableEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
