using System.Numerics;

namespace Win32
{
    public struct BitUtils
    {
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

            bitfield |= (1 << bit);
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
            int _bit = bit % 32;

            return (segments[i] & (1 << _bit)) != 0;
        }

        #endregion

        /// <exception cref="ArgumentOutOfRangeException"/>
        public static void SetBit(int[] segments, int bit, int value)
            => SetBit(segments, bit, value != 0);

        /// <exception cref="ArgumentOutOfRangeException"/>
        public static void SetBit(int[] segments, int bit, bool value)
        {
            int i = bit / 32;
            int _bit = bit % 32;

            ref int segment = ref segments[i];
            if (value) segment |= 1 << _bit;
            else segment &= ~(1 << _bit);
        }
    }
}
