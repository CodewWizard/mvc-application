using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuisnessLayer;
using LoginForm.Models;

namespace LoginForm.Controllers
{
    public class UserController : Controller
    {
        public UserContext db = new UserContext();
        public ActionResult Index()
        {
            return View();
        }
        // GET: User

        [HttpGet]
        [ActionName("Register")]
        public ActionResult Register_Get()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Register")]
        public ActionResult Register_Post(User1 user)
        {
            UserBuisnessLayer userBuisnessLayer = new UserBuisnessLayer();

            if (ModelState.IsValid)
            {
                 if (db.Users.Any(u => u.UserName == user.UserName))
                {
                    ModelState.AddModelError("UserName","Username already taken.");
                    return View(user);
                }

                if (db.Users.Any(u => u.Id == user.Id))
                {
                    ModelState.AddModelError("Id","Id Should be Unique.");
                    return View(user);
                }
                
                    userBuisnessLayer.AddUser(user);
                    return RedirectToAction("Welcome","User");
            }
            return View();
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User1 details)
        {
            using(UserContext db = new UserContext())
            {
                var user = db.Users.FirstOrDefault(u => u.UserName == details.UserName && u.Password == details.Password);
                if(user  != null)
                {
                    Session["Id"] = user.Id.ToString();
                    Session["Name"] = user.Name.ToString();
                    if(user.UserType == "Admin")
                    {
                        return RedirectToAction("AdminDash", "Dash");
                    }
                    else
                    {
                        return RedirectToAction("UserDash", "Dash");
                    }
                }
                ModelState.AddModelError("", "Invalid Username or Password");
                return View();
            }
        }

        public ActionResult Welcome()
        {
     
            return View();
        }

    }
}


  