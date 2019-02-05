using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContosoUniversity.Controllers
{
    public class LoginController : Controller
    {
        private SchoolContext db = new SchoolContext();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserAccount person)
        {
            if(ModelState.IsValid)
            {
                Person user = db.Students.SingleOrDefault(u => u.UserName == person.UserName && u.Password == person.Password);

               if (user != null)
                {
                    Session["ID"] = user.ID;
                    return RedirectToAction("SessionUser", "Login", new { id = user.ID });
                }

                if (user == null)
                {
                    // It's not a student or the authentication is wrong
                    user = db.Instructors.SingleOrDefault(u => u.UserName == person.UserName && u.Password == person.Password);

                    if (user != null)
                    {
                        Session["ID"] = user.ID;
                        return RedirectToAction("SessionUser", "Login", new { id = user.ID });
                     
                    }

                    if (user == null)
                    {
                        ModelState.AddModelError("", "Login or Password is incorrect");
                    }
                }
            }
            return View();    
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SessionUser(int id)
        {
            Person user = db.People.FirstOrDefault(p => p.ID == id);
            return View(user);
        }
        
    } 
}