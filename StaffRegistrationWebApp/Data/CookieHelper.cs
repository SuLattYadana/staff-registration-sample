using StaffRegistrationWebApp.Data.Models;
using StaffRegistrationWebApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StaffRegistrationWebApp.Data
{
    public static class CookieHelper
    {
        public static string CookieName = "mc";

        public static int getAccountID()
        {
            return Convert.ToInt32(getAccountData().id);
        }
        public static string getAccountName()
        {
            return getAccountData().UserName;
        }

        public static Accounttb getAccountData()
        {
            Accounttb account = new Accounttb();
            var authUser = HttpContext.Current.User;
            var authIdentityUser = HttpContext.Current.User.Identity;
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var Cookie = HttpContext.Current.Request.Cookies[CookieName];
                if (Cookie != null)
                {

                    account.id = Convert.ToInt32(Cookie["ID"]);
                    account.UserName = HttpUtility.UrlDecode(Cookie["UserName"]);

                    return account;
                }
                else
                {
                    fillCookie();
                    var Cookie2 = HttpContext.Current.Request.Cookies[CookieName];
                    if (Cookie2 != null)
                    {
                        account.id = Convert.ToInt32(Cookie2["ID"]);
                        account.UserName = HttpUtility.UrlDecode(Cookie2["UserName"]);



                    }
                    return account;
                }
            }
            else
            {
                return account;
            }
        }

        public static void fillCookie()
        {
            AccountRepository accountRepo = new AccountRepository();
            string username = HttpContext.Current.User.Identity.Name;
            Accounttb account = accountRepo.getAccountData(username);
            SetCookie(Convert.ToInt32(account.id), account.Name, account.UserName);
        }

        private static void SetCookie(int ID, string Name, string Username)
        {
            HttpCookie myCookie = HttpContext.Current.Request.Cookies["mc"] ?? new HttpCookie("mc");
            myCookie.Values["ID"] = ID.ToString();
            myCookie.Values["Name"] = HttpUtility.UrlEncode(Name.ToString());
            myCookie.Values["UserName"] = HttpUtility.UrlEncode(Username.ToString());
            myCookie.Expires = getLocalTime(DateTime.UtcNow).AddDays(1);
            HttpContext.Current.Response.Cookies.Add(myCookie);
        }



        public static DateTime getLocalTime(this DateTime utc)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(utc, TimeZoneInfo.FindSystemTimeZoneById("Myanmar Standard Time"));
        }
    }
}