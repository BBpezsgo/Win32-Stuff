namespace Win32.DWrite;

/// <summary>
/// Font file loader interface handles loading font file resources of a particular type from a key.
/// The font file loader interface is recommended to be implemented by a singleton object.
/// IMPORTANT: font file loader implementations must not register themselves with DirectWrite factory
/// inside their constructors and must not unregister themselves in their destructors, because
/// registration and unregistration operations increment and decrement the object reference count respectively.
/// Instead, registration and unregistration of font file loaders with DirectWrite factory should be performed
/// outside of the font file loader implementation as a separate step.
/// </summary>
[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("727cad4e-d6af-4c9e-8a08-d695b11caa49")]
[SupportedOSPlatform("windows")]
public interface IDWriteFontFileLoader
{
    /// <summary>
    /// Creates a font file stream object that encapsulates an open file resource.
    /// The resource is closed when the last reference to fontFileStream is released.
    /// </summary>
    /// <param name="fontFileReferenceKey">Font file reference key that uniquely identifies the font file resource
    /// within the scope of the font loader being used.</param>
    /// <param name="fontFileReferenceKeySize">Size of font file reference key in bytes.</param>
    /// <param name="fontFileStream">Pointer to the newly created font file stream.</param>
    /// <returns>
    /// Standard HRESULT error code.
    /// </returns>
    abstract unsafe void CreateStreamFromKey(
        void* fontFileReferenceKey,
        UINT32 fontFileReferenceKeySize,
        [Out, MarshalAs(UnmanagedType.IUnknown)] out IDWriteFontFileStream fontFileStream
    );
}
