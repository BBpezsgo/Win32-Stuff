namespace Win32.Console;

[Flags]
public enum MouseButton : DWORD
{
    Left = 0x0001,
    Right = 0x0002,
    Middle = 0x0004,
    Button3 = 0x0008,
    Button4 = 0x0010,
}
