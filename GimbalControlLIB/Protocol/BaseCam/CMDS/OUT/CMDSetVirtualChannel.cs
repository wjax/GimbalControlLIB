using ObjectPool.Native;

namespace BaseCamLIB.Protocol.BaseCam.CMDS.OUT
{
    public class CMDSetVirtualChannel : CMDBase
    {
        public static CMDSetVirtualChannel Get() => NativePool<CMDSetVirtualChannel>.Get();
        
        private const int CHANNEL_NUMBER = 32;
        public short value;
        public int channel;

        public CMDSetVirtualChannel() : base()
        {
            id = (byte)CMD_ID.CMD_API_VIRT_CH_CONTROL;
        }

        public void SetData(int _channel, short _value)
        {
            channel = _channel;
            value = _value;
        }

        public override BaseCamPacket Pack()
        {
            var packet = BaseCamPacket.Get((byte)CMD_ID.CMD_API_VIRT_CH_CONTROL);

            for (int i = 0; i < 32; i++)
            {
                if (i + 1 == channel)
                    packet.writeWord(value);
                else
                    packet.writeWord(0);
            }
            
            return packet;
        }

        public override void Reset()
        {
            id = (byte) CMD_ID.UNKNOWN;
            
            base.Reset();
        }
    }
}
