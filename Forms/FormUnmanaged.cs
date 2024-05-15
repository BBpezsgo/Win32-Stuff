namespace Win32.Forms;

[SupportedOSPlatform("windows")]
public class FormUnmanaged : Window
{
    /// <exception cref="WindowsException"/>
    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public Menu? Menu
    {
        get
        {
            HMENU menuHandle = User32.GetMenu(Handle);
            if (menuHandle == HMENU.Zero)
            { return null; }
            return new Menu(menuHandle);
        }
        set
        {
            if (User32.SetMenu(Handle, value?.Handle ?? HMENU.Zero) == FALSE)
            { throw WindowsException.Get(); }

            if (User32.DrawMenuBar(Handle) == FALSE)
            { throw WindowsException.Get(); }
        }
    }

    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public bool IsValid => User32.IsWindow(Handle) != FALSE;

    public unsafe FormUnmanaged() { }

    public unsafe FormUnmanaged(HWND handle) : base(handle) { }

    public void Show(ShowWindowFlags cmdShow) => _ = User32.ShowWindow(Handle, cmdShow);

    /// <exception cref="WindowsException"/>
    public void Close()
    {
        if (User32.PostMessageW(Handle, WindowMessage.WM_CLOSE, WPARAM.Zero, LPARAM.Zero) == FALSE)
        { throw WindowsException.Get(); }
    }

    /// <exception cref="WindowsException"/>
    public void Minimize()
    {
        if (User32.CloseWindow(Handle) == FALSE)
        { throw WindowsException.Get(); }
    }

    /// <exception cref="WindowsException"/>
    public void SetLayeredWindowAttributes(COLORREF key, byte alpha, LWA flags = LWA.Alpha | LWA.ColorKey)
    {
        if (User32.SetLayeredWindowAttributes(Handle, key, alpha, (DWORD)flags) == FALSE)
        { throw WindowsException.Get(); }
    }

    /// <exception cref="WindowsException"/>
    public void SetLayeredWindowAttributes(ValueTuple<byte, byte, byte> key, byte alpha, LWA flags = LWA.Alpha | LWA.ColorKey)
        => SetLayeredWindowAttributes(Gdi32.GdiColor.Make(key.Item1, key.Item2, key.Item3), alpha, flags);
}
