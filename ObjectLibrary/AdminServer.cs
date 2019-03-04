using System;
using SuperSocket.ClientEngine;
using WebSocket4Net;
using System.Security.Cryptography;
using CSharp_easy_RSA_PEM;
using System.Web.Script.Serialization;

namespace ObjectLibrary
{
    public class AdminServer
    {
        bool firstConnect=true;
        int rsaKeySize = 2048;
        JavaScriptSerializer js;
        MessageCoder messageCoder;
        RSACryptoServiceProvider rsaProvider;
        string adminUserName, adminPassword;
        WebSocket _websocket;
        
        public AdminServer()
        {
            js = new JavaScriptSerializer();
        }

        public void login(string hostName, int portNo, string userName, string password)
        {
            string URL = "ws://" + hostName + ":" + portNo+"/websocket";
            adminUserName= userName;
            adminPassword= password;
            _websocket = new WebSocket(URL);
            _websocket.Opened += new EventHandler(websocket_Opened);
            _websocket.Error += new EventHandler<SuperSocket.ClientEngine.ErrorEventArgs>(websocket_Error);
            _websocket.Closed += new EventHandler(websocket_Closed);
            _websocket.MessageReceived += new EventHandler<MessageReceivedEventArgs>(websocket_MessageReceived);
            _websocket.Open();
        }

        private void websocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Console.WriteLine("Raw Server response:" + e.Message);
            if (firstConnect)
            {
                byte[] encryptedKeyAsBytes = Convert.FromBase64String(e.Message);
                byte[] plainBytes = rsaProvider.Decrypt(encryptedKeyAsBytes, false);
                string plainText = System.Text.Encoding.UTF8.GetString(plainBytes);
                Console.WriteLine("plain text=" + plainText);
                messageCoder =js.Deserialize<MessageCoder>(plainText);
                messageCoder.init(); 
                Console.WriteLine("messageKeyString="+messageCoder.messageKey);
                Console.WriteLine("ivTestString=" + messageCoder.ivText);
                Login login = new Login();
                login.password = adminPassword;
                login.userName = adminUserName;
                _websocket.Send(messageCoder.encode(js.Serialize(login)));
                firstConnect = false;
            }
            else
            {
                Console.WriteLine("Decoded Server response:" + messageCoder.decode(e.Message));
            }
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
            firstConnect = true;
            rsaProvider = new RSACryptoServiceProvider(rsaKeySize);
            string publicKeyStr = Crypto.ExportPublicKeyToX509PEM(rsaProvider);
            publicKeyStr = publicKeyStr.Replace("-----BEGIN PUBLIC KEY-----\n", "");
            publicKeyStr = publicKeyStr.Replace("\n-----END PUBLIC KEY-----", "");
            _websocket.Send(publicKeyStr);
        }
    }
}
