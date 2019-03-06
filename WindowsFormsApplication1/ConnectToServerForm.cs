using System;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class ConnectToServerForm : Form
    {
        public ConnectToServerForm()
        {
            InitializeComponent();
        }       

        private void ConnectToServerForm_Load(object sender, EventArgs e)
        {

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            bool isValidate = true;
            string adminUserName,adminUserPassword;
            string adminServerName = serverName.Text.Trim();
            if (String.IsNullOrEmpty(adminServerName))
            {
                MessageBox.Show(this,"Please enter the admin. server name or IP address.","Alert");
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
                if (isValidate)
                {
                    MessageBox.Show("All Ok");
                }
            }
        }
    }
}
