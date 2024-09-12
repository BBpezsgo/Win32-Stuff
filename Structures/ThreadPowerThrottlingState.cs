namespace Win32;

[StructLayout(LayoutKind.Sequential)]
public struct ThreadPowerThrottlingState
{
    public ULONG Version;
    public ULONG ControlMask;
    public ULONG StateMask;
}
