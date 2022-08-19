namespace BaseCamLIB.Protocol.BaseCam.CMDS.OUT
{
    public class CMDSetVirtualChannel : CMDBase
    {
        private const int CHANNEL_NUMBER = 32;
        public short value;
        public int channel;

        public CMDSetVirtualChannel()
        {
            id = (byte)CMD_ID.CMD_API_VIRT_CH_CONTROL;
        }

        public void SetData(int _channel, short _value)
        {
            channel = _channel;
            value = _value;
        }

    }
}
