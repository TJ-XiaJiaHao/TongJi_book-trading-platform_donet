using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using net.Models;

namespace net.DAL
{
    public class BookInformation
    {
        public book book{ get; set; }
        public bookDetail bookDetails { get; set; }
    }
}