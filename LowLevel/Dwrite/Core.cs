using System.Runtime.InteropServices;

namespace Win32.DWrite.LowLevel
{
    [SupportedOSPlatform("windows")]
    public static class Dwrite
    {
        [DllImport("Dwrite.dll", SetLastError = true)]
        public static extern unsafe HRESULT DWriteCreateFactory(
          [In] DWriteFactoryType factoryType,
          [In] REFIID iid,
          [Out] void** factory
        );

        [DllImport("Dwrite.dll", SetLastError = true)]
        public static extern unsafe HRESULT DWriteCreateFactory(
          [In] DWriteFactoryType factoryType,
          [In] REFIID iid,
          [Out, MarshalAs(UnmanagedType.IUnknown)] out object factory
        );

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
        /// <param name=""></param>
        /// <returns></returns>
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
