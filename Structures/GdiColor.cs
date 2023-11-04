namespace Win32
{
    public readonly struct GdiColor
    {
        readonly uint v;

        GdiColor(uint v) => this.v = v;

        public static implicit operator GdiColor(uint v) => new(v);
        public static implicit operator uint(GdiColor v) => v.v;
        public static implicit operator GdiColor(ValueTuple<byte, byte, byte> v) => new(Macros.RGB(v.Item1, v.Item2, v.Item3));
    }
}
