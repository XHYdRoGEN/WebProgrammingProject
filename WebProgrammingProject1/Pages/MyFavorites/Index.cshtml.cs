using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebProgrammingProject1.Context;
using WebProgrammingProject1.Models;

namespace WebProgrammingProject1
{
    public class MyFavorites : PageModel
    {
        private readonly ShareItContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        public List<Posts> posts;
        public List<Likes> Likes;
        public Users ActiveUser;

        public MyFavorites(ShareItContext context, IHttpContextAccessor _httpContextAccessor)
        {
            this._context = context;
            this._httpContextAccessor = _httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
        }
        public IActionResult OnGet()
        {
            if (_session.Get("id") == null)
            {
                return RedirectToPage("/Login/Login");
            }
            else
            {
                int UserId = BitConverter.ToInt32(_session.Get("id"));
                posts = this._context.Likes.Where(x => x.User.UserID == UserId ).Select(x => x.Post).ToList();
                Likes = this._context.Likes.Where(x => x.User.UserID == UserId).ToList();
                ActiveUser = this._context.Users.Where(x => x.UserID == UserId).FirstOrDefault();

                return Page();
            }
        }
    }
}