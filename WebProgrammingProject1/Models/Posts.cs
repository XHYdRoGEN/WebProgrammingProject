using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProgrammingProject1.Models
{
    public class Posts
    {
        [Key]
        public int PostID { get; set; }
        [Required(ErrorMessage ="Bu alan zorunludur !")]
        [Display(Name ="Başlık")]
        public string PostTitle { get; set; }
        [Required(ErrorMessage ="Bu alan zorunludur !")]
        [Display(Name = "Resim URL")]
        public string PostThumbnailUrl { get; set; }
        [Required(ErrorMessage ="Bu alan zorunludur !")]
        [Display(Name = "İçerik")]
        [MaxLength(60,ErrorMessage ="Paylaşım içeriğiniz 60 karakteri geçemez !")]
        public string PostContent { get; set; }
        [Display(Name = "Favori Sayısı")]
        public int PostLikeCount { get; set; }
        [Display(Name = "Yorum Sayısı")]
        public int PostCommentCount { get; set; }

        public Users User { get; set; }
        [Display(Name = "Kategori")]
        [Required(ErrorMessage ="Bu alan zorunludur !")]
        public Categories Category { get; set; }
        public ICollection<Likes> Likes { get; set; }
        public ICollection<Comments> Comments { get; set; }
    }
}
