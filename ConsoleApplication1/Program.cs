using log4net;
using log4net.Config;
using ObjectLibrary;
using System;
using System.IO;
namespace ConsoleApplication1
{
    class Program
    {
        //private static readonly ILog log = LogManager.GetLogger(typeof(Program));
        static void Main(string[] args)
        {
            AdminServer adminServer=new AdminServer();
            if (adminServer.connect("localhost", 4466))
            {
                if (adminServer.login("admin", "password"))
                {
                    FtpServerInfo ftpServerInfo = adminServer.getInitialFtpServerInfo();
                    Console.WriteLine("FTP server Id:");
                    Console.WriteLine(ftpServerInfo.serverId);

                    Console.WriteLine("FTP server description:");
                    Console.WriteLine(ftpServerInfo.description);

                    Console.WriteLine("FTP server control port:");
                    Console.WriteLine(ftpServerInfo.controlPort);

                    Console.WriteLine("FTP server is passive mode enabled:");
                    Console.WriteLine(ftpServerInfo.passiveModeEnabled);

                    Console.WriteLine("FTP server passive mode port range:");
                    Console.WriteLine(ftpServerInfo.passiveModePortRange);
                    
                    Console.WriteLine("User List:");
                    FtpUserInfo ftpUserInfo;
                    foreach (string key in ftpServerInfo.ftpUserInfoList.Keys)
                    {
                        ftpUserInfo= ftpServerInfo.ftpUserInfoList[key];
                        Console.WriteLine("Id={0},Name={1},user Id={2}", key, ftpUserInfo.userName, ftpUserInfo.userId);
                    }
                    Console.WriteLine("Binding Address List:");
                    foreach (BindingAddress ba in ftpServerInfo.bindingAddresses)
                    {
                        Console.WriteLine("IP address={0},is bound={1}", ba.ipAddress, ba.bound);
                    }
                }
                adminServer.disConnect();
            }
            Console.ReadLine();
        }
    }
}
