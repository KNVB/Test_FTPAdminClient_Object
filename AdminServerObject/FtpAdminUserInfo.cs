using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminServerObject
{
    public class FtpAdminUserInfo
    {
        public String userId { get; set; } = "0";
        public String userName { get; set; } = "";
        public String password { get; set; } = "";
        public bool enabled { get; set; } = false;
    }
}
