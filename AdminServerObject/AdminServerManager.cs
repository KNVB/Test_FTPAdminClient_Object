using System;
using System.Collections.Generic;

namespace AdminServerObject
{
    public class AdminServerManager
    {
        public SortedDictionary<string, AdminServer> adminServerList =new SortedDictionary<string, AdminServer>();
        public string lastServerKey = "";
        public int addAdminServer(string adminServerName,int adminPortNo,string adminUserName,string adminUserPassword)
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
        public void disconnectServer(string key)
        {
            AdminServer adminServer;
            adminServer = adminServerList[key];
            adminServer.disConnect();
            adminServerList.Remove(key);
        }
        public void disconnectAllAdminServer()
        {
            List<string> keys = new List<string>();
            
            foreach (string key in adminServerList.Keys)
            {
                keys.Add(key); 
            }
            foreach (string key in keys)
            {
                disconnectServer(key);
            }
            keys.Clear();
            keys = null;
        }
    }
}
