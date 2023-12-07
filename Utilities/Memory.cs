namespace Win32
{
    public readonly struct GlobalObject : IDisposable
    {
        readonly HGLOBAL _handle;

        public GlobalObject(HGLOBAL handle) => this._handle = handle;

        /// <exception cref="WindowsException"/>
        public void Dispose()
        {
            if (Kernel32.GlobalFree(_handle) != HGLOBAL.Zero)
            { throw WindowsException.Get(); }
        }

        /// <exception cref="WindowsException"/>
        unsafe public void* Lock()
        {
            void* result = Kernel32.GlobalLock(_handle);
            if (result == null)
            { throw WindowsException.Get(); }
            return result;
        }

        /// <exception cref="WindowsException"/>
        unsafe public T* Lock<T>() where T : unmanaged
        {
            void* result = Kernel32.GlobalLock(_handle);
            if (result == null)
            { throw WindowsException.Get(); }
            return (T*)result;
        }

        /// <exception cref="WindowsException"/>
        unsafe public void Unlock()
        {
            int lockCount = Kernel32.GlobalUnlock(_handle);
            if (lockCount == 0)
            {
                uint error = Kernel32.GetLastError();
                if (error != 0)
                { throw WindowsException.Get(error); }
            }
        }
    }

    public static class Memory
    {
        /// <exception cref="WindowsException"/>
        public static GlobalObject GlobalAlloc(uint flags, uint size)
        {
            HGLOBAL handle = Kernel32.GlobalAlloc(flags, (UIntPtr)size);
            if (handle == HGLOBAL.Zero)
            { throw WindowsException.Get(); }
            return new GlobalObject(handle);
        }

        /// <exception cref="WindowsException"/>
        unsafe public static void* VirtualAlloc(uint size, DWORD protect, DWORD allocationType)
        {
            void* address = Kernel32.VirtualAlloc(null, size, allocationType, protect);
            if (address == null)
            { throw WindowsException.Get(); }
            return address;
        }

        /// <exception cref="WindowsException"/>
        unsafe public static void VirtualFree(void* address, uint size, DWORD freeType)
        {
            if (Kernel32.VirtualFree(address, size, freeType) == 0)
            { throw WindowsException.Get(); }
        }

        /// <exception cref="WindowsException"/>
        unsafe public static DWORD VirtualProtect(void* address, uint size, DWORD protect)
        {
            DWORD oldProtect = default;
            if (Kernel32.VirtualProtect(address, size, protect, &oldProtect) == 0)
            { throw WindowsException.Get(); }
            return oldProtect;
        }
    }
}
