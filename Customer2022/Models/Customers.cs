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
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string  Address  { get; set; }
    }
}