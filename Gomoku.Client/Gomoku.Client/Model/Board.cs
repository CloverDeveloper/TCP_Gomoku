using Gomoku.Client.Abstract;
using Gomoku.Client.Enum;
using Gomoku.Client.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Gomoku.Client.Model
{
    /// <summary>
    /// 棋盤類別
    /// </summary>
    public class Board : IBoard
    {
        /// <summary>
        /// 棋盤邊界距離
        /// </summary>
        private static readonly int OFFSET = 75;

        /// <summary>
        /// 棋盤間隔距離
        /// </summary>
        private static readonly int NODE_DISTANCE = 75;

        /// <summary>
        /// 棋子半徑距離
        /// </summary>
        private static readonly int NODE_RADIUS = 10;

        /// <summary>
        /// 未匹配到對應位置
        /// </summary>
        private static readonly Point NODE_NO_MATCH = new Point(-1, -1);

        /// <summary>
        /// 棋盤位置
        /// </summary>
        private PieceBase[,] pieces = new PieceBase[9, 9];

        /// <summary>
        /// 最大座標位置
        /// </summary>
        public static readonly int MaxCount = 9;

        // 最後一顆棋子位置
        private static Point lastPiecePoint = NODE_NO_MATCH;
        public Point LastPiecePoint { get => lastPiecePoint; }

        /// <summary>
        /// 判斷是否可以放置棋子
        /// </summary>
        /// <param name="x">棋盤 x 座標</param>
        /// <param name="y">棋盤 y 座標</param>
        /// <returns></returns>
        public bool CanBePlace(int x, int y)
        {
            // 找出最近節點
            Point closeNode = this.GetCloseNode(x, y);
            if (closeNode == NODE_NO_MATCH) return false;

            // 判斷是否已經下過棋子
            if (pieces[closeNode.X, closeNode.Y] != null) return false;

            return true;
        }

        /// <summary>
        /// 取得最近的節點(二維)
        /// </summary>
        /// <returns></returns>
        private Point GetCloseNode(int x, int y)
        {
            // 找出較近的 x 節點
            int borderX = this.GetCloseNode(x);
            // 若 x 節點為 -1 或是大於最大可放置位置，回傳無匹配
            if(borderX == -1 || borderX >= MaxCount)
            {
                return NODE_NO_MATCH;
            }

            // 找出較近的 y 節點
            int borderY = this.GetCloseNode(y);
            // 若 x 節點為 -1 或是大於最大可放置位置，回傳無匹配
            if (borderY == -1 || borderY >= MaxCount)
            {
                return NODE_NO_MATCH;
            }

            // 回傳計算後的節點位置
            return new Point(borderX, borderY);
        }

        /// <summary>
        /// 取得最近的節點(一維)
        /// </summary>
        /// <param name="val">座標值</param>
        /// <returns></returns>
        private int GetCloseNode(int val) 
        {
            // 座標值 < 邊界值 - 棋子半徑 表示點選位置在邊界上
            if (val < OFFSET - NODE_RADIUS) return -1;

            // 棋盤計算起始位置 = 座標值 - 邊界值 
            val -= OFFSET;

            // 取商數代表點位置
            var quotient = val / NODE_DISTANCE;
            // 取餘數判斷為當前點或下一點位置
            var remainder = val % NODE_DISTANCE;

            // 若計算的餘數小於 棋子半徑，回傳當前位置
            if (remainder < NODE_RADIUS)
                return quotient;

            // 判斷餘數若大於 1 個間距 - 棋子半徑，表示為靠近下一個點位置
            if (remainder >= NODE_DISTANCE - NODE_RADIUS)
                return quotient += 1;

            return -1;
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
            // 找出最近節點
            Point closeNode = this.GetCloseNode(x, y);
            if (closeNode == NODE_NO_MATCH) return null;

            // 判斷是否已經下過棋子
            if (pieces[closeNode.X, closeNode.Y] != null) return null;

            // 取得對應棋盤位置的點
            Point boardNode = this.GetBoardNode(closeNode);

            // 根據型態判斷要擺放的顏色
            if (type == PieceType.Black)
            {
                pieces[closeNode.X, closeNode.Y] = new BlackPiece(boardNode.X, boardNode.Y);
            }

            if(type == PieceType.White)
            {
                pieces[closeNode.X, closeNode.Y] = new WhitePiece(boardNode.X, boardNode.Y);
            }

            // 紀錄最後一顆棋子位置
            lastPiecePoint = closeNode;

            return pieces[closeNode.X, closeNode.Y];
        }

        /// <summary>
        /// 取得對應棋盤位置的點
        /// </summary>
        /// <param name="closeNode"></param>
        /// <returns></returns>
        private Point GetBoardNode(Point closeNode)
        {
            Point res = new Point();

            // 換算棋盤上實際距離 = 距離點位置 * 棋隔間距 + 邊界距離
            res.X = closeNode.X * NODE_DISTANCE + OFFSET;
            res.Y = closeNode.Y * NODE_DISTANCE + OFFSET;

            return res;
        }

        /// <summary>
        /// 取得當前棋子種類
        /// </summary>
        /// <param name="x">棋盤 x 座標</param>
        /// <param name="y">棋盤 y 座標</param>
        public PieceType GetCurrentPieceType(int x, int y)
        {
            if (pieces[x, y] == null) return PieceType.Other;

            return pieces[x, y].GetPieceType();
        }
    }
}
