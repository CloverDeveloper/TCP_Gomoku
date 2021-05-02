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
using Gomoku.Client.Enum;
using Gomoku.Client.Extension;
using Gomoku.Client.Interface;
using Gomoku.Client.Model;

namespace Gomoku.Client
{
    public partial class Form1 : Form
    {
        private IGame game;
        private List<PieceBase> tempPieces = new List<PieceBase>();

        public Form1()
        {
            InitializeComponent();
            game = Game.GetInstance();
            this.pb_Board.MouseDown += Board_MouseDown;
            this.pb_Board.MouseMove += Board_MouseMove;
            this.btn_giveup.Click += Btn_GiveUp;
        }

        /// <summary>
        /// 棋盤滑鼠點選事件
        /// </summary>
        private void Board_MouseDown(object sender, MouseEventArgs e)
        {
            PieceBase piece = game.PlaceAPiece(e.X, e.Y);
            if (piece == null) return;

            tempPieces.Add(piece);

            this.Controls.Add(piece);
            this.Controls.SetChildIndex(piece, 1);

            string msg = game.CheckWinner();
            if(msg != string.Empty)
            {
                MessageBox.Show(msg);
                this.ResetGame();
            }
        }

        /// <summary>
        /// 棋盤滑鼠滑動事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Board_MouseMove(object sender, MouseEventArgs e)
        {
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
            this.ResetGame();
        }

        /// <summary>
        /// 重置遊戲
        /// </summary>
        private void ResetGame()
        {
            foreach (PieceBase piece in this.tempPieces)
            {
                this.Controls.Remove(piece);
            }

            game.ResetGame();
        }
    }
}
