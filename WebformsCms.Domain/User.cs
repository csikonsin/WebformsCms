using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebformsCms.Domain
{
    public class Users : Entity
    {
        public string Username { get; set; }
        public string OwinId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
