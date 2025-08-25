namespace Mono.Debugger.Unpack
{
    public enum ModifierKind
    {
        MOD_KIND_COUNT = 1,
        MOD_KIND_THREAD_ONLY = 3,
        MOD_KIND_LOCATION_ONLY = 7,
        MOD_KIND_EXCEPTION_ONLY = 8,
        MOD_KIND_STEP = 10,
        MOD_KIND_ASSEMBLY_ONLY = 11,
        MOD_KIND_SOURCE_FILE_ONLY = 12,
        MOD_KIND_TYPE_NAME_ONLY = 13,
        MOD_KIND_NONE = 14
    }   
}