using log4net;
using log4net.Config;

using SuperSocket.ClientEngine;
using System;
using System.IO;
using System.Threading;
using System.Web.Script.Serialization;

using WebSocket4Net;

namespace ObjectLibrary
{
    public class AdminServer
    {
        bool firstConnect = true;
        private AutoResetEvent _messageReceivedEvent = new AutoResetEvent(false);
        private JavaScriptSerializer jss;
        private KeyGenerator keyGen;
        private ServerResponse serverResponse;
        private WebSocket _websocket;
        private static readonly ILog logger = LogManager.GetLogger(typeof(AdminServer));
        public AdminServer()
        {
            jss = new JavaScriptSerializer();
            keyGen=new KeyGenerator();
            string log4netConfigFilePath = AppDomain.CurrentDomain.BaseDirectory + "log4net.config";
            XmlConfigurator.Configure(new FileInfo(log4netConfigFilePath));
        }
        public bool connect(string hostName, int portNo)
        {
            bool result = false;
            string URL = "ws://" + hostName + ":" + portNo + "/websocket";
            firstConnect = true;
            _websocket = new WebSocket(URL);
            _websocket.Opened += new EventHandler(websocket_Opened);
            _websocket.Error += new EventHandler<SuperSocket.ClientEngine.ErrorEventArgs>(websocket_Error);
            _websocket.Closed += new EventHandler(websocket_Closed);
            _websocket.MessageReceived += new EventHandler<MessageReceivedEventArgs>(websocket_MessageReceived);
            _websocket.Open();
            _messageReceivedEvent.WaitOne();
            //Console.WriteLine("back to connect method");
            result = true;
            return result;
        }
        public bool login(string userName, string password)
        {
            bool result = true;
            Login login = new Login();
            login.password = password;
            login.userName = userName;
            _websocket.Send(keyGen.aesEncode(jss.Serialize(login)));
            _messageReceivedEvent.WaitOne();
            if (serverResponse.responseCode == 0)
            {
                logger.Info("Login to admin. server successfully.");
                result = true;
            }
            else
            {
                result = false;
                logger.Debug("Login to admin. server failure.");
            }
            return result;
        }

        private void websocket_Closed(object sender, EventArgs e)
        {
            logger.Debug("Connection to admin. server is closed");
            _messageReceivedEvent.Set();
        }
        private void websocket_Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            logger.Debug("An error occur:" + e.ToString());
            _messageReceivedEvent.Set();
        }
        private void websocket_Opened(object sender, EventArgs e)
        {
            logger.Debug("Connection opened");
            sendRSAKey();
        }
        private void websocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            logger.Debug("Raw Server response:" + e.Message);
            string decodedMessage="";
            if (firstConnect)
            {
                decodedMessage = keyGen.decodeRSAMessage(e.Message);
                dynamic AESObject = jss.Deserialize<dynamic>(decodedMessage);
                keyGen.initAESCodec(AESObject["messageKey"], AESObject["ivText"]);
                firstConnect = false;
            }
            else
            {
                decodedMessage = keyGen.aesDecode(e.Message);
                serverResponse = jss.Deserialize<ServerResponse>(decodedMessage);
            }
            logger.Debug("Decoded server response:" + decodedMessage);
            _messageReceivedEvent.Set();
        }
        private void sendRSAKey()
        {
            logger.Debug("Send RSA Key");
            _websocket.Send(keyGen.getRSAPublicKey());
        }
    }
}
