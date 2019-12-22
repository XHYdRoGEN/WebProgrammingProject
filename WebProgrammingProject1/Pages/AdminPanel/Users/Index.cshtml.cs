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
    public class UsersModel : PageModel
    {
        private readonly WebProgrammingProject1.Context.ShareItContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        public UsersModel(WebProgrammingProject1.Context.ShareItContext context, IHttpContextAccessor _httpContextAccessor)
        {
            _context = context;
            this._httpContextAccessor = _httpContextAccessor;
            this._session = this._httpContextAccessor.HttpContext.Session;
        }

        public IList<Users> Users { get;set; }

        public IActionResult OnGetAsync()
        {
            if (this._session.Get("admin") != null) { 
                Users =  _context.Users.ToList();
                return Page();
            }
            else
            {
                return RedirectToPage("/Login/Login","/AdminPanel/Users/Index");
            }
        }
    }
}
