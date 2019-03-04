using System;
using WindowsFormsApplication1;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            AdminServer adminServer=new AdminServer();

            adminServer.login("localhost", 4466, "admin", "password");
            Console.ReadLine();
        }
    }
}
