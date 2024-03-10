namespace Win32;

[StructLayout(LayoutKind.Explicit)]
public struct InputInfo
{
    [FieldOffset(0)] public DWORD Type;

    [FieldOffset(4)] public MouseInput Mouse;
    [FieldOffset(4)] public KeyboardInput Keyboard;
    [FieldOffset(4)] public HardwareInput Hardware;
}
