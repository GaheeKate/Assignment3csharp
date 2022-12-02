using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Diagnostics;


namespace Assignment3.Models
{
    public class Teacher
    {
        public int TeacherId;
        public string TeacherFname;
        public string TeacherLname;
        public string Employeenumber;
        public DateTime HireDate;
        public decimal Salary;

        public bool IsValid()
        {
            bool valid = true;

            if (TeacherFname == null || TeacherLname == null || Employeenumber == null || Double.IsNaN(decimal.ToDouble(Salary)) == true)
            {
                valid = false;
            }
            else
            {
                if (TeacherFname.Length < 2 || TeacherFname.Length > 255) valid = false;
                if (TeacherLname.Length < 2 || TeacherLname.Length > 255) valid = false;

                Regex Empnumber = new Regex(@"\w\d{3}");
                if (!Empnumber.IsMatch(Employeenumber)) valid = false;
            }

            return valid;
        }
        public Teacher() { }
    }
 
}