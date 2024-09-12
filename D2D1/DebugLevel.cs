namespace Win32.D2D1;

/// <summary>
/// Indicates the type of information provided by the Direct2D Debug Layer.
/// </summary>
public enum DebugLevel : DWORD
{
    /// <summary>
    /// Direct2D does not produce any debugging output.
    /// </summary>
    NONE = 0,
    /// <summary>
    /// Direct2D sends error messages to the debug layer.
    /// </summary>
    ERROR = 1,
    /// <summary>
    /// Direct2D sends error messages and warnings to the debug layer.
    /// </summary>
    WARNING = 2,
    /// <summary>
    /// Direct2D sends error messages, warnings, and additional diagnostic
    /// information that can help improve performance to the debug layer.
    /// </summary>
    INFORMATION = 3,
}
