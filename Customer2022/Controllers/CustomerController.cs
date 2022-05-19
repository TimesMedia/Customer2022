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
        private readonly string gConnectionString = global::Customer2022.Properties.Settings.Default.ConnectionString;
        
        
        [HttpGet]
        public ActionResult Index()
        {
            DataTable lCustomerTable = new DataTable();
            lCustomerTable.Columns.Add("ContactID", typeof(int));
            lCustomerTable.Columns.Add("Initials", typeof(string));
            lCustomerTable.Columns.Add("FirstName", typeof(string));
            lCustomerTable.Columns.Add("Surname", typeof(string));
            lCustomerTable.Columns.Add("Address1", typeof(string));
            
           // gCustomerTable.PrimaryKey[0] = gCustomerTable.Columns[0];
            
            using (SqlConnection sqlcon = new SqlConnection(gConnectionString))
            {
                sqlcon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("select top 20  'ContactID' = CustomerId ,Initials,FirstName,Surname,Address1 from Customer inner join Product2 on CustomerId = ProductId", sqlcon);
                sqlDa.Fill(lCustomerTable);
            }

            List<Customer> lCustomers = new List<Customer>();
            foreach (DataRow lRow in lCustomerTable.Rows) {

                lCustomers.Add(new Customer() { ContactID = (int)lRow[0], Initials = lRow[1].ToString(),FirstName = lRow[2].ToString(), 
                    Surname = lRow[3].ToString(), Address1 = lRow[4].ToString()});
            }

            return View(lCustomers);
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
                string query = "INSERT INTO Customer (Initials, FirstName, Surname, Address1, EmailAddress, CompanyId, PhoneNumber, ModifiedBy, ModifiedOn, CountryId) VALUES(@Initials, @FirstName, @Surname, @Address1, @EmailAddress, @CompanyId, @PhoneNumber, @ModifiedBy, @ModifiedOn, @CountryId) INSERT INTO DeliveryAddress(Province,City,Suburb,Street,StreetExtension,StreetNo,PostCode,Verified, ModifiedOn,CountryId,ModifiedBy,DeliveryAddressId)VALUES(@DeliveryAddressId,@Province,@City,@Suburb,@Street,@StreetExtension,@StreetNo,@PostCode,@Verified, @ModifiedOn,@CountryId,@ModifiedBy)";
                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                //sqlcmd.Parameters.AddWithValue("@ContactID", customers.ContactID);
                sqlcmd.Parameters.AddWithValue("@Initials", customers.Initials);
                sqlcmd.Parameters.AddWithValue("@FirstName", customers.FirstName);
                sqlcmd.Parameters.AddWithValue("@Surname", customers.Surname);
                sqlcmd.Parameters.AddWithValue("@Address1", customers.Address1);
               sqlcmd.Parameters.AddWithValue("@EmailAddress", customers.EmailAddress);
                sqlcmd.Parameters.AddWithValue("@CompanyId", customers.CompanyId);
                sqlcmd.Parameters.AddWithValue("@PhoneNumber", customers.PhoneNumber);
                sqlcmd.Parameters.AddWithValue("@ModifiedBy", customers.ModifiedBy);
                sqlcmd.Parameters.AddWithValue("@ModifiedOn", customers.ModifiedOn.Date);
                sqlcmd.Parameters.AddWithValue("@CountryId", customers.CountryId);
                sqlcmd.Parameters.AddWithValue("@DeliveryAddressId", customers.DeliveryAddressId);
                sqlcmd.Parameters.AddWithValue("@Province", customers.Province);
                sqlcmd.Parameters.AddWithValue("@City", customers.City);
                sqlcmd.Parameters.AddWithValue("@Suburb", customers.Suburb);
                sqlcmd.Parameters.AddWithValue("@Street", customers.Street);
                sqlcmd.Parameters.AddWithValue("@StreetExtension", customers.StreetExtension);
                sqlcmd.Parameters.AddWithValue("@PostCode", customers.PostCode);
                sqlcmd.Parameters.AddWithValue("@StreetNo", customers.StreetNo);
                sqlcmd.Parameters.AddWithValue("@Verified", customers.Verified);

                sqlcmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

       [HttpGet]
        public ActionResult Edit(int pId = 0)
        {
            DataTable lCustomerTable = new DataTable();

            using (SqlConnection sqlcon = new SqlConnection(gConnectionString))
            {
                sqlcon.Open();
                string query = "SELECT CustomerId, Initials, FirstName, Surname, Address1 From Customer Where CustomerId = " + pId;
                SqlDataAdapter gCustomerAdapter = new SqlDataAdapter(query, sqlcon);
                //gCustomerAdapter.SelectCommand.Parameters.AddWithValue("@CustomerId", id.ToString());
              
                gCustomerAdapter.Fill(lCustomerTable);
            }

            Customer lCustomer = new Customer();
            lCustomer.ContactID = Convert.ToInt32(lCustomerTable.Rows[0][0].ToString());
            lCustomer.Initials = lCustomerTable.Rows[0][1].ToString();
            lCustomer.FirstName = lCustomerTable.Rows[0][2].ToString();
            lCustomer.Surname = lCustomerTable.Rows[0][3].ToString();
            lCustomer.Address1 = lCustomerTable.Rows[0][4].ToString();
            return View("Edit", lCustomer);
   
        }

        //POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(Customer pCustomer)
        {
            DataTable lCustomerTable = new DataTable();
            SqlDataAdapter lCustomerAdapter;
            using (SqlConnection sqlcon = new SqlConnection(gConnectionString))
            {
                sqlcon.Open();
                string query = "SELECT CustomerId, Initials, FirstName, Surname, Address1 From Customer Where CustomerId = " + pCustomer.ContactID;
                lCustomerAdapter = new SqlDataAdapter(query, sqlcon);
                lCustomerAdapter.Fill(lCustomerTable);
            }

            lCustomerTable.Rows[0][1] = pCustomer.Initials;
            lCustomerTable.Rows[0][2] = pCustomer.FirstName;
            lCustomerTable.Rows[0][3] = pCustomer.Surname;
            lCustomerTable.Rows[0][4] = pCustomer.Address1;

            using (SqlConnection sqlcon = new SqlConnection(gConnectionString))
            {
                sqlcon.Open();
                string lCommandText = "UPDATE Customer SET Initials = @Initials, "
                    + "FirstName = @FirstName," 
                    + "Surname = @FirstName," 
                    + "Address1 = @Address1" 
                    + " Where CustomerId = @ContactID";
                SqlCommand lCommand = new SqlCommand(lCommandText, sqlcon);
                lCommand.Parameters.AddWithValue("@ContactID", pCustomer.ContactID);
                lCommand.Parameters.AddWithValue("@Initials", pCustomer.Initials);
                lCommand.Parameters.AddWithValue("@FirstName", pCustomer.FirstName); 
                lCommand.Parameters.AddWithValue("@Surname", pCustomer.Surname);
                lCommand.Parameters.AddWithValue("@Address1", pCustomer.Address1);
                lCustomerAdapter = new SqlDataAdapter();
                lCustomerAdapter.UpdateCommand = lCommand;
                lCustomerAdapter.Update(lCustomerTable);
            }

                //sqlcon.Open();
                //string query = "UPDATE Customer SET  ContactID = @ContactID, Password1 = @Password1 WHere ContactID = @ContactID";
                //SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                //sqlcmd.Parameters.AddWithValue("@ContactID", pCustomer.ContactID);
                //sqlcmd.Parameters.AddWithValue("@Password1", pCustomer.Password1);
              
                //sqlcmd.ExecuteNonQuery();

            return RedirectToAction("Index");
        }

       
        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlcon = new SqlConnection(gConnectionString))
            {
                sqlcon.Open();
                string query = "DELETE From Customer  WHere 'ContactID' = @CustomerId";
                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                sqlcmd.Parameters.AddWithValue("@CustomerId", Convert.ToInt32(id));

                sqlcmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");

            
      
            }
            //||||||| 4b4da9e
           
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
                    //>>>>>>> 6378c03448bd7f410fabb1939ddaa7026d4930d5

                    List<Customer> lCustomerModel = new List<Customer>();
                    foreach (DataRow lRow in lCustomerTable.Rows)
                    {

                        lCustomerModel.Add(new Customer() { ContactID = (int)lRow[0], Surname = lRow[1].ToString(), EmailAddress = lRow[2].ToString(), ProductName = lRow[3].ToString() });
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
