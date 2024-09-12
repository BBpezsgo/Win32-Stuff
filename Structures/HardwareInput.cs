namespace Win32;

[StructLayout(LayoutKind.Sequential)]
public struct HardwareInput
{
    public DWORD Msg;
    public WORD LParam;
    public WORD HParam;
}
