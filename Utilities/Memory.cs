using System.Runtime.CompilerServices;

namespace Win32;

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
