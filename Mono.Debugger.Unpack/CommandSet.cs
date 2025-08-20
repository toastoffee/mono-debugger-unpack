namespace Mono.Debugger.Unpack
{
    public enum CommandSet
    {
        VirtualMachine = 1,
        ObjectReference = 2,
        StringReference = 10,
        Threads = 11,
        ArrayReference = 12,
        EventRequest = 15,
        StackFrame = 16,
        AppDomain = 20,
        Assembly = 21,
        Method = 22,
        Type = 23,
        Module = 24,
        Events = 64
    }   
}