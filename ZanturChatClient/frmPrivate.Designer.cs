namespace ZanturChatClient
{
    partial class frmPrivate
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
            this.btnPrivSend = new System.Windows.Forms.Button();
            this.txtMessages = new System.Windows.Forms.TextBox();
            this.txtChat = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // btnPrivSend
            // 
            this.btnPrivSend.Location = new System.Drawing.Point(207, 198);
            this.btnPrivSend.Name = "btnPrivSend";
            this.btnPrivSend.Size = new System.Drawing.Size(65, 51);
            this.btnPrivSend.TabIndex = 2;
            this.btnPrivSend.Text = "Send";
            this.btnPrivSend.UseVisualStyleBackColor = true;
            this.btnPrivSend.Click += new System.EventHandler(this.btnPrivSend_Click);
            // 
            // txtMessages
            // 
            this.txtMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtMessages.Location = new System.Drawing.Point(12, 12);
            this.txtMessages.Multiline = true;
            this.txtMessages.Name = "txtMessages";
            this.txtMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessages.Size = new System.Drawing.Size(260, 180);
            this.txtMessages.TabIndex = 6;
            // 
            // txtChat
            // 
            this.txtChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtChat.Location = new System.Drawing.Point(12, 198);
            this.txtChat.Multiline = true;
            this.txtChat.Name = "txtChat";
            this.txtChat.Size = new System.Drawing.Size(189, 51);
            this.txtChat.TabIndex = 7;
            this.txtChat.Text = "Chat...";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // frmPrivate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.txtChat);
            this.Controls.Add(this.txtMessages);
            this.Controls.Add(this.btnPrivSend);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmPrivate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmPrivate";
            this.Load += new System.EventHandler(this.frmPrivate_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnPrivSend;
        private System.Windows.Forms.TextBox txtMessages;
        private System.Windows.Forms.TextBox txtChat;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}