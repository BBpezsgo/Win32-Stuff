namespace Win32;

[SupportedOSPlatform("windows")]
public static class GlobalMemory
{
    public static HANDLE LRUNewest(HANDLE h) => h;
    public static HANDLE LRUOldest(HANDLE h) => h;

    /// <exception cref="WindowsException"/>
    public static HGLOBAL Discard(HGLOBAL h)
    {
        HGLOBAL handle = Kernel32.GlobalReAlloc(h, SIZE_T.Zero, GMEM.Moveable);
        if (handle == HGLOBAL.Zero)
        { throw WindowsException.Get(); }
        return handle;
    }

    /// <exception cref="WindowsException"/>
    /// <exception cref="OverflowException"/>
    public static GlobalObject Alloc(int size, UINT flags)
    {
        HGLOBAL handle = Kernel32.GlobalAlloc(flags, checked((uint)size));
        if (handle == HGLOBAL.Zero)
        { throw WindowsException.Get(); }
        return new GlobalObject(handle);
    }
}
