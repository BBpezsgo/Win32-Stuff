namespace Win32.MCI;

[StructLayout(LayoutKind.Sequential)]
public struct MCIPlayParams
{
    public DWORD_PTR Callback;
    public DWORD From;
    public DWORD To;
}
