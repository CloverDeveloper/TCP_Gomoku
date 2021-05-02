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
        /// 判斷是否可以放置棋子
        /// </summary>
        /// <param name="x">棋盤 x 座標</param>
        /// <param name="y">棋盤 y 座標</param>
        /// <returns></returns>
        public bool CanBePlace(int x, int y);
    }
}
