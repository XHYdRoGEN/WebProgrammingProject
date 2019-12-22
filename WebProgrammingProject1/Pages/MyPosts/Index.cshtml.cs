using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebProgrammingProject1.Context;
using WebProgrammingProject1.Models;

namespace WebProgrammingProject1
{
    public class MyPosts : PageModel
    {
        private readonly WebProgrammingProject1.Context.ShareItContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        public MyPosts(WebProgrammingProject1.Context.ShareItContext context, IHttpContextAccessor _httpContextAccessor)
        {
            _context = context;
            this._httpContextAccessor = _httpContextAccessor;
            _session = this._httpContextAccessor.HttpContext.Session;
        }

        public IList<Posts> Posts { get;set; }

        public IActionResult OnGetAsync()
        {
            if(_session.Get("id") == null) {
                return RedirectToPage("/Login/Login");
            }
            else
            {
                int userid = BitConverter.ToInt32(_session.Get("id"));
                Posts =  _context.Posts.Where(x => x.User.UserID == userid).ToList();
                return Page();
            }
        }
    }
}
