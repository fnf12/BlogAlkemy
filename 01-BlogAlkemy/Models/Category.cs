using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _01_BlogAlkemy.Models
{
    public class Category
    {
        public int IdCategory { get; set; }
        [Required]
        [StringLength(50,
            ErrorMessage = "El nombre no debe tener mas de 50 caracteres")]
        public string Name { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
