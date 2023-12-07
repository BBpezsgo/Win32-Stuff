global using GUITHREADINFO = Win32.GuiThreadInfo;

using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public readonly struct GuiThreadInfo
    {
        readonly DWORD StructSize;

        public readonly DWORD Flags;
        public readonly HWND ActiveHandle;
        public readonly HWND FocusHandle;
        public readonly HWND CaptureHandle;
        public readonly HWND MenuOwnerHandle;
        public readonly HWND MoveSizeHandle;
        public readonly HWND CaretHandle;
        public readonly RECT Caret;

        GuiThreadInfo(uint structSize) : this() => this.StructSize = structSize;
        unsafe public static GUITHREADINFO Create() => new((uint)sizeof(GUITHREADINFO));
    }
}
