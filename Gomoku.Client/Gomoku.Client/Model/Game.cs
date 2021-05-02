using Gomoku.Client.Abstract;
using Gomoku.Client.Enum;
using Gomoku.Client.Interface;
using System;
using System.Collections.Generic;
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

        private Game() { }

        /// <summary>
        /// 棋盤類別
        /// </summary>
        private static IBoard board;

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
        public PieceBase PlaceAPiece(int x, int y, PieceType type)
        {
            return board.PlaceAPiece(x, y, type);
        }
    }
}
