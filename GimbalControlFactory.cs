using System;
using System.Collections.Generic;
using System.Text;

namespace GimbalControlLIB
{
    public static class GimbalControlFactory
    {
        public enum GIMBAL_TYPE
        {
            BASECAM
        }

        public static GimbalManager GetGimbalManager(GIMBAL_TYPE type)
        {
            switch (type)
            {
                case GIMBAL_TYPE.BASECAM:
                    return new BaseCamManager();
                default:
                    return null;
            }
        }

    }
}
