namespace Win32.Forms;

#pragma warning disable CA1707 // Identifiers should not contain underscores
public static class ProgressBarControlMessage
{
    public const uint SMOOTH = 0x01;
    public const uint VERTICAL = 0x04;

    public const uint SETRANGE = WindowMessage.WM_USER + 1;
    public const uint SETPOS = WindowMessage.WM_USER + 2;
    public const uint DELTAPOS = WindowMessage.WM_USER + 3;
    public const uint SETSTEP = WindowMessage.WM_USER + 4;
    public const uint STEPIT = WindowMessage.WM_USER + 5;
    public const uint SETRANGE32 = WindowMessage.WM_USER + 6; // lParam = high, wParam = low

    public const uint GETRANGE = WindowMessage.WM_USER + 7; // wParam = return (TRUE ? low : high). lParam = PPBRANGE or NULL
    public const uint GETPOS = WindowMessage.WM_USER + 8;
    public const uint SETBARCOLOR = WindowMessage.WM_USER + 9; // lParam = bar color
    public const uint SETBKCOLOR = CommonControlMessages.SETBKCOLOR; // lParam = bkColor

    public const uint MARQUEE = 0x08;

    public const uint SETMARQUEE = WindowMessage.WM_USER + 10;

    public const uint SMOOTHREVERSE = 0x10;

    public const uint GETSTEP = WindowMessage.WM_USER + 13;
    public const uint GETBKCOLOR = WindowMessage.WM_USER + 14;
    public const uint GETBARCOLOR = WindowMessage.WM_USER + 15;
    public const uint SETSTATE = WindowMessage.WM_USER + 16; // wParam = ST_[State] (NORMAL, ERROR, PAUSED)
    public const uint GETSTATE = WindowMessage.WM_USER + 17;

    public const uint ST_NORMAL = 0x0001;
    public const uint ST_ERROR = 0x0002;
    public const uint ST_PAUSED = 0x0003;
}
