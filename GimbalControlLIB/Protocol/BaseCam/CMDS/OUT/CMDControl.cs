using System;
using ObjectPool.Native;

namespace BaseCamLIB.Protocol.BaseCam.CMDS.OUT;

public class CMDControl : CMDBase
{
    public static CMDControl Get() => NativePool<CMDControl>.Get();
        
    public enum MODE : byte
    {
        NO_CONTROL = 0,
        SPEED = 1,
        ANGLE = 2,
        SPEED_ANGLE = 3,
        RC = 4,
        RC_HIGHRES = 6,
        ANGLE_REL_FRAME = 5
    }

    public MODE[] Modes = new MODE[3];
    public float[] Speeds = new float[3];
    public float[] Angles = new float[3];

    public CMDControl() : base()
    {
        id = (byte)CMD_ID.CMD_CONTROL;
    }

    public override BaseCamPacket Pack()
    {
        var packet = BaseCamPacket.Get((byte) CMD_ID.CMD_CONTROL);
        for (int i = 0; i < 3; i++)
        {
            byte b = (byte)Modes[i];
            // Precion mode
            if (Modes[i] == MODE.SPEED && Math.Abs(Speeds[i]) < CMDBase.degSegCtrl)
            {
                b = (byte)(b | 0b1000_0000);
            }
            packet.writeByte(b);
        }
        for (int i = 0; i < 3; i++)
        {
            packet.writeWord(CMDBase.SpeedCtrl2Units(Speeds[i]));
            packet.writeWord(CMDBase.Angle2Units(Angles[i]));
        }
        return packet;
    }

    public override void Reset()
    {
        id = (byte) CMD_ID.UNKNOWN;
            
        base.Reset();
    }
}