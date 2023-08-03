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
                if (ModelState.IsValid)
                {
                    // Create a new User entity based on the user input
                    var newUser = new User1
                    {
                        Name = user.Name,
                        DOB = user.DOB,
                        UserName = user.UserName,
                        Password = user.Password,
                        Gender = user.Gender,
                        UserType = user.UserType
                    };

                    // Add the new user entity to the context
                    db.Users.Add(newUser);

                    // Save changes to the database
                    db.SaveChanges();

                    ModelState.Clear();
                    TempData["Success"] = "Registration Successful! You can now login";

                    return RedirectToAction("Login");
                }
            

            // If ModelState is not valid, return the view with validation errors
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


  