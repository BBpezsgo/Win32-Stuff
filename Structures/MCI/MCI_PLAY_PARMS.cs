using System.Runtime.InteropServices;

namespace Win32.LowLevel
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MCI_PLAY_PARMS
    {
        public DWORD_PTR dwCallback;
        public DWORD dwFrom;
        public DWORD dwTo;
    }
}
