using System.Diagnostics;
using System.Globalization;

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

        public static WORD MAKEWORD(byte low, byte high)
            => (WORD)(((byte)(low & 0xFF)) | ((byte)(high & 0xFF) << 8));
        public static ULONG MAKELONG(WORD low, WORD high)
            => ((WORD)(low & 0xFFFF)) | (((DWORD)(WORD)(high & 0xFFFF)) << 16);

        public static short GET_WHEEL_DELTA_WPARAM(WPARAM wParam) => unchecked((short)HIWORD(wParam));
        public static ushort GET_KEYSTATE_WPARAM(WPARAM wParam) => LOWORD(wParam);
        public static short GET_NCHITTEST_WPARAM(WPARAM wParam) => unchecked((short)LOWORD(wParam));
        public static ushort GET_XBUTTON_WPARAM(WPARAM wParam) => HIWORD(wParam);
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

    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public readonly struct HResult : IEquatable<HResult>
    {
        readonly HRESULT code;

        public HResult(int code) => this.code = code;

        public readonly bool IsSucceeded => WinErrorMacros.SUCCEEDED(code);
        public readonly bool IsFailed => WinErrorMacros.FAILED(code);
        public readonly bool IsError => WinErrorMacros.IS_ERROR(code);
        public readonly int Severity => WinErrorMacros.HRESULT_SEVERITY(code);
        public readonly int Facility => WinErrorMacros.HRESULT_FACILITY(code);
        public readonly int Code => WinErrorMacros.HRESULT_CODE(code);

        public override bool Equals(object? obj) => obj is HResult result && Equals(result);
        public bool Equals(HResult other) => code == other.code;

        public override int GetHashCode() => HashCode.Combine(code);

        public override string ToString() => code.ToString(CultureInfo.InvariantCulture);
        readonly string GetDebuggerDisplay() => ToString();

        public static bool operator ==(HResult left, HResult right) => left.Equals(right);
        public static bool operator !=(HResult left, HResult right) => !(left == right);

        public static implicit operator HRESULT(HResult hr) => hr.code;
        public static implicit operator HResult(HRESULT hr) => new(hr);
    }
}
