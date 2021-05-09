using System;
using System.Collections.Generic;
using System.Text;
using Gomoku.Client.Abstract;
using Gomoku.Common.Enum;

namespace Gomoku.Client.Model
{
    /// <summary>
    /// 黑棋類別
    /// </summary>
    public class BlackPiece : PieceBase
    {
        public BlackPiece(int x,int y):base(x,y) 
        {
            this.BackgroundImage = Properties.Resources.black;
        }

        /// <summary>
        /// 取得棋子類型
        /// </summary>
        /// <returns></returns>
        public override PieceType GetPieceType()
        {
            return PieceType.Black;
        }
    }
}
