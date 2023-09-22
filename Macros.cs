namespace Win32
{
    public struct Macros
    {
        public static byte LOBYTE(int w)
            => (byte)(w & 0xFF);
        public static byte HIBYTE(int w)
            => (byte)(w >> 8);

        public static WORD LOWORD(int l)
            => (WORD)(l & 0xFFFF);
        public static WORD HIWORD(int l)
            => (WORD)(l >> 16);

        public static byte LOBYTE(System.IntPtr w)
            => Macros.LOBYTE(w.ToInt32());
        public static byte HIBYTE(System.IntPtr w)
            => Macros.HIBYTE(w.ToInt32());

        public static WORD LOWORD(System.IntPtr l)
            => Macros.LOWORD(l.ToInt32());
        public static WORD HIWORD(System.IntPtr l)
            => Macros.HIWORD(l.ToInt32());

        public static byte LOBYTE(System.UIntPtr w)
            => Macros.LOBYTE(unchecked((int)w.ToUInt32()));
        public static byte HIBYTE(System.UIntPtr w)
            => Macros.HIBYTE(unchecked((int)w.ToUInt32()));

        public static WORD LOWORD(System.UIntPtr l)
            => Macros.LOWORD(unchecked((int)l.ToUInt32()));
        public static WORD HIWORD(System.UIntPtr l)
            => Macros.HIWORD(unchecked((int)l.ToUInt32()));

        public static WORD MAKEWORD(int low, int high)
            => (WORD)(((byte)(low & 0xFF)) | ((byte)(high & 0xFF) << 8));
        public static int MAKELONG(int low, int high)
            => (int)(((WORD)(low & 0xFFFF)) | (((DWORD)(WORD)(high & 0xFFFF)) << 16));

        public static short GET_WHEEL_DELTA_WPARAM(WPARAM wParam) => unchecked((short)HIWORD(wParam));
        public static ushort GET_KEYSTATE_WPARAM(WPARAM wParam) => LOWORD(wParam);
        public static short GET_NCHITTEST_WPARAM(WPARAM wParam) => unchecked((short)LOWORD(wParam));
        public static ushort GET_XBUTTON_WPARAM(WPARAM wParam) => HIWORD(wParam);
    }
}
