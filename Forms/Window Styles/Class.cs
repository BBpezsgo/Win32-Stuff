namespace Win32.Forms;

public static class ClassStyles
{
    public const uint VREDRAW = 0x0001;
    public const uint HREDRAW = 0x0002;
    public const uint DBLCLKS = 0x0008;
    public const uint OWNDC = 0x0020;
    public const uint CLASSDC = 0x0040;
    public const uint PARENTDC = 0x0080;
    public const uint NOCLOSE = 0x0200;
    public const uint SAVEBITS = 0x0800;
    public const uint BYTEALIGNCLIENT = 0x1000;
    public const uint BYTEALIGNWINDOW = 0x2000;
    public const uint GLOBALCLASS = 0x4000;

    public const uint IME = 0x00010000;

    // if (_WIN32_WINNT >= 0x0501)
    public const uint DROPSHADOW = 0x00020000;
}
