using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MvcApplication1
{
    public class EmployeeDTO
    {
        
        public List<Employee> employeeList { get; set; }

        public int MessageCode { get; set; }

        
        public string MessageText { get; set; }

    }
}