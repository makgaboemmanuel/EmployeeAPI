using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

/* user added libraries */
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
/* user added libraries */

namespace MvcApplication1.Controllers
{
    public class ValuesController : ApiController
    {
        SqlConnection con = new SqlConnection("server=DESKTOP-84AJQDT; database=Jay_Db; Integrated Security=true");
        // GET api/values
        public EmployeeDTO /* IEnumerable<string> */ Get()
        {
            
            EmployeeDTO edto = new EmployeeDTO();
            List<Employee> elist = new List<Employee>();

            /* cache data load */
            string aquery = "select * from air_employee";
            SqlCommand scom = new SqlCommand(@"insert into cacheposts( requestdate, requesttype, request ) 
                            values ('" +  DateTime.Now.ToString() + "', 'getAllEmployees', '" + aquery +  "')" , con);
            con.Open();
            scom.ExecuteNonQuery(); 

            /* cache data load */
            
            SqlDataAdapter da = new SqlDataAdapter( aquery, con);
            DataTable dt =  new DataTable();
            da.Fill(dt);
            for (int x = 0; x < dt.Rows.Count; x++ )
            {
              
                string enump = dt.Rows[x]["emp_num"].ToString();
                string fname = dt.Rows[x]["emp_title"].ToString();
                string title = dt.Rows[x]["emp_lname"].ToString();
                string lname = dt.Rows[x]["emp_fname"].ToString();
                string intial = dt.Rows[x]["emp_ititial"].ToString();
                string dob = dt.Rows[x]["emp_DOB"].ToString();
                string hidate = dt.Rows[x]["emp_hire_date"].ToString();

                Employee emp = new Employee(enump, fname, title, lname, intial, dob, hidate);
                elist.Add(emp);
            }

            edto.employeeList = elist;
            

            if( dt.Rows.Count > 0 ){
                edto.MessageCode = 1;
                edto.MessageText = "all employees returned";
                return edto;//JsonConvert.SerializeObject(edto);

            }

            else{
                edto.MessageCode = 1;
                edto.MessageText = "no employees found";
                return edto;//JsonConvert.SerializeObject(edto);
            }
        }

        // GET api/values/5
        public EmployeeDTO Get(string id) /* search by id or title or date of birth */
        {
            EmployeeDTO edto = new EmployeeDTO();
            List<Employee> elist = new List<Employee>();

            SqlDataAdapter da = null;
            int ncheck = 0;
            bool mcheck = int.TryParse(id, out ncheck);

            string aquery = null;
            con.Open();

            if ( mcheck == true){

                aquery = "select * from air_employee where emp_num = '" + id + "'";
                da = new SqlDataAdapter(@"select * from air_employee where emp_num = '" + id + "'", con);
            }

            else
            {
                aquery = "select * from air_employee where emp_title like '" + id + "' or emp_DOB like '" + id + "'";
                da = new SqlDataAdapter(@"select * from air_employee where emp_title like '%" + id + "%' or emp_DOB like '%" + id + "%'", con);
            }

            DataTable dt =  new DataTable();
            da.Fill(dt);

            /* cache data load */
           
            SqlCommand scom = new SqlCommand(@"insert into cacheposts( requestdate, requesttype, request ) 
                            values ('" + DateTime.Now.ToString() + "', 'getSearchedEmployees', '" + aquery + "')", con);
            
            scom.ExecuteNonQuery();

            /* cache data load */

            for (int x = 0; x < dt.Rows.Count; x++)
            {

                string enump = dt.Rows[x]["emp_num"].ToString();
                string fname = dt.Rows[x]["emp_title"].ToString();
                string title = dt.Rows[x]["emp_lname"].ToString();
                string lname = dt.Rows[x]["emp_fname"].ToString();
                string intial = dt.Rows[x]["emp_ititial"].ToString();
                string dob = dt.Rows[x]["emp_DOB"].ToString();
                string hidate = dt.Rows[x]["emp_hire_date"].ToString();

                Employee emp = new Employee(enump, fname, title, lname, intial, dob, hidate);
                elist.Add(emp);
            }

            edto.employeeList = elist;
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                edto.MessageCode = 1;
                edto.MessageText = "matching employees returned";
                return edto;
            }

            else
            {
                edto.MessageCode = 0;
                edto.MessageText = "no employees found for the search ";
                return edto;
            }
        }

        // POST api/values /*  [FromBody] */
        
        [HttpPost]
        public string Post( string emp_num , string emp_title, string emp_lname,
            string emp_fname, string emp_ititial, string emp_DOB, string emp_hire_date )
        {
            // return value + " added successfully";


            SqlCommand scom = new SqlCommand(@"insert into air_employee values ('" + emp_num + "', '" + emp_title
                + "', '" + emp_lname + "', '" + emp_fname + "', '" + emp_ititial + "', '" + emp_DOB + "', '" + emp_hire_date + "' )", con);
           

            con.Open();
            int i = scom.ExecuteNonQuery();

            if( i == 1){
                return "Record inserted successfully";
            }
            else{
                return "Record inserted successfully";
            }
        }

        // PUT api/values/5
        public string Put(int id, [FromBody]string value)
        {
            SqlCommand scom = new SqlCommand("update air_employee set emp_lname = '" + value + "' where emp_num = '" + id + "'", con);
            con.Open();
            int i = scom.ExecuteNonQuery();

            if (i == 1)
            {
                return value + " updated successfully with id: " + id;
            }
            else
            {
                return "no data updated";
            }

            /* return value + " updated successfully with id: " + id; */
        }

        // DELETE api/values/5
        public string Delete(int id)
        {
             
            SqlCommand scom = new SqlCommand("delete from air_employee where emp_num = '" + id + "'", con);
            con.Open();
            int i = scom.ExecuteNonQuery();

            if (i == 1)
            {
                return "record with id: " + id + " deleted successfully  " ;
            }
            else
            {
                return "no record deleted";
            }
        }
    }
}