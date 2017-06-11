using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using net.Models;

namespace net.DAL
{
    public class Comment
    {
        public users buyer { get; set; }
        public bookComment comment { get; set; }
    }
}