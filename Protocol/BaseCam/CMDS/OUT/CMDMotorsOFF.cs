using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCamLIB.Protocol.BaseCam.CMDS.OUT
{
    public class CMDMotorsOFF : CMDBase
    {
        public CMDMotorsOFF()
        {
            id = (byte)CMD_ID.CMD_MOTORS_OFF;
        }

        public enum MODE : byte
        {
            NORMAL = 0,
            BREAK = 1,
            SAFE_STOP = 2
        }

        public MODE mode;
    }
}
