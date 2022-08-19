namespace BaseCamLIB.GimbalState
{
    public enum GIMBAL_MODE
    {
        OTHER = 0,
        NON_FOLLOW = 1,
        FOLLOW_YAW = 2,
        
    }

    public class GimbalStatus
    {
        public float RollAngle;
        public float PitchAngle;
        public float YawAngle;

        public float RollENCAngle;
        public float PitchENCAngle;
        public float YawENCAngle;

        public float RollSpeed;
        public float PitchSpeed;
        public float YawSpeed;

        GIMBAL_MODE Mode;

        public GimbalStatus(GIMBAL_MODE _Mode, float _Roll, float _Pitch, float _Yaw)
        {
            Mode = _Mode;
            RollAngle = _Roll;
            PitchAngle = _Pitch;
            YawAngle = _Yaw;
        }

        public GimbalStatus(float rollENC, float pitchENC, float yawENC)
        {
            RollENCAngle = rollENC;
            PitchENCAngle = pitchENC;
            YawENCAngle = yawENC;
        }

        public GimbalStatus()
        {

        }
    }
}
