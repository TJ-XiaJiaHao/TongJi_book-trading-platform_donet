using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace net.Controllers.ViewData
{
    public class uploadFile
    {
        public HttpPostedFileBase files { get; set; }
        public string fileName { get; set; }
    }
}