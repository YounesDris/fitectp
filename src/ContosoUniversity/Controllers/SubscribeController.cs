using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContosoUniversity.Controllers
{
    public class SubscribeController : Controller
    {
        private SchoolContext db = new SchoolContext();

        public ActionResult SubscribeCourse(int? id)
        {
            var query1 = db.Courses.Select(c => c.CourseID);
            var query2 = db.Enrollments.Where(s => s.StudentID == id).Select(s => s.CourseID);
            var finalquery = query1.Except(query2);
            
            ViewBag.ApplyCourse = finalquery;

            TempData["studentID"] = id;

            return View();
        }


        [HttpPost]
        public ActionResult SubscribeCourse(int courseID, Grade grade)
        {
            if (TempData["studentID"] == null)
            {
                return View();
            }

            int id = int.Parse(TempData["studentID"].ToString());

            db.Enrollments.Add(new Enrollment { StudentID = id, CourseID = courseID, Grade = grade });
            db.SaveChanges();
            ViewBag.Message = "Subscription successful !";
            return RedirectToAction("SessionStudent", "Login", new { id });
        }
    }
}