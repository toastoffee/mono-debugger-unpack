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
            
            public void ReadParamFrom(DebuggerPacketParamType paramType)
            {
                switch (paramType)
                {
                    case DebuggerPacketParamType.Byte:
                        _packet.PacketParams.Add(DebuggerPacketParam.MakeValue(_deserializer.ReadByte(), DebuggerPacketParamType.Byte));
                        break;
                    
                    case DebuggerPacketParamType.UInt16:
                        _packet.PacketParams.Add(DebuggerPacketParam.MakeValue(_deserializer.ReadUInt16(), DebuggerPacketParamType.UInt16));
                        break;
                    
                    case DebuggerPacketParamType.UInt32:
                        _packet.PacketParams.Add(DebuggerPacketParam.MakeValue(_deserializer.ReadUInt32(), DebuggerPacketParamType.UInt32));
                        break;
                    
                    case DebuggerPacketParamType.UInt64:
                        _packet.PacketParams.Add(DebuggerPacketParam.MakeValue(_deserializer.ReadUInt64(), DebuggerPacketParamType.UInt64));
                        break;
                    
                    case DebuggerPacketParamType.Id:
                        _packet.PacketParams.Add(DebuggerPacketParam.MakeValue(_deserializer.ReadUInt32(), DebuggerPacketParamType.UInt32));
                        break;
                    
                    case DebuggerPacketParamType.String:
                        _packet.PacketParams.Add(DebuggerPacketParam.MakeValue(_deserializer.ReadString(), DebuggerPacketParamType.String));
                        break;
                    
                    case DebuggerPacketParamType.Variant:
                        _packet.PacketParams.Add(DebuggerPacketParam.MakeValue(_deserializer.ReadByte(), DebuggerPacketParamType.Variant));
                        break;
                    
                    case DebuggerPacketParamType.Boolean:
                        _packet.PacketParams.Add(DebuggerPacketParam.MakeValue(_deserializer.ReadBoolean(), DebuggerPacketParamType.Boolean));
                        break;
                }
            }

        }
        
        
        
        public static void VirtualMachine_GetTypesForSourceFile_0x11(Deserializer deserializer, DebuggerPacket packet)
        {
            // read string and ignore case flag(byte)
            ParamReader paramReader = new ParamReader(deserializer, packet);
            paramReader.ReadParamFrom(DebuggerPacketParamType.String);
            paramReader.ReadParamFrom(DebuggerPacketParamType.Byte);
        }
    }
}
