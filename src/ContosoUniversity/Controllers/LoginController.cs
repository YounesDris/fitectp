using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                // L'étudiant a un compte et ses informations d'authentification sont justes alors cet etudiant est stocké dans une variable de type Person.
                Person user = db.Students.SingleOrDefault(u => u.UserName == person.UserName && u.Password == person.Password);

               // Création de deux sessions et redirection vers sa page de profil si un étudiant s'est identifié sans problèmes.
               if (user != null)
                {
                    Session["ID"] = user.ID;

                    // Pour les filtres paramétrés
                    Session["User"] = user;

                    return RedirectToAction("SessionStudent", "Login", new { id = user.ID });
                }

                // Si user est null alors ce n'est pas un étudiant ou ses informations d'authentification sont incorrects. 
                if (user == null)
                {
                    // L'instructeur a un compte et ses informations d'authentification sont justes alors cet instructeur est stocké dans une variable.
                    user = db.Instructors.SingleOrDefault(u => u.UserName == person.UserName && u.Password == person.Password);

                    // Création de deux sessions et redirection vers sa page de profil si un instructeur s'est identifié sans problèmes.
                    if (user != null)
                    {
                        Session["ID"] = user.ID;

                        // Pour les filtres paramétrés
                        Session["User"] = user;

                        return RedirectToAction("SessionInstructor", "Login", new { id = user.ID });
                     
                    }

                    // L'utilisateur n'a pas de compte ou ses informations d'authentifications sont erronées.
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

        [HttpGet]
        // Question: L'id peut-il être null ? Intérêt du nullable int ? 
        public ActionResult SessionStudent(int? id)
        {
            // Pour accéder à la vue liée au contrôleur SessionStudent, l'utilisateur doit s'authentifier préalablement donc l'ID se retrouve donc dans la route. 
            // Question: Quelle est l'utilité de cette condition ? (ReviewCode)
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student user = db.Students.FirstOrDefault(p => p.ID == id);

            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(user);
        }

        [HttpGet]
        // Question: L'id peut-il être null ? Intérêt du nullable int ? 
        public ActionResult SessionInstructor(int? id)
        {
            // Pour accéder à la vue liée au contrôleur SessionInstructor, l'utilisateur doit s'authentifier préalablement donc l'ID se retrouve donc dans la route. 
            // Question: Quelle est l'utilité de cette condition ? (ReviewCode)

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Instructor user = db.Instructors.FirstOrDefault(p => p.ID == id);

            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(user);
        }

    } 
}