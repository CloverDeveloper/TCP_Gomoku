using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gomoku.Client.Abstract;
using Gomoku.Client.Extension;
using Gomoku.Client.Interface;
using Gomoku.Client.Model;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Gomoku.Common.Model;
using Gomoku.Common.Enum;
using Newtonsoft.Json;

namespace Gomoku.Client
{
    public partial class Form1 : Form
    {
        private Game game;
        private List<PieceBase> tempPieces = new List<PieceBase>();
        private Socket sock;
        private Thread th;

        public Form1()
        {
            InitializeComponent();
            this.btn_SignIn.Enabled = true;
            this.tb_UserName.Enabled = true;
            this.btn_Giveup.Enabled = false;
            this.btn_SendMsg.Enabled = false;

            game = Game.GetInstance();
            this.pb_Board.MouseDown += Board_MouseDown;
            this.pb_Board.MouseMove += Board_MouseMove;
            this.btn_Giveup.Click += Btn_GiveUp;
            this.btn_SignIn.Click += Btn_SignIn;
            this.btn_SendMsg.Click += Btn_SendMsg;
            this.FormClosing += FormClose;
        }

        /// <summary>
        /// 棋盤滑鼠點選事件
        /// </summary>
        private void Board_MouseDown(object sender, MouseEventArgs e)
        {
            if (!this.pb_Board.Enabled) return;

            this.PlaceAPiece(e.X, e.Y);

            string cmdStr = 
                this.GetCommandStr(TCPCommandType.PlaceAPiece, this.tb_UserName.Text, new Point(e.X, e.Y),true);

            this.SendTo(cmdStr);

            this.pb_Board.Enabled = false;
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// 放置棋子方法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void PlaceAPiece(int x,int y)
        {
            PieceBase piece = game.PlaceAPiece(x, y);
            if (piece == null) return;

            tempPieces.Add(piece);

            MethodInvoker callMethod = new MethodInvoker(() => 
            {
                this.Controls.Add(piece);
                this.Controls.SetChildIndex(piece, 1);
            });

            this.BeginInvoke(callMethod);

            string msg = game.CheckWinner();
            if (msg != string.Empty)
            {
                MessageBox.Show(msg);
                this.ResetGame();
                return;
            }
        }

        /// <summary>
        /// 棋盤滑鼠滑動事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Board_MouseMove(object sender, MouseEventArgs e)
        {
            if (!this.pb_Board.Enabled)
            {
                this.Cursor = Cursors.Default;
                return;
            }

            if (game.CanBePlace(e.X, e.Y)) 
            {
                this.Cursor = Cursors.Hand;
            } 
            else 
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 投降
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_GiveUp(object sender,EventArgs e)
        {
            MessageBox.Show($"玩家:{this.tb_UserName.Text}，認輸了!!，遊戲重新開始。");

            this.ResetGame();

            string cmdStr = 
                this.GetCommandStr(TCPCommandType.GiveUp, this.tb_UserName.Text, this.tb_UserName.Text, false);

            this.SendTo(cmdStr);
        }

        /// <summary>
        /// 投降認輸指令
        /// </summary>
        /// <param name="cmdStr"></param>
        private void GiveUpCommand(string cmdStr)
        {
            MessageBox.Show($"玩家:{cmdStr}，認輸了!!，遊戲重新開始。");
            this.ResetGame();
        }

        /// <summary>
        /// 重置遊戲
        /// </summary>
        private void ResetGame()
        {
            MethodInvoker callMethod = new MethodInvoker(() =>
            {
                foreach (PieceBase piece in this.tempPieces)
                {
                    this.Controls.Remove(piece);
                }
            });

            this.BeginInvoke(callMethod);

            game.ResetGame();
        }

        /// <summary>
        /// 玩家登入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_SignIn(object sender,EventArgs e)
        {
            this.btn_SignIn.Enabled = false;

            string errMsg = this.CheckSignInInfo();
            if(errMsg != string.Empty)
            {
                MessageBox.Show(errMsg);
                return;
            }

            // 忽略跨執行緒錯誤
            CheckForIllegalCrossThreadCalls = false; 

            // 使用指定的通訊協定家族 (Family)、通訊端類型和通訊協定，初始化 Socket 類別的新執行個體。
            this.sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                // 建立端點物件
                IPEndPoint ipEP = new IPEndPoint(IPAddress.Parse(this.tb_IP.Text), int.Parse(this.tb_Port.Text));
                this.sock.Connect(ipEP);

                this.th = new Thread(new ThreadStart(Listener));
                this.th.IsBackground = true;
                this.th.Start();
                this.tb_MsgBox.Text = "已連線伺服器" + Environment.NewLine;
                this.SignInCommand();
            }
            catch(Exception ex)
            {
                this.tb_MsgBox.Text = "無法連上伺服器" + Environment.NewLine;
                this.btn_SignIn.Enabled = true;
                return;
            }

            this.btn_Giveup.Enabled = true;
            this.btn_SendMsg.Enabled = true;
            this.pb_Board.Enabled = true;
            this.tb_UserName.Enabled = false;
        }

        /// <summary>
        /// 發送訊息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_SendMsg(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tb_ChatBox.Text)) return;

            string cmdStr = 
                this.GetCommandStr(TCPCommandType.Chat, this.tb_UserName.Text, this.tb_ChatBox.Text, false);

            this.SendTo(cmdStr);

            this.tb_MsgBox.Text += $"自己 : {this.tb_ChatBox.Text}" + Environment.NewLine;

            this.tb_ChatBox.Text = string.Empty;
        }

        /// <summary>
        /// 監聽伺服器狀態
        /// </summary>
        private void Listener()
        {
            // 取得端點
            EndPoint remoteEp = this.sock.RemoteEndPoint;
            byte[] receiveBytes = new byte[1023]; // 接受用 byte 陣列
            int msgLength = 0; // 接收的位元組數目

            while (true)
            {
                try
                {
                    msgLength =
                        this.sock.ReceiveFrom(receiveBytes, 0, receiveBytes.Length, SocketFlags.None, ref remoteEp);
                }
                catch (Exception ex)
                {
                    this.sock.Close();
                    this.lb_Users.Items.Clear();
                    this.btn_SignIn.Enabled = true;
                    this.btn_Giveup.Enabled = false;
                    this.btn_SendMsg.Enabled = false;
                    this.pb_Board.Enabled = false;
                    this.tb_UserName.Enabled = true;
                    this.th.Abort();

                    MessageBox.Show("與伺服器斷線");
                    return;
                }

                // 將傳來的 byte 陣列讀取成字串
                string msg = Encoding.Default.GetString(receiveBytes, 0, msgLength);

                // 反序列化字串訊息為 Command 物件
                TCPCommand cmd = JsonConvert.DeserializeObject<TCPCommand>(msg);

                switch (cmd.Type)
                {
                    case TCPCommandType.SetUserList:
                        this.SetUserListCommand(cmd.JsonString);
                        break;
                    case TCPCommandType.Chat:
                        this.tb_MsgBox.Text += $"{cmd.SendFrom} : {cmd.JsonString}"+Environment.NewLine;
                        break;
                    case TCPCommandType.GameStart:
                        this.GameStartCommand(cmd.JsonString);
                        break;
                    case TCPCommandType.PlaceAPiece:
                        this.PlaceAPieceCommand(cmd.JsonString);
                        break;
                    case TCPCommandType.GiveUp:
                        this.GiveUpCommand(cmd.JsonString);
                        break;
                }
            }
        }

        /// <summary>
        /// 登入指令
        /// </summary>
        private void SignInCommand()
        {
            // 取得登入指令
            string cmdStr = 
                this.GetCommandStr(TCPCommandType.SignIn, this.tb_UserName.Text, this.tb_UserName.Text,false);

            // 發送訊息
            this.SendTo(cmdStr);
        }

        /// <summary>
        /// 接受對方放置棋子方法
        /// </summary>
        private void PlaceAPieceCommand(string cmdStr)
        {
            Point point = JsonConvert.DeserializeObject<Point>(cmdStr);
            if (point == null) return;

            this.PlaceAPiece(point.X, point.Y);

            this.pb_Board.Enabled = true;
        }

        /// <summary>
        /// 發送訊息
        /// </summary>
        /// <param name="cmdStr"></param>
        private void SendTo(string cmdStr)
        {
            // 將登入指令轉為 byte 陣列
            byte[] sendBytes = Encoding.Default.GetBytes(cmdStr);

            this.sock.Send(sendBytes, 0, sendBytes.Length, SocketFlags.None);
        }

        /// <summary>
        /// 設定玩家列表指令
        /// </summary>
        private void SetUserListCommand(string users)
        {
            this.lb_Users.Items.Clear();
            string[] userArray = users.Split(",");
            foreach (string user in userArray)
            {
                this.lb_Users.Items.Add(user);
            }
        }

        /// <summary>
        /// 取得序列化後的指令字串
        /// </summary>
        /// <returns></returns>
        private string GetCommandStr(TCPCommandType type,string sendFrom,object obj,bool needSerial)
        {
            TCPCommand cmd = new TCPCommand();

            cmd.Type = type;
            cmd.SendFrom = sendFrom;
            cmd.JsonString = obj.ToString();
            if(needSerial)
            {
                cmd.JsonString = JsonConvert.SerializeObject(obj);
            }

            return JsonConvert.SerializeObject(cmd);
        }

        /// <summary>
        /// 取得初始化遊戲資訊
        /// </summary>
        private void GameStartCommand(string cmdStr)
        {
            InitGameSetting init = JsonConvert.DeserializeObject<InitGameSetting>(cmdStr);

            if (init == null) return;

            this.pb_Board.Enabled = init.CanPlaceAPiece;
            //this.game.NextPlayer = init.PieceType;
            //this.game.CurrentPlayer = init.PieceType;
        }

        /// <summary>
        /// 檢核連線資訊
        /// </summary>
        /// <returns></returns>
        private string CheckSignInInfo()
        {
            if (string.IsNullOrEmpty(this.tb_UserName.Text))
            {
                return "請輸入使用者名稱";
            }

            if (string.IsNullOrEmpty(this.tb_IP.Text))
            {
                return "請輸入連線 IP";
            }

            if (!IPAddress.TryParse(this.tb_IP.Text,out IPAddress ipRes))
            {
                return "連線 IP 輸入錯誤";
            }

            if (string.IsNullOrEmpty(this.tb_Port.Text))
            {
                return "請輸入連線 Port";
            }

            if (!int.TryParse(this.tb_Port.Text, out int portRes))
            {
                return "連線 Port 輸入錯誤";
            }

            return string.Empty;
        }

        /// <summary>
        /// 表單結束事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormClose(object sender, FormClosingEventArgs e)
        {
            if (this.btn_SignIn.Enabled) return;

            string cmdStr = 
                this.GetCommandStr(TCPCommandType.SignOut, this.tb_UserName.Text, this.tb_UserName.Text, false);

            this.SendTo(cmdStr);

            this.sock.Close();
            Application.ExitThread();
        }
    }
}
