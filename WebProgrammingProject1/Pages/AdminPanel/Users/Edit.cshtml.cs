using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProgrammingProject1.Context;
using WebProgrammingProject1.Models;

namespace WebProgrammingProject1
{
    public class EditUsers : PageModel
    {
        private readonly WebProgrammingProject1.Context.ShareItContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;

        public EditUsers(WebProgrammingProject1.Context.ShareItContext context, IHttpContextAccessor _httpContextAccessor)
        {
            _context = context;
            this._httpContextAccessor = _httpContextAccessor;
            this._session = this._httpContextAccessor.HttpContext.Session;
        }

        [BindProperty]
        public Users Users { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (_session.Get("admin") != null)
            {
                if (id == null)
                {
                    return NotFound();
                }

                Users = await _context.Users.FirstOrDefaultAsync(m => m.UserID == id);

                if (Users == null)
                {
                    return NotFound();
                }
                return Page();
            }
            else
            {
                return RedirectToPage("/Login/Login");
            }
            
        }

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

                _context.Attach(Users).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(Users.UserID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToPage("./Index");
            }
            else
            {
                return RedirectToPage("/Login/Login");
            }
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.UserID == id);
        }
    }
}
