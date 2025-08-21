namespace Mono.Debugger.Unpack
{
    
    public class DebuggerPacketCommand : IEquatable<DebuggerPacketCommand>
    {
        public CommandSet CommandSet;
        public byte CommandId;

        public DebuggerPacketCommand(CommandSet commandSet, byte commandId)
        {
            this.CommandSet = commandSet;
            this.CommandId = commandId;
        }
        
        public override bool Equals(object obj)
            => Equals(obj as DebuggerPacketCommand);

        public bool Equals(DebuggerPacketCommand other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(this, other)) return true;
            return CommandSet == other.CommandSet && CommandId == other.CommandId;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + CommandSet.GetHashCode();
                hash = hash * 31 + CommandId.GetHashCode();
                return hash;
            }
        }
    }
    
    public delegate void PacketParamsHandler(Deserializer deserializer, DebuggerPacket packet);
    
    public static class DebuggerPacketParamsHandlerGetter
    {
        
        private static Dictionary<DebuggerPacketCommand, PacketParamsHandler> _packetCommandHandlers 
            = new Dictionary<DebuggerPacketCommand, PacketParamsHandler>()
            {
                // Virtual Machine Commands
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 1), null},
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 2), null},
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 3), null},
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 4), null},
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 5), null},
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 6), null},
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 7), null},
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 8), null},
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 9), null},
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 10), null},
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 11), DebuggerPacketParamsCommandHandlers.VirtualMachine_GetTypesForSourceFile_0x11},
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 12), null},
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 13), null},
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 14), null},
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 15), null},

                // Events Commands
                {new DebuggerPacketCommand(CommandSet.Events, 0x1), null},
                {new DebuggerPacketCommand(CommandSet.Events, 0x2), null},
                {new DebuggerPacketCommand(CommandSet.Events, 0x3), null},

                // Thread Commands
                {new DebuggerPacketCommand(CommandSet.Threads, 0x1), null},
                {new DebuggerPacketCommand(CommandSet.Threads, 0x2), null},
                {new DebuggerPacketCommand(CommandSet.Threads, 0x3), null},
                {new DebuggerPacketCommand(CommandSet.Threads, 0x4), null},
                {new DebuggerPacketCommand(CommandSet.Threads, 0x5), null},
                {new DebuggerPacketCommand(CommandSet.Threads, 0x6), null},
                {new DebuggerPacketCommand(CommandSet.Threads, 0x7), null},

                // AppDomains Commands
                {new DebuggerPacketCommand(CommandSet.AppDomain, 0x1), null},
                {new DebuggerPacketCommand(CommandSet.AppDomain, 0x2), null},
                {new DebuggerPacketCommand(CommandSet.AppDomain, 0x3), null},
                {new DebuggerPacketCommand(CommandSet.AppDomain, 0x4), null},
                {new DebuggerPacketCommand(CommandSet.AppDomain, 0x5), null},
                {new DebuggerPacketCommand(CommandSet.AppDomain, 0x6), null},
                {new DebuggerPacketCommand(CommandSet.AppDomain, 0x7), null},
                
                // Assembly Commands
                {new DebuggerPacketCommand(CommandSet.Assembly, 0x1), null},
                {new DebuggerPacketCommand(CommandSet.Assembly, 0x2), null},
                {new DebuggerPacketCommand(CommandSet.Assembly, 0x3), null},
                {new DebuggerPacketCommand(CommandSet.Assembly, 0x4), null},
                {new DebuggerPacketCommand(CommandSet.Assembly, 0x5), null},
                {new DebuggerPacketCommand(CommandSet.Assembly, 0x6), null},
                
                // Module Commands
                {new DebuggerPacketCommand(CommandSet.Module, 0x1), null},
                
                // Method Commands
                {new DebuggerPacketCommand(CommandSet.Method, 0x1), null},
                {new DebuggerPacketCommand(CommandSet.Method, 0x2), null},
                {new DebuggerPacketCommand(CommandSet.Method, 0x3), null},
                {new DebuggerPacketCommand(CommandSet.Method, 0x4), null},
                {new DebuggerPacketCommand(CommandSet.Method, 0x5), null},
                {new DebuggerPacketCommand(CommandSet.Method, 0x6), null},
                {new DebuggerPacketCommand(CommandSet.Method, 0x7), null},
                {new DebuggerPacketCommand(CommandSet.Method, 0x8), null},
                {new DebuggerPacketCommand(CommandSet.Method, 0x9), null},
                {new DebuggerPacketCommand(CommandSet.Method, 0x10), null},
                
                // Type Commands
                {new DebuggerPacketCommand(CommandSet.Type, 0x1), null},
                {new DebuggerPacketCommand(CommandSet.Type, 0x2), null},
                {new DebuggerPacketCommand(CommandSet.Type, 0x3), null},
                {new DebuggerPacketCommand(CommandSet.Type, 0x4), null},
                {new DebuggerPacketCommand(CommandSet.Type, 0x5), null},
                {new DebuggerPacketCommand(CommandSet.Type, 0x6), null},
                {new DebuggerPacketCommand(CommandSet.Type, 0x7), null},
                {new DebuggerPacketCommand(CommandSet.Type, 0x8), null},
                {new DebuggerPacketCommand(CommandSet.Type, 0x9), null},
                {new DebuggerPacketCommand(CommandSet.Type, 0x10), null},
                {new DebuggerPacketCommand(CommandSet.Type, 0x11), null},
                {new DebuggerPacketCommand(CommandSet.Type, 0x12), null},
                {new DebuggerPacketCommand(CommandSet.Type, 0x13), null},
                {new DebuggerPacketCommand(CommandSet.Type, 0x14), null},
                
                // StackFrame Commands
                {new DebuggerPacketCommand(CommandSet.StackFrame, 0x1), null},
                {new DebuggerPacketCommand(CommandSet.StackFrame, 0x2), null},
                {new DebuggerPacketCommand(CommandSet.StackFrame, 0x3), null},
                
                // Array Commands
                {new DebuggerPacketCommand(CommandSet.ArrayReference, 0x1), null},
                {new DebuggerPacketCommand(CommandSet.ArrayReference, 0x2), null},
                {new DebuggerPacketCommand(CommandSet.ArrayReference, 0x3), null},
                
                // String Commands
                {new DebuggerPacketCommand(CommandSet.StringReference, 0x1), null},
                {new DebuggerPacketCommand(CommandSet.StringReference, 0x2), null},
                {new DebuggerPacketCommand(CommandSet.StringReference, 0x3), null},
                
                // Object Commands
                {new DebuggerPacketCommand(CommandSet.StringReference, 0x1), null},
                
                // Composite Commands
                {new DebuggerPacketCommand(CommandSet.Events, 100), null},
            };
        
        private static Dictionary<DebuggerPacketCommand, PacketParamsHandler> _packetReplyHandlers 
            = new Dictionary<DebuggerPacketCommand, PacketParamsHandler>()
            {
                // Virtual Machine Commands
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 0x1), null},
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 0x2), null},
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 0x3), null},
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 0x4), null},
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 0x5), null},
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 0x6), null},
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 0x7), null},
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 0x8), null},
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 0x9), null},
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 0x10), null},
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 0x11), null},
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 0x12), null},
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 0x13), null},
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 0x14), null},
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 0x15), null},

                // Events Commands
                {new DebuggerPacketCommand(CommandSet.Events, 0x1), null},
                {new DebuggerPacketCommand(CommandSet.Events, 0x2), null},
                {new DebuggerPacketCommand(CommandSet.Events, 0x3), null},

                // Thread Commands
                {new DebuggerPacketCommand(CommandSet.Threads, 0x1), null},
                {new DebuggerPacketCommand(CommandSet.Threads, 0x2), null},
                {new DebuggerPacketCommand(CommandSet.Threads, 0x3), null},
                {new DebuggerPacketCommand(CommandSet.Threads, 0x4), null},
                {new DebuggerPacketCommand(CommandSet.Threads, 0x5), null},
                {new DebuggerPacketCommand(CommandSet.Threads, 0x6), null},
                {new DebuggerPacketCommand(CommandSet.Threads, 0x7), null},

                // AppDomains Commands
                {new DebuggerPacketCommand(CommandSet.AppDomain, 0x1), null},
                {new DebuggerPacketCommand(CommandSet.AppDomain, 0x2), null},
                {new DebuggerPacketCommand(CommandSet.AppDomain, 0x3), null},
                {new DebuggerPacketCommand(CommandSet.AppDomain, 0x4), null},
                {new DebuggerPacketCommand(CommandSet.AppDomain, 0x5), null},
                {new DebuggerPacketCommand(CommandSet.AppDomain, 0x6), null},
                {new DebuggerPacketCommand(CommandSet.AppDomain, 0x7), null},
                
                // Assembly Commands
                {new DebuggerPacketCommand(CommandSet.Assembly, 0x1), null},
                {new DebuggerPacketCommand(CommandSet.Assembly, 0x2), null},
                {new DebuggerPacketCommand(CommandSet.Assembly, 0x3), null},
                {new DebuggerPacketCommand(CommandSet.Assembly, 0x4), null},
                {new DebuggerPacketCommand(CommandSet.Assembly, 0x5), null},
                {new DebuggerPacketCommand(CommandSet.Assembly, 0x6), null},
                
                // Module Commands
                {new DebuggerPacketCommand(CommandSet.Module, 0x1), null},
                
                // Method Commands
                {new DebuggerPacketCommand(CommandSet.Method, 0x1), null},
                {new DebuggerPacketCommand(CommandSet.Method, 0x2), null},
                {new DebuggerPacketCommand(CommandSet.Method, 0x3), null},
                {new DebuggerPacketCommand(CommandSet.Method, 0x4), null},
                {new DebuggerPacketCommand(CommandSet.Method, 0x5), null},
                {new DebuggerPacketCommand(CommandSet.Method, 0x6), null},
                {new DebuggerPacketCommand(CommandSet.Method, 0x7), null},
                {new DebuggerPacketCommand(CommandSet.Method, 0x8), null},
                {new DebuggerPacketCommand(CommandSet.Method, 0x9), null},
                {new DebuggerPacketCommand(CommandSet.Method, 0x10), null},
                
                // Type Commands
                {new DebuggerPacketCommand(CommandSet.Type, 0x1), null},
                {new DebuggerPacketCommand(CommandSet.Type, 0x2), null},
                {new DebuggerPacketCommand(CommandSet.Type, 0x3), null},
                {new DebuggerPacketCommand(CommandSet.Type, 0x4), null},
                {new DebuggerPacketCommand(CommandSet.Type, 0x5), null},
                {new DebuggerPacketCommand(CommandSet.Type, 0x6), null},
                {new DebuggerPacketCommand(CommandSet.Type, 0x7), null},
                {new DebuggerPacketCommand(CommandSet.Type, 0x8), null},
                {new DebuggerPacketCommand(CommandSet.Type, 0x9), null},
                {new DebuggerPacketCommand(CommandSet.Type, 0x10), null},
                {new DebuggerPacketCommand(CommandSet.Type, 0x11), null},
                {new DebuggerPacketCommand(CommandSet.Type, 0x12), null},
                {new DebuggerPacketCommand(CommandSet.Type, 0x13), null},
                {new DebuggerPacketCommand(CommandSet.Type, 0x14), null},
                
                // StackFrame Commands
                {new DebuggerPacketCommand(CommandSet.StackFrame, 0x1), null},
                {new DebuggerPacketCommand(CommandSet.StackFrame, 0x2), null},
                {new DebuggerPacketCommand(CommandSet.StackFrame, 0x3), null},
                
                // Array Commands
                {new DebuggerPacketCommand(CommandSet.ArrayReference, 0x1), null},
                {new DebuggerPacketCommand(CommandSet.ArrayReference, 0x2), null},
                {new DebuggerPacketCommand(CommandSet.ArrayReference, 0x3), null},
                
                // String Commands
                {new DebuggerPacketCommand(CommandSet.StringReference, 0x1), null},
                {new DebuggerPacketCommand(CommandSet.StringReference, 0x2), null},
                {new DebuggerPacketCommand(CommandSet.StringReference, 0x3), null},
                
                // Object Commands
                {new DebuggerPacketCommand(CommandSet.StringReference, 0x1), null},
                
                // Composite Commands
                {new DebuggerPacketCommand(CommandSet.Events, 100), null},
            };

        public static PacketParamsHandler GetPacketParamsHandler(CommandSet commandSet, byte commandId, DebuggerPacketType type)
        {
            if (type == DebuggerPacketType.Command)
            {
                return GetPacketParamsCommandHandler(commandSet, commandId);
            }
            else if(type == DebuggerPacketType.Reply)
            {
                return GetPacketParamsReplyHandler(commandSet, commandId);
            }
            else
            {
                return null;
            }
        }
        
        private static PacketParamsHandler GetPacketParamsCommandHandler(CommandSet commandSet, byte commandId)
        {
            return _packetCommandHandlers[new DebuggerPacketCommand(commandSet, commandId)];
        }

        private static PacketParamsHandler GetPacketParamsReplyHandler(CommandSet commandSet, byte commandId)
        {
            return _packetReplyHandlers[new DebuggerPacketCommand(commandSet, commandId)];
        }
        
        
    }   
}