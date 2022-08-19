using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCamLIB.Protocol.BaseCam.CMDS.OUT
{
    public class CMDControl : CMDBase
    {
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

        public CMDControl()
        {
            id = (byte)CMD_ID.CMD_CONTROL;
        }
    }
}
