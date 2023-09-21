using System;
using Avalonia.Interactivity;
using CommsLIB.Base;
using GimbalControlLIB;
using Microsoft.Extensions.Logging;
using ReactiveUI;

namespace GimbalControlTesterAvalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private GimbalManager manager;
        
        private float _rollENC;
        public float RollENC
        {
            get => _rollENC;
            set
            {
                //this.RaiseAndSetIfChanged(ref _rollENC, value);
                //if (Math.Abs(_rollENC - value) > 0.1)
                //{
                    //_rollENC = value;
                    this.RaiseAndSetIfChanged(ref _rollENC, value);
                //}
            }
        }

        private float _pitchENC;
        public float PitchENC
        {
            get => _pitchENC;
            set
            {
                // if (Math.Abs(_pitchENC - value) > 0.1)
                // {
                    //_pitchENC = value;
                    this.RaiseAndSetIfChanged(ref _pitchENC, value);
                // }
                //this.RaiseAndSetIfChanged(ref _pitchENC, value);
            }
        }

        private float _yawENC;
        public float YawENC
        {
            get => _yawENC;
            set
            {
                // if (Math.Abs(_yawENC - value) > 0.1)
                // {
                    //_yawENC = value;
                    this.RaiseAndSetIfChanged(ref _yawENC, value);
                // }
                //this.RaiseAndSetIfChanged(ref _yawENC, value);
            }
        }

        public MainWindowViewModel()
        {
            CommsLIB.Logging.LoggingManager.SetLoggingFactory(GetLoggerFactory4Comms());
            manager = GimbalControlFactory.GetGimbalManager(GimbalControlFactory.GIMBAL_TYPE.BASECAM);
            manager.init(new ConnUri("tcp://192.168.88.101:9003"), false, false);
            manager.GimbalConnectionEvent += OnGimbalConnection;
            manager.GimbalDataEvent += OnGimbalData;
            manager.start();
        }
        
        public LoggerFactory GetLoggerFactory4Comms()
        {
            LoggerFactory loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new NLog.Extensions.Logging.NLogLoggerProvider());
            return loggerFactory;
        }
        
        private void OnGimbalData(BaseCamLIB.GimbalState.GimbalStatus status)
        {
            //Console.WriteLine($"YawENC {status.YawENCAngle} Yaw {status.YawAngle} PitchENC {status.PitchENCAngle}" + DateTime.Now.ToString("HH:mm:ss.fff"));
            RollENC = status.RollENCAngle;
            PitchENC = status.PitchENCAngle;
            YawENC = status.YawENCAngle;

            // status.RollENCAngle.ToString();
            // status.PitchENCAngle.ToString();
            // status.YawENCAngle.ToString();
            
            // Dispatcher.UIThread.Post(() =>
            // {
            //     TB_Roll.Text = status.RollENCAngle.ToString();
            //     TB_Pitch.Text = status.PitchENCAngle.ToString();
            //     TB_Yaw.Text = status.YawENCAngle.ToString();
            // });
            
        }
        
        private void OnGimbalConnection(bool connected)
        {
            if (connected)
                manager.RequestRealTimeData(50);
        }
    }
}