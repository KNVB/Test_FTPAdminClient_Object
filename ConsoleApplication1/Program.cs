using System;
using ObjectLibrary;
namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            AdminServer adminServer=new AdminServer();

            if (adminServer.connect("localhost", 4466))
            {
                adminServer.login("admin", "password");
            }
            Console.ReadLine();
        }
    }
}
