namespace Win32;

[SupportedOSPlatform("windows")]
public static class VirtualMemory
{
    /// <exception cref="WindowsException"/>
    /// <exception cref="OverflowException"/>
    public static unsafe void* Alloc(int size, MemoryProtectionFlags protect = MemoryProtectionFlags.ReadWrite, MemoryFlags allocationType = MemoryFlags.Commit | MemoryFlags.Reserve)
    {
        void* address = Kernel32.VirtualAlloc(null, checked((uint)size), (DWORD)allocationType, (DWORD)protect);
        if (address == null)
        { throw WindowsException.Get(); }
        return address;
    }

    /// <exception cref="WindowsException"/>
    /// <exception cref="OverflowException"/>
    public static unsafe T* Alloc<T>(MemoryProtectionFlags protect = MemoryProtectionFlags.ReadWrite, MemoryFlags allocationType = MemoryFlags.Commit | MemoryFlags.Reserve)
        where T : unmanaged
    {
        void* address = Kernel32.VirtualAlloc(null, checked((uint)sizeof(T)), (DWORD)allocationType, (DWORD)protect);
        if (address == null)
        { throw WindowsException.Get(); }
        return (T*)address;
    }

    /// <exception cref="WindowsException"/>
    /// <exception cref="OverflowException"/>
    public static unsafe void Free(void* address, int size, DWORD freeType)
    {
        if (Kernel32.VirtualFree(address, checked((uint)size), freeType) == 0)
        { throw WindowsException.Get(); }
    }

    /// <exception cref="WindowsException"/>
    public static unsafe void Free(void* address)
    {
        if (Kernel32.VirtualFree(address, 0, (DWORD)MemoryFlags.Release) == 0)
        { throw WindowsException.Get(); }
    }

    /// <exception cref="WindowsException"/>
    /// <exception cref="OverflowException"/>
    public static unsafe DWORD Protect(void* address, int size, DWORD protect)
    {
        if (Kernel32.VirtualProtect(address, checked((uint)size), protect, out uint oldProtect) == 0)
        { throw WindowsException.Get(); }
        return oldProtect;
    }
}
