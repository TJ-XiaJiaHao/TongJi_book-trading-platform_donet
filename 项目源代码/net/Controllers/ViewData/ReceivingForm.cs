using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace net.Controllers.ViewModel
{
    public class ReceivingForm
    {
        [Required(ErrorMessage = "Enter receiver name")]
        public string name { get; set; }
        [Required(ErrorMessage = "Enter phone number")]
        [RegularExpression(@"^[0-9]\d*$", ErrorMessage = "Please enter validate phone number")]
        public string phone { get; set; }
        [Required(ErrorMessage = "Enter phone number")]
        public string province { get; set; }
        public string city { get; set; }
        [Required(ErrorMessage = "Enter phone number")]
        public string street { get; set; }
        [Required(ErrorMessage = "Enter phone number")]
        public string address { get; set; }
    }
}