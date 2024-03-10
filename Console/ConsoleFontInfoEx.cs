namespace Win32.Console;

[StructLayout(LayoutKind.Sequential)]
public struct ConsoleFontInfoEx
{
    readonly ULONG StructSize;

    public DWORD FontIndex;
    public SHORT FontWidth;
    public SHORT FontSize;
    public UINT FontFamily;
    public UINT FontWeight;
    public unsafe fixed WCHAR FaceName[32];

    ConsoleFontInfoEx(DWORD structSize) : this() => StructSize = structSize;
    public static ConsoleFontInfoEx Create() => new((DWORD)Marshal.SizeOf<ConsoleFontInfoEx>());
}
