using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectLibrary
{
    public class ServerResponse
    {
        public string action { get; set; }
        public int responseCode { get; set; }
        public string returnMessage { get; set; }
        public Dictionary<String, Object> returnObjects { get; set; }
    }
}
