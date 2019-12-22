using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebProgrammingProject1.Context;
using WebProgrammingProject1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using WebProgrammingProject1.Pages.Posts;
using Microsoft.AspNetCore.Mvc.Localization;
using WebProgrammingProject1.Resources;

namespace WebProgrammingProject1
{
    public class PostsModel : PageModel
    {
        private readonly ShareItContext context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        //private IStringLocalizer<SharedResources> sharedResources;

        public bool isAuth = false;

        public Posts post;
        public List<Posts> Posts { get; set; }
        public Users ActiveUser;
        public List<Likes> Likes;
        public List<Categories> Categories;
        public PostsModel(ShareItContext _context, IHttpContextAccessor _httpContextAccessor/*, IStringLocalizer<SharedResources> sharedResources*/)
        {
            this._httpContextAccessor = _httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
            context = _context;
            //this.sharedResources = sharedResources;
        }

        public IActionResult OnGet(int? categoryId=null)
        {
            //messageDEneme = localizer["Bir değer girin"];
            if (_session.Get("user") == null)
            {
                isAuth = false;
                return RedirectToPage("/Login/Login");
            }
            else
            {
                isAuth = true;
                byte[] id = _session.Get("id");
                int UserId = BitConverter.ToInt32(id);
                ActiveUser = this.context.Users.Where(x => x.UserID == UserId).FirstOrDefault();
                Likes = this.context.Likes.Include(x=>x.Post).Where(x => x.User.UserID == UserId).ToList();
                if(categoryId == null) { 
                Posts = context.Posts.ToList();
                }
                else
                {
                    Categories cat = new Categories();
                    cat = context.Categories.Include(c=>c.Posts).Where(x => x.CategoryID == categoryId).FirstOrDefault();
                    Posts = cat.Posts.ToList();
                }
                Categories = this.context.Categories.ToList();
            }

            return Page();
        }

        public JsonResult OnGetAddFavorite(int favorite,int postId)
        {
            try
            {
                Users user = new Users();
                user = this.context.Users.Where(x => x.UserID == BitConverter.ToInt32(_session.Get("id"))).FirstOrDefault();
                if (favorite == 1)
                {
                    Likes like = new Likes();
                    like.Post = this.context.Posts.Where(x => x.PostID == postId).FirstOrDefault();
                    like.User = user;
                    this.context.Likes.Add(like);

                    Posts post = (from p in context.Posts where p.PostID == postId select p).FirstOrDefault();
                    post.PostLikeCount++;
                }
                else
                {
                    Likes like = new Likes();
                    like = this.context.Likes.Where(x => x.User.UserID == user.UserID).Where(x => x.Post.PostID == postId).First();
                    this.context.Likes.Remove(like);
                    Posts post = (from p in context.Posts where p.PostID == postId select p).FirstOrDefault();
                    post.PostLikeCount--;
                }
                this.context.SaveChanges();
                Likes = this.context.Likes.Where(x => x.User.UserID == user.UserID).ToList();
                return new JsonResult(true);

            }
            catch (Exception)
            {
                return new JsonResult(false);
            }
        }

        public IActionResult OnGetShowAll(int PostId)
        {
            post = this.context.Posts.Include(c=>c.Comments).Include(c=>c.User).Where(x => x.PostID == PostId).FirstOrDefault();
            post.Comments = this.context.Comments.Include(x=>x.User).Include(x=>x.Post).Where(x => x.Post.PostID == post.PostID).ToList();
            return Partial("ShowAllModal",post);
        }

        public IActionResult OnGetComment(int postId, string comment)
        {
            post = this.context.Posts.Include(c=>c.Comments).Include(c=>c.User).Where(x => x.PostID == postId).FirstOrDefault();
            Users user = new Users();
            user = this.context.Users.Include(c=>c.Comments).Where(x => x.UserID == BitConverter.ToInt32(_session.Get("id"))).FirstOrDefault();
            Comments cm = new Comments();
            cm.CommentContent = comment;
            cm.User = user;
            cm.Post = post;
            post.Comments.Add(cm);
            post.PostCommentCount++;
            this.context.SaveChanges();
            post.Comments = this.context.Comments.Include(x => x.User).Include(x => x.Post).Where(x => x.Post.PostID == post.PostID).ToList();

            return Partial("ShowAllModal", post);
        }
    }
}