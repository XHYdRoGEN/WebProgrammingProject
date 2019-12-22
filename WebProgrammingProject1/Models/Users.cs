using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProgrammingProject1.Models
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }
        [Required(ErrorMessage ="Bu alan zorunludur !")]
        [Display(Name ="Ad")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Bu alan zorunludur !")]
        [Display(Name = "Kullanıcı Adı")]
        public string UserNickName { get; set; }
        [Required(ErrorMessage = "Bu alan zorunludur !")]
        [Display(Name = "Soyad")]
        public string UserSurname { get; set; }
        [Required(ErrorMessage = "Bu alan zorunludur !")]
        [Display(Name = "Email")]
        public string UserEmail { get; set; }
        [Required(ErrorMessage = "Bu alan zorunludur !")]
        [Display(Name = "Şifre")]
        public string UserPassword { get; set; }
        [Display(Name = "Admin Mi?")]
        public bool isAdmin { get; set; }
        public ICollection<Posts> Posts { get; set; }
        public ICollection<Comments> Comments { get; set; }
        public ICollection<Likes> Likes { get; set; }

    }
}
