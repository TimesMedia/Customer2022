using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Customer2022.ViewModels
{
    public class ResetPasswordVM
    { [NotMapped]
      [Display(Name="Current Password")]
        public string CurrentPassword { get; set; }
        [Display(Name ="New Password")]
        public string NewPassword { get; set; }
    }
}