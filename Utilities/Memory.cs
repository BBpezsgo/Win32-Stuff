using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

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

    public static class Memory
    {
        /// <summary>
        /// Source: <see href="https://www.appsloveworld.com/cplus/100/243/c-check-if-pointer-is-pointing-to-valid-memory-cant-use-null-checks-here"/>
        /// </summary>
        public static unsafe bool IsPointerValid(void* pointer)
        {
            if ((((uint)pointer) & 7) == 7)
            { return false; } // not valid address at all (maybe random pointer?)

            byte prefix;
            try
            {
                prefix = *(((byte*)pointer) - 1); //get the prefix of this data
            }
            catch (NullReferenceException) // catch all unique exceptions (windows exceptions) 
            {
                return false; // can't reach this memory
            }

            return prefix switch
            {
                0 => false, // running release mode with debugger
                unchecked((byte)-128) => false, // running release mode without debugger
                unchecked((byte)-2) => false, // running debug mode with debugger
                unchecked((byte)-35) => false, // running debug mode without debugger
                _ => true,  // still alive!
            };
        }

        public static unsafe bool HasData<T>(ReadOnlySpan<T> data)
            where T : IEquatable<T>
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (!data[i].Equals(default)) return true;
            }
            return false;
        }

        /// <summary>
        /// Reads in a block from a file and converts it to the struct
        /// type specified by the template parameter
        /// </summary>
        /// <remarks>
        /// Source: <see href="https://gist.github.com/augustoproiete/b51f29f74f5f5b2c59c39e47a8afc3a3"/>
        /// </remarks>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="IOException"/>
        /// <exception cref="ObjectDisposedException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        /// <exception cref="MissingMethodException"/>
        /// <exception cref="InvalidOperationException"/>
        public static unsafe T FromBinaryReader<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] T>(BinaryReader reader)
            where T : unmanaged
            => Memory.FromBinaryReader<T>(reader, sizeof(T));

        /// <summary>
        /// Reads in a block from a file and converts it to the struct
        /// type specified by the template parameter
        /// </summary>
        /// <remarks>
        /// Source: <see href="https://gist.github.com/augustoproiete/b51f29f74f5f5b2c59c39e47a8afc3a3"/>
        /// </remarks>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="IOException"/>
        /// <exception cref="ObjectDisposedException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        /// <exception cref="MissingMethodException"/>
        /// <exception cref="InvalidOperationException"/>
        public static T? FromBinaryReader<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] T>(BinaryReader reader, int size)
        {
            byte[] bytes = reader.ReadBytes(size);

            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            T? result = Marshal.PtrToStructure<T>(handle.AddrOfPinnedObject());
            handle.Free();

            return result;
        }

        /// <exception cref="ArgumentException"/>
        /// <exception cref="IOException"/>
        /// <exception cref="ObjectDisposedException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        /// <exception cref="ArgumentNullException"/>
        public static unsafe void FromBinaryReader<T>(BinaryReader reader, ref T destination)
            where T : unmanaged
        {
            byte[] bytes = reader.ReadBytes(Marshal.SizeOf<T>());
            T* ptr = (T*)Unsafe.AsPointer(ref destination);
            Marshal.Copy(bytes, 0, (nint)ptr, bytes.Length);
        }
    }
}
