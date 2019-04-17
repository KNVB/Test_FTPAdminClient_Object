using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminServerObject
{
    public class Request
    {
        public string action { get; set; }
        public Dictionary<String, Object> ObjectMap { get; set; } = new Dictionary<string, object>();
    }
}
