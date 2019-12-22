using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProgrammingProject1.Models
{
    public class Categories
    {
        [Key]
        public int CategoryID { get; set; }
        [Required(ErrorMessage ="Bu alan zorunludur !")]
        [Display(Name ="Kategori Adı")]
        public string CategoryName { get; set; }

        public virtual ICollection<Posts> Posts { get; set; }


    }
}
