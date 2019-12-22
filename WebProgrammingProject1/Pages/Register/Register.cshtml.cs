using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebProgrammingProject1.Context;
using WebProgrammingProject1.Models;

namespace WebProgrammingProject1
{
    public class RegisterModel : PageModel
    {
        private readonly ShareItContext context;
        public Users User;
        public RegisterModel(ShareItContext context)
        {
            this.context = context;
        }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            string name = HttpContext.Request.Form["User.UserName"];
            string password = HttpContext.Request.Form["User.UserPassword"];
            string username = HttpContext.Request.Form["User.UserNickName"];
            string email = HttpContext.Request.Form["User.UserEmail"];
            string surname = HttpContext.Request.Form["User.UserSurname"];
            Users usr = new Users();
            usr.UserEmail = email;
            usr.UserNickName = username;
            usr.UserName = name;
            usr.UserPassword = password;
            usr.UserSurname = surname;
            usr.isAdmin = false;
            try
            {
                this.context.Users.AddAsync(usr);
                this.context.SaveChanges();
                return RedirectToPage("/Login/Login");
            }
            catch (Exception)
            {

                throw;
            }
            return Page();
        }

    }
}