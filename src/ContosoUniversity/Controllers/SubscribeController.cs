﻿using ContosoUniversity.DAL;
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
            // Selectionner l'ensemble des cours dispensés au sein de l'université.
            var query1 = db.Courses.Select(c => c.CourseID);

            // Selectionner l'ensemble des cours auxquels l'étudiant est inscrit.
            var query2 = db.Enrollments.Where(s => s.StudentID == id).Select(s => s.CourseID);

            // Selectionner l'ensemble des cours auxquels l'étudiant peut postuler.
            var finalquery = query1.Except(query2);

            // Selectionner l'ensemble des cours et leurs libellés auxquels l'étudiant peut postuler. 
            var available = db.Courses.Where(c => finalquery.Contains(c.CourseID)).Select(c => new { c.CourseID, c.Title });

            // Ce ViewBag permet de partager les cours et leurs libellés du contrôleur à la vue.
            ViewBag.ApplyCourse = available;

            // Ce tempData permet de passer l'ID à l'action suivante. 
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

            db.Enrollments.Add(new Enrollment { StudentID = id, CourseID = courseID });
            db.SaveChanges();
            ViewBag.Message = "Subscription successful !";
            return RedirectToAction("SessionStudent", "Login", new { id });
        }
    }
}