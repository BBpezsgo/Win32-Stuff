using System.Runtime.InteropServices;

namespace Win32.Gdi32
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PaintStruct
    {
        public HDC HDC;
        public bool Erase;
        public RECT Paint;
        public bool Restore;
        public bool IncUpdate;
        readonly int Reserved;
    }
}
