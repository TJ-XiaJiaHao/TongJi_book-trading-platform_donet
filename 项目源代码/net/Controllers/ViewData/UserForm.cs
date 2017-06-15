using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace net.Controllers.ViewModel
{
    public class UserForm
    {
        [Required(ErrorMessage = "Enter your nick name")]
        public string name { get; set; }
        [RegularExpression(@"^[0-2]\d*$", ErrorMessage = "please enter validate sex")]
        public string sex { get; set; }
        [Required(ErrorMessage = "Enter your email")]
        [RegularExpression(@"^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$", ErrorMessage = "Please enter validate email")]
        public string email { get; set; }
        [Required(ErrorMessage = "Enter your phone number")]
        [RegularExpression(@"^[0-9]\d*$", ErrorMessage = "please enter validate phone number")]
        public string phone { get; set; }
    }
}