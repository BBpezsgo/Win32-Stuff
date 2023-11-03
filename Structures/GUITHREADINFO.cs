using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public readonly struct GuiThreadInfo
    {
        readonly DWORD cbSize;
        public readonly DWORD flags;
        public readonly HWND hwndActive;
        public readonly HWND hwndFocus;
        public readonly HWND hwndCapture;
        public readonly HWND hwndMenuOwner;
        public readonly HWND hwndMoveSize;
        public readonly HWND hwndCaret;
        public readonly RECT rcCaret;

        GuiThreadInfo(uint cbSize) : this() => this.cbSize = cbSize;
        unsafe public static GUITHREADINFO Create() => new((uint)sizeof(GUITHREADINFO));
    }
}
