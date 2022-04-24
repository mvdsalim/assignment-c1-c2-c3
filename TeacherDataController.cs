using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using n01495499.Models;
using MySql.Data.MySqlClient;

namespace n01495499.Controllers
{
    public class TeacherDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();
        
        //This Controller Will access the teacher table of our school database.
        /// <summary>
        /// Returns a list of teacher in the system
        /// </summary>
        /// <example>GET api/TeacherData/Listteacher</example>
        /// <returns>
        /// A list of teacher (first names and last names)
        /// </returns>
        [HttpGet]
        public IEnumerable<Teacher> Listteacher(string SearchKey = null)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teacher where cast(Salary as decimal(10,2))=cast(@keynumber as decimal(10,2)) or lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Parameters.AddWithValue("@keynumber", SearchKey);
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teacher Names
            List<Teacher> teacher = new List<Teacher>{};

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {

                //Access Column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFName = ResultSet["teacherfname"].ToString();
                string TeacherLName = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                decimal Salary = Convert.ToDecimal(ResultSet["Salary"]);
                DateTime HireDate = DateTime.Parse(ResultSet["HireDate"].ToString());

                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFName = TeacherFName;
                NewTeacher.TeacherLName = TeacherLName;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.Salary = Salary;
                NewTeacher.HireDate = HireDate;

                //Add the Teacher Name to the List
                teacher.Add(NewTeacher);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of teacher names
            return teacher;
        }

        /// <summary>
        /// Finds an teacher based on the teacher ID
        /// </summary>
        /// <example> GET api/teacherdata/findteacher/{id}</example>
        /// <param name="id">The ID of the teacher</param>
        /// <returns>the name of the teacher</returns>
        [HttpGet]
        [Route("api/teacherdata/findteacher/{id}")]
        public Teacher FindTeacher(int id)
        {
            //when we want to contact the database, use a query

            Teacher NewTeacher = new Teacher();

            //accessing the database through connection string
            MySqlConnection Conn = School.AccessDatabase();

            //open the connection to the db
            Conn.Open();

            //creating a new mysql command query
            MySqlCommand Cmd = Conn.CreateCommand();

            //setting the command query to the string we generated in query variable
            Cmd.CommandText = "Select * from teacher where teacherid = @id";
            Cmd.Parameters.AddWithValue("@id", id);
            Cmd.Prepare();

            //read through the results for our query
            MySqlDataReader ResultSet = Cmd.ExecuteReader();

            //iterating through our results -- even if there is one one
            while (ResultSet.Read())
            {
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFName = ResultSet["teacherfname"].ToString();
                string TeacherLName = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                // DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFName = TeacherFName;
                NewTeacher.TeacherLName = TeacherLName;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                // NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;
 
            }

            return NewTeacher;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <example> POST : /api/AuthorData/DeleteAuthor/3</example>
        [HttpPost]
        public void DeleteTeacher(int id)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "Delete from teacher where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }

        [HttpPost]
        public void AddTeacher(Teacher NewTeacher)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "insert into teacher (teacherfname, teacherlname, employeenumber, hiredate, salary) values (@TeacherFName, @TeacherLName, @EmployeeNumber, CURRENT_DATE(), @Salary)";
            cmd.Parameters.AddWithValue("@TeacherFName", NewTeacher.TeacherFName);
            cmd.Parameters.AddWithValue("@TeacherLName", NewTeacher.TeacherLName); 
            cmd.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.EmployeeNumber);
            cmd.Parameters.AddWithValue("@Salary", NewTeacher.Salary);

            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();

        }

        public void UpdateTeacher (int id, [FromBody]Teacher TeacherInfo)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "update teacher set teacherfname=@TeacherFName, teacherlname=@TeacherLName, employeenumber=@EmployeeNumber, hiredate=CURRENT_DATE(), salary=@Salary where teacherid=@TeacherId";
            cmd.Parameters.AddWithValue("@TeacherFName", TeacherInfo.TeacherFName);
            cmd.Parameters.AddWithValue("@TeacherLName", TeacherInfo.TeacherLName);
            cmd.Parameters.AddWithValue("@EmployeeNumber", TeacherInfo.EmployeeNumber);
            cmd.Parameters.AddWithValue("@Salary", TeacherInfo.Salary);
            cmd.Parameters.AddWithValue("@TeacherId", id);

            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }
    }
}
