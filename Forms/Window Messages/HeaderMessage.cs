namespace Win32.Forms;

public static class HeaderMessage
{
    const uint First = 0x1200;

    public const uint GETITEMCOUNT = First + 0;
    public const uint INSERTITEMA = First + 1;
    public const uint INSERTITEMW = First + 10;
    public const uint INSERTITEM = INSERTITEMW;
    public const uint DELETEITEM = First + 2;
    public const uint GETITEMA = First + 3;
    public const uint GETITEMW = First + 11;
    public const uint GETITEM = GETITEMW;
    public const uint SETITEMA = First + 4;
    public const uint SETITEMW = First + 12;
    public const uint SETITEM = SETITEMW;
    public const uint LAYOUT = First + 5;
}
