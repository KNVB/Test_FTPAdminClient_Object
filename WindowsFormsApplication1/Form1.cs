using System;
using System.Collections;
using ObjectLibrary;
using System.Windows.Forms;
using System.Collections.Generic;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private SortedDictionary<string, AdminServer> adminServerList = new SortedDictionary<string, AdminServer>();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            AdminServer adminServer;
            foreach (string key in adminServerList.Keys)
            {
                adminServer = adminServerList[key];
                adminServer.disConnect();
            }
           
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            popupConnectToServerDiaglog();
        }     

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            popupConnectToServerDiaglog();
        }
        
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        private void popupConnectToServerDiaglog()
        {
            ConnectToServerForm ctsf = new ConnectToServerForm(adminServerList);
            DialogResult dialogresult = ctsf.ShowDialog();
            if (dialogresult.Equals(DialogResult.OK))
            {
                adminServerList.Add(ctsf.adminServer.serverName + ":" + ctsf.adminServer.portNo, ctsf.adminServer);
                refreshUI();
            }
            ctsf.Dispose();
        }
        private void refreshUI()
        {
            TreeView treeView = new TreeView();
            treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            treeView.TabIndex = 0;
            splitContainer.Panel1.Controls.Clear();
            splitContainer.Panel2.Controls.Clear();
            foreach (string key in adminServerList.Keys)
            {
                treeView.Nodes.Add(key);
            }
            splitContainer.Panel1.Controls.Add(treeView);
        }       
    }
}
