using System.Runtime.InteropServices;

namespace Win32.LowLevel
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ObjectAttributes
    {
        public ULONG Length;
        public HANDLE RootDirectory;
        unsafe public UnicodeString* ObjectName;
        public ULONG Attributes;
        unsafe public void* SecurityDescriptor; // PSECURITY_DESCRIPTOR;
        unsafe public void* SecurityQualityOfService; // PSECURITY_QUALITY_OF_SERVICE
    }
}
