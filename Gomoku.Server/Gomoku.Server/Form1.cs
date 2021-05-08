using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.Collections;
using Gomoku.Common.Model;
using Newtonsoft.Json;
using Gomoku.Common.Enum;

namespace Gomoku.Server
{
    public partial class Form1 : Form
    {
        TcpListener server;
        Socket client;
        Thread th_Server;
        Thread th_Client;
        Hashtable ht;

        public Form1()
        {
            InitializeComponent();
            this.tb_serverIP.Text = this.GetServerIP();
            this.ht = new Hashtable();
            this.btn_activation.Click += Btn_ActivationServer;
            this.FormClosing += FormClose;
        }

        /// <summary>
        /// 取得 IP 位址
        /// </summary>
        /// <returns></returns>
        private string GetServerIP()
        {
            IPAddress[] ips = Dns.GetHostEntry(Dns.GetHostName()).AddressList;

            foreach(IPAddress ip in ips)
            {
                if(ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 啟動 Server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ActivationServer(object sender,EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            this.btn_activation.Enabled = false;

            this.th_Server = new Thread(new ThreadStart(ServerListener));
            this.th_Server.IsBackground = true;
            this.th_Server.Start();
        }

        /// <summary>
        /// 監聽 Server 事件
        /// </summary>
        private void ServerListener()
        {
            if (!int.TryParse(this.tb_serverPort.Text, out int port))
            {
                MessageBox.Show("Port 輸入錯誤");
                this.btn_activation.Enabled = true;
                return;
            }

            // 設定伺服器端點
            IPEndPoint ipEP = new IPEndPoint(IPAddress.Parse(this.tb_serverIP.Text), port);

            // 使用本機端點初始化類別
            this.server = new TcpListener(ipEP);

            // 設定最大連線數
            this.server.Start(2);

            while (true)
            {
                this.client = this.server.AcceptSocket();
                this.th_Client = new Thread(new ThreadStart(ClientListener));
                this.th_Client.IsBackground = true;
                this.th_Client.Start();
            }
        }

        /// <summary>
        /// 客戶端監聽事件
        /// </summary>
        private void ClientListener()
        {
            Socket sock = this.client;
            Thread thread = this.th_Client;

            while (true)
            {
                try
                {
                    byte[] receiveByte = new byte[1023];

                    int msgLength = sock.Receive(receiveByte);

                    // 將傳來的 byte 陣列讀取成字串
                    string msg = Encoding.Default.GetString(receiveByte, 0, msgLength);

                    // 反序列化字串訊息為 Command 物件
                    TCPCommand cmd = JsonConvert.DeserializeObject<TCPCommand>(msg);

                    switch (cmd.Type)
                    {
                        case TCPCommandType.SignIn:
                            this.SignInCommand(cmd.JsonString, sock);
                            break;
                        case TCPCommandType.SignOut:
                            this.SignOutCommand(cmd.JsonString, sock, thread);
                            break;
                        case TCPCommandType.Chat:
                            this.ChatCommand(cmd);
                            break;
                    }

                }
                catch (Exception ex) { }
            }
        }

        /// <summary>
        /// 玩家登入指令事件
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="sock"></param>
        private void SignInCommand(string jsonStr,Socket sock)
        {
            // 將登入玩家加入 hashTable 並對應 sock
            this.ht.Add(jsonStr, sock);

            // 取得所有玩家名稱字串
            string userStr = this.GetUserStr();

            // 取得序列化後的指令字串
            string cmdStr = this.GetCommandStr(TCPCommandType.SetUserList, string.Empty, userStr, false);

            // 傳送訊息
            this.SendAll(cmdStr);
        }

        /// <summary>
        /// 玩家離開指令
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <param name="sock"></param>
        private void SignOutCommand(string jsonStr, Socket sock,Thread th)
        {
            this.ht.Remove(jsonStr);

            // 取得所有玩家名稱字串
            string userStr = this.GetUserStr();

            // 取得序列化後的指令字串
            string cmdStr = this.GetCommandStr(TCPCommandType.SetUserList, string.Empty,userStr, false);

            // 傳送訊息
            this.SendAll(cmdStr);

            th.Abort();
            sock.Close();
        }

        /// <summary>
        /// 傳送聊天指令
        /// </summary>
        /// <param name="cmd"></param>
        private void ChatCommand(TCPCommand cmd)
        {
            Socket sock = this.GetTargetSocket(cmd);

            if (sock == null) return;

            string cmdStr = this.GetCommandStr(cmd.Type, cmd.SendFrom, cmd.JsonString, false);

            this.SendTo(cmdStr, sock);
        }

        /// <summary>
        /// 取得對象 Socket
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        private Socket GetTargetSocket(TCPCommand cmd)
        {
            string otherUserName = string.Empty;

            foreach (string userName in this.ht.Keys)
            {
                if (userName != cmd.SendFrom)
                {
                    otherUserName = userName;
                }
            }

            if (otherUserName == string.Empty || !this.ht.Contains(otherUserName)) return null;

            return (Socket)this.ht[otherUserName];
        }

        /// <summary>
        /// 發送所有訊息
        /// </summary>
        /// <param name="str">訊息資料</param>
        private void SendAll(string str)
        {
            byte[] sendBytes = Encoding.Default.GetBytes(str);

            foreach(Socket sk in this.ht.Values)
            {
                sk.Send(sendBytes, 0, sendBytes.Length, SocketFlags.None);
            }
        }

        /// <summary>
        /// 發送至對象端
        /// </summary>
        /// <param name="cmdStr"></param>
        /// <param name="sock"></param>
        private void SendTo(string cmdStr,Socket sock)
        {
            byte[] sendBytes = Encoding.Default.GetBytes(cmdStr);

            sock.Send(sendBytes, 0, sendBytes.Length, SocketFlags.None);
        }

        /// <summary>
        /// 取得玩家列表字串
        /// </summary>
        /// <returns></returns>
        private string GetUserStr() 
        {
            string res = string.Empty;

            int count = 0;
            foreach(var keyVal in this.ht.Keys)
            {
                res += keyVal;
                if(count != this.ht.Count - 1)
                {
                    res += ",";
                }
                count += 1;
            }

            return res;
        }

        /// <summary>
        /// 取得序列化的指令物件
        /// </summary>
        /// <param name="type">指令類別</param>
        /// <param name="obj">需要序列化的資料</param>
        /// <param name="needSerial">是否需要序列化</param>
        /// <returns></returns>
        private string GetCommandStr(TCPCommandType type,string sendFrom,object obj,bool needSerial)
        {
            TCPCommand cmd = new TCPCommand();

            cmd.Type = type;
            cmd.SendFrom = sendFrom;
            cmd.JsonString = obj.ToString();
            if (needSerial)
            {
                cmd.JsonString = JsonConvert.SerializeObject(obj);
            }

            return JsonConvert.SerializeObject(cmd);
        }

        /// <summary>
        /// 表單關閉事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormClose(object sender,FormClosingEventArgs e)
        {
            Application.ExitThread();
        }
    }
}
