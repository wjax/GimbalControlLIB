﻿using System;
using ObjectPool;

namespace BaseCamLIB.Protocol.BaseCam.CMDS;

////////////////////// Command ID definitions ////////////////
public enum CMD_ID : byte
{
    UNKNOWN = 0,
    CMD_READ_PARAMS = 82,
    CMD_WRITE_PARAMS = 87,
    CMD_REALTIME_DATA = 68,
    CMD_BOARD_INFO = 86,
    CMD_CALIB_ACC = 65,
    CMD_CALIB_GYRO = 103,
    CMD_CALIB_EXT_GAIN = 71,
    CMD_USE_DEFAULTS = 70,
    CMD_CALIB_POLES = 80,
    CMD_RESET = 114,
    CMD_HELPER_DATA = 72,
    CMD_CALIB_OFFSET = 79,
    CMD_CALIB_BAT = 66,
    CMD_MOTORS_ON = 77,
    CMD_MOTORS_OFF = 109,
    CMD_CONTROL = 67,
    CMD_TRIGGER_PIN = 84,
    CMD_EXECUTE_MENU = 69,
    CMD_GET_ANGLES = 73,
    CMD_CONFIRM = 67,
    CMD_BOARD_INFO_3 = 20,
    CMD_READ_PARAMS_3 = 21,
    CMD_WRITE_PARAMS_3 = 22,
    CMD_REALTIME_DATA_3 = 23,
    CMD_REALTIME_DATA_4 = 25,
    CMD_SELECT_IMU_3 = 24,
    CMD_READ_PROFILE_NAMES = 28,
    CMD_WRITE_PROFILE_NAMES = 29,
    CMD_QUEUE_PARAMS_INFO_3 = 30,
    CMD_SET_ADJ_VARS_VAL = 31,
    CMD_SAVE_PARAMS_3 = 32,
    CMD_READ_PARAMS_EXT = 33,
    CMD_WRITE_PARAMS_EXT = 34,
    CMD_AUTO_PID = 35,
    CMD_SERVO_OUT = 36,
    CMD_I2C_WRITE_REG_BUF = 39,
    CMD_I2C_READ_REG_BUF = 40,
    CMD_WRITE_EXTERNAL_DATA = 41,
    CMD_READ_EXTERNAL_DATA = 42,
    CMD_READ_ADJ_VARS_CFG = 43,
    CMD_WRITE_ADJ_VARS_CFG = 44,
    CMD_API_VIRT_CH_CONTROL = 45,
    CMD_ADJ_VARS_STATE = 46,
    CMD_EEPROM_WRITE = 47,
    CMD_EEPROM_READ = 48,
    CMD_CALIB_INFO = 49,
    CMD_BOOT_MODE_3 = 51,
    CMD_SYSTEM_STATE = 52,
    CMD_READ_FILE = 53,
    CMD_WRITE_FILE = 54,
    CMD_FS_CLEAR_ALL = 55,
    CMD_AHRS_HELPER = 56,
    CMD_RUN_SCRIPT = 57,
    CMD_SCRIPT_DEBUG = 58,
    CMD_CALIB_MAG = 59,
    CMD_GET_ANGLES_EXT = 61,
    CMD_READ_PARAMS_EXT2 = 62,
    CMD_WRITE_PARAMS_EXT2 = 63,
    CMD_GET_ADJ_VARS_VAL = 64,
    CMD_CALIB_MOTOR_MAG_LINK = 74,
    CMD_GYRO_CORRECTION = 75,
    CMD_DATA_STREAM_INTERVAL = 85,
    CMD_REALTIME_DATA_CUSTOM = 88,
    CMD_BEEP_SOUND = 89,
    CMD_ENCODERS_CALIB_OFFSET_4 = 26,
    CMD_ENCODERS_CALIB_FLD_OFFSET_4 = 27,
    CMD_CONTROL_CONFIG = 90,
    CMD_CALIB_ORIENT_CORR = 91,
    CMD_COGGING_CALIB_INFO = 92,
    CMD_CALIB_COGGING = 93,
    CMD_CALIB_ACC_EXT_REF = 94,
    CMD_PROFILE_SET = 95,
    CMD_CAN_DEVICE_SCAN = 96,
    CMD_CAN_DRV_HARD_PARAMS = 97,
    CMD_CAN_DRV_STATE = 98,
    CMD_CAN_DRV_CALIBRATE = 99,
    CMD_READ_RC_INPUTS = 100,
    CMD_REALTIME_DATA_CAN_DRV = 101,
    CMD_EVENT = 102,
    CMD_READ_PARAMS_EXT3 = 104,
    CMD_WRITE_PARAMS_EXT3 = 105,
    CMD_EXT_IMU_DEBUG_INFO = 106,
    CMD_SET_DEBUG_PORT = 249,
    CMD_MAVLINK_INFO = 250,
    CMD_MAVLINK_DEBUG = 251,
    CMD_DEBUG_VARS_INFO_3 = 253,
    CMD_DEBUG_VARS_3 = 254,
    CMD_ERROR = 255
}

public abstract class CMDBase : IPoolable, IDisposable
{
    private const float degUnit = 0.02197265625f;
    private const float degENCUnit = 0.000021457672119140625f;
    private const float degSegUnit = 0.06103701895f;

    private const float degSegCtrl = 0.1220740379f;

    public byte id;

    public CMDBase()
    {
        Console.WriteLine("CMDBase created");    
    }
    
    public static float Units2Angle(byte L, byte H)
    {
        short n = (short) (L | (H << 8));
        return n * degUnit;
    }

    public static float Units2AngleENC(byte L, byte M, byte H)
    {
        uint raw = 0;
        raw |= (((uint) H) << 16) & 0xff0000;
        raw |= (((uint) M) << 8) & 0x00ff00;
        raw |= L;
        return raw * degENCUnit;
    }

    public static float Units2Speed(byte L, byte H)
    {
        short n = (short) (L | (H << 8));
        return n * degSegUnit;
    }

    public static short SpeedCtrl2Units(float speed)
    {
        short n = (short) (speed / degSegCtrl);
        return n;
    }

    public static short Angle2Units(float angle)
    {
        short n = (short) (angle / degUnit);
        return n;
    }

    public abstract BaseCamPacket Pack();

    #region Pool

    public virtual void Reset()
    {
        //disposedValue = false;
    }

    public Action ReturnToPool { get; set; }

    #endregion

    #region IDisposable

    private bool disposedValue;

    protected void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                ReturnToPool();
            }
            
            //disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~CMDBase()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    #endregion
}

public interface ICommandDePacker<T> where T : CMDBase
{
    public static abstract T Depack(BaseCamPacket packet);
}