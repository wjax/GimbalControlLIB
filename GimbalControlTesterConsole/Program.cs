// See https://aka.ms/new-console-template for more information

using CommsLIB.Base;
using GimbalControlLIB;

Console.WriteLine("Hello, Gimbal");

GimbalManager manager;
manager = GimbalControlFactory.GetGimbalManager(GimbalControlFactory.GIMBAL_TYPE.BASECAM);
manager.init(new ConnUri("tcp://192.168.88.101:9003"), false, false);
manager.GimbalConnectionEvent += OnGimbalConnection;
manager.GimbalDataEvent += OnGimbalData;
manager.start();

void OnGimbalData(BaseCamLIB.GimbalState.GimbalStatus status)
{
    //Console.WriteLine($"YawENC {status.YawENCAngle} Yaw {status.YawAngle} PitchENC {status.PitchENCAngle}" + DateTime.Now.ToString("HH:mm:ss.fff"));
    // RollENC = status.RollENCAngle;
    // PitchENC = status.PitchENCAngle;
    // YawENC = status.YawENCAngle;
}

void OnGimbalConnection(bool connected)
{
    manager.RequestRealTimeData(50);
}

Console.ReadLine();