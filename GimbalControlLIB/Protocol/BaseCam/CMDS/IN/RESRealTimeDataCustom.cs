using System;
using ObjectPool.Native;

namespace BaseCamLIB.Protocol.BaseCam.CMDS.IN;

public class RESRealTimeDataCustom : CMDBase, ICommandDePacker<RESRealTimeDataCustom>
{
    public static RESRealTimeDataCustom Get() => NativePool<RESRealTimeDataCustom>.Get();
        
    public ushort TimeMS;
    public float[] IMUAngles = new float[3];
    public float[] TargetAngles = new float[3];
    public float[] TargetSpeeds = new float[3];
    public float[] StatorAngles = new float[3];

    public float[] EncoderRaw = new float[3];

    public RESRealTimeDataCustom() : base()
    {
        id = (byte)CMD_ID.CMD_REALTIME_DATA_CUSTOM;
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

    public static RESRealTimeDataCustom Depack(BaseCamPacket packet)
    {
        var cmd = Get();

        cmd.TimeMS = packet.readUShort();
        for (int i = 0; i < 3; i++)
            cmd.IMUAngles[i] = CMDBase.Units2Angle(packet.readByte(), packet.readByte());
        for (int i = 0; i < 3; i++)
            cmd.TargetAngles[i] = CMDBase.Units2Angle(packet.readByte(), packet.readByte());
        for (int i = 0; i < 3; i++)
            cmd.TargetSpeeds[i] = CMDBase.Units2Speed(packet.readByte(), packet.readByte());
        for (int i = 0; i < 3; i++)
            cmd.StatorAngles[i] = CMDBase.Units2Angle(packet.readByte(), packet.readByte());

        for (int i = 0; i < 3; i++)
        {
            cmd.EncoderRaw[i] = CMDBase.Units2AngleENC(packet.readByte(), packet.readByte(), packet.readByte());
        }

        return cmd;
    }
}