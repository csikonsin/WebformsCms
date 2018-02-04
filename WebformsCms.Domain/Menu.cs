using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebformsCms.Domain
{
    [Table("Menus")]
    public class Menus : AuditableEntity
    {
        public int? ParentId { get; set; }
        public string ParentControlId { get; set; }
        public string Name { get; set; }
        public string RelativeUrl { get; set; }

        public bool? Homepage { get; set; }

    }
}
