using System;
using System.Collections.Generic;
using System.Text;
using ObjectPool.Native;

namespace BaseCamLIB.Protocol.BaseCam.CMDS.IN
{
    public class RESRealTimeData4 : CMDBase, ICommandDePacker<RESRealTimeData4>
    {
        public static RESRealTimeData4 Get() => NativePool<RESRealTimeData4>.Get();
        
        public float[] IMUAngles = new float[3];
        public float[] IMUAnglesFrame = new float[3];
        public float[] TargetAngles = new float[3];
        public float[] StatorAngles = new float[3];

        public RESRealTimeData4() : base()
        {
            id = (byte)CMD_ID.CMD_REALTIME_DATA_4;
        }

        public override BaseCamPacket Pack()
        {
            throw new NotImplementedException();
        }

        public override void Reset()
        {
            id = (byte) CMD_ID.UNKNOWN;
            
            base.Reset();
        }

        public static RESRealTimeData4 Depack(BaseCamPacket packet)
        {
            var cmd = Get();

            packet.skipBytes(32);

            for (int i = 0; i < 3; i++)
                cmd.IMUAngles[i] = CMDBase.Units2Angle(packet.readByte(), packet.readByte());
            for (int i = 0; i < 3; i++)
                cmd.IMUAnglesFrame[i] = CMDBase.Units2Angle(packet.readByte(), packet.readByte());
            for (int i = 0; i < 3; i++)
                cmd.TargetAngles[i] = CMDBase.Units2Angle(packet.readByte(), packet.readByte());

            packet.skipBytes(13);

            for (int i = 0; i < 3; i++)
                cmd.StatorAngles[i] = CMDBase.Units2Angle(packet.readByte(), packet.readByte());

            return cmd;
        }
    }
}
