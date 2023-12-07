using System.Runtime.InteropServices;

namespace Win32.COM
{
    /// <summary>
    /// The interface that represents text rendering settings for glyph rasterization and filtering.
    /// </summary>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("2f0da53a-2add-47cd-82ee-d9ec34688e75")]
    public interface IDWriteRenderingParams
    {
    }
}
