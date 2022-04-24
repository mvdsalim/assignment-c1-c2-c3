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
    public class ClassDataController : ApiController
    {
        // The database context studentClass which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the studentClass table of our school database.
        /// <summary>
        /// Returns a list of studentClass in the system
        /// </summary>
        /// <example>GET api/studentClassData/ListstudentClass</example>
        /// <returns>
        /// A list of studentClass (first names and last names)
        /// </returns>
        [HttpGet]
        public IEnumerable<studentClass> ListstudentClass(string SearchKey = null)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from classes";
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of studentClass Names
            List<studentClass> studentClass = new List<studentClass> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {

                //Access Column information by the DB column name as an index
                int ClassId = (int)ResultSet["ClassId"];
                string ClassName = ResultSet["ClassName"].ToString();
                int TeacherId = (int)(ResultSet["TeacherId"]);

                studentClass NewstudentClass = new studentClass();
                NewstudentClass.ClassId = ClassId;
                NewstudentClass.ClassName = ClassName;
                NewstudentClass.TeacherId = TeacherId;

                //Add the studentClass Name to the List
                studentClass.Add(NewstudentClass);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of studentClass names
            return studentClass;
        }

        /// <summary>
        /// Finds an studentClass based on the studentClass ID
        /// </summary>
        /// <example> GET api/studentClassdata/findstudentClass/{id}</example>
        /// <param name="id">The ID of the studentClass</param>
        /// <returns>the name of the studentClass</returns>
        [HttpGet]
        [Route("api/studentClassdata/findstudentClass/{id}")]
        public studentClass FindstudentClass(int id)
        {
            //when we want to contact the database, use a query

            studentClass NewstudentClass = new studentClass();

            //accessing the database through connection string
            MySqlConnection Conn = School.AccessDatabase();

            //open the connection to the db
            Conn.Open();

            //creating a new mysql command query
            MySqlCommand Cmd = Conn.CreateCommand();

            //setting the command query to the string we generated in query variable
            Cmd.CommandText = "Select * from classes where classid = @id";
            Cmd.Parameters.AddWithValue("@id", id);
            Cmd.Prepare();

            //read through the results for our query
            MySqlDataReader ResultSet = Cmd.ExecuteReader();

            //iterating through our results -- even if there is one one
            while (ResultSet.Read())
            {
                int ClassId = (int)ResultSet["ClassId"];
                string ClassName = ResultSet["ClassName"].ToString();
                int TeacherId = (int)ResultSet["TeacherId"];

                NewstudentClass.ClassId = ClassId;
                NewstudentClass.ClassName = ClassName;
                NewstudentClass.TeacherId = TeacherId;

            }

            return NewstudentClass;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <example> POST : /api/AuthorData/DeleteAuthor/3</example>
        [HttpPost]
        public void DeletestudentClass(int id)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "Delete from classes where classid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }

        [HttpPost]
        public void AddstudentClass(studentClass NewstudentClass)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "insert into classes (className, TeacherId) values (@className, @TeacherId)";
            
            cmd.Parameters.AddWithValue("@className", NewstudentClass.ClassName);
            cmd.Parameters.AddWithValue("@TeacherId", NewstudentClass.TeacherId);

            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();

        }

        public void UpdatestudentClass(int id, [FromBody] studentClass studentClassInfo)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "update classes set className=@className, TeacherId=@TeacherId where classid=@studentClassId";
            cmd.Parameters.AddWithValue("@className", studentClassInfo.ClassName);
            cmd.Parameters.AddWithValue("@TeacherId", studentClassInfo.TeacherId);
            cmd.Parameters.AddWithValue("@studentClassId", id);

            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }
    }
}
