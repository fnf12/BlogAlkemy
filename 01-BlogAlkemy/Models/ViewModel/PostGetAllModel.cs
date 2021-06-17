using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _01_BlogAlkemy.Models.ViewModel
{
    public class PostGetAllModel
    {
        public int IdPost { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual Category Category { get; set; }
    }
}
