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
    }
}
