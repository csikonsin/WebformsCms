using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebformsCms.Domain
{
    public class Modules : AuditableEntity
    {
        [Required]
        public int MenuId { get; set; }
        [Required]
        public int ModuleType { get; set; }
        public int? ParentId { get; set; }
        public int Position { get; set; }
    }
}
