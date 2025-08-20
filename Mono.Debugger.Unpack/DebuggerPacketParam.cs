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

        private static DebuggerPacketParam MakeValue(Object value, DebuggerPacketParamType type)
        {
            DebuggerPacketParam param = new DebuggerPacketParam();
            
            param.Value = value;
            param.Type = type;

            return param;
        }
        
        public static DebuggerPacketParam MakeString(String value)
        {
            return MakeValue(value, DebuggerPacketParamType.String);
        }

        public static DebuggerPacketParam MakeByte(byte value)
        {
            return MakeValue(value, DebuggerPacketParamType.Byte);
        }
    }   
}