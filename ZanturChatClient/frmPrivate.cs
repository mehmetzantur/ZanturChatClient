using NetworksApi.TCP.CLIENT;
using System;
using System.Windows.Forms;

namespace ZanturChatClient
{
    public partial class frmPrivate : Form
    {
        public string userName { get; set; }
        public string message { get; set; }


        public frmPrivate()
        {
            InitializeComponent();
        }

        private void frmPrivate_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
            //privReceivedMessage(userName,message);
        }

        public void privReceivedMessage(string user,string message)
        {
            txtMessages.Text += user + " : " + message + "\r\n";
            txtMessages.SelectionStart = txtMessages.Text.Length;
            txtMessages.ScrollToCaret();
        }

        private void btnPrivSend_Click(object sender, EventArgs e)
        {

            ((frmClient)Application.OpenForms["frmClient"]).sendPrivMessage(userName,txtChat.Text);

            //if (client != null && client.IsConnected)
            //{
            //    client.Send(txtChat.Text);
            //    txtChat.Clear();
            //    txtMessages.SelectionStart = txtMessages.Text.Length;
            //    txtMessages.ScrollToCaret();
            //}
            //txtMessages.SelectionStart = txtMessages.Text.Length;
            //txtMessages.ScrollToCaret();
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
           // this.Text = userName;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("ok");
        }
    }
}
