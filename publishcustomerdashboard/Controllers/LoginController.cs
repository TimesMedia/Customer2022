using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Customer2022.Models;
using System.Data.SqlClient;
using System.Data;

namespace Customer2022.Controllers
{   
    public class LoginController : Controller
    {
        string connectionString = @"Data Source=PKLDEV01\\SQLEXPRESS;Initial Catalog=Customersdashboard;User ID=Tebello7;Password=P@ssw0rd01;"
                                   + "Enlist=False;Pooling=True;Max Pool Size=10;Connect Timeout=100";
        //string connectionString = "Data Source=IT-RBK-099\\SQLEXPRESS; Initial Catalog= ASPCRUD; Integrated Security=true; ";
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

       
        public ActionResult Index(Customers user)
        {

            return View();

        }


        //GET: Customer/ResetPassword
        public ActionResult ResetPassword()
        {
            Customers customermodel = new Customers();
            DataTable dtblcus = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = "SELECT * From Contact Where ContactID = @ContactID";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlcon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@ContactID", customermodel.ContactID);
                sqlDa.Fill(dtblcus);
            }
            if (dtblcus.Rows.Count == 1)
            {
                customermodel.ContactID = Convert.ToInt32(dtblcus.Rows[0][0].ToString());
                customermodel.Password = dtblcus.Rows[0][1].ToString();

                return View(customermodel);
            }
            else
                return RedirectToAction("Resetpassword");
        }

        //POST: Customer/ResetPassword
        [HttpPost]
        public ActionResult ResetPassword(Customers customers)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = "UPDATE Contact SET  ContactID = @ContactID, Password =@Password WHere ContactID = @ContactID";
                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                sqlcmd.Parameters.AddWithValue("@ContactID", customers.ContactID);
                sqlcmd.Parameters.AddWithValue("@Password", customers.Password);
                sqlcmd.ExecuteNonQuery();
            }
            return View();
        }
    }
}