using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminServerObject
{
    public class BindingAddress
    {
        public String ipAddress { get; set; } = "*";
        public bool bound { get; set; } = true;
    }
}
