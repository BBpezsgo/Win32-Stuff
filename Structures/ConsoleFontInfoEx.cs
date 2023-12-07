using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct ConsoleFontInfoEx
    {
        readonly ULONG StructSize;

        public DWORD FontIndex;
        public SHORT FontWidth;
        public SHORT FontSize;
        public UINT FontFamily;
        public UINT FontWeight;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string FaceName;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        ConsoleFontInfoEx(DWORD structSize) : this() => StructSize = structSize;
#pragma warning restore CS8618

        public static ConsoleFontInfoEx Create() => new((DWORD)Marshal.SizeOf<ConsoleFontInfoEx>());
    }
}
