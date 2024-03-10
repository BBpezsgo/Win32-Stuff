namespace Win32.Forms;

#pragma warning disable CA1707 // Identifiers should not contain underscores
public static class ScrollBarControlMessage
{
    public const uint SETPOS = 0x00E0;
    public const uint GETPOS = 0x00E1;
    public const uint SETRANGE = 0x00E2;
    public const uint SETRANGEREDRAW = 0x00E6;
    public const uint GETRANGE = 0x00E3;
    public const uint ENABLE_ARROWS = 0x00E4;
    public const uint SETSCROLLINFO = 0x00E9;
    public const uint GETSCROLLINFO = 0x00EA;
    public const uint GETSCROLLBARINFO = 0x00EB;
}

public static class SIF
{
    public const uint RANGE = 0x0001;
    public const uint PAGE = 0x0002;
    public const uint POS = 0x0004;
    public const uint DISABLENOSCROLL = 0x0008;
    public const uint TRACKPOS = 0x0010;
    public const uint ALL = RANGE | PAGE | POS | TRACKPOS;
}
