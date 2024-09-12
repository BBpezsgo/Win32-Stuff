namespace Win32.Console;

[Flags]
public enum EventType : WORD
{
    Key = 0x0001,
    Mouse = 0x0002,
    WindowBufferSize = 0x0004,
    Menu = 0x0008,
    Focus = 0x0010,
}
