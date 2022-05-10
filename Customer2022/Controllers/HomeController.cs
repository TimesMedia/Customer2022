using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Customer2022.Models;
using Customer2022.ViewModels;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;

namespace Customer2022.Controllers
{

    public class HomeController : Controller
    {
        ASPCRUDEntities2 db = new ASPCRUDEntities2();
        SqlConnection con = new SqlConnection();
        string connectionString = @"Data Source=.\SQLEXPRESS; Database=ASPCRUD;Integrated Security=True";
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Index(Customers user)
        {
            bool isUserExists = db.Contacts.Any(u => u.Username == user.Username && u.Password == user.Password);
            if (isUserExists)
            {
                Session["ContactID"] = db.Contacts.Where(u => u.Username == user.Username && u.Password == user.Password).Single().ContactId;
                return View("dashboard");
            }
            else
            {
                ViewBag.Fail = "Ooops, invalid Credentials";
                
            }
            return View();
        }
        //GET: Customer/ResetPassword
        public ActionResult ResetPassword()
        {
            return View();
        }

        //POST: Customer/ResetPassword
        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordVM rp)
        {
            int uid = Convert.ToInt32(Session["ContactID"]);
            Contact u = db.Contacts.Find(uid);
            if (u.Password == rp.NewPassword)
            {
                u.Password = rp.NewPassword;
                db.Entry(u).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Message = " Your Password is updated";
            }
            else 
            { 
                ViewBag.Message = "Invalid Current Password"; 
            }
            return View("dashboard");
        }

        public ActionResult dashboard()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}