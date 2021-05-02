using Gomoku.Client.Abstract;
using Gomoku.Client.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gomoku.Client.Interface
{
    /// <summary>
    /// 遊戲介面
    /// </summary>
    public interface IGame
    {
        /// <summary>
        /// 放置棋子
        /// </summary>
        /// <param name="x">棋盤 x 座標</param>
        /// <param name="y">棋盤 y 座標</param>
        /// <param name="type">棋子類別</param>
        /// <returns></returns>
        public PieceBase PlaceAPiece(int x, int y);

        /// <summary>
        /// 判斷是否可以放置棋子
        /// </summary>
        /// <param name="x">棋盤 x 座標</param>
        /// <param name="y">棋盤 y 座標</param>
        /// <returns></returns>
        public bool CanBePlace(int x, int y);

        /// <summary>
        /// 檢查是否有贏家
        /// </summary>
        /// <returns></returns>
        public string CheckWinner();

        /// <summary>
        /// 重置遊戲
        /// </summary>
        public void ResetGame();
    }
}
