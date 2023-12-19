using System.Runtime.InteropServices;

namespace Win32.DWrite
{
    /// <summary>
    /// The format of text used for text layout.
    /// </summary>
    /// <remarks>
    /// This object may not be thread-safe and it may carry the state of text format change.
    /// </remarks>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("9c906818-31d7-4fd3-a151-7c5e225db55a")]
    [SupportedOSPlatform("windows")]
    public interface IDWriteTextFormat
    {
        /// <summary>
        /// Set alignment option of text relative to layout box's leading and trailing edge.
        /// </summary>
        /// <param name="textAlignment">Text alignment option</param>
        /// <returns>
        /// Standard HRESULT error code.
        /// </returns>
        abstract void SetTextAlignment(
            DWRITE_TEXT_ALIGNMENT textAlignment
        );

        /// <summary>
        /// Set alignment option of paragraph relative to layout box's top and bottom edge.
        /// </summary>
        /// <param name="paragraphAlignment">Paragraph alignment option</param>
        /// <returns>
        /// Standard HRESULT error code.
        /// </returns>
        abstract void SetParagraphAlignment(
            DWRITE_PARAGRAPH_ALIGNMENT paragraphAlignment
        );

        /// <summary>
        /// Set word wrapping option.
        /// </summary>
        /// <param name="wordWrapping">Word wrapping option</param>
        /// <returns>
        /// Standard HRESULT error code.
        /// </returns>
        abstract void SetWordWrapping(
            DWRITE_WORD_WRAPPING wordWrapping
        );

        /// <summary>
        /// Set paragraph reading direction.
        /// </summary>
        /// <param name="readingDirection">Text reading direction</param>
        /// <returns>
        /// Standard HRESULT error code.
        /// </returns>
        /// <remarks>
        /// The flow direction must be perpendicular to the reading direction.
        /// Setting both to a vertical direction or both to horizontal yields
        /// DWRITE_E_FLOWDIRECTIONCONFLICTS when calling GetMetrics or Draw.
        /// </remark>
        abstract void SetReadingDirection(
            DWRITE_READING_DIRECTION readingDirection
        );

        /// <summary>
        /// Set paragraph flow direction.
        /// </summary>
        /// <param name="flowDirection">Paragraph flow direction</param>
        /// <returns>
        /// Standard HRESULT error code.
        /// </returns>
        /// <remarks>
        /// The flow direction must be perpendicular to the reading direction.
        /// Setting both to a vertical direction or both to horizontal yields
        /// DWRITE_E_FLOWDIRECTIONCONFLICTS when calling GetMetrics or Draw.
        /// </remark>
        abstract void SetFlowDirection(
            DWRITE_FLOW_DIRECTION flowDirection
        );

        /// <summary>
        /// Set incremental tab stop position.
        /// </summary>
        /// <param name="incrementalTabStop">The incremental tab stop value</param>
        /// <returns>
        /// Standard HRESULT error code.
        /// </returns>
        abstract void SetIncrementalTabStop(
            FLOAT incrementalTabStop
        );

        /// <summary>
        /// Set trimming options for any trailing text exceeding the layout width
        /// or for any far text exceeding the layout height.
        /// </summary>
        /// <param name="trimmingOptions">Text trimming options.</param>
        /// <param name="trimmingSign">Application-defined omission sign. This parameter may be NULL if no trimming sign is desired.</param>
        /// <remarks>
        /// Any inline object can be used for the trimming sign, but CreateEllipsisTrimmingSign
        /// provides a typical ellipsis symbol. Trimming is also useful vertically for hiding
        /// partial lines.
        /// </remarks>
        /// <returns>
        /// Standard HRESULT error code.
        /// </returns>
        abstract unsafe void SetTrimming(
            [In] DWRITE_TRIMMING* trimmingOptions,
            [In, Optional, MarshalAs(UnmanagedType.IUnknown)] object trimmingSign
        );

        /// <summary>
        /// Set line spacing.
        /// </summary>
        /// <param name="lineSpacingMethod">How to determine line height.</param>
        /// <param name="lineSpacing">The line height, or rather distance between one baseline to another.</param>
        /// <param name="baseline">Distance from top of line to baseline. A reasonable ratio to lineSpacing is 80%.</param>
        /// <remarks>
        /// For the default method, spacing depends solely on the content.
        /// For uniform spacing, the given line height will override the content.
        /// </remarks>
        /// <returns>
        /// Standard HRESULT error code.
        /// </returns>
        abstract void SetLineSpacing(
            DWRITE_LINE_SPACING_METHOD lineSpacingMethod,
            FLOAT lineSpacing,
            FLOAT baseline
        );

        /// <summary>
        /// Get alignment option of text relative to layout box's leading and trailing edge.
        /// </summary>
        abstract DWRITE_TEXT_ALIGNMENT GetTextAlignment();

        /// <summary>
        /// Get alignment option of paragraph relative to layout box's top and bottom edge.
        /// </summary>
        abstract DWRITE_PARAGRAPH_ALIGNMENT GetParagraphAlignment();

        /// <summary>
        /// Get word wrapping option.
        /// </summary>
        abstract DWRITE_WORD_WRAPPING GetWordWrapping();

        /// <summary>
        /// Get paragraph reading direction.
        /// </summary>
        abstract DWRITE_READING_DIRECTION GetReadingDirection();

        /// <summary>
        /// Get paragraph flow direction.
        /// </summary>
        abstract DWRITE_FLOW_DIRECTION GetFlowDirection();

        /// <summary>
        /// Get incremental tab stop position.
        /// </summary>
        abstract FLOAT GetIncrementalTabStop();

        /// <summary>
        /// Get trimming options for text overflowing the layout width.
        /// </summary>
        /// <param name="trimmingOptions">Text trimming options.</param>
        /// <param name="trimmingSign">Trimming omission sign. This parameter may be NULL.</param>
        /// <returns>
        /// Standard HRESULT error code.
        /// </returns>
        abstract unsafe void GetTrimming(
            [Out] DWRITE_TRIMMING* trimmingOptions,
            [Out, MarshalAs(UnmanagedType.IUnknown)] out object trimmingSign
        );

        /// <summary>
        /// Get line spacing.
        /// </summary>
        /// <param name="lineSpacingMethod">How line height is determined.</param>
        /// <param name="lineSpacing">The line height, or rather distance between one baseline to another.</param>
        /// <param name="baseline">Distance from top of line to baseline.</param>
        /// <returns>
        /// Standard HRESULT error code.
        /// </returns>
        abstract unsafe void GetLineSpacing(
            [Out] DWRITE_LINE_SPACING_METHOD* lineSpacingMethod,
            [Out] FLOAT* lineSpacing,
            [Out] FLOAT* baseline
        );

        /// <summary>
        /// Get the font collection.
        /// </summary>
        /// <param name="fontCollection">The current font collection.</param>
        /// <returns>
        /// Standard HRESULT error code.
        /// </returns>
        abstract void GetFontCollection(
            [Out, MarshalAs(UnmanagedType.IUnknown)] out IDWriteFontCollection fontCollection
        );

        /// <summary>
        /// Get the length of the font family name, in characters, not including the terminating NULL character.
        /// </summary>
        abstract UINT32 GetFontFamilyNameLength();

        /// <summary>
        /// Get a copy of the font family name.
        /// </summary>
        /// <param name="fontFamilyName">Character array that receives the current font family name</param>
        /// <param name="nameSize">Size of the character array in character count including the terminated NULL character.</param>
        /// <returns>
        /// Standard HRESULT error code.
        /// </returns>
        abstract unsafe void GetFontFamilyName(
            WCHAR* fontFamilyName,
            UINT32 nameSize
        );

        /// <summary>
        /// Get the font weight.
        /// </summary>
        abstract DWRITE_FONT_WEIGHT GetFontWeight();

        /// <summary>
        /// Get the font style.
        /// </summary>
        abstract DWRITE_FONT_STYLE GetFontStyle();

        /// <summary>
        /// Get the font stretch.
        /// </summary>
        abstract DWiteFontStretch GetFontStretch();

        /// <summary>
        /// Get the font em height.
        /// </summary>
        abstract FLOAT GetFontSize();

        /// <summary>
        /// Get the length of the locale name, in characters, not including the terminating NULL character.
        /// </summary>
        abstract UINT32 GetLocaleNameLength();

        /// <summary>
        /// Get a copy of the locale name.
        /// </summary>
        /// <param name="localeName">Character array that receives the current locale name</param>
        /// <param name="nameSize">Size of the character array in character count including the terminated NULL character.</param>
        /// <returns>
        /// Standard HRESULT error code.
        /// </returns>
        abstract unsafe void GetLocaleName(
            WCHAR* localeName,
            UINT32 nameSize
        );
    }
}
