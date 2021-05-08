using Gomoku.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku.Common.Model
{
    /// <summary>
    /// 指令類別
    /// </summary>
    public class TCPCommand
    {
        /// <summary>
        /// 指令類別
        /// </summary>
        public TCPCommandType Type { get; set; }

        /// <summary>
        /// 發送玩家
        /// </summary>
        public string SendFrom { get; set; }

        /// <summary>
        /// 序列化的訊息
        /// </summary>
        public string JsonString { get; set; }
    }
}
