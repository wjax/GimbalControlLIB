using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using CommsLIB.Base;
using GimbalControlLIB;
using ReactiveUI;

namespace GimbalControlTesterAvalonia.Views
{
    public partial class MainWindow : Window
    {
        // #region MVVM
        // private Dictionary<string, PropertyChangedEventArgs> _cachedEventArgs = new Dictionary<string, PropertyChangedEventArgs>();
        // public event PropertyChangedEventHandler PropertyChanged;
        //
        // public virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = null)
        // {
        //     PropertyChangedEventArgs eventArgs = default;
        //     if (!_cachedEventArgs.TryGetValue(propertyName, out eventArgs))
        //         _cachedEventArgs[propertyName] = eventArgs = new PropertyChangedEventArgs(propertyName);
        //
        //     PropertyChanged?.Invoke(this, eventArgs);
        // }
        //
        // public virtual bool Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        // {
        //     if (Equals(storage, value))
        //         return false;
        //     storage = value;
        //     NotifyPropertyChanged(propertyName);
        //     return true;
        // }
        // #endregion
        //
        // GimbalManager manager;
        //
        // private float _rollENC;
        // public float RollENC
        // {
        //     get => _rollENC;
        //     set => Set(ref _rollENC, value);
        // }
        //
        // private float _pitchENC;
        // public float PitchENC
        // {
        //     get => _pitchENC;
        //     set => Set(ref _pitchENC, value);
        // }
        //
        // private float _yawENC;
        // public float YawENC
        // {
        //     get => _yawENC;
        //     set => Set(ref _yawENC, value);
        // }
        
        public MainWindow()
        {
            InitializeComponent();
            // manager = GimbalControlFactory.GetGimbalManager(GimbalControlFactory.GIMBAL_TYPE.BASECAM);
            // manager.init(new ConnUri("tcp://192.168.88.101:9003"), false, false);
            // manager.GimbalConnectionEvent += OnGimbalConnection;
            // manager.GimbalDataEvent += OnGimbalData;
            // manager.start();
            // DataContext = this;
        }
        
                
        // private void OnGimbalData(BaseCamLIB.GimbalState.GimbalStatus status)
        // {
        //     //Console.WriteLine($"YawENC {status.YawENCAngle} Yaw {status.YawAngle} PitchENC {status.PitchENCAngle}" + DateTime.Now.ToString("HH:mm:ss.fff"));
        //     // RollENC = status.RollENCAngle;
        //     // PitchENC = status.PitchENCAngle;
        //     // YawENC = status.YawENCAngle;
        //
        //     // status.RollENCAngle.ToString();
        //     // status.PitchENCAngle.ToString();
        //     // status.YawENCAngle.ToString();
        //     
        //     Dispatcher.UIThread.Post(() =>
        //     {
        //         TB_Roll.Text = status.RollENCAngle.ToString();
        //     TB_Pitch.Text = status.PitchENCAngle.ToString();
        //     TB_Yaw.Text = status.YawENCAngle.ToString();
        //     });
        //     
        // }

        // private void OnGimbalConnection(bool connected)
        // {
        //     if (connected)
        //         manager.RequestRealTimeData(50);
        // }

        // private void Button_Click_MOTORSOFF(object sender, RoutedEventArgs e)
        // {
        //     manager.MotorsONOFF(false);
        // }

        // private void Button_Click_MOTORSON(object sender, RoutedEventArgs e)
        // {
        //     manager.MotorsONOFF(true);
        // }
        //
        // private void Button_Click_HOME(object sender, RoutedEventArgs e)
        // {
        //     manager.GoHomePosition();
        // }

        // private void Button_Click_CONTROL(object sender, RoutedEventArgs e)
        // {
        //     try
        //     {
        //         float angleR = float.Parse(TB_AngleR.Text);
        //         float angleP = float.Parse(TB_AngleP.Text);
        //         float angleY = float.Parse(TB_AngleY.Text);
        //
        //         float speedR = float.Parse(TB_SpeedR.Text);
        //         float speedP = float.Parse(TB_SpeedP.Text);
        //         float speedY = float.Parse(TB_SpeedY.Text);
        //
        //         manager.Move(new float[3] { angleR, angleP, angleY }, new float[3] { speedR, speedP, speedY }, MOVEMENT_MODE.ANGLE);
        //     }
        //     finally { }
        // }
        //
        // private void Button_Click_ZOOM(object sender, RoutedEventArgs e)
        // {
        //     try
        //     {
        //         short zoom = short.Parse(TB_ZOOM.Text);
        //
        //         manager.SetAPIVirtualChannel(1, zoom);
        //     }
        //     finally { }
        // }
    }
}