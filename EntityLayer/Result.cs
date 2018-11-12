using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer
{
    public class Result
    {
        public bool IsSuccessful { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
    }
}
