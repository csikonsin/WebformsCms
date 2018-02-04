using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebformsCms.Domain
{
    public class Websites : Entity
    {
        [Required]
        [MaxLength(200)]
        public String Name { get; set; }

        [MaxLength(200)]
        public String Title { get; set; }
    }
}
