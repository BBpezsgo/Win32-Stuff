namespace Win32;

[SupportedOSPlatform("windows")]
public readonly struct Library : IDisposable
{
    readonly HMODULE Handle;

    /// <exception cref="WindowsException"/>
    public unsafe Library(string libraryFileName)
    {
        fixed (WCHAR* libraryFileNamePtr = libraryFileName)
        { Handle = Kernel32.LoadLibraryW(libraryFileNamePtr); }

        if (Handle == 0)
        { throw WindowsException.Get(); }
    }

    /// <exception cref="WindowsException"/>
    public unsafe Library(string libraryFileName, nint fileHandle, uint flags)
    {
        fixed (WCHAR* libraryFileNamePtr = libraryFileName)
        { Handle = Kernel32.LoadLibraryExW(libraryFileNamePtr, fileHandle, flags); }

        if (Handle == 0)
        { throw WindowsException.Get(); }
    }

    /// <exception cref="WindowsException"/>
    public void Dispose()
    {
        if (Kernel32.FreeLibrary(Handle) == 0)
        { throw WindowsException.Get(); }
    }

    /// <exception cref="WindowsException"/>
    public unsafe void* GetProc(string procName)
    {
        fixed (WCHAR* procNamePtr = procName)
        {
            void* procPtr = Kernel32.GetProcAddress(Handle, procNamePtr);

            if (procPtr == null)
            { throw WindowsException.Get(); }

            return procPtr;
        }
    }

    /// <exception cref="WindowsException"/>
    public unsafe TDelegate GetProc<TDelegate>(string procName)
    {
        void* procPtr = GetProc(procName);
        return Marshal.GetDelegateForFunctionPointer<TDelegate>((nint)procPtr);
    }

    /// <exception cref="WindowsException"/>
    public unsafe Delegate GetProc(string procName, Type delegateType)
    {
        void* procPtr = GetProc(procName);
        return Marshal.GetDelegateForFunctionPointer((nint)procPtr, delegateType);
    }
}
