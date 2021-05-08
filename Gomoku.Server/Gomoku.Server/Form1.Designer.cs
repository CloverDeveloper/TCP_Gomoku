
namespace Gomoku.Server
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_serverIP = new System.Windows.Forms.TextBox();
            this.tb_serverPort = new System.Windows.Forms.TextBox();
            this.btn_activation = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "ServerIP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "ServerPort";
            // 
            // tb_serverIP
            // 
            this.tb_serverIP.Location = new System.Drawing.Point(101, 12);
            this.tb_serverIP.Name = "tb_serverIP";
            this.tb_serverIP.ReadOnly = true;
            this.tb_serverIP.Size = new System.Drawing.Size(125, 27);
            this.tb_serverIP.TabIndex = 2;
            // 
            // tb_serverPort
            // 
            this.tb_serverPort.Location = new System.Drawing.Point(101, 45);
            this.tb_serverPort.Name = "tb_serverPort";
            this.tb_serverPort.Size = new System.Drawing.Size(125, 27);
            this.tb_serverPort.TabIndex = 3;
            // 
            // btn_activation
            // 
            this.btn_activation.Location = new System.Drawing.Point(232, 12);
            this.btn_activation.Name = "btn_activation";
            this.btn_activation.Size = new System.Drawing.Size(176, 60);
            this.btn_activation.TabIndex = 4;
            this.btn_activation.Text = "啟動伺服器";
            this.btn_activation.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 86);
            this.Controls.Add(this.btn_activation);
            this.Controls.Add(this.tb_serverPort);
            this.Controls.Add(this.tb_serverIP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_serverIP;
        private System.Windows.Forms.TextBox tb_serverPort;
        private System.Windows.Forms.Button btn_activation;
    }
}

