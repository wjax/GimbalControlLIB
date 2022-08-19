using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCamLIB.Protocol.BaseCam.CMDS.OUT
{
    public class CMDRealTimeDataCustom : CMDBase
    {
        public readonly byte[] flags = new byte[4];
        public readonly byte[] reserved = new byte[6];

        public CMDRealTimeDataCustom()
        {
            id = (byte)CMD_ID.CMD_REALTIME_DATA_CUSTOM;

            flags[0] = 0x0F;
            flags[1] = 0x08;
            flags[2] = 0x00;
            flags[3] = 0x00;

            for (int i = 0; i < reserved.Length; i++)
                reserved[i] = 0x00;
        }
    }
}
