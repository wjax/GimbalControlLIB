using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCamLIB.Protocol.BaseCam.CMDS.IN
{
    public class RESRealTimeDataCustom : CMDBase
    {
        public ushort TimeMS;
        public float[] IMUAngles = new float[3];
        public float[] TargetAngles = new float[3];
        public float[] TargetSpeeds = new float[3];
        public float[] StatorAngles = new float[3];

        public float[] EncoderRaw = new float[3];

        public RESRealTimeDataCustom()
        {
            id = (byte)CMD_ID.CMD_REALTIME_DATA_CUSTOM;
        }
    }
}
