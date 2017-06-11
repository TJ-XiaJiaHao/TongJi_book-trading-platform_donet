using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace net.Controllers.ViewModel
{
    public class OrderItem
    {
        public int o_id { get; set; }
        public int c_id { get; set; }
        public int s_id { get; set; }
        public string status { get; set; }
    }
}