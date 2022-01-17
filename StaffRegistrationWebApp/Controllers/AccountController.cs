using StaffRegistrationWebApp.Data;
using StaffRegistrationWebApp.Data.Models;
using StaffRegistrationWebApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace StaffRegistrationWebApp.Controllers
{
    public class AccountController : Controller
    {
        AppDBContext ctx;
        AccountRepository accountRepo = null;

        public AccountController()
        {
            ctx = new AppDBContext();
            accountRepo = new AccountRepository(ctx);
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Accounttb account)
        {

            var accObj = accountRepo.login(account);
            if (accObj != null)
            {
                FormsAuthentication.SetAuthCookie(accObj.UserName, false);

                SetCookie(accObj.id, accObj.Name, accObj.UserName);
                return RedirectToAction("Index", "Staff");
            }
            else
            {
                ViewBag.Status = "Unauthorize";
                return RedirectToAction("Index", "Account");
            }
        }

        public ActionResult LogOut()
        {
            DeleteCookie();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Account");
        }
        public void DeleteCookie()
        {
            if (Request.Cookies["mc"] != null)
            {
                HttpCookie myCookie = new HttpCookie("mc");
                myCookie.Expires = CookieHelper.getLocalTime(DateTime.UtcNow).AddDays(-1);
                Response.Cookies.Add(myCookie);
            }
        }

        public void SetCookie(int ID, string Name, string Username)
        {
            HttpCookie myCookie = HttpContext.Request.Cookies["mc"] ?? new HttpCookie("mc");
            myCookie.Values["ID"] = ID.ToString();
            myCookie.Values["Name"] = HttpUtility.UrlEncode(Name.ToString());
            myCookie.Values["UserName"] = HttpUtility.UrlEncode(Username.ToString());
            myCookie.Expires = CookieHelper.getLocalTime(DateTime.UtcNow).AddDays(1);
            HttpContext.Response.Cookies.Add(myCookie);
        }
    }
}