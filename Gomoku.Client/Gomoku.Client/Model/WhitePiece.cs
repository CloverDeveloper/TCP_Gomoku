using Gomoku.Client.Abstract;
using Gomoku.Client.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gomoku.Client.Model
{
    /// <summary>
    /// 白棋類別
    /// </summary>
    public class WhitePiece : PieceBase
    {
        public WhitePiece(int x,int y):base(x,y)
        {
            this.BackgroundImage = Properties.Resources.white;
        }

        /// <summary>
        /// 取得棋子類型
        /// </summary>
        /// <returns></returns>
        public override PieceType GetPieceType()
        {
            return PieceType.White;
        }
    }
}
