namespace Win32;

[StructLayout(LayoutKind.Sequential)]
public struct KeyboardInput
{
    public WORD VirtualKey;
    public WORD Scan;
    public DWORD Flags;
    public DWORD Time;
    public ULONG_PTR ExtraInfo;
}
