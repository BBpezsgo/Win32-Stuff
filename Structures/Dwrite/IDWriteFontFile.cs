using System.Runtime.InteropServices;

namespace Win32.DWrite
{
    /// <summary>
    /// The interface that represents a reference to a font file.
    /// </summary>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("739d886a-cef5-47dc-8769-1a8b41bebbb0")]
    public interface IDWriteFontFile
    {
        /// <summary>
        /// This method obtains the pointer to the reference key of a font file. The pointer is only valid until the object that refers to it is released.
        /// </summary>
        /// <param name="fontFileReferenceKey">Pointer to the font file reference key.
        /// IMPORTANT: The pointer value is valid until the font file reference object it is obtained from is released.</param>
        /// <param name="fontFileReferenceKeySize">Size of font file reference key in bytes.</param>
        /// <returns>
        /// Standard HRESULT error code.
        /// </returns>
        unsafe abstract void GetReferenceKey(
            void** fontFileReferenceKey,
            [Out] UINT32* fontFileReferenceKeySize
        );

        /// <summary>
        /// Obtains the file loader associated with a font file object.
        /// </summary>
        /// <param name="fontFileLoader">The font file loader associated with the font file object.</param>
        /// <returns>
        /// Standard HRESULT error code.
        /// </returns>
        abstract void GetLoader(
            [Out, MarshalAs(UnmanagedType.IUnknown)] out IDWriteFontFileLoader fontFileLoader
        );

        /// <summary>
        /// Analyzes a file and returns whether it represents a font, and whether the font type is supported by the font system.
        /// </summary>
        /// <param name="isSupportedFontType">TRUE if the font type is supported by the font system, FALSE otherwise.</param>
        /// <param name="fontFileType">The type of the font file. Note that even if isSupportedFontType is FALSE,
        /// the fontFileType value may be different from DWRITE_FONT_FILE_TYPE_UNKNOWN.</param>
        /// <param name="fontFaceType">The type of the font face that can be constructed from the font file.
        /// Note that even if isSupportedFontType is FALSE, the fontFaceType value may be different from
        /// DWRITE_FONT_FACE_TYPE_UNKNOWN.</param>
        /// <param name="numberOfFaces">Number of font faces contained in the font file.</param>
        /// <returns>
        /// Standard HRESULT error code if there was a processing error during analysis.
        /// </returns>
        /// <remarks>
        /// IMPORTANT: certain font file types are recognized, but not supported by the font system.
        /// For example, the font system will recognize a file as a Type 1 font file,
        /// but will not be able to construct a font face object from it. In such situations, Analyze will set
        /// isSupportedFontType output parameter to FALSE.
        /// </remarks>
        unsafe abstract void Analyze(
            [Out] BOOL* isSupportedFontType,
            [Out] DWRITE_FONT_FILE_TYPE* fontFileType,
            [Out, Optional] DWRITE_FONT_FACE_TYPE* fontFaceType,
            [Out] UINT32* numberOfFaces
        );
    }
}
