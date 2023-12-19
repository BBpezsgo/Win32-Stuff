using System.Runtime.InteropServices;

namespace Win32.DWrite
{
    /// <summary>
    /// The font file enumerator interface encapsulates a collection of font files. The font system uses this interface
    /// to enumerate font files when building a font collection.
    /// </summary>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("72755049-5ff7-435d-8348-4be97cfa6c7c")]
    [SupportedOSPlatform("windows")]
    public interface IDWriteFontFileEnumerator
    {
        /// <summary>
        /// Advances to the next font file in the collection. When it is first created, the enumerator is positioned
        /// before the first element of the collection and the first call to MoveNext advances to the first file.
        /// </summary>
        /// <param name="hasCurrentFile">Receives the value TRUE if the enumerator advances to a file, or FALSE if
        /// the enumerator advanced past the last file in the collection.</param>
        /// <returns>
        /// Standard HRESULT error code.
        /// </returns>
        abstract unsafe void MoveNext(
            [Out] BOOL* hasCurrentFile
        );

        /// <summary>
        /// Gets a reference to the current font file.
        /// </summary>
        /// <param name="fontFile">Pointer to the newly created font file object.</param>
        /// <returns>
        /// Standard HRESULT error code.
        /// </returns>
        abstract void GetCurrentFontFile(
            [Out, MarshalAs(UnmanagedType.IUnknown)] out IDWriteFontFile fontFile
        );
    }
}
