//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace net.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class bookType
    {
        public bookType()
        {
            this.book = new HashSet<book>();
        }
    
        public int type_id { get; set; }
        public string type_name { get; set; }
    
        public virtual ICollection<book> book { get; set; }
    }
}
