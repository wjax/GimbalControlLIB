using System;
using System.Collections.Generic;
using System.Text;
using ObjectPool.Native;

namespace BaseCamLIB.Protocol.BaseCam.CMDS.OUT
{
    public class CMDMotorsOFF : CMDBase
    {
        public static CMDMotorsOFF Get() => NativePool<CMDMotorsOFF>.Get();
        
        public CMDMotorsOFF() : base()
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

        public override BaseCamPacket Pack()
        {
            var packet = BaseCamPacket.Get((byte)CMD_ID.CMD_MOTORS_OFF);
            packet.writeByte((byte)mode);

            return packet;
        }

        public override void Reset()
        {
            id = (byte) CMD_ID.UNKNOWN;
            
            base.Reset();
        }
    }
}
