using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment3.Models;

namespace Assignment3.Controllers
{
    public class ClassController : Controller
    {
        // GET: Class
        public ActionResult Index()
        {
            return View();
        }
        //GET: /Class/List
        public ActionResult List(string SearchKey = null) // optional
        {
            ClassDataController controller = new ClassDataController();
            IEnumerable<Class> Classes = controller.ListClasses(SearchKey);
            return View(Classes);
        }

        //GET: /Class/Show/{id}

        public ActionResult Show(int id)
        {
            ClassDataController controller = new ClassDataController();


            Class NewClass = controller.FindClass(id);
            Teacher SelectedTeacher = controller.FindTeacherfromclass(id);

            ViewBag.SelectedTeacher = SelectedTeacher;

            return View(NewClass);
        }

        /// <summary>
        /// Routes to a dynamically generated "Teacher Update" Page. Gathers information from the database.
        /// </summary>
        /// <param name="id">Id of the Teacher</param>
        /// <returns>A dynamic "Update Teacher" webpage which provides the current information of the teacher and asks the user for new information as part of a form.</returns>
        /// <example>GET : /Teacher/New/5</example>
        public ActionResult New(int id)
        {
            ClassDataController controller = new ClassDataController();

            Class NewClass = controller.FindClass(id);
            Teacher SelectedTeacher = controller.FindTeacherfromclass(id);

            ViewBag.SelectedTeacher = SelectedTeacher;

            return View(NewClass);
        }



        /// <summary>
        /// Receives a POST request containing information about an existing teacher in the system, with new values. Conveys this information to the API, and redirects to the "Teacher Show" page of our updated teacher.
        /// </summary>
        /// <param name="id">Id of the teacher to update</param>
        /// <param name="TeacherFname">The updated first name of the teacher</param>
        /// <param name="TeacherLname">The updated last name of the teacher</param>
        /// <param name="Employeenumber">The updated bio of the teacher.</param>
        /// <param name="Salary">The updated email of the teacher.</param>
        /// <returns>A dynamic webpage which provides the current information of the teacher.</returns>
        /// <example>
        /// POST : /Teacher/Update/10
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"TeacherFname":"Gahee",
        ///	"TeacherLname":"Choi",
        ///	"Hiredate":"2022-11-30",
        ///	"Employeenumber":"A123",
        ///	"Salary":"100.00"
        /// }
        /// </example>
        [HttpPost]
        public ActionResult UpdateC(int id, int TeacherId)
        {


            Class TeacherInfo = new Class();
            TeacherInfo.TeacherId = TeacherId;

            ClassDataController controller = new ClassDataController();

            controller.UpdateTeacher(id, TeacherInfo);

            return RedirectToAction("Show/" + id);

        }







    }




}