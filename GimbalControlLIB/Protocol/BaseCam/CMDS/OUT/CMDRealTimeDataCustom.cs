using ObjectPool.Native;

namespace BaseCamLIB.Protocol.BaseCam.CMDS.OUT;

public class CMDRealTimeDataCustom : CMDBase
{
    public static CMDRealTimeDataCustom Get() => NativePool<CMDRealTimeDataCustom>.Get();
        
    public readonly byte[] flags = new byte[4];
    public readonly byte[] reserved = new byte[6];

    public CMDRealTimeDataCustom() : base()
    {
        id = (byte)CMD_ID.CMD_REALTIME_DATA_CUSTOM;

        flags[0] = 0x0F;
        flags[1] = 0x08;
        flags[2] = 0x00;
        flags[3] = 0x00;

        for (int i = 0; i < reserved.Length; i++)
            reserved[i] = 0x00;
    }

    public override BaseCamPacket Pack()
    {
        var packet = BaseCamPacket.Get((byte)CMD_ID.CMD_REALTIME_DATA_CUSTOM);

        for (int i = 0; i < flags.Length; i++)
            packet.writeByte(flags[i]);

        for (int i = 0; i < reserved.Length; i++)
            packet.writeByte(reserved[i]);

        return packet;
    }

    public override void Reset()
    {
        id = (byte) CMD_ID.UNKNOWN;
            
        base.Reset();
    }
}