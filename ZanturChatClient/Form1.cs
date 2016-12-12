using NetworksApi.TCP.CLIENT;
using System;
using System.Windows.Forms;

namespace ZanturChatClient
{

    public delegate void UpdateText(string txt);
    public delegate void UpdateListBox(ListBox box, string value, bool remove);
    public delegate void UpdateCounter(int count);

    public partial class frmClient : Form
    {
        public frmClient()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }


        Client client;
        private void ConnectServer()
        {
            if (txtUserName.Text != "" && txtIp.Text != "" && txtPort.Text != "")
            {
                client = new Client();
                client.ClientName = txtUserName.Text;
                client.ServerIp = txtIp.Text;
                client.ServerPort = txtPort.Text;
                client.OnClientConnected += new OnClientConnectedDelegate(client_OnClientConnected);
                client.OnClientConnecting += new OnClientConnectingDelegate(client_OnclientConnecting);
                client.OnClientDisconnected += new OnClientDisconnectedDelegate(client_OnClientDisconnected);
                client.OnClientError += new OnClientErrorDelegate(client_OnClientError);
                client.OnClientFileSending += new OnClientFileSendingDelegate(client_OnClientFileSending);
                client.OnDataReceived += new OnClientReceivedDelegate(client_OnDataReceived);
                client.Connect();
                user = txtUserName.Text;
                this.Text = "Zantur Chat Client  |  User:" + user;
            }

            else
            {
                MessageBox.Show("Please enter user name, ip and port numbers!");

            }

            txtMessages.SelectionStart = txtMessages.Text.Length;
            txtMessages.ScrollToCaret();
        }








        private void ChangeListBox(ListBox box, string value, bool remove)
        {
            if (box.InvokeRequired)
            {
                Invoke(new UpdateListBox(ChangeListBox), new object[] { box, value, remove });
            }
            else
            {
                if (remove)
                {
                    box.Items.Remove(value);
                }
                else
                {
                    box.Items.Add(value);
                }
            }
        }








        private void ChangeCounter(int count)
        {
            if (tabPage1.InvokeRequired)
            {
                Invoke(new UpdateCounter(ChangeCounter), new object[] { count });
            }
            else
            {
                tabPage1.Text = "Online Users (" + count.ToString() + ")";
            }
        }






        private void client_OnDataReceived(object Sender, ClientReceivedArguments R)
        {
            string[] data = R.ReceivedData.Split('#');
            if (data[0] != "adminMsg" && data[0] != "userMsg" && data[0] != "allMsg" && data[0] != "userAllMsg" && data[0] != "privMsg" )
            {
                //MessageBox.Show(data[0]);
                //MessageBox.Show(data[1]);
                if (listBox1.Items != null)
                {
                    listBox1.Items.Clear();
                }
                for (int i = 0; i < Convert.ToInt32(data[0]); i++)
                {
                    ChangeListBox(listBox1, data[i+1], false);
                }
                
                ChangeCounter(Convert.ToInt32(data[0]));
            }
            else if(data[0] == "adminMsg")
            {
                //ChangeTextBox(R.ReceivedData);
                ChangeTextBox("Admin:" + data[1]);
            }
            else if (data[0] == "userMsg")
            {
                //ChangeTextBox(R.ReceivedData);
                ChangeTextBox("User:" + data[1]);
            }
            else if (data[0] == "allMsg")
            {
                //ChangeTextBox(R.ReceivedData);
                ChangeTextBox("Server:" + data[1]);
            }
            else if (data[0] == "userAllMsg")
            {
                //ChangeTextBox(R.ReceivedData);
                ChangeTextBox(data[1]);
            }
            else if (data[0] == "privMsg")
            {
                //ChangeTextBox(R.ReceivedData);
                //ChangeTextBox(data[0] + " : " + data[1]);
                //MessageBox.Show(data[1] + " : " + data[2]);


                bool found = false;
                foreach (TabPage tab in tabControl2.TabPages)
                {
                    if ( data[1] == tab.Name && tab.Name != "tabLobby" )
                    {
                        //tabControl1.SelectedTab = tab;
                        found = true;
                        //MessageBox.Show("tab var");
                    }

                    if (!found)
                    {
                       
                        
                        tabControl2.BeginInvoke((Action)(() =>
                        {
                            if (!found)
                            {
                                TabPage myTabPage = new TabPage(data[1]);
                                myTabPage.Name = data[1];
                                myTabPage.Controls.Add(createNewChatTxt(data[1]));
                                tabControl2.TabPages.Add(myTabPage);
                            }
                            tabControl2.SelectTab(data[1]);
                            TextBox txtCreatedPrivMessages = (TextBox)tabControl2.Controls.Find("txtUser_" + data[1], true)[0];

                            if (txtCreatedPrivMessages != null)
                            {
                                txtCreatedPrivMessages.Text += data[1] + " : " + data[2] + "\r\n";
                                txtCreatedPrivMessages.SelectionStart = txtCreatedPrivMessages.Text.Length;
                                txtCreatedPrivMessages.ScrollToCaret();
                            }
                            txtChat.Focus();

                        }));
                        





                        
                    }

                }

                
            }

        }





        private TextBox createNewChatTxt(string name)
        {
            TextBox txtPrivMessage = new TextBox();
            txtPrivMessage.Name = "txtUser_" + name;
            txtPrivMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            txtPrivMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            txtPrivMessage.Location = new System.Drawing.Point(0, 0);
            txtPrivMessage.Multiline = true;
            txtPrivMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            txtPrivMessage.Size = new System.Drawing.Size(555, 398);
            txtPrivMessage.ReadOnly = true;
            txtPrivMessage.BackColor = System.Drawing.Color.White;
            return txtPrivMessage;
        }





        private void client_OnClientFileSending(object Sender, ClientFileSendingArguments R)
        {

        }

        private void client_OnClientError(object Sender, ClientErrorArguments R)
        {
            ChangeTextBox(R.ErrorMessage);

        }

        private void client_OnClientDisconnected(object Sender, ClientDisconnectedArguments R)
        {

            ChangeTextBox(R.EventMessage);
        }

        private void client_OnclientConnecting(object Sender, ClientConnectingArguments R)
        {
            ChangeTextBox(R.EventMessage);
        }

        private void client_OnClientConnected(object Sender, ClientConnectedArguments R)
        {

            ChangeTextBox(R.EventMessage);
        }








        private void ChangeTextBox(string txt)
        {
            if (txtMessages.InvokeRequired)
            {
                Invoke(new UpdateText(ChangeTextBox), new object[] { txt });

            }
            else
            {

                string a = txt.StartsWith("Connected").ToString();
                if (a == "True")
                {
                    tabControl1.TabPages.Insert(0, tabPage1);
                    tabControl1.SelectTab(tabPage1);
                }
                else if (txt == "Disconnected")
                {
                    tabControl1.TabPages.Remove(tabPage1);
                }

                txtMessages.Text += txt + "\r\n";
                txtMessages.SelectionStart = txtMessages.Text.Length;
                txtMessages.ScrollToCaret();

            }
        }










        private void frmClient_Load(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage2);
            tabControl1.TabPages.Remove(tabPage1);

        }







        string user;
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (client != null && client.IsConnected)
            {
                client.Disconnect();
                btnStart.Text = "Connect";
            }
            else
            {
                ConnectServer();
                btnStart.Text = "Disconnect";
            }
        }








        private void btnSend_Click(object sender, EventArgs e)
        {
            if (client != null && client.IsConnected)
            {
                if (tabControl2.SelectedTab == tabLobby)
                {
                    client.Send("userAllMsg#" + user + " : " + txtChat.Text);
                    //txtMessages.Text += user + " : " + txtChat.Text + "\r\n";
                    txtMessages.SelectionStart = txtMessages.Text.Length;
                    txtMessages.ScrollToCaret();
                }
                else
                {
                    sendPrivMessage(selectedTabName, txtChat.Text);
                    TextBox txtCreatedPrivMessages = (TextBox)tabControl2.Controls.Find("txtUser_" + selectedTabName, true)[0];

                    if (txtCreatedPrivMessages != null)
                    {
                        txtCreatedPrivMessages.Text += user + " : " + txtChat.Text + "\r\n";
                        txtCreatedPrivMessages.SelectionStart = txtCreatedPrivMessages.Text.Length;
                        txtCreatedPrivMessages.ScrollToCaret();
                    }
                }
                
                
            }
            txtMessages.SelectionStart = txtMessages.Text.Length;
            txtMessages.ScrollToCaret();
            txtChat.Focus();
            txtChat.Text = "";
            txtChat.Focus();
        }

        private void frmClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode);
        }

        
        
        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedItem.ToString() != user)
            {
                int index = this.listBox1.IndexFromPoint(e.Location);

                if (index != System.Windows.Forms.ListBox.NoMatches)
                {
                    //MessageBox.Show(listBox1.SelectedItem.ToString());
                    if (!CheckOpened(listBox1.SelectedItem.ToString()))
                    {
                        bool found = false;
                        foreach (TabPage tab in tabControl2.TabPages)
                        {
                            if (listBox1.SelectedItem.ToString().Equals(tab.Text))
                            {
                                //tabControl1.SelectedTab = tab;
                                found = true;
                                tabControl2.SelectTab(tab.Text);
                            }
                        }
                        if (!found)
                        {


                            TabPage myTabPage = new TabPage(listBox1.SelectedItem.ToString());
                            myTabPage.Name = listBox1.SelectedItem.ToString();
                            myTabPage.Text = listBox1.SelectedItem.ToString();
                            myTabPage.Controls.Add(createNewChatTxt(listBox1.SelectedItem.ToString()));
                            tabControl2.TabPages.Add(myTabPage);
                            tabControl2.SelectTab(listBox1.SelectedItem.ToString());
                            txtChat.Focus();

                        }
                    }



                }
            }
        }





        private bool CheckOpened(string name)
        {
            FormCollection fc = Application.OpenForms;

            foreach (Form frm in fc)
            {
                if (frm.Text == name)
                {
                    return true;
                }
            }
            return false;
        }




        public void sendPrivMessage(string userName, string message)
        {
            if (client != null && client.IsConnected)
            {
                client.Send("privMsg#" + userName + "#" + message);
                
            }
        }

        string selectedTabName;
        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedTabName = tabControl2.SelectedTab.Text;
        }

        private void txtChat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSend.PerformClick();
                e.Handled = true;
            }
        }
    }
}
