using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trip
{
    public class BugEntity
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string Author { get; set; }
        public string Operator { get; set; }
        public string Priority { get; set; }
        public string CreateTS { get; set; }
        public string LastUpdatedTime { get; set; }
        public string StartTime { get; set; } 
    }
}
