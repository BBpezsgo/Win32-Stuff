namespace Win32.COM;

[StructLayout(LayoutKind.Sequential)]
public struct BindOptions
{
    readonly DWORD StructSize;

    public DWORD Flags;
    public DWORD Mode;
    public DWORD TickCountDeadline;

    BindOptions(uint structSize) : this() => StructSize = structSize;

    public static unsafe BindOptions Create() => new((uint)sizeof(BindOptions));
}
