using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using Customer2022.Models;


namespace Customer2022.Controllers
{
    public class CustomerController : Controller
    {
        string connectionString = @"Data Source=PKLDEV01\SQLEXPRESS;Initial Catalog=CustomersDashBoard;User ID=Tebello7;Password=P@ssw0rd01;"
                                  + "Enlist=False;Pooling=True;Max Pool Size=10;Connect Timeout=100";

        [HttpGet]
        public ActionResult Index()
        {
            DataTable dtblCustomer = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * from Contact Order By FirstName", sqlcon);
                sqlDa.Fill(dtblCustomer);
            }
            return View(dtblCustomer);
        }



        // GET: Customer/Create
        public ActionResult Register()
        {
            return View(new Customers());
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Register(Customers customers)
        {
            // TODO: Add insert logic here
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = "INSERT INTO Contact VALUES(@ContactID,@FirstName,@Surname,@Subscription,@Invoice,@Username,@Password)";
                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                sqlcmd.Parameters.AddWithValue("@ContactID", customers.ContactID);
                sqlcmd.Parameters.AddWithValue("@FirstName", customers.FirstName);
                sqlcmd.Parameters.AddWithValue("@Surname", customers.Surname);
                sqlcmd.Parameters.AddWithValue("@Subscription", customers.Subscription);
                sqlcmd.Parameters.AddWithValue("@Invoice", customers.Invoice);
                sqlcmd.Parameters.AddWithValue("@Password", customers.Password);
                sqlcmd.Parameters.AddWithValue("@Username", customers.Username);
                sqlcmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            Customers customermodel = new Customers();
            DataTable dtblcus = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = "SELECT * From Contact Where ContactID = @ContactID";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlcon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@ContactID", id);
                sqlDa.Fill(dtblcus);
            }
            if (dtblcus.Rows.Count == 1)
            {
                customermodel.ContactID = Convert.ToInt32(dtblcus.Rows[0][0].ToString());
                customermodel.FirstName = dtblcus.Rows[0][1].ToString();
                customermodel.Surname = dtblcus.Rows[0][2].ToString();
                customermodel.Subscription = dtblcus.Rows[0][3].ToString();

                return View(customermodel);
            }
            else
                return RedirectToAction("Index");
        }

        //POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(Customers customers)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = "UPDATE Contact SET  ContactID = @ContactID, Password = @Password WHere ContactID = @ContactID";
                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                sqlcmd.Parameters.AddWithValue("@ContactID", customers.ContactID);
                sqlcmd.Parameters.AddWithValue("@Password", customers.Password);
              
                sqlcmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

       
        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = "DELETE From Contact  WHere ContactID = @ContactID";
                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                sqlcmd.Parameters.AddWithValue("@ContactID", id);

                sqlcmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");

            
        }
        [HttpGet]
        public ActionResult View(int id)
        {
            DataTable dtblCustomer = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT FirstName, Surname,Invoice, Password from Contact ", sqlcon);
                sqlDa.Fill(dtblCustomer);
            }
            return View(dtblCustomer);
        }
        //GET: Customer/ResetPassword
        public ActionResult ResetPassword()
        {
            return View("Index");
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
