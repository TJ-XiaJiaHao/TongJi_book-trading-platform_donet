//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web;
//using System.IO;

//namespace DLL.UploadFile
//{
//    public class UploadFile
//    {
//        public byte[] getDate(HttpPostedFileBase file)
//        {
//            byte[] date = null;
//            using (var binary = new BinaryReader(file.InputStream))
//            {
//                date = binary.ReadBytes(file.ContentLength);
//            }
//            return date;
//        }
//        public void upload(HttpPostedFileBase file,string fileName)
//        {
//            byte[] date = getDate(file);
//            if (date != null && date.Length != 0)
//            {
//                string path = HttpContext.Current.Server.MapPath("~/" + fileName);
//                System.IO.File.WriteAllBytes(path, date);
//            }
//        }
//    }
//}
