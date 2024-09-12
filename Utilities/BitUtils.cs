using System.Numerics;

namespace Win32;

public static class BitUtils
{
    #region Make Stuff

    public static WORD MakeWord(BYTE low, BYTE high) => (WORD)(low | (high << 8));

    public static ULONG MakeLong(WORD low, WORD high) => low | (((DWORD)high) << 16);

    public static ULONG MakeLong(short low, short high) => BitUtils.MakeLong(unchecked((WORD)low), unchecked((WORD)high));

    #endregion

    #region LowHighByte

    public static BYTE LowByte(int value) => (BYTE)(value & 0xFF);
    public static BYTE HighByte(int value) => (BYTE)(value >> 8);

    public static WORD LowWord(int value) => (WORD)(value & 0xFFFF);
    public static WORD HighWord(int value) => (WORD)(value >> 16);

    public static BYTE LowByte(nint value) => BitUtils.LowByte(value.ToInt32());
    public static BYTE HighByte(nint value) => BitUtils.HighByte(value.ToInt32());

    public static WORD LowWord(nint value) => BitUtils.LowWord(value.ToInt32());
    public static WORD HighWord(nint value) => BitUtils.HighWord(value.ToInt32());

    public static BYTE LowByte(nuint value) => BitUtils.LowByte(unchecked((int)value.ToUInt32()));
    public static BYTE HighByte(nuint value) => BitUtils.HighByte(unchecked((int)value.ToUInt32()));

    public static WORD LowWord(nuint value) => BitUtils.LowWord(unchecked((int)value.ToUInt32()));
    public static WORD HighWord(nuint value) => BitUtils.HighWord(unchecked((int)value.ToUInt32()));

    public static WORD LowWord(uint value) => BitUtils.LowWord(unchecked((int)value));
    public static WORD HighWord(uint value) => BitUtils.HighWord(unchecked((int)value));

    public static BYTE LowByte<T>(T value) where T : IConvertible => BitUtils.LowByte(unchecked(value.ToInt32(null)));
    public static BYTE HighByte<T>(T value) where T : IConvertible => BitUtils.HighByte(unchecked(value.ToInt32(null)));

    public static WORD LowWord<T>(T value) where T : IConvertible => BitUtils.LowWord(unchecked(value.ToInt32(null)));
    public static WORD HighWord<T>(T value) where T : IConvertible => BitUtils.HighWord(unchecked(value.ToInt32(null)));

    #endregion

    #region SetMask

    public static void SetMask(ref int bitfield, int mask) => bitfield |= mask;

    public static void SetMask(ref byte bitfield, byte mask) => bitfield |= mask;

    public static void SetMask<TBitfield, TMask>(ref TBitfield bitfield, TMask mask)
        where TBitfield : IBitwiseOperators<TBitfield, TMask, TBitfield>
        => bitfield |= mask;

    #endregion

    #region ResetMask

    public static void ResetMask(ref int bitfield, int mask) => bitfield &= ~mask;

    public static void ResetMask(ref byte bitfield, byte mask) => bitfield &= (byte)~mask;

    public static void ResetMask<TBitfield, TMask>(ref TBitfield bitfield, TMask mask)
        where TBitfield : IBitwiseOperators<TBitfield, TMask, TBitfield>
        where TMask : IBitwiseOperators<TMask, TMask, TMask>
        => bitfield &= ~mask;

    #endregion

    #region SetBitValue

    /// <exception cref="ArgumentOutOfRangeException"/>
    public static void SetBit(ref int bitfield, int bit, int value)
        => SetBit(ref bitfield, bit, value != 0);

    /// <exception cref="ArgumentOutOfRangeException"/>
    public static void SetBit(ref int bitfield, int bit, bool value)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(bit);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(bit, 32);

        if (value) bitfield |= 1 << bit;
        else bitfield &= ~(1 << bit);
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public static void SetBit(ref byte bitfield, int bit, bool value)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(bit);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(bit, 8);

        if (value) bitfield = (byte)(bitfield | (1 << bit));
        else bitfield = (byte)(bitfield & ~(1 << bit));
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public static void SetBit<T>(ref T bitfield, int bit, bool value)
        where T : IBitwiseOperators<T, int, T>
    {
        ArgumentOutOfRangeException.ThrowIfNegative(bit);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(bit, 8);

        if (value) bitfield |= 1 << bit;
        else bitfield &= ~(1 << bit);
    }

    #endregion

    #region SetBit

    /// <exception cref="ArgumentOutOfRangeException"/>
    public static void SetBit(ref int bitfield, int bit)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(bit);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(bit, 32);

        bitfield |= 1 << bit;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public static void SetBit(ref byte bitfield, int bit)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(bit);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(bit, 8);

        bitfield = (byte)(bitfield | (1 << bit));
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public static void SetBit<T>(ref T bitfield, int bit)
        where T : IBitwiseOperators<T, int, T>
    {
        ArgumentOutOfRangeException.ThrowIfNegative(bit);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(bit, 8);

        bitfield |= 1 << bit;
    }

    #endregion

    #region ResetBit

    /// <exception cref="ArgumentOutOfRangeException"/>
    public static void ResetBit(ref int bitfield, int bit)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(bit);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(bit, 32);

        bitfield &= ~(1 << bit);
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public static void ResetBit(ref byte bitfield, int bit)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(bit);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(bit, 8);

        bitfield = (byte)(bitfield & ~(1 << bit));
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public static void ResetBit<T>(ref T bitfield, int bit)
        where T : IBitwiseOperators<T, int, T>
    {
        ArgumentOutOfRangeException.ThrowIfNegative(bit);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(bit, 32);

        bitfield &= ~(1 << bit);
    }

    #endregion

    #region GetBit

    /// <exception cref="ArgumentOutOfRangeException"/>
    public static bool GetBit(int bitfield, int bit)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(bit);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(bit, 32);

        return (bitfield & (1 << bit)) != 0;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public static bool GetBit(int[] segments, int bit)
    {
        int i = bit / 32;
        bit %= 32;

        return (segments[i] & (1 << bit)) != 0;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public static bool GetBit(ReadOnlySpan<int> segments, int bit)
    {
        int i = bit / 32;
        bit %= 32;

        return (segments[i] & (1 << bit)) != 0;
    }

    #endregion

    /// <exception cref="ArgumentOutOfRangeException"/>
    public static void SetBit(int[] segments, int bit, int value)
        => SetBit(segments, bit, value != 0);

    /// <exception cref="ArgumentOutOfRangeException"/>
    public static void SetBit(int[] segments, int bit, bool value)
    {
        int i = bit / 32;
        bit %= 32;

        ref int segment = ref segments[i];
        if (value) segment |= 1 << bit;
        else segment &= ~(1 << bit);
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public static void SetBit(Span<int> segments, int bit, int value)
        => SetBit(segments, bit, value != 0);

    /// <exception cref="ArgumentOutOfRangeException"/>
    public static void SetBit(Span<int> segments, int bit, bool value)
    {
        int i = bit / 32;
        bit %= 32;

        ref int segment = ref segments[i];
        if (value) segment |= 1 << bit;
        else segment &= ~(1 << bit);
    }
}
