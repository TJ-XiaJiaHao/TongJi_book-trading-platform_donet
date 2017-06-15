using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Validation;
using System.ComponentModel.DataAnnotations;

namespace net.Controllers.ViewModel
{
    public class RegisterForm
    {
        //Required表示必填项
        [Required(ErrorMessage = "Enter your nick name")]
        public string nickName { get; set; }
        [Required(ErrorMessage = "Enter your phone number")]
        [RegularExpression(@"^[0-9]\d*$", ErrorMessage = "please enter validate phone number")]
        public string phone { get; set; }
        [Required(ErrorMessage = "Enter your email")]
        [RegularExpression(@"^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$", ErrorMessage = "enter validate email")]
        public string email { get; set; }
        [Required(ErrorMessage = "Enter your password")]
        public string password { get; set; }
    }
}