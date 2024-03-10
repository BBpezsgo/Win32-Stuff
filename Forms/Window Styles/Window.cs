namespace Win32.Forms;

public static partial class WindowStyles
{
    public const uint OVERLAPPED = 0x00000000;
    public const uint POPUP = 0x80000000;
    public const uint CHILD = 0x40000000;
    public const uint MINIMIZE = 0x20000000;
    public const uint VISIBLE = 0x10000000;
    public const uint DISABLED = 0x08000000;
    public const uint CLIPSIBLINGS = 0x04000000;
    public const uint CLIPCHILDREN = 0x02000000;
    public const uint MAXIMIZE = 0x01000000;
    public const uint CAPTION = 0x00C00000; /* WS_BORDER | WS_DLGFRAME  */
    public const uint BORDER = 0x00800000;
    public const uint DLGFRAME = 0x00400000;
    public const uint VSCROLL = 0x00200000;
    public const uint HSCROLL = 0x00100000;
    public const uint SYSMENU = 0x00080000;
    public const uint THICKFRAME = 0x00040000;
    public const uint GROUP = 0x00020000;
    public const uint TABSTOP = 0x00010000;

    public const uint MINIMIZEBOX = 0x00020000;
    public const uint MAXIMIZEBOX = 0x00010000;

    public const uint TILED = OVERLAPPED;
    public const uint ICONIC = MINIMIZE;
    public const uint SIZEBOX = THICKFRAME;
    public const uint TILEDWINDOW = OVERLAPPEDWINDOW;
}

// Common Window Styles
public static partial class WindowStyles
{
    public const uint OVERLAPPEDWINDOW = OVERLAPPED | CAPTION | SYSMENU | THICKFRAME | MINIMIZEBOX | MAXIMIZEBOX;

    public const uint POPUPWINDOW = POPUP | BORDER | SYSMENU;

    public const uint CHILDWINDOW = CHILD;
}

// Extended Window Styles
public static class WindowStylesEx
{
    public const uint DLGMODALFRAME = 0x00000001;
    public const uint NOPARENTNOTIFY = 0x00000004;
    public const uint TOPMOST = 0x00000008;
    public const uint ACCEPTFILES = 0x00000010;
    public const uint TRANSPARENT = 0x00000020;
    public const uint MDICHILD = 0x00000040;
    public const uint TOOLWINDOW = 0x00000080;
    public const uint WINDOWEDGE = 0x00000100;
    public const uint CLIENTEDGE = 0x00000200;
    public const uint CONTEXTHELP = 0x00000400;
    public const uint RIGHT = 0x00001000;
    public const uint LEFT = 0x00000000;
    public const uint RTLREADING = 0x00002000;
    public const uint LTRREADING = 0x00000000;
    public const uint LEFTSCROLLBAR = 0x00004000;
    public const uint RIGHTSCROLLBAR = 0x00000000;
    public const uint CONTROLPARENT = 0x00010000;
    public const uint STATICEDGE = 0x00020000;
    public const uint APPWINDOW = 0x00040000;
    public const uint OVERLAPPEDWINDOW = WINDOWEDGE | CLIENTEDGE;
    public const uint PALETTEWINDOW = WINDOWEDGE | TOOLWINDOW | TOPMOST;
    public const uint LAYERED = 0x00080000;
    /// <summary>
    /// Disable inheritance of mirroring by children
    /// </summary>
    public const uint NOINHERITLAYOUT = 0x00100000;
    public const uint NOREDIRECTIONBITMAP = 0x00200000;
    /// <summary>
    /// Right to left mirroring
    /// </summary>
    public const uint LAYOUTRTL = 0x00400000;
    public const uint COMPOSITED = 0x02000000;
    public const uint NOACTIVATE = 0x08000000;
}
