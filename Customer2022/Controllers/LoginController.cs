using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Customer2022.Models;
using System.Data.SqlClient;


namespace Customer2022.Controllers
{   
    public class LoginController : Controller
    {
        SqlConnection con = new SqlConnection();
        //SqlCommand com = new SqlCommand();
        //SqlDataReader dr;
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        void connectionString()
        {
            con.ConnectionString = @"Data Source=.\SQLEXPRESS; Database=ASPCRUD;Integrated Security=True";
        }
        //public ActionResult Verify(Usermodel user)
        //{
        //    connectionString();
        //    con.Open();
        //    com.Connection = con;
        //    com.CommandText = "select * from Members where Username = '"+ user.Username+"' and Password ='"+user.Password+"' " ;
        //    dr = com.ExecuteReader();
        //    if (dr.Read())
        //    {
        //        con.Close();
        //        return View();
        //    }
        //    else
        //    {
        //        con.Close();
        //        return View();
        //    }
           
        //}
        
        [HttpPost]
        public ActionResult Index(Usermodel user)

        {

            if (user.Username == "Username" && user.Password == "Password")
            {
                ViewBag.Message = "Login Successful";
                return View("Processlogin");
            }
            else
            {
                ViewBag.Message = "Login failed";
                return View("Index");
            }
        }
    }
}