using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _01_BlogAlkemy.Models.ViewModel
{
    public class PostModel
    {
        public int IdPost { get; set; }
        [Required]
        public int IdCategory { get; set; }
        [Required]
        [StringLength(maximumLength: 100,
           ErrorMessage = "El titulo no debe tener mas de 100 caracteres")]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual Category Category { get; set; }
    }
}
