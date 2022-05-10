using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Customer2022.Models
{
    public class Customers
    {
        [Required]
        public int ContactID { get; set; }
        [DisplayName("Customer Name")]
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Subscription  { get; set; }
        [Required]
        public int Invoice { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}