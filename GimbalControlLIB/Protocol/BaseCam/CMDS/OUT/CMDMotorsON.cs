using ObjectPool.Native;

namespace BaseCamLIB.Protocol.BaseCam.CMDS.OUT;

public class CMDMotorsON : CMDBase
{
    public static CMDMotorsON Get() => NativePool<CMDMotorsON>.Get();
        
    public CMDMotorsON() : base()
    {
        id = (byte)CMD_ID.CMD_MOTORS_ON;
    }

    public override BaseCamPacket Pack()
    {
        var packet = BaseCamPacket.Get((byte)CMD_ID.CMD_MOTORS_ON);

        return packet;
    }

    public override void Reset()
    {
        id = (byte) CMD_ID.UNKNOWN;
            
        base.Reset();
    }
}