using System.Runtime.InteropServices;

#pragma warning disable SYSLIB1054 // Use 'LibraryImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time

namespace Win32.DWrite.LowLevel
{
    public static class Dwrite
    {
        [DllImport("Dwrite.dll", SetLastError = true)]
        unsafe public static extern HRESULT DWriteCreateFactory(
          [In] DWriteFactoryType factoryType,
          [In] REFIID iid,
          [Out] void** factory
        );

        [DllImport("Dwrite.dll", SetLastError = true)]
        unsafe public static extern HRESULT DWriteCreateFactory(
          [In] DWriteFactoryType factoryType,
          [In] REFIID iid,
          [Out, MarshalAs(UnmanagedType.IUnknown)] out object factory
        );

        unsafe public static HRESULT DWriteCreateFactory<T>(
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
        unsafe public static extern HRESULT CreateTextFormat(
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
