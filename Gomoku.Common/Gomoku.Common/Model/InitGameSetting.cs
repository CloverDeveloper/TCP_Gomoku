using Gomoku.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku.Common.Model
{
    /// <summary>
    /// 初始化遊戲資訊
    /// </summary>
    public class InitGameSetting
    {
        /// <summary>
        /// 棋子類型
        /// </summary>
        public PieceType PieceType { get; set; }

        /// <summary>
        /// 是否可以放置棋子
        /// </summary>
        public bool CanPlaceAPiece { get; set; }
    }
}
