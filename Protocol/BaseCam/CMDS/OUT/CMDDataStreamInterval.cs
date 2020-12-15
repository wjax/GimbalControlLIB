using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCamLIB.Protocol.BaseCam.CMDS.OUT
{
    public class CMDDataStreamInterval : CMDBase
    {
        public enum CMD_ID_REQUESTED : byte
        {
            CMD_REALTIME_DATA_3 = 23,
            CMD_REALTIME_DATA_4 = 24,
            CMD_REALTIME_DATA_CUSTOM = 88
            //CMD_AHRS_HELPER,
            //CMD_EVENT
        }

        public CMD_ID_REQUESTED CMD_ID_Req;
        public ushort IntervalMS;
        public readonly byte[] flags = new byte[8];
        public readonly byte[] reserved = new byte[10];

        public CMDDataStreamInterval()
        {
            id = (byte)CMD_ID.CMD_DATA_STREAM_INTERVAL;
            // set flags for DATA CUSTOM
            flags[0] = 0x0F;
            flags[1] = 0x08;
        }

    }
}
