using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebProgrammingProject1.Context;
using WebProgrammingProject1.CustomAttributes;
using WebProgrammingProject1.Models;

namespace WebProgrammingProject1
{
    public class LoginModel : PageModel
    {
        private readonly ShareItContext context;

        public Users User;
        public LoginModel(ShareItContext _context)
        {
            context = _context;
        }
        public void OnGet()
        {
            
        }
        
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                string username = HttpContext.Request.Form["User.UserName"];
                string password = HttpContext.Request.Form["User.UserPassword"];
                User = context.Users.Where(x => x.UserNickName == username && x.UserPassword == password).FirstOrDefault();
                if (User != null)
                {
                    byte[] bytes = Encoding.ASCII.GetBytes(User.UserName);
                    byte[] id = BitConverter.GetBytes(User.UserID);

                    if (User.isAdmin == true)
                    {
                        HttpContext.Session.Set("admin", bytes);
                    }
                    HttpContext.Session.Set("user", bytes);
                    HttpContext.Session.Set("id", id);
                    return RedirectToPage("/Posts/Posts");
                }
                else
                {
                    return Page();
                }
            }

            return Page();
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("id");
            HttpContext.Session.Remove("user");
            HttpContext.Session.Remove("admin");

            return RedirectToPage("/Index");
        }


    }
}