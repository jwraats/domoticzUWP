using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomoticzUWP.Models
{
    public class Light
    {
        public Boolean IsDimmer { get; set; }
        public String Name { get; set; }
        public String SubType { get; set; }
        public String Type { get; set; }
        public Int32 idx { get; set; }
    }
}
