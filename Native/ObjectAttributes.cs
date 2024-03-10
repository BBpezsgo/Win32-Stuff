namespace Win32.Native;

/// <summary>
/// The <see cref="ObjectAttributes"/> structure specifies attributes that can be
/// applied to objects or object handles by routines that create
/// objects and/or return handles to objects.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct ObjectAttributes
{
    public ULONG StructSize;
    public HANDLE RootDirectory;
    public unsafe UnicodeString* ObjectName;
    public ULONG Attributes;
    public unsafe void* SecurityDescriptor; // PSECURITY_DESCRIPTOR;
    public unsafe void* SecurityQualityOfService; // PSECURITY_QUALITY_OF_SERVICE
}
