using BaseCamLIB.GimbalState;
using BaseCamLIB.Protocol.BaseCam;
using BaseCamLIB.Protocol.BaseCam.CMDS;
using BaseCamLIB.Protocol.BaseCam.CMDS.IN;
using BaseCamLIB.Protocol.BaseCam.CMDS.OUT;
using CommsLIBLite.Base;
using CommsLIBLite.Communications;
using System.Text;

namespace GimbalControlLIB
{
    internal class BaseCamManager : GimbalManager
    {
        private BaseCamFrameWrapper baseCamFrameWrapper;
        private TCPNETCommunicator<BaseCamPacket> baseCamLink;

        private GimbalStatus gimbalStatus = new GimbalStatus();

        StringBuilder textLogger = new StringBuilder();

        private int yawDirection = 1;
        private int pitchDirection = 1;

        public BaseCamManager() : base()
        {
            baseCamFrameWrapper = new BaseCamFrameWrapper();
            baseCamFrameWrapper.FrameAvailableEvent += BaseCamFrameWrapper_FrameAvailableEvent;
            baseCamLink = new TCPNETCommunicator<BaseCamPacket>(baseCamFrameWrapper, true);
            baseCamLink.ConnectionStateEvent += OnConnection;
        }

        private void OnConnection(string ID, ConnUri uri, bool connected)
        {
            FireConnectionEvent(connected);
        }

        private void BaseCamFrameWrapper_FrameAvailableEvent(string ID, BaseCamPacket packet)
        {
            CMDBase cmd = BaseCamCMDParsers.UnpackCMD(packet);

            if (cmd is RESRealTimeDataCustom)
            {
                RESRealTimeDataCustom cmdData = cmd as RESRealTimeDataCustom;
                //textLogger.Clear();
                //textLogger.Append("CMDRealTimeDataCustom - Size: ").Append(packet.Len)
                //    .Append(" Roll: ").Append(cmdData.IMUAngles[0])
                //    .Append(" Pitch: ").Append(-cmdData.IMUAngles[1])
                //    .Append(" Yaw: ").Append(cmdData.IMUAngles[2])
                //    //.Append(" RAW STATOR Roll: ").Append(cmdData.StatorAngles[0])
                //    //.Append(" RAW STATOR Pitch: ").Append(cmdData.StatorAngles[1])
                //    //.Append(" RAW STATOR Yaw: ").Append(cmdData.StatorAngles[2]);
                //    //.Append(" RAW ENC Roll: ").Append(cmdData.EncoderRaw[0])
                //    //.Append(" RAW ENC Pitch: ").Append(-cmdData.EncoderRaw[1])
                //    .Append(" RAW ENC Yaw: ").Append(cmdData.EncoderRaw[2]);

                //System.Diagnostics.Debug.WriteLine(textLogger.ToString());
                gimbalStatus.RollAngle = cmdData.IMUAngles[0];
                gimbalStatus.PitchAngle = -1 * cmdData.IMUAngles[1];
                gimbalStatus.YawAngle = cmdData.IMUAngles[2];
                gimbalStatus.RollENCAngle = cmdData.EncoderRaw[0];
                gimbalStatus.PitchENCAngle = pitchDirection * cmdData.EncoderRaw[1];
                gimbalStatus.YawENCAngle = yawDirection * cmdData.EncoderRaw[2];

                FireDataEvent(gimbalStatus);
            }
            else if (cmd is RESRealTimeData4)
            {
                RESRealTimeData4 cmdData = cmd as RESRealTimeData4;
                //text.Append("CMDRealTimeData4 - Size: ").Append(packet.Len)
                //    .Append(" Roll: ").Append(cmdData.IMUAngles[0])
                //    .Append(" Pitch: ").Append(cmdData.IMUAngles[1])
                //    .Append(" Yaw: ").Append(cmdData.IMUAngles[2])
                //    .Append(" RAW STATOR Roll: ").Append(cmdData.StatorAngles[0])
                //    .Append(" RAW STATOR Pitch: ").Append(cmdData.StatorAngles[1])
                //    .Append(" RAW STATOR Yaw: ").Append(cmdData.StatorAngles[2])
                //    .Append(" Frame Roll: ").Append(cmdData.IMUAnglesFrame[0])
                //    .Append(" Frame Pitch: ").Append(cmdData.IMUAnglesFrame[1])
                //    .Append(" Frame Yaw: ").Append(cmdData.IMUAnglesFrame[2]);

                //System.Diagnostics.Debug.WriteLine(text.ToString());
                //FireDataEvent(new GimbalStatus(GIMBAL_MODE.NON_FOLLOW, cmdData.IMUAngles[0], cmdData.IMUAngles[1], cmdData.IMUAngles[2]));
            }
        }

        private void SendPacket(CMDBase cmd)
        {
            BaseCamPacket packet = BaseCamCMDParsers.PackCMD(cmd);
            if (packet != null)
            {
                byte[] buff = packet.getNetworkBytes(true);
                baseCamLink.SendASync(buff, buff.Length);
            }
        }

        public override void Move(float[] angles, float[] speeds, MOVEMENT_MODE mode)
        {
            // Invert Pitch as Alexmos and us follow different axis
            angles[1] *= -1;

            CMDControl c = new CMDControl();
            try
            {
                for (int i = 0; i < c.Modes.Length; i++)
                    c.Modes[i] = mode == MOVEMENT_MODE.ANGLE ? CMDControl.MODE.ANGLE:CMDControl.MODE.SPEED;

                for (int i = 0; i < c.Modes.Length; i++)
                {
                    c.Angles[i] = angles[i];
                    c.Speeds[i] = speeds[i];
                }
                
                SendPacket(c);
            }
            finally { }
        }

        public override void MoveDifferential(float[] dangles, float[] speeds)
        {
            CMDControl c = new CMDControl();
            try
            {
                for (int i = 0; i < c.Modes.Length; i++)
                    c.Modes[i] = CMDControl.MODE.ANGLE;

                for (int i = 0; i < c.Modes.Length; i++)
                {
                    c.Speeds[i] = speeds[i];
                }

                c.Angles[0] = gimbalStatus.RollAngle + dangles[0];
                c.Angles[1] = gimbalStatus.PitchAngle + dangles[1];
                c.Angles[2] = gimbalStatus.YawAngle + dangles[2];

                SendPacket(c);
            }
            finally { }
        }

        public override void MotorsONOFF(bool ON)
        {
            CMDBase c;
            if (ON)
                c = new CMDMotorsON();
            else
                c = new CMDMotorsOFF();

            SendPacket(c);
        }

        public override void GoHomePosition()
        {
            CMDExecuteMenu c = new CMDExecuteMenu
            {
                Action = CMDExecuteMenu.ACTION.MENU_CMD_HOME_POSITION
            };
            SendPacket(c);
        }

        public override void CenterYaw()
        {
            CMDExecuteMenu c = new CMDExecuteMenu
            {
                Action = CMDExecuteMenu.ACTION.MENU_CENTER_YAW_SHORTEST
            };
            SendPacket(c);
        }

        public override void UserCustomAction()
        {
            MenuButtonPress();
        }

        private void MenuButtonPress()
        {
            CMDExecuteMenu c = new CMDExecuteMenu
            {
                Action = CMDExecuteMenu.ACTION.MENU_CMD_BUTTON_PRESS
            };
            SendPacket(c);
        }

        public override void RequestRealTimeData(object data)
        {
            int interval = 100;
            if (data != null)
                interval = (data as int?) ?? 100;

            CMDDataStreamInterval c = new CMDDataStreamInterval()
            {
                CMD_ID_Req = CMDDataStreamInterval.CMD_ID_REQUESTED.CMD_REALTIME_DATA_CUSTOM,
                IntervalMS = (ushort)interval
            };
           
            SendPacket(c);
        }

        public override void init(ConnUri uri, bool yawInverted, bool pitchInverted)
        {
            baseCamLink.Init(uri, true, "BASECAM", 0);
            yawDirection = yawInverted ? -1 : 1;
            pitchDirection = pitchInverted ? -1 : 1;
        }

        public override void start()
        {
            baseCamLink.Start();
            baseCamFrameWrapper.Start();
        }

        public override void stop()
        {
            _ = baseCamLink.Stop();
            baseCamFrameWrapper.Stop();
        }

        public override void SetMode(GIMBAL_MODE mode)
        {
            
        }

        public override void SetAPIVirtualChannel(int _channel, short _value)
        {
            CMDSetVirtualChannel c = new CMDSetVirtualChannel()
            {
                channel = _channel,
                value = _value
            };

            System.Diagnostics.Debug.WriteLine($"Value Zoom {_value}");
            SendPacket(c);
        }
    }
}
