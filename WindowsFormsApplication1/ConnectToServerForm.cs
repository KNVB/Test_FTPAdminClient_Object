using AdminServerObject;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class ConnectToServerForm : Form
    {
        public AdminServer adminServer { get; set; }
        private SortedDictionary<string, AdminServer> adminServerList =null;
        private int adminPortNo = -1;
        private string adminServerName = "";
        private string adminUserName = "", adminUserPassword = "";
        
        public ConnectToServerForm(SortedDictionary<string, AdminServer> adminServerList)
        {
            InitializeComponent();
            adminServer = null;
            this.adminServerList = adminServerList;
        }       

        private void ConnectToServerForm_Load(object sender, EventArgs e)
        {

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
            this.Dispose();
        }
        private bool isAllInputValid()
        {
            bool isValidate = true;
            adminServerName = serverName.Text.Trim();
            if (String.IsNullOrEmpty(adminServerName))
            {
                MessageBox.Show(this, "Please enter the admin. server name or IP address.", "Alert");
                serverName.Focus();
                isValidate = false;
            }
            else
            {
                if (String.IsNullOrEmpty(portNo.Text))
                {
                    MessageBox.Show(this, "Please enter the admin. server port no. (0-65535).", "Alert");
                    portNo.Focus();
                    isValidate = false;
                }
                else
                {
                    adminPortNo = Convert.ToInt32(portNo.Text);
                    adminUserName = userName.Text.Trim();
                    if (String.IsNullOrEmpty(adminUserName))
                    {
                        MessageBox.Show(this, "Please enter the admin. user name.", "Alert");
                        userName.Focus();
                        isValidate = false;
                    }
                    else
                    {
                        adminUserPassword = password.Text.Trim();
                        if (String.IsNullOrEmpty(adminUserPassword))
                        {
                            MessageBox.Show(this, "Please enter the admin. user password.", "Alert");
                            password.Focus();
                            isValidate = false;
                        }
                    }
                }
            }
            return isValidate;
        }
        private void loginButton_Click(object sender, EventArgs e)
        {
            if (isAllInputValid())
            {
                if (adminServerList.ContainsKey(adminServerName+":"+ adminPortNo))
                {
                    MessageBox.Show(this, "The specified server have been added", "Alert");
                }
                else
                {
                    Cursor.Current = Cursors.WaitCursor;
                    adminServer = new AdminServer();
                    if (adminServer.connect(adminServerName, adminPortNo))
                    {
                        if (adminServer.login(adminUserName, adminUserPassword))
                        {
                            this.DialogResult = DialogResult.OK;
                            Cursor.Current = Cursors.Default;
                            this.Close();
                        }
                        else
                            MessageBox.Show(this, "Invalid admin. user or password", "Alert");
                    }
                    else
                    {
                        MessageBox.Show(this, "Invalid admin. server name or port no.", "Alert");
                    }
                    Cursor.Current = Cursors.Default;
                }
            }
        }
        private void password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loginButton_Click(this, new EventArgs());
            }
        }
    }
}
