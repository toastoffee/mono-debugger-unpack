using System.Diagnostics;

namespace Mono.Debugger.Unpack
{
    public enum DebuggerPacketParamType
    {
        Byte,
        UInt16,
        UInt32,
        UInt64,
        Id,
        String,
        Variant,
        Boolean
    }
    
    public class DebuggerPacketParam
    {
        public DebuggerPacketParamType Type { get; set; }
        private Object Value {get; set;}
        
        public T GetValue<T>() => Value is T t ? t : throw new InvalidCastException();

        public static DebuggerPacketParam MakeValue(Object value, DebuggerPacketParamType type)
        {
            DebuggerPacketParam param = new DebuggerPacketParam();
            
            param.Value = value;
            param.Type = type;

            return param;
        }

        public void LogParam()
        {
            switch (Type)
            {
                case DebuggerPacketParamType.Byte:
                    Console.Write($"[{Type.ToString()}] {(byte)Value}");
                    break;
                case DebuggerPacketParamType.UInt16:
                    Console.Write($"[{Type.ToString()}] {(UInt16)Value}");
                    break;
                case DebuggerPacketParamType.UInt32:
                    Console.Write($"[{Type.ToString()}] {(UInt32)Value}");
                    break;
                case DebuggerPacketParamType.UInt64:
                    Console.Write($"[{Type.ToString()}] {(UInt64)Value}");
                    break;
                case DebuggerPacketParamType.String:
                    Console.Write($"[{Type.ToString()}] {(String)Value}");
                    break;
                case DebuggerPacketParamType.Variant:
                    Console.Write($"[{Type.ToString()}] {(byte)Value}");
                    break;
                case DebuggerPacketParamType.Boolean:
                    Console.Write($"[{Type.ToString()}] {(Boolean)Value}");
                    break;
            }
        }
        
    }   
}