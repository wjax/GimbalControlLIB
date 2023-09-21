using ObjectPool.Native;

namespace BaseCamLIB.Protocol.BaseCam.CMDS.OUT;

public class CMDDataStreamInterval : CMDBase
{
    public static CMDDataStreamInterval Get() => NativePool<CMDDataStreamInterval>.Get();
        
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

    public CMDDataStreamInterval() : base()
    {
        id = (byte)CMD_ID.CMD_DATA_STREAM_INTERVAL;
        // set flags for DATA CUSTOM
        flags[0] = 0x0F;
        flags[1] = 0x08;
    }

    public override BaseCamPacket Pack()
    {
        var packet = BaseCamPacket.Get((byte)CMD_ID.CMD_DATA_STREAM_INTERVAL);
        packet.writeByte((byte)CMD_ID_Req);
        packet.writeWord(IntervalMS);
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