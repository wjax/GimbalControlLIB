using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCamLIB.Protocol.BaseCam.CMDS.IN
{
    public class RESRealTimeData4 : CMDBase
    {
        public float[] IMUAngles = new float[3];
        public float[] IMUAnglesFrame = new float[3];
        public float[] TargetAngles = new float[3];
        public float[] StatorAngles = new float[3];

        public RESRealTimeData4()
        {
            id = (byte)CMD_ID.CMD_REALTIME_DATA_4;

        }
    }
}
