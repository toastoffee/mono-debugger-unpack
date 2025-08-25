
namespace Mono.Debugger.Unpack
{
    using System.Text;
    
    public enum DebuggerPacketType
    {
        Command,
        Reply,
        HandShake
    }
    
    public class DebuggerPacket
    {
        public DebuggerPacketType PacketType { get; private set; }
        public uint Length { get; private set; }
        public uint Id { get; private set; }
        
        public CommandSet CmdSet { get; private set; }
        public byte Cmd { get; private set; }
        
        public ErrorCode ErrCode { get; private set; }

        public List<DebuggerPacketParam> PacketParams { get; private set; } = new List<DebuggerPacketParam>();


        public static DebuggerPacket ConvertFrom(byte[] buffer, int bufferSize)
        {
            if(bufferSize < 11) throw new Exception("Invalid buffer size");
            
            if (bufferSize == 13)
            {
                string dataStr = Encoding.UTF8.GetString(buffer, 0, 13);

                if (dataStr == "DWP-Handshake")
                {
                    DebuggerPacket handShakePacket = new DebuggerPacket();
                    handShakePacket.PacketType = DebuggerPacketType.HandShake;
                    return handShakePacket;
                }
            }
            
            var deserializer = new Deserializer(buffer, bufferSize);

            DebuggerPacket packet = new DebuggerPacket();
            packet.Length =  deserializer.ReadUInt32();
            packet.Id = deserializer.ReadUInt32();
            packet.PacketType = deserializer.ReadByte() == 0x80 ? DebuggerPacketType.Reply : DebuggerPacketType.Command;

            if (packet.PacketType == DebuggerPacketType.Command)
            {
                packet.CmdSet = (CommandSet) deserializer.ReadByte();
                packet.Cmd = deserializer.ReadByte();
            }
            else
            {
                packet.ErrCode = (ErrorCode) deserializer.ReadUInt16();
            }
            
            PacketParamsHandler? packetParamsHandler;

            packetParamsHandler = DebuggerPacketParamsHandlerGetter.GetPacketParamsHandler(packet.CmdSet, packet.Cmd, packet.PacketType);

            if (packetParamsHandler != null)
            {
                packetParamsHandler.Invoke(deserializer, packet);
            }

            return packet;
        }


        public void LogPacket()
        {
            if (PacketType == DebuggerPacketType.HandShake)
            {
                Console.WriteLine($"[{PacketType.ToString()}]");
            }
            else
            {

                if (PacketType == DebuggerPacketType.Command)
                {
                    Console.WriteLine($"[{PacketType.ToString()}] [Length]{Length} [id]{Id} [CommandSet]{CmdSet.ToString()} [CommandId]{Cmd}");
                }
                else
                {
                    Console.WriteLine($"[{PacketType.ToString()}] [Length]{Length} [id]{Id} [ErrorCode]{ErrCode.ToString()}");
                }
            }


            for (int i = 0; i < PacketParams.Count; i++)
            {
                Console.Write($"[param_{i+1}] ");
                PacketParams[i].LogParam();
                Console.Write($"\n");
            }
            Console.Write("\n");
        }
    }    
}
