using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace ObjectLibrary
{
    public class AdminServer
    {
        JavaScriptSerializer js;
        string adminUserName, adminPassword;
        public AdminServer()
        {
            js = new JavaScriptSerializer();
        }
        public bool connect(string hostName,int portNo)
        {
            bool result=true;
            return result;
        }
        public bool login(string userName, string password)
        {
         }
    }
}
