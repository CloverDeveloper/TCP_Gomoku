using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Gomoku.Client.Abstract;

namespace Gomoku.Client.Extension
{
    /// <summary>
    /// 棋子類別擴充方法
    /// </summary>
    public static class PieceExtension
    {
        public static PictureBox Clone(this PieceBase piece) 
        {
            var res = new PictureBox();

            return res;
        }
    }
}
