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
    public class StudentsController : ApiController
    {
        private SchoolContext db = new SchoolContext();

        // GET: api/Students
        public IQueryable<Student> GetPeople()
        {
            return db.Students;
        }

        // GET: api/Students/5
        [ResponseType(typeof(Student))]
        public IHttpActionResult GetStudent(int id)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            Dictionary<string, object> listInfos = new Dictionary<string, object>();

            List<string> ListeID = new List<string>();

            listInfos.Add("id :", student.ID);
            listInfos.Add("lastname :", student.LastName);
            listInfos.Add("firstname :", student.FirstMidName);
            listInfos.Add("enrollment date :", student.EnrollmentDate.ToString());
            listInfos.Add("enrollments :", student.Enrollments);

            foreach (var item in student.Enrollments)
            {
                ListeID.Add("CourseId :" + item.CourseID);
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

        private bool StudentExists(int id)
        {
            return db.People.Count(e => e.ID == id) > 0;
        }
    }
}