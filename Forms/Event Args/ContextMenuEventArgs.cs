namespace Win32.Forms;

public readonly struct ContextMenuEventArgs
{
    public Window Context { get; }
    public POINT Position { get; }

    public ContextMenuEventArgs(nuint wParam, nint lParam)
    {
        Context = (Window)(HWND)wParam;
        Position = new POINT(BitUtils.LowWord(lParam), BitUtils.HighWord(lParam));
    }
}
