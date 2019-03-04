using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.ClientEngine;
using WebSocket4Net;

namespace WindowsFormsApplication1
{
    public class AdminServer
    {
        public AdminServer()
        {

        }

        public void login(string hostName, int portNo, string userName, string password)
        {
            string URL = "ws://" + hostName + ":" + portNo+"/websocket";
            WebSocket _websocket = new WebSocket(URL);
            _websocket.Opened += new EventHandler(websocket_Opened);
            _websocket.Error += new EventHandler<SuperSocket.ClientEngine.ErrorEventArgs>(websocket_Error);
            _websocket.Closed += new EventHandler(websocket_Closed);
            _websocket.MessageReceived += new EventHandler<MessageReceivedEventArgs>(websocket_MessageReceived);
            _websocket.Open();
        }

        private void websocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Console.Write("Raw Server response:" + e.Message);
        }

        private void websocket_Error(object sender, ErrorEventArgs e)
        {
            Console.WriteLine("An error occur:" + e.ToString());
        }

        private void websocket_Closed(object sender, EventArgs e)
        {
            Console.WriteLine("Connection to admin. server is closed");
        }

        private void websocket_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("Connection opened");
            sendRSAKey();
        }
        private void sendRSAKey()
        {

        }
    }
}
