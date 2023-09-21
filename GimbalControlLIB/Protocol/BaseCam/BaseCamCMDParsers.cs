using BaseCamLIB.Protocol.BaseCam.CMDS;
using BaseCamLIB.Protocol.BaseCam.CMDS.IN;

namespace BaseCamLIB.Protocol.BaseCam;

public static class BaseCamCMDParsers
{
    private const int SIZE_CMD_MOTORSOFF = BaseCamPacket.SBGC_CMD_NON_PAYLOAD_BYTES + 1;
    private const int SIZE_CMD_MOTORSON = BaseCamPacket.SBGC_CMD_NON_PAYLOAD_BYTES;
    private const int SIZE_CMD_EXECUTE_MENU = BaseCamPacket.SBGC_CMD_NON_PAYLOAD_BYTES + 1;
    private const int SIZE_CMD_DATA_STREAM_INTERVAL = BaseCamPacket.SBGC_CMD_NON_PAYLOAD_BYTES + 1 + 2 + 8 + 10;
    private const int SIZE_CMD_REAL_TIME_DATA_CUSTOM = BaseCamPacket.SBGC_CMD_NON_PAYLOAD_BYTES + 4 + 6;
    private const int SIZE_CMD_REAL_TIME_DATA_4 = BaseCamPacket.SBGC_CMD_NON_PAYLOAD_BYTES;
    private const int SIZE_CMD_CONTROL = BaseCamPacket.SBGC_CMD_NON_PAYLOAD_BYTES + 3 + 4*3;
    private const int SIZE_CMD_SETVIRTUALCHANNEL = BaseCamPacket.SBGC_CMD_NON_PAYLOAD_BYTES + 2 * 32;

    //private static readonly Dictionary<byte, CMDBase> Cmds2Reuse = new Dictionary<byte, CMDBase>();


    // public static BaseCamPacket PackCMD(CMDBase cmd)
    // {
    //     switch (cmd.id)
    //     {
    //         case (byte)CMD_ID.CMD_MOTORS_OFF:
    //             return PackCMDMotorsOFF(cmd as CMDMotorsOFF);
    //         case (byte)CMD_ID.CMD_MOTORS_ON:
    //             return PackCMDMotorsON(cmd as CMDMotorsON);
    //         case (byte)CMD_ID.CMD_EXECUTE_MENU:
    //             return PackCMDExecuteMenuAction(cmd as CMDExecuteMenu);
    //         case (byte)CMD_ID.CMD_DATA_STREAM_INTERVAL:
    //             return PackCMDDataStreamInterval(cmd as CMDDataStreamInterval);
    //         case (byte)CMD_ID.CMD_REALTIME_DATA_CUSTOM:
    //             return PackCMDRealTimeDataCustom(cmd as CMDRealTimeDataCustom);
    //         case (byte)CMD_ID.CMD_REALTIME_DATA_4:
    //             return PackCMDRealTimeData4(cmd as CMDRealTimeData4);
    //         case (byte)CMD_ID.CMD_CONTROL:
    //             return PackCMDControl(cmd as CMDControl);
    //         case (byte)CMD_ID.CMD_API_VIRT_CH_CONTROL:
    //             return PackCMDSetVirtualChannel(cmd as CMDSetVirtualChannel);
    //         default:
    //             return null;
    //     }
    // }

    public static CMDBase UnpackCMD(BaseCamPacket packet)
    {
        switch (packet.ID)
        {
            case (byte)CMD_ID.CMD_CONFIRM:
                return RESConfirm.Depack(packet);
            case (byte)CMD_ID.CMD_REALTIME_DATA_CUSTOM:
                return RESRealTimeDataCustom.Depack(packet);
            case (byte)CMD_ID.CMD_REALTIME_DATA_4:
                return RESRealTimeData4.Depack(packet);
            default:
                return null;
        }
    }


    // private static CMDBase GetCMDFromPool(byte ID)
    // {
    //     if (Cmds2Reuse.ContainsKey(ID))
    //         return Cmds2Reuse[ID];
    //     else
    //     {
    //         CMDBase b;
    //         switch (ID)
    //         {
    //             case (byte)CMD_ID.CMD_CONFIRM:
    //                 b = new RESConfirm();
    //                 break;
    //             case (byte)CMD_ID.CMD_REALTIME_DATA_CUSTOM:
    //                 b = new RESRealTimeDataCustom();
    //                 break;
    //             case (byte)CMD_ID.CMD_REALTIME_DATA_4:
    //                 b = new RESRealTimeData4();
    //                 break;
    //             default:
    //                 throw new ArgumentException("Missing CMDBase");
    //         }
    //         Cmds2Reuse.Add(ID, b);
    //         return b;
    //     }
    // }

    // private static CMDBase UnpackRESConfirm (BaseCamPacket packet)
    // {
    //     CMDBase cmd = GetCMDFromPool(packet.ID);
    //
    //     return cmd;
    // }

    // private static CMDBase UnpackRESRealTimeDataCustom(BaseCamPacket packet)
    // {
    //     RESRealTimeDataCustom cmd = (RESRealTimeDataCustom)GetCMDFromPool(packet.ID);
    //
    //     cmd.TimeMS = packet.readUShort();
    //     for (int i = 0; i < 3; i++)
    //         cmd.IMUAngles[i] = CMDBase.Units2Angle(packet.readByte(), packet.readByte());
    //     for (int i = 0; i < 3; i++)
    //         cmd.TargetAngles[i] = CMDBase.Units2Angle(packet.readByte(), packet.readByte());
    //     for (int i = 0; i < 3; i++)
    //         cmd.TargetSpeeds[i] = CMDBase.Units2Speed(packet.readByte(), packet.readByte());
    //     for (int i = 0; i < 3; i++)
    //         cmd.StatorAngles[i] = CMDBase.Units2Angle(packet.readByte(), packet.readByte());
    //
    //     for (int i = 0; i < 3; i++)
    //     {
    //         cmd.EncoderRaw[i] = CMDBase.Units2AngleENC(packet.readByte(), packet.readByte(), packet.readByte());
    //         //uint raw = 0;
    //         //byte low = packet.readByte();
    //         //byte mid = packet.readByte();
    //         //byte high = packet.readByte();
    //
    //         //raw |= (((uint)high) << 16) & 0xff0000;
    //         //raw |= (((uint)mid) << 8) & 0x00ff00;
    //         //raw |= low;
    //         ////raw |= packet.readByte();
    //         ////raw |= (((uint)packet.readByte()) << 8)&0x00ff00;
    //         ////raw |= (((uint)packet.readByte()) << 16)&0xff0000;
    //         //raw = (uint)(raw * 0.0021457672119140625);
    //         //cmd.EncoderRaw[i] = raw;
    //     }
    //
    //
    //
    //     return cmd;
    // }

    // private static CMDBase UnpackRESRealTimeData4(BaseCamPacket packet)
    // {
    //     RESRealTimeData4 cmd = (RESRealTimeData4)GetCMDFromPool(packet.ID);
    //
    //     packet.skipBytes(32);
    //
    //     for (int i = 0; i < 3; i++)
    //         cmd.IMUAngles[i] = CMDBase.Units2Angle(packet.readByte(), packet.readByte());
    //     for (int i = 0; i < 3; i++)
    //         cmd.IMUAnglesFrame[i] = CMDBase.Units2Angle(packet.readByte(), packet.readByte());
    //     for (int i = 0; i < 3; i++)
    //         cmd.TargetAngles[i] = CMDBase.Units2Angle(packet.readByte(), packet.readByte());
    //
    //     packet.skipBytes(13);
    //
    //     for (int i = 0; i < 3; i++)
    //         cmd.StatorAngles[i] = CMDBase.Units2Angle(packet.readByte(), packet.readByte());
    //
    //     return cmd;
    // }

    // private static BaseCamPacket PackCMDMotorsOFF(CMDMotorsOFF cmd)
    // {
    //     BaseCamPacket packet = new BaseCamPacket(cmd.id, SIZE_CMD_MOTORSOFF);
    //     packet.writeByte((byte)cmd.mode);
    //
    //     return packet;
    // }

    // private static BaseCamPacket PackCMDMotorsON(CMDMotorsON cmd)
    // {
    //     BaseCamPacket packet = new BaseCamPacket(cmd.id, SIZE_CMD_MOTORSON);
    //
    //     return packet;
    // }

    // private static BaseCamPacket PackCMDExecuteMenuAction(CMDExecuteMenu cmd)
    // {
    //     BaseCamPacket packet = new BaseCamPacket(cmd.id, SIZE_CMD_EXECUTE_MENU);
    //     packet.writeByte((byte)cmd.Action);
    //
    //     return packet;
    // }

    // private static BaseCamPacket PackCMDDataStreamInterval(CMDDataStreamInterval cmd)
    // {
    //     BaseCamPacket packet = new BaseCamPacket(cmd.id, SIZE_CMD_DATA_STREAM_INTERVAL);
    //     packet.writeByte((byte)cmd.CMD_ID_Req);
    //     packet.writeWord(cmd.IntervalMS);
    //     for (int i = 0; i < cmd.flags.Length; i++)
    //         packet.writeByte(cmd.flags[i]);
    //
    //     for (int i = 0; i < cmd.reserved.Length; i++)
    //         packet.writeByte(cmd.reserved[i]);
    //
    //     return packet;
    //
    // }
        
    // private static BaseCamPacket PackCMDRealTimeDataCustom(CMDRealTimeDataCustom cmd)
    // {
    //     BaseCamPacket packet = new BaseCamPacket(cmd.id, SIZE_CMD_REAL_TIME_DATA_CUSTOM);
    //
    //     for (int i = 0; i < cmd.flags.Length; i++)
    //         packet.writeByte(cmd.flags[i]);
    //
    //     for (int i = 0; i < cmd.reserved.Length; i++)
    //         packet.writeByte(cmd.reserved[i]);
    //
    //     return packet;
    //
    // }

    // private static BaseCamPacket PackCMDRealTimeData4(CMDRealTimeData4 cmd)
    // {
    //     BaseCamPacket packet = new BaseCamPacket(cmd.id, SIZE_CMD_REAL_TIME_DATA_4);
    //
    //     return packet;
    //
    // }

    // private static BaseCamPacket PackCMDControl(CMDControl cmd)
    // {
    //     BaseCamPacket packet = new BaseCamPacket(cmd.id, SIZE_CMD_CONTROL);
    //     foreach (byte b in cmd.Modes)
    //         packet.writeByte(b);
    //     for (int i = 0; i < 3; i++)
    //     {
    //         packet.writeWord(CMDBase.SpeedCtrl2Units(cmd.Speeds[i]));
    //         packet.writeWord(CMDBase.Angle2Units(cmd.Angles[i]));
    //     }
    //     return packet;
    // }

    // private static BaseCamPacket PackCMDSetVirtualChannel(CMDSetVirtualChannel cmd)
    // {
    //     BaseCamPacket packet = new BaseCamPacket(cmd.id, SIZE_CMD_SETVIRTUALCHANNEL);
    //
    //     for (int i = 0; i < 32; i++)
    //     {
    //         if (i + 1 == cmd.channel)
    //             packet.writeWord(cmd.value);
    //         else
    //             packet.writeWord(0);
    //     }
    //     return packet;
    // }

}