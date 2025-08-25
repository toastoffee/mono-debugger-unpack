namespace Mono.Debugger.Unpack
{
    public enum StackFrameFlags
    {
        FRAME_FLAG_DEBUGGER_INVOKE = 1,

        // Use to allow the debugger to display managed-to-native transitions in stack frames.
        FRAME_FLAG_NATIVE_TRANSITION = 2
    }    
}
