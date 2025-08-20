using System.Runtime.InteropServices;

namespace Mono.Debugger.Unpack
{
    public class Deserializer
    {
        private byte[] _buffer;
        private int _bufferSize;
        private int _offset;
        private bool _isBigEndian;

        public Deserializer(byte[] buffer, int bufferSize)
        {
            this._buffer = new byte[bufferSize];
            Array.Copy(buffer, this._buffer, bufferSize);
            
            this._bufferSize = bufferSize;
            this._offset = 0;
            this._isBigEndian = true;
        }

        public bool CanReadMore(int size = 1)
        {
            return _offset + size <= _bufferSize;
        }

        public byte[] ReadBytes(int size)
        {
            if(!CanReadMore(size)) throw new IndexOutOfRangeException();
            
            byte[] tempBuffer = new byte[size];
            Array.Copy(_buffer, _offset, tempBuffer, 0, size);
            _offset += size;
            
            if(_isBigEndian) Array.Reverse(tempBuffer);
            return tempBuffer;
        }

        public byte ReadByte()
        {
            if (!CanReadMore()) throw new IndexOutOfRangeException();
            
            return _buffer[_offset++];
        }
        
        public UInt16 ReadUInt16()
        {
            var tempBuffer = ReadBytes(2);
            return BitConverter.ToUInt16(tempBuffer, 0);
        }

        public UInt32 ReadUInt32()
        {
            var tempBuffer = ReadBytes(4);
            return BitConverter.ToUInt32(tempBuffer, 0);
        }

        public UInt64 ReadUInt64()
        {
            var tempBuffer = ReadBytes(8);
            return BitConverter.ToUInt64(tempBuffer, 0);
        }

        public UInt32 ReadId()
        {
            return ReadUInt32();
        }

        public string ReadString()
        {
            int strlen = (int) ReadUInt32();
            if(!CanReadMore(strlen)) throw new IndexOutOfRangeException();
                
            string result = System.Text.Encoding.UTF8.GetString(_buffer, _offset, strlen);
            _offset += strlen;
            
            return result;
        }
        
        public bool ReadBoolean()
        {
            int value = (int) ReadUInt32();
            return value != 0;
        }
    }   
}