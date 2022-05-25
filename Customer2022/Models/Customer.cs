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
        public string Initials { get; set; }
        
        public int TitleId { get; set; }
        [DisplayName("Customer Name")]
        [Required]
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public int CompanyId { get; set; }
        public string Address1 { get; set; }
        public string Address3 { get; set; }
        public int Address5 { get; set; }
        public int AddressType { get; set; }
        public int PostAddressId { get; set; }
        public int PhysicalAddressId { get; set; }
        public int CountryId { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public int Liability { get; set; }
        public int Correspondence2 { get; set; }
        public DateTime VerificationDate  { get; set; }
        public int CheckpointPaymentTransactionId { get; set; }
        public DateTime CheckpointDatePayment {get;set;}
        public int CheckpointValue { get; set; }
        public DateTime CheckpointDateInvoice { get; set; }
        public int AutomaticPaymentAllocation { get; set; }
        public string ModifiedBy { get; set; }
        public int Marketing { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ProductName { get; set; }
        public string Password1 { get; set; }
        public string Province  { get; set; }
        public string City { get; set; }
        public string  Suburb { get; set; }
        public string Street { get; set; }
        public string StreetExtension { get; set; }
        public int PostCode { get; set; }
        public int Verified { get; set; }
        public string StreetNo { get; set; }
        
    }
   
}