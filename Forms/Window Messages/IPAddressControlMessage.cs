namespace Win32.Forms;

public static class IPAddressControlMessage
{
    /// <summary>
    /// no parameters
    /// </summary>
    public const uint CLEARADDRESS = WindowMessage.WM_USER + 100;
    /// <summary>
    /// lparam = TCP/IP address
    /// </summary>
    public const uint SETADDRESS = WindowMessage.WM_USER + 101;
    /// <summary>
    /// lresult = # of non black fields.  lparam = LPDWORD for TCP/IP address
    /// </summary>
    public const uint GETADDRESS = WindowMessage.WM_USER + 102;
    /// <summary>
    /// wparam = field, lparam = range
    /// </summary>
    public const uint SETRANGE = WindowMessage.WM_USER + 103;
    /// <summary>
    /// wparam = field
    /// </summary>
    public const uint SETFOCUS = WindowMessage.WM_USER + 104;
    /// <summary>
    /// no parameters
    /// </summary>
    public const uint ISBLANK = WindowMessage.WM_USER + 105;
}
