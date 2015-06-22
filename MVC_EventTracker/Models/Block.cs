using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_EventTracker.Models
{
    public class Block
    {
        public int BlockID { get; set; }
        public int EventID { get; set; }
        public DateTime BlockStart { get; set; }
        public DateTime BlockEnd { get; set; }
    }
}