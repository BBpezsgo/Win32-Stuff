﻿namespace Win32.Net;

[StructLayout(LayoutKind.Sequential)]
public struct IpOptionInformation32
{
    public byte Ttl;
    public byte Tos;
    public byte Flags;
    public byte OptionsSize;
    public unsafe byte* OptionsData;
}
