using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;

namespace ContosoUniversity.Controllers.api
{
    public class InstructorsController : ApiController
    {
        private SchoolContext db = new SchoolContext();

        // GET: api/Instructors
        public IQueryable<Instructor> GetPeople()
        {
            return db.Instructors;
        }

        // GET: api/Instructors/5
        [ResponseType(typeof(Instructor))]
        public IHttpActionResult GetInstructor(int id)
        {
            Instructor instructor = db.Instructors.Find(id);
            if (instructor == null)
            {
                return NotFound();
            }

            Dictionary<string, object> listInfos = new Dictionary<string, object>();

            List<string> ListeID = new List<string>();

            listInfos.Add("id :", instructor.ID);
            listInfos.Add("Schedule :", instructor.Courses);


            foreach (var item in instructor.Courses)
            {
                ListeID.Add("CourseId :" + item.CourseID);
                ListeID.Add("StartDate :" + item.Department.StartDate);
            }

            return Ok(listInfos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InstructorExists(int id)
        {
            return db.People.Count(e => e.ID == id) > 0;
        }
    }
}