namespace Win32.Net;

[StructLayout(LayoutKind.Sequential)]
public readonly struct IcmpEchoReply
{
    public readonly IpAddress Address;
    public readonly IpStatus Status;
    public readonly ULONG RoundTripTime;
    public readonly ushort DataSize;
    readonly ushort Reserved;
    public readonly unsafe void* Data;
    public readonly IpOptionInformation Options;
}
