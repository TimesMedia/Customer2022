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
        string connectionString = @"Data Source=.\SQLEXPRESS; Database=ASPCRUD;Integrated Security=True";

        [HttpGet]
        public ActionResult Index()
        {
            DataTable dtblCustomer = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * from Contact", sqlcon);
                sqlDa.Fill(dtblCustomer);
            }
            return View(dtblCustomer);
        }



        // GET: Customer/Create
        public ActionResult Create()
        {
            return View(new Customers());
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(Customers customers)
        {

            // TODO: Add insert logic here
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = " INSERT INTO Contact VALUES(@Name,@Mobile,@Address)";
                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                sqlcmd.Parameters.AddWithValue("@Name", customers.Name);
                sqlcmd.Parameters.AddWithValue("@Mobile", customers.Mobile);
                sqlcmd.Parameters.AddWithValue("@Address", customers.Address);
                sqlcmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");


        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id) { 
         Customers customermodel = new Customers();
        DataTable dtblcus = new DataTable();
        using(SqlConnection sqlcon = new SqlConnection(connectionString)){
                sqlcon.Open();
                string query = "SELECT * From Contact Where ContactID = @ContactID";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query,sqlcon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@ContactID",id);
                sqlDa.Fill(dtblcus);
        }
            if(dtblcus.Rows.Count == 1)
            {
                customermodel.ContactID = Convert.ToInt32(dtblcus.Rows[0][0].ToString());
                customermodel.Name = dtblcus.Rows[0][1].ToString();
                customermodel.Mobile = dtblcus.Rows[0][2].ToString();
                customermodel.Address = dtblcus.Rows[0][3].ToString();
                return View(customermodel);
            }
            else
                  return RedirectToAction("Index");
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(Customers customers)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = "UPDATE Contact SET Name = @Name, Mobile = @Mobile, Address = @Address WHere ContactID = @ContactID";
                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                sqlcmd.Parameters.AddWithValue("@ContactID", customers.ContactID);
                sqlcmd.Parameters.AddWithValue("@Name", customers.Name);
                sqlcmd.Parameters.AddWithValue("@Mobile", customers.Mobile);
                sqlcmd.Parameters.AddWithValue("@Address", customers.Address);
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
                sqlcmd.Parameters.AddWithValue("@ContactID",id);
              
                sqlcmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        
    }
}
