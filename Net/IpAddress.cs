namespace Win32.Net;

[StructLayout(LayoutKind.Explicit)]
public readonly struct IpAddress : IEquatable<IpAddress>
{
    [FieldOffset(0)] readonly byte Byte1;
    [FieldOffset(1)] readonly byte Byte2;
    [FieldOffset(2)] readonly byte Byte3;
    [FieldOffset(3)] readonly byte Byte4;

    [FieldOffset(0)] readonly ushort UShort1;
    [FieldOffset(2)] readonly ushort Ushort2;

    [FieldOffset(0)] readonly uint UInt;

    public IpAddress(byte _1, byte _2, byte _3, byte _4)
    {
        Byte1 = _1;
        Byte2 = _2;
        Byte3 = _3;
        Byte4 = _4;
    }

    public IpAddress(ushort _1, ushort _2)
    {
        UShort1 = _1;
        Ushort2 = _2;
    }

    public IpAddress(uint _1)
    {
        UInt = _1;
    }

    public override string ToString() => $"{Byte1}.{Byte2}.{Byte3}.{Byte4}";
    public override int GetHashCode() => unchecked((int)UInt);
    public override bool Equals(object? obj) => obj is IpAddress address && Equals(address);
    public bool Equals(IpAddress other) => UInt == other.UInt;

    public static implicit operator uint(IpAddress v) => v.UInt;
    public static implicit operator System.Net.IPAddress(IpAddress v) => new([v.Byte1, v.Byte2, v.Byte3, v.Byte4]);

    public static implicit operator IpAddress(uint v) => new(v);
    public static implicit operator IpAddress(System.Net.IPAddress v)
    {
        byte[] bytes = v.MapToIPv4().GetAddressBytes();
        return new IpAddress(bytes[0], bytes[1], bytes[2], bytes[3]);
    }

    public static bool operator ==(IpAddress left, IpAddress right) => left.Equals(right);
    public static bool operator !=(IpAddress left, IpAddress right) => !left.Equals(right);
}