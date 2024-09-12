namespace Win32.Forms;

/// <summary>
/// Contains information about a notification message.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct NotificationMessageDetails
{
    /// <summary>
    /// A window handle to the control sending the message.
    /// </summary>
    public readonly HWND SenderWindow;
    /// <summary>
    /// An identifier of the control sending the message.
    /// </summary>
    public readonly UINT SenderControlId;
    /// <summary>
    /// A notification code.
    /// </summary>
    public readonly UINT Code;
}
