namespace Win32.Forms;

public readonly struct MouseEventArgs
{
    public ushort X { get; }
    public ushort Y { get; }
    public MouseEventFlags Flags { get; }

    public MouseEventArgs(nuint wParam, nint lParam)
    {
        Flags = (MouseEventFlags)wParam;
        X = BitUtils.LowWord(lParam);
        Y = BitUtils.HighWord(lParam);
    }

    public override string ToString() => $"({X} {Y} ; {Flags})";
}
