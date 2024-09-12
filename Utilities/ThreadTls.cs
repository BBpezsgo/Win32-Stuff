namespace Win32;

[SupportedOSPlatform("windows")]
public readonly struct ThreadTls : IDisposable
{
    readonly DWORD Index;

    public ThreadTls(DWORD index) => Index = index;

    /// <exception cref="WindowsException"/>
    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public unsafe void* Value
    {
        get
        {
            void* result = Kernel32.TlsGetValue(Index);
            if (result == null)
            { throw WindowsException.Get(); }
            return result;
        }
        set
        {
            if (Kernel32.TlsSetValue(Index, value) == FALSE)
            { throw WindowsException.Get(); }
        }
    }

    /// <exception cref="WindowsException"/>
    public void Dispose()
    {
        if (Kernel32.TlsFree(Index) == FALSE)
        { throw WindowsException.Get(); }
    }

    /// <exception cref="WindowsException"/>
    public static ThreadTls Alloc()
    {
        DWORD result = Kernel32.TlsAlloc();
        if (result == unchecked((DWORD)0xFFFFFFFF))
        { throw WindowsException.Get(); }
        return new ThreadTls(result);
    }
}
