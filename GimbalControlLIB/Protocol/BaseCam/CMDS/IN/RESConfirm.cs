using System;
using ObjectPool.Native;

namespace BaseCamLIB.Protocol.BaseCam.CMDS.IN;

public class RESConfirm : CMDBase, ICommandDePacker<RESConfirm>
{
    public static RESConfirm Get() => NativePool<RESConfirm>.Get();
        
    public CMD_ID ConfirmedID;

    public RESConfirm() : base()
    {
        id = (byte)CMD_ID.CMD_CONFIRM;
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

    public static RESConfirm Depack(BaseCamPacket packet) => Get();
}