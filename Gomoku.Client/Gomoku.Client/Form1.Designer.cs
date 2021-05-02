
namespace Gomoku.Client
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pb_Board = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lb_Users = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_IP = new System.Windows.Forms.TextBox();
            this.tb_Port = new System.Windows.Forms.TextBox();
            this.tb_UserName = new System.Windows.Forms.TextBox();
            this.btn_Login = new System.Windows.Forms.Button();
            this.tb_MsgBox = new System.Windows.Forms.TextBox();
            this.tb_ChatBox = new System.Windows.Forms.TextBox();
            this.btn_SendMsg = new System.Windows.Forms.Button();
            this.btn_giveup = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Board)).BeginInit();
            this.SuspendLayout();
            // 
            // pb_Board
            // 
            this.pb_Board.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pb_Board.BackgroundImage")));
            this.pb_Board.Location = new System.Drawing.Point(0, 0);
            this.pb_Board.Name = "pb_Board";
            this.pb_Board.Size = new System.Drawing.Size(750, 750);
            this.pb_Board.TabIndex = 0;
            this.pb_Board.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(772, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "伺服器 IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(772, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "伺服器 Port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(772, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 19);
            this.label3.TabIndex = 3;
            this.label3.Text = "玩家名稱";
            // 
            // lb_Users
            // 
            this.lb_Users.Enabled = false;
            this.lb_Users.FormattingEnabled = true;
            this.lb_Users.ItemHeight = 19;
            this.lb_Users.Location = new System.Drawing.Point(772, 292);
            this.lb_Users.Name = "lb_Users";
            this.lb_Users.Size = new System.Drawing.Size(168, 42);
            this.lb_Users.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(772, 270);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 19);
            this.label4.TabIndex = 5;
            this.label4.Text = "遊戲成員";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(772, 352);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 19);
            this.label5.TabIndex = 6;
            this.label5.Text = "聊天室";
            // 
            // tb_IP
            // 
            this.tb_IP.Location = new System.Drawing.Point(772, 31);
            this.tb_IP.Name = "tb_IP";
            this.tb_IP.Size = new System.Drawing.Size(168, 27);
            this.tb_IP.TabIndex = 7;
            // 
            // tb_Port
            // 
            this.tb_Port.Location = new System.Drawing.Point(772, 93);
            this.tb_Port.Name = "tb_Port";
            this.tb_Port.Size = new System.Drawing.Size(168, 27);
            this.tb_Port.TabIndex = 8;
            // 
            // tb_UserName
            // 
            this.tb_UserName.Location = new System.Drawing.Point(772, 156);
            this.tb_UserName.Name = "tb_UserName";
            this.tb_UserName.Size = new System.Drawing.Size(168, 27);
            this.tb_UserName.TabIndex = 9;
            // 
            // btn_Login
            // 
            this.btn_Login.Location = new System.Drawing.Point(772, 204);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(168, 49);
            this.btn_Login.TabIndex = 10;
            this.btn_Login.Text = "登入伺服器";
            this.btn_Login.UseVisualStyleBackColor = true;
            // 
            // tb_MsgBox
            // 
            this.tb_MsgBox.Location = new System.Drawing.Point(772, 374);
            this.tb_MsgBox.Multiline = true;
            this.tb_MsgBox.Name = "tb_MsgBox";
            this.tb_MsgBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_MsgBox.Size = new System.Drawing.Size(168, 251);
            this.tb_MsgBox.TabIndex = 11;
            // 
            // tb_ChatBox
            // 
            this.tb_ChatBox.Location = new System.Drawing.Point(772, 631);
            this.tb_ChatBox.Name = "tb_ChatBox";
            this.tb_ChatBox.Size = new System.Drawing.Size(168, 27);
            this.tb_ChatBox.TabIndex = 12;
            // 
            // btn_SendMsg
            // 
            this.btn_SendMsg.Location = new System.Drawing.Point(772, 664);
            this.btn_SendMsg.Name = "btn_SendMsg";
            this.btn_SendMsg.Size = new System.Drawing.Size(168, 51);
            this.btn_SendMsg.TabIndex = 13;
            this.btn_SendMsg.Text = "發話";
            this.btn_SendMsg.UseVisualStyleBackColor = true;
            // 
            // btn_giveup
            // 
            this.btn_giveup.Location = new System.Drawing.Point(772, 721);
            this.btn_giveup.Name = "btn_giveup";
            this.btn_giveup.Size = new System.Drawing.Size(168, 29);
            this.btn_giveup.TabIndex = 14;
            this.btn_giveup.Text = "投降";
            this.btn_giveup.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 753);
            this.Controls.Add(this.btn_giveup);
            this.Controls.Add(this.btn_SendMsg);
            this.Controls.Add(this.tb_ChatBox);
            this.Controls.Add(this.tb_MsgBox);
            this.Controls.Add(this.btn_Login);
            this.Controls.Add(this.tb_UserName);
            this.Controls.Add(this.tb_Port);
            this.Controls.Add(this.tb_IP);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lb_Users);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pb_Board);
            this.Name = "Form1";
            this.Text = "五子棋";
            ((System.ComponentModel.ISupportInitialize)(this.pb_Board)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pb_Board;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lb_Users;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_IP;
        private System.Windows.Forms.TextBox tb_Port;
        private System.Windows.Forms.TextBox tb_UserName;
        private System.Windows.Forms.Button btn_Login;
        private System.Windows.Forms.TextBox tb_MsgBox;
        private System.Windows.Forms.TextBox tb_ChatBox;
        private System.Windows.Forms.Button btn_SendMsg;
        private System.Windows.Forms.Button btn_lose;
        private System.Windows.Forms.Button btn_giveup;
    }
}

