using BaseCamLIB.GimbalState;
using CommsLIBLite.Base;

namespace GimbalControlLIB
{
    public enum MOVEMENT_MODE
    {
        ANGLE,
        SPEED
    }

    public abstract class GimbalManager
    {
        public delegate void GimbalDataDelegate(GimbalStatus status);
        public event GimbalDataDelegate GimbalDataEvent;

        public delegate void GimbalConnectionDelegate(bool connected);
        public event GimbalConnectionDelegate GimbalConnectionEvent;

        public abstract void init(ConnUri uri, bool yawInverted, bool pitchInverted);
        public abstract void start();
        public abstract void stop();

        public abstract void Move(float[] angles, float[] speeds, MOVEMENT_MODE moveMode);
        public abstract void MoveDifferential(float[] dangles, float[] speeds);
        public abstract void MotorsONOFF(bool ON);
        public abstract void GoHomePosition();
        public abstract void CenterYaw();
        public abstract void UserCustomAction();
        public abstract void SetMode(GIMBAL_MODE mode);
        public abstract void RequestRealTimeData(object data);
        public abstract void SetAPIVirtualChannel(int channel, short value);

        public void FireDataEvent(GimbalStatus status)
        {
            GimbalDataEvent?.Invoke(status);
        }

        public void FireConnectionEvent(bool connected)
        {
            GimbalConnectionEvent?.Invoke(connected);
        }

    }
}
