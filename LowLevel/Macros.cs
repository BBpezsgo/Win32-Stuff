namespace Win32.LowLevel
{
    public struct Macros
    {
        public static COLORREF RGB(BYTE r, BYTE g, BYTE b) => unchecked((COLORREF)(r | (g << 8) | (b << 16)));

        public static bool IS_INTRESOURCE(ULONG_PTR _r)
            => (((ULONG)_r) >> 16) == 0;
        public static bool IS_INTRESOURCE(ULONG _r)
            => ((_r) >> 16) == 0;
        public static unsafe CHAR* MAKEINTRESOURCEA(WORD i)
             => (CHAR*)(ULONG_PTR)i;
        public static unsafe WCHAR* MAKEINTRESOURCEW(WORD i)
             => (WCHAR*)(ULONG_PTR)i;

        public static byte LOBYTE(int w)
            => (byte)(w & 0xFF);
        public static byte HIBYTE(int w)
            => (byte)(w >> 8);

        public static WORD LOWORD(int l)
            => (WORD)(l & 0xFFFF);
        public static WORD HIWORD(int l)
            => (WORD)(l >> 16);

        public static byte LOBYTE(nint w)
            => Macros.LOBYTE(w.ToInt32());
        public static byte HIBYTE(nint w)
            => Macros.HIBYTE(w.ToInt32());

        public static WORD LOWORD(nint l)
            => Macros.LOWORD(l.ToInt32());
        public static WORD HIWORD(nint l)
            => Macros.HIWORD(l.ToInt32());

        public static byte LOBYTE(nuint w)
            => Macros.LOBYTE(unchecked((int)w.ToUInt32()));
        public static byte HIBYTE(nuint w)
            => Macros.HIBYTE(unchecked((int)w.ToUInt32()));

        public static WORD LOWORD(nuint l)
            => Macros.LOWORD(unchecked((int)l.ToUInt32()));
        public static WORD HIWORD(nuint l)
            => Macros.HIWORD(unchecked((int)l.ToUInt32()));

        public static byte LOBYTE<T>(T w) where T : IConvertible
            => Macros.LOBYTE(unchecked(w.ToInt32(null)));
        public static byte HIBYTE<T>(T w) where T : IConvertible
            => Macros.HIBYTE(unchecked(w.ToInt32(null)));

        public static WORD LOWORD<T>(T l) where T : IConvertible
            => Macros.LOWORD(unchecked(l.ToInt32(null)));
        public static WORD HIWORD<T>(T l) where T : IConvertible
            => Macros.HIWORD(unchecked(l.ToInt32(null)));

        public static WORD MAKEWORD(byte low, byte high)
            => (WORD)(((byte)(low & 0xFF)) | ((byte)(high & 0xFF) << 8));
        public static ULONG MAKELONG(WORD low, WORD high)
            => ((WORD)(low & 0xFFFF)) | (((DWORD)(WORD)(high & 0xFFFF)) << 16);

        public static short GET_WHEEL_DELTA_WPARAM(WPARAM wParam) => unchecked((short)HIWORD(wParam));
        public static ushort GET_KEYSTATE_WPARAM(WPARAM wParam) => LOWORD(wParam);
        public static short GET_NCHITTEST_WPARAM(WPARAM wParam) => unchecked((short)LOWORD(wParam));
        public static ushort GET_XBUTTON_WPARAM(WPARAM wParam) => HIWORD(wParam);
    }

    public struct LanguageMacros
    {
        public static ULONG MAKELCID(WORD l, WORD s) => Macros.MAKELONG(l, s);

        public static WORD MAKELANGID(WORD p, WORD s) => (WORD)((s << 10) | p);
        public static WORD PRIMARYLANGID(WORD l) => (WORD)(l & 0b_0000_0011_1111_1111); // 0x3ff
        public static WORD SUBLANGID(WORD l) => (WORD)(l >> 10);

        public static WORD LANGIDFROMLCID(WORD lcid) => lcid;
        public static WORD SORTIDFROMLCID(DWORD lcid) => (WORD)((lcid >> 16) & 0b_1111);

        public const WORD LANG_NEUTRAL = 0x00;  // Default custom (MUI) locale language
        // public const WORD LANG_USER_DEFAULT = 0x01;  // User default locale language
        // public const WORD LANG_SYSTEM_DEFAULT = 0x02;  // System default locale language
        public const WORD LANG_INVARIANT = 0x7F;  // Invariant locale language

        public const WORD SUBLANG_NEUTRAL = 0x00;  // Neutral sublanguage
        public const WORD SUBLANG_INVARIANT = 0x00;  // Invariant sublanguage
        public const WORD SUBLANG_DEFAULT = 0x01;  // User default sublanguage
        public const WORD SUBLANG_SYS_DEFAULT = 0x02;  // System default sublanguage
        public const WORD SUBLANG_CUSTOM_DEFAULT = 0x03;  // Default custom sublanguage
        public const WORD SUBLANG_CUSTOM_UNSPECIFIED = 0x04;  // Unspecified custom sublanguage
        public const WORD SUBLANG_UI_CUSTOM_DEFAULT = 0x05;  // Default custom MUI sublanguage

        public static readonly WORD LANG_SYSTEM_DEFAULT = MAKELANGID(LANG_NEUTRAL, SUBLANG_SYS_DEFAULT);
        public static readonly WORD LANG_USER_DEFAULT = MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT);

        public static readonly ULONG LOCALE_SYSTEM_DEFAULT = MAKELCID(LANG_SYSTEM_DEFAULT, SORT_DEFAULT);
        public static readonly ULONG LOCALE_USER_DEFAULT = MAKELCID(LANG_USER_DEFAULT, SORT_DEFAULT);
        public static readonly ULONG LOCALE_NEUTRAL = MAKELCID(MAKELANGID(LANG_NEUTRAL, SUBLANG_NEUTRAL), SORT_DEFAULT);
        public static readonly ULONG LOCALE_INVARIANT = MAKELCID(MAKELANGID(LANG_INVARIANT, SUBLANG_NEUTRAL), SORT_DEFAULT);
        public static readonly ULONG LOCALE_CUSTOM_DEFAULT = MAKELCID(MAKELANGID(LANG_NEUTRAL, SUBLANG_CUSTOM_DEFAULT), SORT_DEFAULT);
        public static readonly ULONG LOCALE_CUSTOM_UNSPECIFIED = MAKELCID(MAKELANGID(LANG_NEUTRAL, SUBLANG_CUSTOM_UNSPECIFIED), SORT_DEFAULT);
        public static readonly ULONG LOCALE_CUSTOM_UI_DEFAULT = MAKELCID(MAKELANGID(LANG_NEUTRAL, SUBLANG_UI_CUSTOM_DEFAULT), SORT_DEFAULT);

        public const int LOCALE_NAME_MAX_LENGTH = 85;

        public const WORD SORT_DEFAULT = 0x0; // sorting default
        public const WORD SORT_INVARIANT_MATH = 0x1; // Invariant (Mathematical Symbols)

    }

    public struct WinErrorMacros
    {
        public static HRESULT MAKE_HRESULT(ULONG sev, ULONG fac, ULONG code) =>
            (HRESULT)((sev << 31) | (fac << 16) | code);

        public static int HRESULT_CODE(HRESULT hr) => hr & 0xFFFF;
        public static int HRESULT_FACILITY(HRESULT hr) => (hr >> 16) & 0x1fff;
        public static int HRESULT_SEVERITY(HRESULT hr) => (hr >> 31) & 0x1;
        public static bool SUCCEEDED(HRESULT hr) => hr >= 0;
        public static bool FAILED(HRESULT hr) => hr < 0;
        public static bool IS_ERROR(HRESULT Status) => unchecked((ULONG)Status) >> 31 == 1;
    }
}
