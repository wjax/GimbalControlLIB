using System;
using System.Collections.Generic;
using System.Text;
using ObjectPool;
using ObjectPool.Native;

namespace BaseCamLIB.Protocol.BaseCam
{
    public class BaseCamPacket : IPoolable, IDisposable
    {
        #region defines
        // Size of header and checksums
        public const short SBGC_CMD_NON_PAYLOAD_BYTES = 6;
        // Max. size of a command after packing to bytes
        public const short SBGC_CMD_MAX_BYTES = 255;
        // Max. size of a payload data
        public const short SBGC_CMD_DATA_SIZE = (SBGC_CMD_MAX_BYTES - SBGC_CMD_NON_PAYLOAD_BYTES);
        // Start char protocol 1
        public const byte SBGC_STC_V2 = 0x24;

        private const int INDEX_SOF = 0;
        private const int INDEX_ID = 1;
        private const int INDEX_LEN = 2;
        private const int INDEX_HEADER_CHECKSUM = 3;
        private const int INDEX_PAYLOAD = 4;
        #endregion

        public byte[] data;

        private int pos;
        public byte ID
        {
            set => data[INDEX_ID] = value;
            get => data[INDEX_ID];
        }
        public byte Len {
            set => data[INDEX_LEN] = value;
            get => data[INDEX_LEN];
        }
        public byte HChecksum
        {
            set => data[INDEX_HEADER_CHECKSUM] = value;
            get => data[INDEX_HEADER_CHECKSUM];
        }
        
        //internal byte len;
        private ushort crc;

        public BaseCamPacket()
        {
            data = new byte[SBGC_CMD_MAX_BYTES];
            data[INDEX_SOF] = SBGC_STC_V2;
            
            Console.WriteLine("BaseCamPacket created");
        }

        public BaseCamPacket(byte _id) : this(_id, SBGC_CMD_MAX_BYTES)
        {
            
        }

        public BaseCamPacket(byte _id, int SIZE_PACKET)
        {
            data = new byte[SIZE_PACKET];
            data[INDEX_SOF] = SBGC_STC_V2;
            ID = _id;
        }

        public BaseCamPacket clone()
        {
            var cloned = new BaseCamPacket(ID);
            cloned.pos = pos;
            Array.Copy(data, cloned.data, data.Length);
            cloned.crc = crc;

            return cloned;
        }

        /* Check if limit reached after reading data buffer */
        internal bool checkLimit()
        {
            return Len == pos;
        }

        internal int getBytesAvailable()
        {
            return Len - pos;
        }


        internal void init(byte _id)
        {
            ID = _id;
            Len = 0;
            pos = INDEX_PAYLOAD;
        }

        internal void reset()
        {
            Len = 0;
            pos = INDEX_PAYLOAD;
        }


        internal byte readByte()
        {
            if (pos < (INDEX_PAYLOAD + Len))
            {
                return data[pos++];
            }
            else
            {
                pos++;
                return 0;
            }
        }

        internal short readShort()
        {
            return (short)(readByte() | (readByte() << 8)); 
        }

        internal ushort readUShort()
        {
            return (ushort)(readByte() | (readByte() << 8));
        }

        internal void writeByte(byte b)
        {
            if (Len < data.Length)
            {
                data[INDEX_PAYLOAD + Len++] = b;
            }
        }

        internal void writeWord(ushort w)
        {
            writeByte((byte)w); // low
            writeByte((byte)(w >> 8)); // high
        }

        internal void writeWord(short w)
        {
            writeByte((byte)w); // low
            writeByte((byte)(w >> 8)); // high
        }

        internal void skipBytes(byte size)
        {
            while (size-- > 0)
            {
                readByte();
            }
        }

        internal BaseCamPacket UpdateChecksums()
        {
            //header 
            HChecksum = (byte)(ID + Len);
            // payload
            CalcCRC16();

            return this;
        }

        private int getPacketLength()
        {
            return Len + SBGC_CMD_NON_PAYLOAD_BYTES;
        }

        public byte[] getNetworkBytes(bool updateChecksums, out int length)
        {
            if (updateChecksums)
                UpdateChecksums();

            data[SBGC_CMD_NON_PAYLOAD_BYTES + Len - 2] = (byte)crc;
            data[SBGC_CMD_NON_PAYLOAD_BYTES + Len - 1] = (byte)(crc >> 8);
            
            length = getPacketLength();

            return data;
        }

        public int getNetworkBytes(ref byte[] buff, bool updateChecksums)
        {
            if (buff.Length < getPacketLength())
                return 0;

            if (updateChecksums)
                UpdateChecksums();

            buff[INDEX_SOF] = SBGC_STC_V2;
            buff[INDEX_ID] = ID;
            buff[INDEX_LEN] = Len;
            buff[INDEX_HEADER_CHECKSUM] = HChecksum;
            for (int i = INDEX_PAYLOAD; i < INDEX_PAYLOAD + Len; i++)
                buff[i] = data[i];

            buff[SBGC_CMD_NON_PAYLOAD_BYTES + Len - 2] = (byte)crc;
            buff[SBGC_CMD_NON_PAYLOAD_BYTES + Len - 1] = (byte)(crc >> 8);

            return getPacketLength();
        }

        public ushort CalcCRC16()
        {
            crc = crc16((ushort)(Len + 3), data, INDEX_ID);
            return crc;
        }

        public static ushort crc16(ushort length, byte[] data, ushort offset)
        {
            ushort counter; ushort polynom = 0x8005;
            ushort crc_register = 0;//(ushort)((ushort)crc[0] | ((ushort)crc[1] << 8));
            byte shift_register;
            byte data_bit, crc_bit;
            for (counter = offset; counter < length+offset; counter++)
            {
                for (shift_register = 0x01; shift_register > 0x00; shift_register <<= 1)
                {
                    data_bit =(byte)( (data[counter] & shift_register) != 0 ? 1 : 0);
                    crc_bit = (byte)(crc_register >> 15);
                    crc_register <<= 1;
                    if (data_bit != crc_bit) crc_register ^= polynom;
                }
            }
            //crc[0] = (byte)crc_register;
            //crc[1] = (byte)(crc_register >> 8);
            return crc_register;
        }

        #region pooling
        public static BaseCamPacket Get(byte id)
        {
            var packet = NativePool<BaseCamPacket>.Get();
            packet.init(id);

            return packet;
        }
        public void Reset()
        {
            reset();
        }

        public Action ReturnToPool { get; set; }
        #endregion
        
        #region Dispose
        private bool disposedValue;
        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    ReturnToPool();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~SLAPacket()
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
}
