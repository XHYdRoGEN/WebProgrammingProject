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
    public class EditCategory : PageModel
    {
        private readonly WebProgrammingProject1.Context.ShareItContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        public EditCategory(WebProgrammingProject1.Context.ShareItContext context, IHttpContextAccessor _httpContextAccessor)
        {
            _context = context;
            this._httpContextAccessor = _httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
        }

        [BindProperty]
        public Categories Categories { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (_session.Get("admin") == null)
            {
                return RedirectToPage("/Login/Login");
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }

                Categories = await _context.Categories.FirstOrDefaultAsync(m => m.CategoryID == id);

                if (Categories == null)
                {
                    return NotFound();
                }
                return Page();
            }
            
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (_session.Get("admin") == null)
            {
                return RedirectToPage("/Login/Login");
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                _context.Attach(Categories).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriesExists(Categories.CategoryID))
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
            
        }

        private bool CategoriesExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryID == id);
        }
    }
}
