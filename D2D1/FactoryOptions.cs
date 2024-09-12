namespace Win32.D2D1;

/// <summary>
/// Contains the debugging level of an <see cref="ID2D1Factory"/> object.
/// </summary>
/// <remarks>
/// To enable debugging, you must install the
/// <see href="https://learn.microsoft.com/en-us/windows/win32/Direct2D/direct2ddebuglayer-overview">Direct2D Debug Layer</see>.
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
public struct FactoryOptions
{
    /// <summary>
    /// The debugging level of the <see cref="ID2D1Factory"/> object.
    /// </summary>
    public DebugLevel DebugLevel;
}
