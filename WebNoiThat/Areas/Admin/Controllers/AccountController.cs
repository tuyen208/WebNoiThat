using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebNoiThat.Areas.Admin.Model;
using WebNoiThat.Models;
using System.Data.Entity;
using System.Security.Principal;
using System.Reflection;

namespace WebNoiThat.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        // GET: Admin/Account
        private readonly QLNoiThat _context;
        public AccountController()
        {
            _context = new QLNoiThat();
        }

    }
}