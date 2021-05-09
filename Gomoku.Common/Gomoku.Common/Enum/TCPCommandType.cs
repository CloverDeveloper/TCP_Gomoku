using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku.Common.Enum
{
    /// <summary>
    /// 指令類別
    /// </summary>
    public enum TCPCommandType
    {
        PlaceAPiece,
        Chat,
        GiveUp,
        SignIn,
        SignOut,
        SetUserList,
        GameStart
    }
}
