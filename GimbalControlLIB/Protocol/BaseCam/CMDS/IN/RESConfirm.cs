using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCamLIB.Protocol.BaseCam.CMDS.IN
{
    public class RESConfirm : CMDBase
    {
        public CMD_ID ConfirmedID;

        public RESConfirm()
        {
            id = (byte)CMD_ID.CMD_CONFIRM;
        }
    }
}
