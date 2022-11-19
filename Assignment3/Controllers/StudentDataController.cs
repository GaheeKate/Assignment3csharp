using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Assignment3.Models; 
using MySql.Data.MySqlClient;

namespace Assignment3.Controllers
{
    public class StudentDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the Students table of our school database.
        /// <summary>
        /// Returns a list of Students in the system
        /// </summary>
        /// <example>GET api/StudentData/ListStudents/fre
        /// <returns>
        /// <Student>
        /// <EnrolDate>2018-08-16T00:00:00</EnrolDate>
        /// <StudentFname>Jason</StudentFname>
        /// <StudentId>7</StudentId>
        /// <StudentLname>Freeman</StudentLname>
        /// <StudentNumber>N1694</StudentNumber>
        /// </Student>
        /// </returns>
      
        [HttpGet]
        [Route("api/StudentData/ListStudents/{SearchKey?}")]

        public IEnumerable<Student> ListStudents(string SearchKey = null) //method read only
        {
            //Create an instance of a connection
            //Access database through access database method which we created in the Models 
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
       
            cmd.CommandText = "Select * from students where lower(studentfname) like lower(@key) or lower(studentlname) like lower(@key) or lower(concat(studentfname, ' ', studentlname)) like lower(@key)";
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();
            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teacher
            List<Student> Students = new List<Student>{};

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {

                int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                string StudentFname = (string)ResultSet["studentfname"];
                string StudentLname = (string)ResultSet["studentlname"];
                string StudentNumber = (string)ResultSet["studentnumber"];
                DateTime EnrolDate = (DateTime)ResultSet["enroldate"];
                

                Student NewStudent = new Student();
                NewStudent.StudentId = StudentId;
                NewStudent.StudentFname = StudentFname;
                NewStudent.StudentLname = StudentLname;
                NewStudent.StudentNumber = StudentNumber;
                NewStudent.EnrolDate = EnrolDate;

                 
                //Add the Teacher to the List
                Students.Add(NewStudent);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of teachers
            return Students;
        }



        //find Students
        /// <summary>
        /// Returns an individual teacher from the database by specifying the primary key studentid
        /// </summary>
        /// <param name="id">the student's ID in the database</param>
        /// <returns>A student object including classes taught by the student</returns>
        /// <example>GET api/StudentData/FindStudent/1
        /// <returns>
        /// <EnrolDate>2018-06-18T00:00:00</EnrolDate>
        /// <StudentFname>Sarah</StudentFname>
        /// <StudentId>1</StudentId>
        /// <StudentLname>Valdez</StudentLname>
        /// <StudentNumber>N1678</StudentNumber>
        ///</returns>

        [HttpGet]
        public Student FindStudent(int id)
        {
            Student NewStudent = new Student();
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from students where studentid = "+id;
            

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                string StudentFname = (string)ResultSet["studentfname"];
                string StudentLname = (string)ResultSet["studentlname"];
                string StudentNumber = (string)ResultSet["studentnumber"];
                DateTime EnrolDate = (DateTime)ResultSet["enrolDate"];
                


                NewStudent.StudentId = StudentId;
                NewStudent.StudentFname = StudentFname;
                NewStudent.StudentLname = StudentLname;
                NewStudent.StudentNumber = StudentNumber;
                NewStudent.EnrolDate = EnrolDate;

            }

            return NewStudent;
        }
    }
}
