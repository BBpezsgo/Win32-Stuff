using System.Runtime.InteropServices;

namespace Win32.LowLevel
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MCI_OPEN_PARMS
    {
        public DWORD_PTR dwCallback;
        public MCIDEVICEID wDeviceID;
        public unsafe WCHAR* lpstrDeviceType;
        public unsafe WCHAR* lpstrElementName;
        public unsafe WCHAR* lpstrAlias;
    }
}
