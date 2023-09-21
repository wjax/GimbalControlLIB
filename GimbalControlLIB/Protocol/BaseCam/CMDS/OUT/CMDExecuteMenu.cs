using ObjectPool.Native;

namespace BaseCamLIB.Protocol.BaseCam.CMDS.OUT;

public class CMDExecuteMenu : CMDBase
{
    public static CMDExecuteMenu Get() => NativePool<CMDExecuteMenu>.Get();
        
    public enum ACTION : byte
    {
        MENU_CMD_NO = 0,
        MENU_CMD_PROFILE1 = 1,
        MENU_CMD_PROFILE2 = 2,
        MENU_CMD_PROFILE3 = 3,
        MENU_CMD_SWAP_PITCH_ROLL = 4 ,
        MENU_CMD_SWAP_YAW_ROLL = 5 ,
        MENU_CMD_CALIB_ACC = 6 ,
        MENU_CMD_RESET = 7 ,
        MENU_CMD_SET_ANGLE = 8 ,
        MENU_CMD_CALIB_GYRO = 9 ,
        MENU_CMD_MOTOR_TOGGLE = 10 ,
        MENU_CMD_MOTOR_ON = 11 ,
        MENU_CMD_MOTOR_OFF = 12 ,
        MENU_CMD_FRAME_UPSIDE_DOWN = 13 ,
        MENU_CMD_PROFILE4 = 14 ,
        MENU_CMD_PROFILE5 = 15 ,
        MENU_CMD_AUTO_PID = 16 ,
        MENU_CMD_LOOK_DOWN = 17 ,
        MENU_CMD_HOME_POSITION = 18 ,
        MENU_CMD_RC_BIND = 19 ,
        MENU_CMD_CALIB_GYRO_TEMP = 20 ,
        MENU_CMD_CALIB_ACC_TEMP = 21 ,
        MENU_CMD_BUTTON_PRESS = 22 ,
        MENU_CMD_RUN_SCRIPT1 = 23 ,
        MENU_CMD_RUN_SCRIPT2 = 24 ,
        MENU_CMD_RUN_SCRIPT3 = 25 ,
        MENU_CMD_RUN_SCRIPT4 = 26 ,
        MENU_CMD_RUN_SCRIPT5 = 27 ,
        MENU_CMD_CALIB_MAG = 33 ,
        MENU_CMD_LEVEL_ROLL_PITCH = 34 ,
        MENU_CMD_CENTER_YAW = 35 ,
        MENU_CMD_UNTWIST_CABLES = 36 ,
        MENU_CMD_SET_ANGLE_NO_SAVE = 37 ,
        MENU_HOME_POSITION_SHORTEST = 38 ,
        MENU_CENTER_YAW_SHORTEST = 39 ,
        MENU_ROTATE_YAW_180 = 40 ,
        MENU_ROTATE_YAW_180_FRAME_REL = 41 ,
        MENU_SWITCH_YAW_180_FRAME_REL = 42 ,
        MENU_SWITCH_POS_ROLL_90 = 43 ,
        MENU_START_TIMELAPSE = 44 ,
        MENU_CALIB_MOMENTUM = 45 ,
        MENU_LEVEL_ROLL = 46 ,
        MENU_REPEAT_TIMELAPSE = 47 ,
        MENU_LOAD_PROFILE_SET1 = 48 ,
        MENU_LOAD_PROFILE_SET2 = 49 ,
        MENU_LOAD_PROFILE_SET3 = 50 ,
        MENU_LOAD_PROFILE_SET4 = 51 ,
        MENU_LOAD_PROFILE_SET5 = 52 ,
        MENU_LOAD_PROFILE_SET_BACKUP = 53 ,
        MENU_INVERT_RC_ROLL = 54 ,
        MENU_INVERT_RC_PITCH = 55 ,
        MENU_INVERT_RC_YAW = 56 ,
        MENU_SNAP_TO_FIXED_POSITION = 57 ,
        MENU_CAMERA_REC_PHOTO_EVENT = 58 ,
        MENU_CAMERA_PHOTO_EVENT = 59 ,
        MENU_MOTORS_SAFE_STOP = 60 ,
        MENU_CALIB_ACC_AUTO = 61 ,
        MENU_RESET_IMU = 62

    }
    public ACTION Action;
    public CMDExecuteMenu() : base()
    {
        id = (byte)CMD_ID.CMD_EXECUTE_MENU;
        Action = ACTION.MENU_CMD_HOME_POSITION;
    }

    public override BaseCamPacket Pack()
    {
        var packet = BaseCamPacket.Get((byte) CMD_ID.CMD_EXECUTE_MENU);
        packet.writeByte((byte)Action);

        return packet;
    }

    public override void Reset()
    {
        id = (byte) CMD_ID.UNKNOWN;
            
        base.Reset();
    }
}