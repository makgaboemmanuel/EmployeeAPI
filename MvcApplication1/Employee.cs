using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1
{
    public class Employee
    {

        public string emp_num { get; set; }


        public string emp_title { get; set; }


        public string emp_lname { get; set; }


        public string emp_fname { get; set; }


        public string emp_ititial { get; set; }


        public string emp_DOB { get; set; }


        public string emp_hire_date { get; set; }

        
        public Employee(string emp_num, string emp_title, string emp_lname, string emp_fname, string emp_ititial
            , string emp_DOB, string emp_hire_date)
        {
            this.emp_num = emp_num;
            this.emp_title = emp_title;
            this.emp_lname = emp_lname;
            this.emp_fname = emp_fname;
            this.emp_ititial = emp_ititial;
            this.emp_DOB = emp_DOB;
            this.emp_hire_date = emp_hire_date;
        } 
    
    }

}