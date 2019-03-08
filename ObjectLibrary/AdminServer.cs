using log4net;
using log4net.Config;

using System;
using System.Collections;
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
        private MessageCoder messageCoder;
        private ServerResponse serverResponse;
        private WebSocket _websocket;
        private string errorMessage = "";
        private static readonly ILog logger = LogManager.GetLogger(typeof(AdminServer));
        public int portNo;
        public string serverName  { get; set; }
        public AdminServer()
        {
            jss = new JavaScriptSerializer();
            messageCoder = new MessageCoder();
            string log4netConfigFilePath = AppDomain.CurrentDomain.BaseDirectory + "log4net.config";
            XmlConfigurator.Configure(new FileInfo(log4netConfigFilePath));
        }
        public bool connect(string hostName, int portNo)
        {
            bool result = false;
            this.portNo = portNo;
            this.serverName = hostName;
            string URL = "ws://" + hostName + ":" + portNo + "/websocket";
            firstConnect = true;
            errorMessage = "";
            _websocket = new WebSocket(URL);
            _websocket.Opened += new EventHandler(websocket_Opened);
            _websocket.Error += new EventHandler<SuperSocket.ClientEngine.ErrorEventArgs>(websocket_Error);
            _websocket.Closed += new EventHandler(websocket_Closed);
            _websocket.MessageReceived += new EventHandler<MessageReceivedEventArgs>(websocket_MessageReceived);
            _websocket.Open();
            
            _messageReceivedEvent.WaitOne();
            if (String.IsNullOrEmpty(errorMessage))
                result = true;
            else
                result = false;
            return result;
        }
        public void disConnect()
        {
            _websocket.Close();
        }
        public bool login(string userName, string password)
        {
            bool result = true;
            Login login = new Login();
            login.password = password;
            login.userName = userName;
            _websocket.Send(messageCoder.aesEncode(jss.Serialize(login)));
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
        public FtpServerInfo getInitialFtpServerInfo()
        {
            FtpServerInfo result=null;
            Request request = new Request();
            request.action = "GetInitialFtpServerInfo";
            _websocket.Send(messageCoder.aesEncode(jss.Serialize(request)));
            _messageReceivedEvent.WaitOne();

            result= jss.Deserialize<FtpServerInfo>(jss.Serialize(serverResponse.returnObjects["ftpServerInfo"]));
            
            return result;
        }
        private void websocket_Closed(object sender, EventArgs e)
        {
            logger.Debug("Connection to admin. server is closed");
            errorMessage = e.ToString();
            _messageReceivedEvent.Set();
        }
        private void websocket_Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            logger.Debug("An error occur:" + e.ToString());
            errorMessage = e.ToString();
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
                decodedMessage = messageCoder.decodeRSAMessage(e.Message);
                dynamic AESObject = jss.Deserialize<dynamic>(decodedMessage);
                messageCoder.initAESCodec(AESObject["messageKey"], AESObject["ivText"]);
                firstConnect = false;
            }
            else
            {
                decodedMessage = messageCoder.aesDecode(e.Message);
                serverResponse = jss.Deserialize<ServerResponse>(decodedMessage);
            }
            logger.Debug("Decoded server response:" + decodedMessage);
            _messageReceivedEvent.Set();
        }
        private void sendRSAKey()
        {
            logger.Debug("Send RSA Key");
            _websocket.Send(messageCoder.getRSAPublicKey());
        }        
    }
}
