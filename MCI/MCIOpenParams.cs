namespace Win32.MCI;

[StructLayout(LayoutKind.Sequential)]
public struct MCIOpenParams
{
    public DWORD_PTR Callback;
    public MCIDEVICEID DeviceID;
    public unsafe WCHAR* DeviceType;
    public unsafe WCHAR* ElementName;
    public unsafe WCHAR* Alias;
}
