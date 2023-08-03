using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoginForm.Models;

namespace LoginForm.Controllers
{
    public class DashController : Controller
    {
    public UserContext db = new UserContext();
        // GET: Dash
        public ActionResult UserDash()
        {
            return View();
        }

        public ActionResult AdminDash()
        {
            var users = db.Users.ToList();
            return View(users);
        }
    }
}