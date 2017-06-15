using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using net.Models;

namespace net.DAL
{
    public class BookDetails
    {
        public book book { get; set; }
        public bookDetail bookDetails { get; set; }
        public List<BookInformation> similarBook { get; set; }
        public List<Comment> CommentList { get; set; }
        public users seller { get; set; }
    }
}