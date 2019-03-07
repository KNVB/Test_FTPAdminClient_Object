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
                if (adminServer.login("admin1", "password"))
                {
                   
                }
                adminServer.disConnect();
            }
            Console.ReadLine();
        }
    }
}
