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
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 11), DebuggerPacketParamsCommandHandlers.VirtualMachine_GetTypesForSourceFile_11},
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 12), null},
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 13), null},
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 14), null},
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 15), null},

                // Events Commands
                {new DebuggerPacketCommand(CommandSet.EventRequest, 1), null},
                {new DebuggerPacketCommand(CommandSet.EventRequest, 2), DebuggerPacketParamsCommandHandlers.EventRequest_RequestClear_2},
                {new DebuggerPacketCommand(CommandSet.EventRequest, 3), null},

                // Thread Commands
                {new DebuggerPacketCommand(CommandSet.Threads, 1), null},
                {new DebuggerPacketCommand(CommandSet.Threads, 2), null},
                {new DebuggerPacketCommand(CommandSet.Threads, 3), null},
                {new DebuggerPacketCommand(CommandSet.Threads, 4), null},
                {new DebuggerPacketCommand(CommandSet.Threads, 5), null},
                {new DebuggerPacketCommand(CommandSet.Threads, 6), null},
                {new DebuggerPacketCommand(CommandSet.Threads, 7), null},

                // AppDomains Commands
                {new DebuggerPacketCommand(CommandSet.AppDomain, 1), null},
                {new DebuggerPacketCommand(CommandSet.AppDomain, 2), null},
                {new DebuggerPacketCommand(CommandSet.AppDomain, 3), null},
                {new DebuggerPacketCommand(CommandSet.AppDomain, 4), null},
                {new DebuggerPacketCommand(CommandSet.AppDomain, 5), null},
                {new DebuggerPacketCommand(CommandSet.AppDomain, 6), null},
                {new DebuggerPacketCommand(CommandSet.AppDomain, 7), null},
                
                // Assembly Commands
                {new DebuggerPacketCommand(CommandSet.Assembly, 1), null},
                {new DebuggerPacketCommand(CommandSet.Assembly, 2), null},
                {new DebuggerPacketCommand(CommandSet.Assembly, 3), null},
                {new DebuggerPacketCommand(CommandSet.Assembly, 4), null},
                {new DebuggerPacketCommand(CommandSet.Assembly, 5), null},
                {new DebuggerPacketCommand(CommandSet.Assembly, 6), null},
                
                // Module Commands
                {new DebuggerPacketCommand(CommandSet.Module, 1), null},
                
                // Method Commands
                {new DebuggerPacketCommand(CommandSet.Method, 1), null},
                {new DebuggerPacketCommand(CommandSet.Method, 2), null},
                {new DebuggerPacketCommand(CommandSet.Method, 3), null},
                {new DebuggerPacketCommand(CommandSet.Method, 4), null},
                {new DebuggerPacketCommand(CommandSet.Method, 5), null},
                {new DebuggerPacketCommand(CommandSet.Method, 6), null},
                {new DebuggerPacketCommand(CommandSet.Method, 7), null},
                {new DebuggerPacketCommand(CommandSet.Method, 8), null},
                {new DebuggerPacketCommand(CommandSet.Method, 9), null},
                {new DebuggerPacketCommand(CommandSet.Method, 10), null},
                
                // Type Commands
                {new DebuggerPacketCommand(CommandSet.Type, 1), null},
                {new DebuggerPacketCommand(CommandSet.Type, 2), null},
                {new DebuggerPacketCommand(CommandSet.Type, 3), null},
                {new DebuggerPacketCommand(CommandSet.Type, 4), null},
                {new DebuggerPacketCommand(CommandSet.Type, 5), null},
                {new DebuggerPacketCommand(CommandSet.Type, 6), null},
                {new DebuggerPacketCommand(CommandSet.Type, 7), null},
                {new DebuggerPacketCommand(CommandSet.Type, 8), null},
                {new DebuggerPacketCommand(CommandSet.Type, 9), null},
                {new DebuggerPacketCommand(CommandSet.Type, 10), null},
                {new DebuggerPacketCommand(CommandSet.Type, 11), null},
                {new DebuggerPacketCommand(CommandSet.Type, 12), null},
                {new DebuggerPacketCommand(CommandSet.Type, 13), null},
                {new DebuggerPacketCommand(CommandSet.Type, 14), null},
                
                // StackFrame Commands
                {new DebuggerPacketCommand(CommandSet.StackFrame, 1), null},
                {new DebuggerPacketCommand(CommandSet.StackFrame, 2), null},
                {new DebuggerPacketCommand(CommandSet.StackFrame, 3), null},
                
                // Array Commands
                {new DebuggerPacketCommand(CommandSet.ArrayReference, 1), null},
                {new DebuggerPacketCommand(CommandSet.ArrayReference, 2), null},
                {new DebuggerPacketCommand(CommandSet.ArrayReference, 3), null},
                
                // String Commands
                {new DebuggerPacketCommand(CommandSet.StringReference, 1), null},
                {new DebuggerPacketCommand(CommandSet.StringReference, 2), null},
                {new DebuggerPacketCommand(CommandSet.StringReference, 3), null},
                
                // Object Commands
                {new DebuggerPacketCommand(CommandSet.ObjectReference, 1), null},
                
                // Composite Commands
                {new DebuggerPacketCommand(CommandSet.Events, 100), null},
            };
        
        private static Dictionary<DebuggerPacketCommand, PacketParamsHandler> _packetReplyHandlers 
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
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 11), null},
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 12), null},
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 13), null},
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 14), null},
                {new DebuggerPacketCommand(CommandSet.VirtualMachine, 15), null},

                // Events Commands
                {new DebuggerPacketCommand(CommandSet.Events, 1), null},
                {new DebuggerPacketCommand(CommandSet.Events, 2), null},
                {new DebuggerPacketCommand(CommandSet.Events, 3), null},

                // Thread Commands
                {new DebuggerPacketCommand(CommandSet.Threads, 1), null},
                {new DebuggerPacketCommand(CommandSet.Threads, 2), null},
                {new DebuggerPacketCommand(CommandSet.Threads, 3), null},
                {new DebuggerPacketCommand(CommandSet.Threads, 4), null},
                {new DebuggerPacketCommand(CommandSet.Threads, 5), null},
                {new DebuggerPacketCommand(CommandSet.Threads, 6), null},
                {new DebuggerPacketCommand(CommandSet.Threads, 7), null},

                // AppDomains Commands
                {new DebuggerPacketCommand(CommandSet.AppDomain, 1), null},
                {new DebuggerPacketCommand(CommandSet.AppDomain, 2), null},
                {new DebuggerPacketCommand(CommandSet.AppDomain, 3), null},
                {new DebuggerPacketCommand(CommandSet.AppDomain, 4), null},
                {new DebuggerPacketCommand(CommandSet.AppDomain, 5), null},
                {new DebuggerPacketCommand(CommandSet.AppDomain, 6), null},
                {new DebuggerPacketCommand(CommandSet.AppDomain, 7), null},
                
                // Assembly Commands
                {new DebuggerPacketCommand(CommandSet.Assembly, 1), null},
                {new DebuggerPacketCommand(CommandSet.Assembly, 2), null},
                {new DebuggerPacketCommand(CommandSet.Assembly, 3), null},
                {new DebuggerPacketCommand(CommandSet.Assembly, 4), null},
                {new DebuggerPacketCommand(CommandSet.Assembly, 5), null},
                {new DebuggerPacketCommand(CommandSet.Assembly, 6), null},
                
                // Module Commands
                {new DebuggerPacketCommand(CommandSet.Module, 1), null},
                
                // Method Commands
                {new DebuggerPacketCommand(CommandSet.Method, 1), null},
                {new DebuggerPacketCommand(CommandSet.Method, 2), null},
                {new DebuggerPacketCommand(CommandSet.Method, 3), null},
                {new DebuggerPacketCommand(CommandSet.Method, 4), null},
                {new DebuggerPacketCommand(CommandSet.Method, 5), null},
                {new DebuggerPacketCommand(CommandSet.Method, 6), null},
                {new DebuggerPacketCommand(CommandSet.Method, 7), null},
                {new DebuggerPacketCommand(CommandSet.Method, 8), null},
                {new DebuggerPacketCommand(CommandSet.Method, 9), null},
                {new DebuggerPacketCommand(CommandSet.Method, 10), null},
                
                // Type Commands
                {new DebuggerPacketCommand(CommandSet.Type, 1), null},
                {new DebuggerPacketCommand(CommandSet.Type, 2), null},
                {new DebuggerPacketCommand(CommandSet.Type, 3), null},
                {new DebuggerPacketCommand(CommandSet.Type, 4), null},
                {new DebuggerPacketCommand(CommandSet.Type, 5), null},
                {new DebuggerPacketCommand(CommandSet.Type, 6), null},
                {new DebuggerPacketCommand(CommandSet.Type, 7), null},
                {new DebuggerPacketCommand(CommandSet.Type, 8), null},
                {new DebuggerPacketCommand(CommandSet.Type, 9), null},
                {new DebuggerPacketCommand(CommandSet.Type, 10), null},
                {new DebuggerPacketCommand(CommandSet.Type, 11), null},
                {new DebuggerPacketCommand(CommandSet.Type, 12), null},
                {new DebuggerPacketCommand(CommandSet.Type, 13), null},
                {new DebuggerPacketCommand(CommandSet.Type, 14), null},
                
                // StackFrame Commands
                {new DebuggerPacketCommand(CommandSet.StackFrame, 1), null},
                {new DebuggerPacketCommand(CommandSet.StackFrame, 2), null},
                {new DebuggerPacketCommand(CommandSet.StackFrame, 3), null},
                
                // Array Commands
                {new DebuggerPacketCommand(CommandSet.ArrayReference, 1), null},
                {new DebuggerPacketCommand(CommandSet.ArrayReference, 2), null},
                {new DebuggerPacketCommand(CommandSet.ArrayReference, 3), null},
                
                // String Commands
                {new DebuggerPacketCommand(CommandSet.StringReference, 1), null},
                {new DebuggerPacketCommand(CommandSet.StringReference, 2), null},
                {new DebuggerPacketCommand(CommandSet.StringReference, 3), null},
                
                // Object Commands
                {new DebuggerPacketCommand(CommandSet.ObjectReference, 1), null},
                
                // Composite Commands
                {new DebuggerPacketCommand(CommandSet.Events, 100), null},
            };

        public static PacketParamsHandler? GetPacketParamsHandler(CommandSet commandSet, byte commandId, DebuggerPacketType type)
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
        
        private static PacketParamsHandler? GetPacketParamsCommandHandler(CommandSet commandSet, byte commandId)
        {
            _packetCommandHandlers.TryGetValue(new DebuggerPacketCommand(commandSet, commandId), out var value);
            return value;
        }

        private static PacketParamsHandler? GetPacketParamsReplyHandler(CommandSet commandSet, byte commandId)
        {
            _packetReplyHandlers.TryGetValue(new DebuggerPacketCommand(commandSet, commandId), out var value);
            return value;
        }
        
        
    }   
}