namespace Mono.Debugger.Unpack
{
    public static class DebuggerPacketParamsCommandHandlers
    {
        public static void VirtualMachine_GetTypesForSourceFile_0x11(Deserializer deserializer, DebuggerPacket packet)
        {
            // read string and ignore case flag(byte)
            packet.PacketParams.Add();
        }
    }
}
