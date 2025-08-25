namespace Mono.Debugger.Unpack
{
    public enum InvokeFlags
    {
        INVOKE_FLAG_DISABLE_BREAKPOINTS = 1,
        INVOKE_FLAG_SINGLE_THREADED = 2,

        // Allow for returning the changed value types after an invocation
        INVOKE_FLAG_RETURN_OUT_THIS = 4,

        // Allows the return of modified value types after invocation
        INVOKE_FLAG_RETURN_OUT_ARGS = 8,

        // Performs a virtual method invocation
        INVOKE_FLAG_VIRTUAL = 16
    }    
}
