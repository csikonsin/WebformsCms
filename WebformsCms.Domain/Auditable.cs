using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebformsCms.Domain
{
    public abstract class Entity
    {
        [Required]
        public int Id { get; set; }
    }

    public abstract class AuditableEntity : Entity
    {
        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public int CreatedById { get; set; }

        public DateTime? ModifiedAt { get; set; }
        public int? ModifiedById { get; set; }
    }
}
