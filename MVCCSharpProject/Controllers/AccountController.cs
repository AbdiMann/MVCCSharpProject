using MVCCSharpProject.Models;
using MVCCSharpProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCCSharpProject.Controllers
{
    public class AccountController : Controller
    {

        Repository repository = new Repository();
   another space

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginForm loginForm)
        {
            if (ModelState.IsValid)
            {
                var user = repository.Authenticate(loginForm.UserName, loginForm.Password);
                if (user != null)
                {
                    Session["User"] = user;
                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["ErrorMessage"] = "Invalid Username or Password.";
                }
            }
            return View(loginForm);
        }




        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["User"] = null;
            TempData["SuccessMessage"] = "You have been logged out of the system.";
            return RedirectToAction("Login");
        }
    }
}
