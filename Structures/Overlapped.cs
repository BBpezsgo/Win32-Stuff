namespace Win32;

[StructLayout(LayoutKind.Sequential)]
public struct Overlapped
{
    [StructLayout(LayoutKind.Explicit)]
    public struct DUMMYUNION
    {
        [FieldOffset(0)] public DWORD Offset;
        [FieldOffset(0)] public DWORD OffsetHigh;
        [FieldOffset(0)] public IntPtr Pointer;
    }

    public ULONG_PTR Internal;
    public ULONG_PTR InternalHigh;
    public DUMMYUNION DUMMYUNIONNAME;
    public HANDLE EventHandle;
}
