using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebProgrammingProject1.Context;
using WebProgrammingProject1.Models;

namespace WebProgrammingProject1
{
    public class CreateUsers : PageModel
    {
        private readonly WebProgrammingProject1.Context.ShareItContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;

        public CreateUsers(WebProgrammingProject1.Context.ShareItContext context, IHttpContextAccessor _httpContextAccessor)
        {
            _context = context;
            this._httpContextAccessor = _httpContextAccessor;
            this._session = this._httpContextAccessor.HttpContext.Session;
        }

        public IActionResult OnGet()
        {
            if (_session.Get("admin") != null)
            {
                return Page();

            }
            else
            {
                return RedirectToPage("/Login/Login");
            }
        }

        [BindProperty]
        public Users Users { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (_session.Get("admin") != null)
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                _context.Users.Add(Users);
                if(Users.isAdmin == true) { 
                Admins admin = new Admins();
                admin.AdminName = Users.UserName;
                admin.AdminUsername = Users.UserNickName;
                admin.AdminPassword = Users.UserPassword;
                _context.Admins.Add(admin);
                }
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            else
            {
                return RedirectToPage("/Login/Login");
            }
        }
    }
}
