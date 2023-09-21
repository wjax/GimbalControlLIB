using ObjectPool.Native;

namespace BaseCamLIB.Protocol.BaseCam.CMDS.OUT;

public class CMDRealTimeData4 : CMDBase
{
    public static CMDRealTimeData4 Get() => NativePool<CMDRealTimeData4>.Get();
        
    public CMDRealTimeData4() : base()
    {
        id = (byte)CMD_ID.CMD_REALTIME_DATA_4;
    }

    public override BaseCamPacket Pack()
    {
        var packet = BaseCamPacket.Get((byte)CMD_ID.CMD_REALTIME_DATA_4);

        return packet;
    }

    public override void Reset()
    {
        id = (byte) CMD_ID.UNKNOWN;
            
        base.Reset();
    }
}