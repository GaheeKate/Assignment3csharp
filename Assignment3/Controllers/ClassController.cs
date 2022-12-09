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
        /// Routes to a dynamically generated "Class New" Page. Gathers information from the database.
        /// </summary>
        /// <param name="id">Id of the Class</param>
        /// <returns>A dynamic "Class New" webpage which provides the current information of the teacher and asks the user for new information as part of a form.</returns>
        /// <example>GET : /Class/New/5</example>
        public ActionResult New(int id)
        {
            ClassDataController controller = new ClassDataController();

            Class NewClass = controller.FindClass(id);
            Teacher SelectedTeacher = controller.FindTeacherfromclass(id);

            ViewBag.SelectedTeacher = SelectedTeacher;

            return View(NewClass);
        }



        /// <summary>
        /// Receives a POST request containing information about an existing class in the system, with new values. Conveys this information to the API, and redirects to the "Class Show" page of our updated class.
        /// </summary>
        /// <param name="id">Id of the class to update</param>
        /// <param name="TeacherId">The updated or newly added Teacher ID</param>
        /// <returns>A dynamic webpage which provides the current information of the class.</returns>
        /// <example>
        /// POST : /Class/UpdateC/10
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"TeacherId":"5",
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