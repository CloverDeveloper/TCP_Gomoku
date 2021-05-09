using Gomoku.Client.Abstract;
using Gomoku.Client.Interface;
using Gomoku.Common.Enum;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Gomoku.Client.Model
{
    /// <summary>
    /// 遊戲類別
    /// </summary>
    public class Game : IGame
    {
        // 執行緒鎖定用 obj
        private static object obj = new object();

        private static Game instance = null;

        /// <summary>
        /// 棋盤類別
        /// </summary>
        private static Board board;

        private PieceType nextPlayer = PieceType.Black;
        private PieceType currentPlayer = PieceType.Black;

        private Game() { }


        /// <summary>
        /// 取得執行個體
        /// </summary>
        /// <returns></returns>
        public static Game GetInstance()
        {
            if(instance == null) 
            {
                lock (obj)
                {
                    if(instance == null)
                    {
                        instance = new Game();
                        board = new Board();
                    }
                }
            }

            return instance;
        }

        //public PieceType NextPlayer
        //{
        //    get { return this.nextPlayer; }
        //    set { this.nextPlayer = value; }
        //}

        //public PieceType CurrentPlayer
        //{
        //    get { return this.currentPlayer; }
        //    set { this.currentPlayer = value; }
        //}

        /// <summary>
        /// 判斷是否可以放置棋子
        /// </summary>
        /// <param name="x">棋盤 x 座標</param>
        /// <param name="y">棋盤 y 座標</param>
        /// <returns></returns>
        public bool CanBePlace(int x, int y)
        {
            return board.CanBePlace(x,y);
        }

        /// <summary>
        /// 放置棋子
        /// </summary>
        /// <param name="x">棋盤 x 座標</param>
        /// <param name="y">棋盤 y 座標</param>
        /// <param name="type">棋子類別</param>
        /// <returns></returns>
        public PieceBase PlaceAPiece(int x, int y)
        {
            PieceBase piece = board.PlaceAPiece(x, y, this.nextPlayer);
            if(piece != null)
            {
                this.currentPlayer = this.nextPlayer;

                if (this.nextPlayer == PieceType.Black)
                {
                    this.nextPlayer = PieceType.White;
                }
                else
                {
                    this.nextPlayer = PieceType.Black;
                }
            }

            return piece;
        }

        /// <summary>
        /// 判斷是否有人獲勝
        /// </summary>
        /// <returns></returns>
        public string CheckWinner()
        {
            Point lastPoint = board.LastPiecePoint;

            int indexX = lastPoint.X;
            int indexY = lastPoint.Y;

            int count;
            int verticalCount = 1;
            int horizontalCount = 1;
            int upSlashCount = 1;
            int downSlashCount = 1;

            // 尋覽棋盤的 8 個方向
            for(int xDir = -1;xDir <= 1; xDir += 1)
            {
                // 尋找基數，每更換一個方向就重製
                count = 1;
                for (int yDir = -1;yDir <= 1;yDir += 1)
                {
                    if (xDir == 0 && yDir == 0) continue;

                    // 若所有方向皆未到達獲勝數就持續執行
                    while(verticalCount < 5 && horizontalCount < 5 && upSlashCount < 5 && downSlashCount < 5) 
                    {
                        // 設定方向查詢位置
                        int targetX = indexX + count * xDir;
                        int targetY = indexY + count * yDir;

                        // 判斷是否 < 0 或 > 最大放置位置 = 邊界
                        // 判斷該位置棋子種類是否與當前棋子相同
                        if (
                            targetX < 0 || targetX >= Board.MaxCount ||
                            targetY < 0 || targetY >= Board.MaxCount ||
                            board.GetCurrentPieceType(targetX, targetY) != currentPlayer
                          ) break;

                        // 水平線計數
                        if ((xDir == -1 && yDir == 0) || (xDir == 1 && yDir == 0))
                        {
                            horizontalCount += 1;
                        }

                        // 垂直線計數
                        if ((xDir == 0 && yDir == 1) || (xDir == 0 && yDir == -1))
                        {
                            verticalCount += 1;
                        }

                        // 左上到右下線計數
                        if ((xDir == -1 && yDir == -1) || (xDir == 1 && yDir == 1))
                        {
                            upSlashCount += 1;
                        }

                        // 右下到左上線計數
                        if ((xDir == -1 && yDir == 1) || (xDir == 1 && yDir == -1))
                        {
                            downSlashCount += 1;
                        }

                        count += 1;
                    }
                }
            }

            var res = string.Empty;

            // 若有方向到達獲勝數就回傳勝利者
            if (verticalCount == 5 || horizontalCount == 5 || upSlashCount == 5 || downSlashCount == 5)
            {
                res = "白棋獲勝";
                if (currentPlayer == PieceType.Black)
                {
                    res = "黑棋獲勝";
                }
            }

            return res;
        }

        /// <summary>
        /// 重置遊戲
        /// </summary>
        public void ResetGame()
        {
            board.ResetBoard();
        }
    }
}
