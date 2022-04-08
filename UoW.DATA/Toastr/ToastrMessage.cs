using System;
using System.Collections.Generic;
using System.Text;

namespace UoW.DATA.Toastr
{
    public class ToastrMessage
    {
        public string Message { get; set; }
        public string Title { get; set; }
        public ToastrType type { get; set; }
    }
}
