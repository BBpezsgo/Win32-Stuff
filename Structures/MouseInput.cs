namespace Win32;

[StructLayout(LayoutKind.Sequential)]
public struct MouseInput
{
    public LONG X;
    public LONG Y;
    public DWORD MouseData;
    public DWORD Flags;
    public DWORD Time;
    public ULONG_PTR ExtraInfo;
}
