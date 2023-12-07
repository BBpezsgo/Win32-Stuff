using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential)]
    public struct GroupAffinity
    {
        public ULONG_PTR Mask;
        public WORD Group;

        readonly WORD Reserved1;
        readonly WORD Reserved2;
        readonly WORD Reserved3;
    }
}
