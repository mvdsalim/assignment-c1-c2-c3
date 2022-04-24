using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using n01495499.Models;

namespace n01495499.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View("student/view");
        }

        // GET: /Student/List
        [HttpGet]
        public ActionResult List(string SearchKey = null)
        {
            //this class will help us gather information from the db
            StudentDataController controller = new StudentDataController();
            IEnumerable<Student> Students = controller.Liststudent(SearchKey);
            return View(Students);
        }

        // GET : /Student/Show/{id}
        [HttpGet]
        [Route("Student/Show/{id}")]
        public ActionResult Show(int id)
        {
            StudentDataController controller = new StudentDataController();
            Student NewStudent = controller.FindStudent(id);

            return View(NewStudent);
        }







        // GET : /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            StudentDataController controller = new StudentDataController();
            Student NewTeacher = controller.FindStudent(id);

            return View(NewTeacher);
        }

        // POST : /Teacher/Delete/{id}
        public ActionResult Delete(int id)
        {
            StudentDataController controller = new StudentDataController();
            controller.Deletestudent(id);
            return RedirectToAction("List");
        }

        // GET : /Teacher/New
        public ActionResult New()
        {
            return View();
        }

        // POST : /Teacher/Create
        [HttpPost]
        public ActionResult Create(string StudentFName, string StudentLName, int ClassId)
        {
            //Identify this method is running 
            //Identify the inputs provided from the form

            Student NewTeacher = new Student();
            NewTeacher.StudentFName = StudentFName;
            NewTeacher.StudentLName = StudentLName;
            NewTeacher.ClassId = ClassId;

            StudentDataController controller = new StudentDataController();
            controller.Addstudent(NewTeacher);

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
            StudentDataController controller = new StudentDataController();
            Student SelectedTeacher = controller.FindStudent(id);
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
        public ActionResult Update(int id, string StudentFName, string StudentLName, int ClassId)
        {
            Student NewTeacher = new Student();
            NewTeacher.StudentFName = StudentFName;
            NewTeacher.StudentLName = StudentLName;
            NewTeacher.ClassId = ClassId;

            StudentDataController controller = new StudentDataController();
            controller.Updatestudent(id, NewTeacher);

            return RedirectToAction("Show/" + id);
        }
    }
}