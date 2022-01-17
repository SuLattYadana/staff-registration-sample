using StaffRegistrationWebApp.Data;
using StaffRegistrationWebApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StaffRegistrationWebApp.Repository
{
    public class AccountRepository
    {
        private AppDBContext _ctx;
        public AccountRepository(AppDBContext ctx)
        {
            _ctx = ctx;
        }
        public AccountRepository()
        {
        }

        public IQueryable<Accounttb> GetStaff()
        {
            return _ctx.Accounts.AsQueryable();
        }
        public Accounttb login(Accounttb accountObj)
        {
            var accObj = _ctx.Accounts.ToList();
            Accounttb obj = _ctx.Accounts.FirstOrDefault(a => a.UserName == accountObj.UserName && a.Password == accountObj.Password);
            return obj;
        }

        public Accounttb getAccountData(string username)
        {
            Accounttb result = GetStaff().FirstOrDefault(a => a.UserName == username);
            return result;
        }
    }
}