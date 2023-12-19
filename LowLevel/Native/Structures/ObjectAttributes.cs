using System.Runtime.InteropServices;

namespace Win32.LowLevel
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ObjectAttributes
    {
        public ULONG Length;
        public HANDLE RootDirectory;
        public unsafe UnicodeString* ObjectName;
        public ULONG Attributes;
        public unsafe void* SecurityDescriptor; // PSECURITY_DESCRIPTOR;
        public unsafe void* SecurityQualityOfService; // PSECURITY_QUALITY_OF_SERVICE
    }
}
