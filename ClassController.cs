using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using n01495499.Models;

namespace n01495499.Controllers
{
    public class ClassController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View("class/view");
        }

        // GET: /Teacher/List
        [HttpGet]
        public ActionResult List(string SearchKey = null)
        {
            //this class will help us gather information from the db
            ClassDataController controller = new ClassDataController();
            IEnumerable<studentClass> Teachers = controller.ListstudentClass(SearchKey);
            return View(Teachers);
        }

        // GET : /Teacher/Show/{id}
        [HttpGet]
        [Route("Teacher/Show/{id}")]
        public ActionResult Show(int id)
        {
            ClassDataController controller = new ClassDataController();
            studentClass SelectedTeacher = controller.FindstudentClass(id);

            return View(SelectedTeacher);
        }

        // GET : /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            ClassDataController controller = new ClassDataController();
            studentClass NewTeacher = controller.FindstudentClass(id);

            return View(NewTeacher);
        }

        // POST : /Teacher/Delete/{id}
        public ActionResult Delete(int id)
        {
            ClassDataController controller = new ClassDataController();
            controller.DeletestudentClass(id);
            return RedirectToAction("List");
        }

        // GET : /Teacher/New
        public ActionResult New()
        {
            return View();
        }

        // POST : /Teacher/Create
        [HttpPost]
        public ActionResult Create(string ClassName, int TeacherId)
        {
            //Identify this method is running 
            //Identify the inputs provided from the form

            studentClass NewTeacher = new studentClass();
            NewTeacher.ClassName = ClassName;
            NewTeacher.TeacherId = TeacherId;

            ClassDataController controller = new ClassDataController();
            controller.AddstudentClass(NewTeacher);

            return RedirectToAction("List");
        }

        /// <summary>
        /// Routes a dynamically generated "Teacher Update" Page. Gathers information from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A dynamic "Update Teacher" webpage which proves the current information of the author and asks the user for new information as part of a form</returns>
        // <example>GET : /Teacher/Update/{id}</example>
        public ActionResult Update(int id)
        {
            ClassDataController controller = new ClassDataController();
            studentClass SelectedTeacher = controller.FindstudentClass(id);
            return View(SelectedTeacher);
        }


        /// <summary>
        /// Receive a POST request containing information about an existing teacher in the system, with new values. Conveys this information to the API, and redirects to the "Teacher Show" page of our updated teacher.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="TeacherFName"></param>
        /// <param name="TeacherLName"></param>
        /// <param name="EmployeeNumber"></param>
        /// <param name="Salary"></param>
        /// <returns>A dynamic webpage which provides the current information of the teacher.</returns>
        /// <example>
        // POST : /Teacher/Update/{id}
        /// FORM DATTA / POST DATA / REQUEST BODY
        /// {
        /// "TeacherFName":"Jenny",
        /// "TeacherLName":"Dcruz",
        /// "EmployeeNumber":"N01469587",
        /// "Salary":"3434.23"
        /// }
        /// </example>
        [HttpPost]
        public ActionResult Update(int id, string classname, int teacherid)
        {
            studentClass TeacherInfo = new studentClass();
            TeacherInfo.ClassName = classname;
            TeacherInfo.TeacherId = teacherid;

            ClassDataController controller = new ClassDataController();
            controller.UpdatestudentClass(id, TeacherInfo);

            return RedirectToAction("Show/" + id);
        }
    }
}