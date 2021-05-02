using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Gomoku.Client.Enum;

namespace Gomoku.Client.Abstract
{
    /// <summary>
    /// 棋子抽象類別
    /// </summary>
    public abstract class PieceBase : PictureBox
    {
        /// <summary>
        /// 基本棋子大小
        /// </summary>
        private const int baseSize = 50;

        /// <summary>
        /// 建構子
        /// </summary>
        public PieceBase(int x,int y)
        {
            this.BackColor = Color.FromArgb(235, 201, 129);
            this.Size = new Size(baseSize, baseSize);
            this.Location = new Point(x - baseSize / 2, y - baseSize / 2);
        }

        /// <summary>
        /// 取得棋子類型
        /// </summary>
        public abstract PieceType GetPieceType();
    }
}
