using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Win32.DWrite.LowLevel
{
    /// <summary>
    /// DirectWrite
    /// </summary>
    [SupportedOSPlatform("windows")]
    public static class Dwrite
    {
        /// <summary>
        /// Creates a DirectWrite factory object that is used for subsequent
        /// creation of individual DirectWrite objects.
        /// </summary>
        /// <param name="factoryType">
        /// A value that specifies whether the factory object will be shared or isolated.
        /// </param>
        /// <param name="iid">
        /// A GUID value that identifies the DirectWrite factory interface, such as <see langword="__uuidof"/>(<see cref="IDWriteFactory"/>).
        /// </param>
        /// <param name="factory">
        /// An address of a pointer to the newly created DirectWrite factory object.
        /// </param>
        /// <returns>
        /// If this function succeeds, it returns <see cref="HResult.OK"/>.
        /// Otherwise, it returns an <c>HRESULT</c> error code.
        /// </returns>
        [DllImport("Dwrite.dll", SetLastError = true)]
        public static extern unsafe HRESULT DWriteCreateFactory(
          [In] DWriteFactoryType factoryType,
          [In] REFIID iid,
          [Out] void** factory
        );

        /// <inheritdoc cref="DWriteCreateFactory(DWriteFactoryType, Guid, void**)"/>
        [DllImport("Dwrite.dll", SetLastError = true)]
        public static extern unsafe HRESULT DWriteCreateFactory(
          [In] DWriteFactoryType factoryType,
          [In] REFIID iid,
          [Out, MarshalAs(UnmanagedType.IUnknown)] out object factory
        );

        /// <inheritdoc cref="DWriteCreateFactory(DWriteFactoryType, Guid, void**)"/>
        [RequiresUnreferencedCode("COM interop")]
        public static unsafe HRESULT DWriteCreateFactory<T>(
            DWriteFactoryType factoryType,
            REFIID iid,
            out T? factory
        )
        {
            HRESULT result = DWriteCreateFactory(factoryType, iid, out object _factory);
            factory = (T?)_factory;
            return result;
        }

        /// <summary>
        /// Creates a text format object used for text layout.
        /// </summary>
        /// <param name="fontFamilyName">
        /// An array of characters that contains the name of the font family
        /// </param>
        /// <param name="fontCollection">
        /// A pointer to a font collection object.
        /// When this is , indicates the system font collection.
        /// </param>
        /// <param name="fontWeight">
        /// A value that indicates the font weight for the text object created by this method.
        /// </param>
        /// <param name="fontStyle">
        /// A value that indicates the font style for the text object created by this method.
        /// </param>
        /// <param name="fontStretch">
        /// A value that indicates the font stretch for the text object created by this method.
        /// </param>
        /// <param name="fontSize">
        /// The logical size of the font in DIP ("device-independent pixel") units.
        /// A DIP equals 1/96 inch.
        /// </param>
        /// <param name="localeName">
        /// An array of characters that contains the locale name.
        /// </param>
        /// <param name="textFormat">
        /// When this method returns, contains an address of a pointer to a
        /// newly created text format object, or <see langword="null"/> in case of failure.
        /// </param>
        /// <returns>
        /// If this method succeeds, it returns <see cref="HResult.OK"/>.
        /// Otherwise, it returns an <c>HRESULT</c> error code.
        /// </returns>
        [DllImport("Dwrite.dll", SetLastError = true)]
        public static extern unsafe HRESULT CreateTextFormat(
          [In] WCHAR* fontFamilyName,
          [MarshalAs(UnmanagedType.IUnknown)] IDWriteFontCollection fontCollection,
                DWRITE_FONT_WEIGHT fontWeight,
                DWRITE_FONT_STYLE fontStyle,
                DWiteFontStretch fontStretch,
                FLOAT fontSize,
          [In] WCHAR* localeName,
          [Out, MarshalAs(UnmanagedType.IUnknown)] out IDWriteTextFormat textFormat
        );
    }
}
