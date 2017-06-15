using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace net.Controllers.ViewModel
{
    public class OrderJson : JsonResult
    {
        public int pageCnt { get; set; }
        public int curPage { get; set; }
        public string key { get; set; }
    }
}