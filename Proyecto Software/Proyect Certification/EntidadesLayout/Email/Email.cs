using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntidadesLayout.Email
{
    public class Email
    {
        public string From { get; set; }
        public string To { get; set; } 
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Cc { get; set; }
        public string Cco { get; set; }
    }
}
