using ContosoUniversity.DAL;
using ContosoUniversity.Models;
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

        public ActionResult LoginStudent()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult LoginStudent(Student student)
        {
            Student user = db.Students.SingleOrDefault(u => u.UserName == student.UserName && u.Password == student.Password);

            if (user != null)
            {
                // Storing UserName in a Session
                Session["UserName"] = user.UserName;

                // Storing ID in a Session
                Session["ID"] = user.ID;


                // Redirect to the home page 
                return RedirectToAction("SessionUser", "Login", new { id = user.ID });
            }
            else
            {
                ModelState.AddModelError("", "Login or Password is incorrect");
            }

            return View();
        }

        public ActionResult LoginInstructor()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginInstructor(Instructor instructor)
        {
            Instructor user = db.Instructors.SingleOrDefault(u => u.UserName == instructor.UserName && u.Password == instructor.Password);

            if (user != null)
            {
                // Storing UserName in a Session
                Session["UserName"] = user.UserName;

                // Storing ID in a Session
                Session["ID"] = user.ID;

                // Redirect to the home page 
                return RedirectToAction("SessionUser", "Login", new { id = user.ID });
            }
            else
            {
                ModelState.AddModelError("", "Login or Password is incorrect");
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