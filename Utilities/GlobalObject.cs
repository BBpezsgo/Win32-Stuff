using System.Globalization;

namespace Win32;

[SupportedOSPlatform("windows")]
public readonly struct GlobalObject :
    IDisposable,
    IEquatable<GlobalObject>
{
    readonly HGLOBAL Handle;

    public GlobalObject(HGLOBAL handle) => Handle = handle;

    /// <exception cref="WindowsException"/>
    public unsafe void* Lock()
    {
        void* result = Kernel32.GlobalLock(Handle);
        if (result == null)
        { throw WindowsException.Get(); }
        return result;
    }

    /// <exception cref="WindowsException"/>
    public unsafe T* Lock<T>() where T : unmanaged
    {
        void* result = Kernel32.GlobalLock(Handle);
        if (result == null)
        { throw WindowsException.Get(); }
        return (T*)result;
    }

    /// <exception cref="WindowsException"/>
    public unsafe void Unlock()
    {
        int lockCount = Kernel32.GlobalUnlock(Handle);
        if (lockCount == 0)
        {
            uint error = Kernel32.GetLastError();
            if (error != 0)
            { throw WindowsException.Get(error); }
        }
    }

    public static bool operator ==(GlobalObject left, GlobalObject right) => left.Equals(right);
    public static bool operator !=(GlobalObject left, GlobalObject right) => !left.Equals(right);

    public override bool Equals(object? obj) => obj is GlobalObject @object && Equals(@object);
    public bool Equals(GlobalObject other) => Handle == other.Handle;
    public override int GetHashCode() => Handle.GetHashCode();
    public override string ToString() => "0x" + Handle.ToString("x", CultureInfo.InvariantCulture).PadLeft(16, '0');

    /// <exception cref="WindowsException"/>
    public void Dispose()
    {
        if (Kernel32.GlobalFree(Handle) != HGLOBAL.Zero)
        { throw WindowsException.Get(); }
    }
}
