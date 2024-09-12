namespace Win32;

#pragma warning disable CA1034 // Nested types should not be visible
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
