namespace Win32.Forms;

public readonly struct MouseWheelEventArgs
{
    public ushort X { get;  }
    public ushort Y { get;  }
    public MouseEventFlags Flags { get;  }
    public short Delta { get;  }

    public MouseWheelEventArgs(nuint wParam, nint lParam)
    {
        Flags = (MouseEventFlags)BitUtils.LowWord(wParam);
        Delta = unchecked((short)BitUtils.HighWord(wParam));
        X = BitUtils.LowWord(lParam);
        Y = BitUtils.HighWord(lParam);
    }

    public override string ToString() => $"({X} {Y} ; {Flags} ; {Delta})";
}
