namespace Win32.Gdi32;

[StructLayout(LayoutKind.Sequential)]
public struct PaintStruct
{
    public HDC DCHandle;
    public bool Erase;
    public RECT Paint;
    public bool Restore;
    public bool IncUpdate;
    readonly int Reserved;
}
