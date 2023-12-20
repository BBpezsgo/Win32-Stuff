using System.Globalization;

namespace Win32
{
    [SupportedOSPlatform("windows")]
    public readonly struct GlobalObject :
        IDisposable,
        IEquatable<GlobalObject>
    {
        readonly HGLOBAL _handle;

        public GlobalObject(HGLOBAL handle) => this._handle = handle;

        /// <exception cref="WindowsException"/>
        public unsafe void* Lock()
        {
            void* result = Kernel32.GlobalLock(_handle);
            if (result == null)
            { throw WindowsException.Get(); }
            return result;
        }

        /// <exception cref="WindowsException"/>
        public unsafe T* Lock<T>() where T : unmanaged
        {
            void* result = Kernel32.GlobalLock(_handle);
            if (result == null)
            { throw WindowsException.Get(); }
            return (T*)result;
        }

        /// <exception cref="WindowsException"/>
        public unsafe void Unlock()
        {
            int lockCount = Kernel32.GlobalUnlock(_handle);
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
        public bool Equals(GlobalObject other) => _handle == other._handle;
        public override int GetHashCode() => _handle.GetHashCode();
        public override string ToString() => "0x" + _handle.ToString("x", CultureInfo.InvariantCulture).PadLeft(16, '0');

        /// <exception cref="WindowsException"/>
        public void Dispose()
        {
            if (Kernel32.GlobalFree(_handle) != HGLOBAL.Zero)
            { throw WindowsException.Get(); }
        }
    }

    [SupportedOSPlatform("windows")]
    public static class VirtualMemory
    {
        /// <exception cref="WindowsException"/>
        /// <exception cref="OverflowException"/>
        public static unsafe void* Alloc(int size, DWORD protect = MemoryProtectionFlags.ReadWrite, DWORD allocationType = MEM.MEM_COMMIT | MEM.MEM_RESERVE)
        {
            void* address = Kernel32.VirtualAlloc(null, checked((uint)size), allocationType, protect);
            if (address == null)
            { throw WindowsException.Get(); }
            return address;
        }

        /// <exception cref="WindowsException"/>
        /// <exception cref="OverflowException"/>
        public static unsafe T* Alloc<T>(DWORD protect = MemoryProtectionFlags.ReadWrite, DWORD allocationType = MEM.MEM_COMMIT | MEM.MEM_RESERVE)
            where T : unmanaged
        {
            void* address = Kernel32.VirtualAlloc(null, checked((uint)sizeof(T)), allocationType, protect);
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
            if (Kernel32.VirtualFree(address, 0, MEM.MEM_RELEASE) == 0)
            { throw WindowsException.Get(); }
        }

        /// <exception cref="WindowsException"/>
        /// <exception cref="OverflowException"/>
        public static unsafe DWORD Protect(void* address, int size, DWORD protect)
        {
            DWORD oldProtect = default;
            if (Kernel32.VirtualProtect(address, checked((uint)size), protect, &oldProtect) == 0)
            { throw WindowsException.Get(); }
            return oldProtect;
        }
    }

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
}
