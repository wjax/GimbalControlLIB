using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCamLIB.Protocol.BaseCam.CMDS.OUT
{
    public class CMDRealTimeData4 : CMDBase
    {
        public CMDRealTimeData4()
        {
            id = (byte)CMD_ID.CMD_REALTIME_DATA_4;

        }
    }
}
