namespace Win32;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct ModuleInfo
{
    public void* BaseOfDll;
    public DWORD SizeOfImage;
    public void* EntryPoint;
}
