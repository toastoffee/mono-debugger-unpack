namespace Mono.Debugger.Unpack
{
    public enum ErrorCode
    {
        Success = 0,
        InvalidObject = 20,
        InvalidFieldId = 25,
        InvalidFrameId = 30,
        NotImplemented = 100,
        NotSuspended = 101,
        InvalidArgument = 102,
        Unloaded = 103,
        NoInvocation = 104,
        AbsentInformation = 105,
        NoSeqPointAtIlOffset = 106,
    }
}