namespace Win32.Console;

[StructLayout(LayoutKind.Sequential)]
public struct ConsoleFontInfo
{
    public DWORD FontIndex;
    public COORD FontSize;
}
