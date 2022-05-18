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
        readonly string gConnectionString = global::Customer2022.Properties.Settings.Default.ConnectionString;
        //string connectionString = @"Data Source=PKLDEV01\SQLEXPRESS;Initial Catalog=Customersdashboard;User ID=Tebello7;Password=P@ssw0rd01;"
                                 // + "Enlist=False;Pooling=True;Max Pool Size=10;Connect Timeout=100";

        //string connectionString = "Data Source=IT-RBK-099\\SQLEXPRESS; Initial Catalog= ASPCRUD; Integrated Security = true ";
        [HttpGet]
        public ActionResult Index()
        {
            DataTable lCustomerTable = new DataTable();
            lCustomerTable.Columns.Add("ContactID", typeof(int));
            lCustomerTable.Columns.Add("Initials", typeof(string));
            lCustomerTable.Columns.Add("FirstName", typeof(string));
            lCustomerTable.Columns.Add("Surname", typeof(string));
            lCustomerTable.Columns.Add("Address1", typeof(string));
            
            using (SqlConnection sqlcon = new SqlConnection(gConnectionString))
            {
                sqlcon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("select top 20  'ContactID' = CustomerId ,Initials,FirstName,Surname,Address1 from Customer inner join Product2 on CustomerId = ProductId", sqlcon);
                sqlDa.Fill(lCustomerTable);
            }

            List<Customer> lCustomerModel = new List<Customer>();
            foreach (DataRow lRow in lCustomerTable.Rows) {

                lCustomerModel.Add(new Customer() { ContactID = (int)lRow[0], Initials = lRow[1].ToString(),FirstName = lRow[2].ToString(), Surname = lRow[3].ToString(), Address1 = lRow[4].ToString()});
            }

            return View(lCustomerModel);
        }



        // GET: Customer/Create
        public ActionResult Create()
        {
            return View(new Customer());
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(Customer customers)
        {
            // TODO: Add insert logic here
            using (SqlConnection sqlcon = new SqlConnection(gConnectionString))
            {
                sqlcon.Open();
                string query = "INSERT INTO Customer VALUES(@ContactID,@Initials,@FirstName,@Surname,@Address1,@EmailAddress,@PhoneNumber)";
                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                sqlcmd.Parameters.AddWithValue("@ContactID", customers.ContactID);
                sqlcmd.Parameters.AddWithValue("@Initials", customers.Initials);
                sqlcmd.Parameters.AddWithValue("@FirstName", customers.FirstName);
                sqlcmd.Parameters.AddWithValue("@Surname", customers.Surname);
                sqlcmd.Parameters.AddWithValue("@Address1", customers.Address1);
                sqlcmd.Parameters.AddWithValue("@EmailAddress", customers.EmailAddress);
                sqlcmd.Parameters.AddWithValue("@PhoneNumber", customers.PhoneNumber);
                sqlcmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            Customer customermodel = new Customer();
            DataTable dtblcus = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(gConnectionString))
            {
                sqlcon.Open();
                string query = "SELECT * From Customer Where 'ContactID' = @CustomerId";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlcon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@CustomerId", id.ToString());
                sqlDa.Fill(dtblcus);
            }
            if (dtblcus.Rows.Count == 1)
            {
                customermodel.ContactID = Convert.ToInt32(dtblcus.Rows[0][0].ToString());
                customermodel.Initials = dtblcus.Rows[0][1].ToString();
                customermodel.Surname = dtblcus.Rows[0][2].ToString();
                customermodel.Subscription = dtblcus.Rows[0][3].ToString();

                return View(customermodel);
            }
            else
                return RedirectToAction("Index");
        }

        //POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(Customer customers)
        {
            using (SqlConnection sqlcon = new SqlConnection(gConnectionString))
            {
                sqlcon.Open();
                string query = "UPDATE Customer SET  ContactID = @ContactID, Password1 = @Password1 WHere ContactID = @ContactID";
                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                sqlcmd.Parameters.AddWithValue("@ContactID", customers.ContactID);
                sqlcmd.Parameters.AddWithValue("@Password1", customers.Password1);
              
                sqlcmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

       
        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlcon = new SqlConnection(gConnectionString))
            {
                sqlcon.Open();
                string query = "DELETE From Customer  WHere ContactID = @ContactID";
                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                sqlcmd.Parameters.AddWithValue("@ContactID", id);

                sqlcmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");

            
        }
        [HttpGet]
        public ActionResult View()
        {
            DataTable lCustomerTable = new DataTable();
            lCustomerTable.Columns.Add("ContactID", typeof(int));
            lCustomerTable.Columns.Add("Surname", typeof(string));
            lCustomerTable.Columns.Add("EmailAddress", typeof(string));
            lCustomerTable.Columns.Add("ProductName", typeof(string));
            using (SqlConnection sqlcon = new SqlConnection(gConnectionString))
            {
                sqlcon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("select top 20  'ContactID' = CustomerId ,Surname,ProductName,EmailAddress from Customer inner join Product2 on CustomerId = ProductId", sqlcon);
                sqlDa.Fill(lCustomerTable);
            }

            List<Customer> lCustomerModel = new List<Customer>();
            foreach (DataRow lRow in lCustomerTable.Rows)
            {

                lCustomerModel.Add(new Customer() { ContactID = (int)lRow[0], Surname = lRow[1].ToString(), EmailAddress= lRow[2].ToString(), ProductName = lRow[3].ToString() });
            }

            return View(lCustomerModel);
        }
        //GET: Customer/ResetPassword
        public ActionResult ResetPassword()
        {
            return View("Index");
        }

        //POST: Customer/ResetPassword
        [HttpPost]
        public ActionResult ResetPassword(Customer customers)
        {
            using (SqlConnection sqlcon = new SqlConnection(gConnectionString))
            {
                sqlcon.Open();
                string query = "UPDATE Customer SET  ContactID = @ContactID, Password1 =@Password1 WHere ContactID = @ContactID";
                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                sqlcmd.Parameters.AddWithValue("@ContactID", customers.ContactID);
                sqlcmd.Parameters.AddWithValue("@Password", customers.Password1);
                sqlcmd.ExecuteNonQuery();
            }
            return View();
        }
    }
}
