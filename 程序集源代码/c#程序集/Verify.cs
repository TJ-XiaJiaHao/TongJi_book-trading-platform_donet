using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DLL.Verify
{
    public class Verify
    {
        public bool IsEmail(string email)
        {
            String strExp = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            Regex r = new Regex(strExp);
            Match m = r.Match(email);
            return m.Success;
        }
        public bool IsHandset(string str_handset)
        {
            return Regex.IsMatch(str_handset, @"^[1]+[3,5]+\d{9}");
        }
    }
}