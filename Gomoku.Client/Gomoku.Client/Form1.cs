using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gomoku.Client.Abstract;
using Gomoku.Client.Extension;
using Gomoku.Client.Model;

namespace Gomoku.Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.pb_Board.MouseDown += BoardMouseDown;
        }

        /// <summary>
        /// 滑鼠點選事件
        /// </summary>
        private void BoardMouseDown(object sender, MouseEventArgs e)
        {
            var piece = new BlackPiece(e.X, e.Y);
            this.Controls.Add(piece);
            this.Controls.SetChildIndex(piece, 1);
        }
    }
}
