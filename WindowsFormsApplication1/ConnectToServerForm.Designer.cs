namespace WindowsFormsApplication1
{
    partial class ConnectToServerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label3 = new System.Windows.Forms.Label();
            this.serverName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.userName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.TextBox();
            this.portNo = new System.Windows.Forms.NumericUpDown();
            this.loginButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.portNo)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "Server name:";
            // 
            // serverName
            // 
            this.serverName.Location = new System.Drawing.Point(85, 11);
            this.serverName.Name = "serverName";
            this.serverName.Size = new System.Drawing.Size(171, 22);
            this.serverName.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "Port no.:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "User name:";
            // 
            // userName
            // 
            this.userName.Location = new System.Drawing.Point(85, 77);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(171, 22);
            this.userName.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 113);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "Password:";
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(85, 113);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(171, 22);
            this.password.TabIndex = 7;
            // 
            // portNo
            // 
            this.portNo.Location = new System.Drawing.Point(85, 45);
            this.portNo.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.portNo.Name = "portNo";
            this.portNo.Size = new System.Drawing.Size(171, 22);
            this.portNo.TabIndex = 8;
            this.portNo.Value = new decimal(new int[] {
            4466,
            0,
            0,
            0});
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(29, 167);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(75, 23);
            this.loginButton.TabIndex = 9;
            this.loginButton.Text = "Connect";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(181, 167);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 10;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // ConnectToServerForm
            // 
            this.ClientSize = new System.Drawing.Size(280, 211);
            this.ControlBox = false;
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.portNo);
            this.Controls.Add(this.password);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.userName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.serverName);
            this.Controls.Add(this.label3);
            this.Name = "ConnectToServerForm";
            this.Text = "Connect To Server";
            this.Load += new System.EventHandler(this.ConnectToServerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.portNo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox serverName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox userName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.NumericUpDown portNo;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.Button cancelButton;
    }
}