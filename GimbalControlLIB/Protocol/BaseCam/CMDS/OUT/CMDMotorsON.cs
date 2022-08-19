using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCamLIB.Protocol.BaseCam.CMDS.OUT
{
    public class CMDMotorsON : CMDBase
    {
        public CMDMotorsON()
        {
            id = (byte)CMD_ID.CMD_MOTORS_ON;
        }

    }
}
