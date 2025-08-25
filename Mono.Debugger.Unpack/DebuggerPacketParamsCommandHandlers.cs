namespace Mono.Debugger.Unpack
{
    public static class DebuggerPacketParamsCommandHandlers
    {

        private class ParamReader
        {
            private DebuggerPacket _packet;
            private Deserializer _deserializer;
            
            public ParamReader(Deserializer deserializer, DebuggerPacket packet)
            {
                this._packet = packet;
                this._deserializer = deserializer;
            }
            
            public DebuggerPacketParam ReadParamFrom(DebuggerPacketParamType paramType)
            {
                DebuggerPacketParam param;
                switch (paramType)
                {
                    case DebuggerPacketParamType.Byte:
                        param = DebuggerPacketParam.MakeValue(_deserializer.ReadByte(), DebuggerPacketParamType.Byte);
                        break;
                    
                    case DebuggerPacketParamType.UInt16:
                        param = DebuggerPacketParam.MakeValue(_deserializer.ReadUInt16(), DebuggerPacketParamType.UInt16);
                        break;
                    
                    case DebuggerPacketParamType.UInt32:
                        param = DebuggerPacketParam.MakeValue(_deserializer.ReadUInt32(), DebuggerPacketParamType.UInt32);
                        break;
                    
                    case DebuggerPacketParamType.UInt64:
                        param = DebuggerPacketParam.MakeValue(_deserializer.ReadUInt64(), DebuggerPacketParamType.UInt64);
                        break;
                    
                    case DebuggerPacketParamType.Id:
                        param = DebuggerPacketParam.MakeValue(_deserializer.ReadUInt32(), DebuggerPacketParamType.UInt32);
                        break;
                    
                    case DebuggerPacketParamType.String:
                        param = DebuggerPacketParam.MakeValue(_deserializer.ReadString(), DebuggerPacketParamType.String);
                        break;
                    
                    case DebuggerPacketParamType.Variant:
                        param = DebuggerPacketParam.MakeValue(_deserializer.ReadByte(), DebuggerPacketParamType.Variant);
                        break;
                    
                    case DebuggerPacketParamType.Boolean:
                        param = DebuggerPacketParam.MakeValue(_deserializer.ReadBoolean(), DebuggerPacketParamType.Boolean);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                _packet.PacketParams.Add(param);
                return param;
            }

        }
        
        
        
        public static void VirtualMachine_GetTypesForSourceFile_11(Deserializer deserializer, DebuggerPacket packet)
        {
            // read string and ignore case flag(byte)
            ParamReader paramReader = new ParamReader(deserializer, packet);
            paramReader.ReadParamFrom(DebuggerPacketParamType.String);
            paramReader.ReadParamFrom(DebuggerPacketParamType.Byte);
        }
        
        public static void EventRequest_RequestClear_1(Deserializer deserializer, DebuggerPacket packet)
        {
            // event kind , suspended policy, modifiers count
            ParamReader paramReader = new ParamReader(deserializer, packet);
            paramReader.ReadParamFrom(DebuggerPacketParamType.Byte);
            paramReader.ReadParamFrom(DebuggerPacketParamType.Byte);
            var modifiers = paramReader.ReadParamFrom(DebuggerPacketParamType.Byte);
            var modifiersCount = modifiers.GetValue<byte>();
        }
        
        
        public static void EventRequest_RequestClear_2(Deserializer deserializer, DebuggerPacket packet)
        {
            // event Type and request id
            ParamReader paramReader = new ParamReader(deserializer, packet);
            paramReader.ReadParamFrom(DebuggerPacketParamType.Byte);
            paramReader.ReadParamFrom(DebuggerPacketParamType.UInt32);
        }
    }
}
