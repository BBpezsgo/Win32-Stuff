namespace Win32.COM;

[StructLayout(LayoutKind.Sequential)]
public struct BindOptions
{
    readonly DWORD StructSize;

    public DWORD Flags;
    public DWORD Mode;
    public DWORD TickCountDeadline;

    BindOptions(uint cbStruct) : this() => this.StructSize = cbStruct;

    public static unsafe BindOptions Create() => new((uint)sizeof(BindOptions));
}
