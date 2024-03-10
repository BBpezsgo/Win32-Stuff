namespace Win32.Net;

[StructLayout(LayoutKind.Sequential)]
public readonly struct IcmpEchoReply32
{
    public readonly IpAddress Address;
    public readonly ULONG Status;
    public readonly ULONG RoundTripTime;
    public readonly ushort DataSize;
    readonly ushort Reserved;
    public readonly unsafe void* Data;
    public readonly IpOptionInformation32 Options;
}
