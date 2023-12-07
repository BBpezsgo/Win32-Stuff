global using CREATESTRUCT = Win32.CreateStruct;

using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct CreateStruct
    {
        unsafe public void* CreateParams;
        public HINSTANCE InstanceHandle;
        public HMENU MenuHandle;
        public HWND ParentHandle;
        public int WindowHeightPx;
        public int WindowWidthPx;
        public int PositionY;
        public int PositionX;
        public LONG Style;
        unsafe public char* Name;
        unsafe public char* Class;
        public DWORD StyleEx;
    }
}
