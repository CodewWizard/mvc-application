using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        [ValidateAntiForgeryToken]
        public ActionResult Register_Post(User1 user)
        {
            if (ModelState.IsValid)
            {
                if (db.Users.Any(u => u.UserName == user.UserName))
                {
                    ModelState.AddModelError("UserName", "UserName is already taken");
                    return View(user);
                }


                User1 newUser = new User1
                {
                    Name = user.Name,
                    DOB = user.DOB,
                    Gender = user.Gender,
                    UserName = user.UserName,
                    Password = user.Password,
                    UserType = user.UserType
                };

                db.Users.Add(newUser);
                db.Entry(newUser).Reload();
                db.SaveChanges();
                ModelState.Clear();
                TempData["Success"] = "Registration Successfull !  You can now login";
                return RedirectToAction("Login","User");
            }
            return View(user);
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
                ModelState.AddModelError("", "Invalid UserName or password");
                return View();
            }
        }

        

    }
}


  