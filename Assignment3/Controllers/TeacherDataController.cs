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
    public class TeacherDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();



        //This Controller Will access the Teachers table of our school database.
        /// <summary>
        /// Returns a list of Teachers in the system when Searchkey is null and returns a information of teacher 
        /// by searchkey for last or first or full name, or
        /// by searchkey for salary,
        /// by searchkey for hiredate
        /// </summary>
        /// <example>GET api/TeacherData/ListTeachers/re
        /// <returns>
        /// <Teacher>
        /// <Hiredate>2014-06-22T00:00:00</Hiredate>
        /// <Salary>74.20</Salary>
        /// <TeacherFname>Lauren</TeacherFname>
        /// <TeacherId>4</TeacherId>
        /// <TeacherLname>Smith</TeacherLname>
        /// </Teacher> 
        /// <example>GET api/TeacherData/ListTeachers/74
        /// <returns>
        /// <Teacher>
        /// <Hiredate>2014-06-22T00:00:00</Hiredate>
        /// <Salary>74.20</Salary>
        /// <TeacherFname>Lauren</TeacherFname>
        /// <TeacherId>4</TeacherId>
        /// <TeacherLname>Smith</TeacherLname>
        /// </Teacher> 
        /// </returns>

        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey=null) //method read only
        {
            //Create an instance of a connection
            //Access database through access database method which we created in the Models 
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers where lower(teacherfname) like lower(@key) " +
                "or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key) " +
                "or hiredate like @key or salary like @key";
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();
            
            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teacher
            List<Teacher> Teachers = new List<Teacher> {};

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLName = (string)ResultSet["teacherlname"];
                DateTime Hiredate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];

                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;  
                NewTeacher.TeacherFname=TeacherFname;
                NewTeacher.TeacherLname=TeacherLName;
                NewTeacher.Hiredate=Hiredate;
                NewTeacher.Salary = Salary;

                 
                //Add the Teacher to the List
                Teachers.Add(NewTeacher);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of teachers
            return Teachers;
        }





        /// <summary>
        /// Returns an individual teacher from the database by specifying the primary key TeacherId
        /// </summary>
        /// <param name="id">the teacher's ID in the database</param>
        /// <returns>A teacher object including classes taught by the teacher</returns>
        /// <example>GET api/TeacherData/FindTeacher/1
        /// <returns>
        /// <Hiredate>2016-08-05T00:00:00</Hiredate>
        /// <Salary>55.30</Salary>
        /// <TeacherFname>Alexander</TeacherFname>
        /// <TeacherId>1</TeacherId>
        /// <TeacherLname>Bennett</TeacherLname>
        ///</returns>


        //find Students
        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers where teacherid = " + id;


            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFname = (string)ResultSet["TeacherFname"];
                string TeacherLName = (string)ResultSet["teacherlname"];
                DateTime Hiredate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLName;
                NewTeacher.Hiredate = Hiredate;
                NewTeacher.Salary = Salary;

            }

            return NewTeacher;
        }



        //FindClass
        /// <summary>
        /// Returns classes taught by a teacher by specifying the primary key TeacherId
        /// </summary>
        /// <param name="id">the TeacherId ID in the database</param>
        /// <returns>class objects by the TeacherId ID</returns>
        /// <example>GET api/TeacherData/FindClass/2
        /// <returns>
        ///<Class>
        ///<ClassCode>http5102</ClassCode>
        ///<ClassId>2</ClassId>
        ///<ClassName>Project Management</ClassName>
        ///<FisihiDate>2018-12-14T00:00:00</FisihiDate>
        ///<StartDate>2018-09-04T00:00:00</StartDate>
        ///<TeacherId>2</TeacherId>
        ///</Class>
        ///<Class>
        ///<ClassCode>http5201</ClassCode>
        ///<ClassId>6</ClassId>
        ///<ClassName>Security & Quality Assurance</ClassName>
        ///<FisihiDate>2019-04-27T00:00:00</FisihiDate>
        ///<StartDate>2019-01-08T00:00:00</StartDate>
        ///<TeacherId>2</TeacherId>
        ///</Class>
        ///</returns>

        [HttpGet]

        public IEnumerable<Class> FindClass(int id)
        {
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "SELECT * FROM classes join teachers on classes.teacherid = teachers.teacherid where teachers.teacherid = " + id;


            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();


            //Create an empty list of classes
            List<Class> classes = new List<Class> { };

            while (ResultSet.Read())
            {
                Class NewClass = new Class();

                //Access Column information by the DB column name as an index
                
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                int ClassId = (int)ResultSet["classid"];
                string ClassCode = (string)ResultSet["classcode"];
                DateTime StartDate = (DateTime)ResultSet["startdate"];
                DateTime FisihiDate = (DateTime)ResultSet["finishdate"];
                string ClassName = (string)ResultSet["classname"];

                NewClass.TeacherId = TeacherId;
                NewClass.ClassId = ClassId;
                NewClass.ClassCode = ClassCode;
                NewClass.StartDate = StartDate;
                NewClass.FisihiDate = FisihiDate;
                NewClass.ClassName = ClassName;

                classes.Add(NewClass);


            }

            return classes;
        }


    }
}
