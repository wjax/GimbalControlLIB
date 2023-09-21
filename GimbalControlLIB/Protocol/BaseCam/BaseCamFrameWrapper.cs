using CommsLIB.Communications.FrameWrappers;

namespace BaseCamLIB.Protocol.BaseCam;

public class BaseCamFrameWrapper : SyncFrameWrapper<BaseCamPacket>
{
    private BaseCamPacket in_cmd;
    private const byte SBGC_CMD_START_BYTE = 0x24;

    private enum STATE
    {
        WAIT,
        GOT_MARKER,
        GOT_ID,
        GOT_LEN,
        GOT_HEADER,
        GOT_DATA,
        GOT_CRCL,
    };
    private STATE state;
	
    int len;
    ushort crc;

    public BaseCamFrameWrapper(): base(false)
    {

    }

    public override void AddBytes(byte[] bytes, int length)
    {
        for (int i = 0; i < length; i++)
            ProcessByte(bytes[i]);
    }

    private void ProcessByte(byte c)
    {
        switch (state)
        {
            case STATE.WAIT:
                if (c == SBGC_CMD_START_BYTE)
                {
                    state = STATE.GOT_MARKER;
                }
                else
                {
                    //onParseError();
                }
                break;

            case STATE.GOT_MARKER:
                in_cmd.init(c); // got command id
                state = STATE.GOT_ID;
                break;

            case STATE.GOT_ID:
                len = c;
                state = STATE.GOT_LEN;
                break;

            case STATE.GOT_LEN:
                if (c == (byte)(in_cmd.ID + len) && len <= BaseCamPacket.SBGC_CMD_DATA_SIZE)
                { // checksum and size ok
                    state = len == 0 ? STATE.GOT_DATA : STATE.GOT_HEADER;
                    in_cmd.HChecksum = c;
                }
                else
                {
                    //onParseError();
                    state = STATE.WAIT;
                }
                break;

            case STATE.GOT_HEADER:
                in_cmd.writeByte(c);
                if (in_cmd.Len == len)
                    state = STATE.GOT_DATA;
                break;

            case STATE.GOT_DATA:
                crc = in_cmd.CalcCRC16();

                if (c == (byte)crc)
                {
                    // checksum ok
                    state = STATE.GOT_CRCL;
                }
                else
                {
                    state = STATE.WAIT;
                }
                break;
            case STATE.GOT_CRCL:
                if (c == ((byte)(crc >> 8)))
                {
                    //FIRE EVENT 
                    FireEvent(in_cmd);
                }
                state = STATE.WAIT;
                break;
        }
    }

    public override void Start()
    {
        state = STATE.WAIT;
        in_cmd = new BaseCamPacket();
    }

    public override void Stop()
    {
           
    }

    public override byte[] Data2BytesSync(BaseCamPacket data, out int count)
    {
        byte[] buffer = data.getNetworkBytes(true, out count);

        return buffer;
    }
}