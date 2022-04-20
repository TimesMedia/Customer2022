using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Customer2022.Models
{
    public class Customers
    {
        public int ContactID { get; set; }
        [DisplayName("Customer Name")]
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Subscription  { get; set; }
        public int Invoice { get; set; }
    }
}