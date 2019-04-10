using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectLibrary
{
    public class FtpAdminClient
    {
        internal SortedDictionary<string, AdminServer> adminServerList = new SortedDictionary<string, AdminServer>();
        internal string lastServerKey = "";
        public FtpAdminClient()
        {

        }
        internal void disconnectServer(string key)
        {
            AdminServer adminServer;
            adminServer = adminServerList[key];
            adminServer.disConnect();
            adminServerList.Remove(key);
        }
        internal void disconnectAllAdminServer()
        {
            string[] keys = adminServerList.Keys.ToArray();
            for (int i = 0; i < keys.Length; i++)
            {
                disconnectServer(keys[i]);
            }
        }

        internal int addRemoteServer(string adminServerName, int adminPortNo, string adminUserName, string adminUserPassword)
        {
            int result = 0;
            try
            {
                if (adminServerList.ContainsKey(adminServerName + ":" + adminPortNo))
                {
                    result = 1;
                }
                else
                {
                    AdminServer adminServer = new AdminServer();
                    if (adminServer.connect(adminServerName, adminPortNo))
                    {
                        if (adminServer.login(adminUserName, adminUserPassword))
                        {
                            adminServerList.Add(adminServerName + ":" + adminPortNo, adminServer);
                            lastServerKey = adminServerName + ":" + adminPortNo;
                        }
                        else
                            result = 3;
                    }
                    else
                    {
                        result = 2;
                    }
                }
            }
            catch (Exception err)
            {
                throw new Exception("An error occurs when login to admin. server:" + err.Message);
            }
            return result;
        }
    }
}
