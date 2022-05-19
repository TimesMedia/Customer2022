using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Customer2022.Models
{
    public class Customer
    {
        [Required]
        public int ContactID { get; set; }
        [DisplayName("Customer Initials")]
        [Required]
        public string Initials { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string Surname { get; set; }
       
        public string Subscription  { get; set; }
        
        public int InvoiceId { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        public string  PhoneNumber { get; set; }
        public string Password1 { get; set; }
        public string ProductName  { get; set; }
        public string Address1 { get; set; }
    }
   
}