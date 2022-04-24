using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using n01495499.Models;
using System.Diagnostics;

namespace n01495499.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Teacher/List
        [HttpGet]
        public ActionResult List(string SearchKey = null)
        {
            //this class will help us gather information from the db
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.Listteacher(SearchKey);
            return View(Teachers);
        }

        // GET : /Teacher/Show/{id}
        [HttpGet]
        [Route("Teacher/Show/{id}")]
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);

            return View(SelectedTeacher);
        }

        // GET : /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            return View(NewTeacher);
        }

        // POST : /Teacher/Delete/{id}
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        // GET : /Teacher/New
        public ActionResult New()
        {
            return View();
        }

        // POST : /Teacher/Create
        [HttpPost]
        public ActionResult Create(string TeacherFName, string TeacherLName, string EmployeeNumber, decimal Salary)
        {
            //Identify this method is running 
            //Identify the inputs provided from the form

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFName = TeacherFName;
            NewTeacher.TeacherLName = TeacherLName;
            NewTeacher.EmployeeNumber = EmployeeNumber;
            NewTeacher.Salary = Salary;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

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
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);
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
        public ActionResult Update(int id, string TeacherFName, string TeacherLName, string EmployeeNumber, decimal Salary)
        {
            Teacher TeacherInfo = new Teacher();
            TeacherInfo.TeacherFName = TeacherFName;
            TeacherInfo.TeacherLName = TeacherLName;
            TeacherInfo.EmployeeNumber = EmployeeNumber;
            TeacherInfo.Salary = Salary;

            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, TeacherInfo);

            return RedirectToAction("Show/" + id);
        }
    }
}