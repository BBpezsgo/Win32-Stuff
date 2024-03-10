namespace Win32;

[SuppressMessage("Design", "CA1027:Mark enums with FlagsAttribute")]
public enum MessageBoxButton : uint
{
    /// <summary>
    /// The message box contains one push button: OK. This is the default.
    /// </summary>
    Ok = 0x00000000,

    /// <summary>
    /// The message box contains two push buttons: OK and Cancel.
    /// </summary>
    OkCancel = 0x00000001,

    /// <summary>
    /// The message box contains three push buttons: Abort, Retry, and Ignore.
    /// </summary>
    AbortRetryIgnore = 0x00000002,

    /// <summary>
    /// The message box contains three push buttons: Yes, No, and Cancel.
    /// </summary>
    YesNoCancel = 0x00000003,

    /// <summary>
    /// The message box contains two push buttons: Yes and No.
    /// </summary>
    YesNo = 0x00000004,

    /// <summary>
    /// The message box contains two push buttons: Retry and Cancel.
    /// </summary>
    RetryCancel = 0x00000005,

    /// <summary>
    /// The message box contains three push buttons: Cancel, Try Again, Continue. Use this message box type instead of MB_ABORTRETRYIGNORE.
    /// </summary>
    CancelRetryContinue = 0x00000006,

    /// <summary>
    /// Adds a Help button to the message box. When the user clicks the Help button or presses F1, the system sends a WM_HELP message to the owner.
    /// </summary>
    Help = 0x00004000,
}

[SuppressMessage("Design", "CA1027:Mark enums with FlagsAttribute")]
public enum MessageBoxIcon : uint
{
    /// <summary>
    /// A stop-sign icon appears in the message box.
    /// </summary>
    Error = 0x00000010,

    /// <summary>
    /// A question-mark icon appears in the message box.
    /// </summary>
    [Obsolete("""
    The question-mark message icon is no longer recommended because
    it does not clearly represent a specific type of message and
    because the phrasing of a message as a question could apply
    to any message type. In addition, users can confuse the message
    symbol question mark with Help information.
    Therefore, do not use this question mark message symbol in
    your message boxes. The system continues to support
    its inclusion only for backward compatibility.
    """)]
    Question = 0x00000020,

    /// <summary>
    /// An exclamation-point icon appears in the message box.
    /// </summary>
    Warning = 0x00000030,

    /// <summary>
    /// An icon consisting of a lowercase letter i in a circle appears in the message box.
    /// </summary>
    Information = 0x00000040,
}

[SuppressMessage("Design", "CA1027:Mark enums with FlagsAttribute")]
public enum MessageBoxDefaultButton : uint
{
    /// <summary>
    /// The first button is the default button.
    /// <see cref="Button1"/> is the default unless <see cref="Button2"/>,
    /// <see cref="Button3"/>, or <see cref="Button4"/> is specified.
    /// </summary>
    Button1 = 0x00000000,

    /// <summary>
    /// The second button is the default button.
    /// </summary>
    Button2 = 0x00000100,

    /// <summary>
    /// The third button is the default button.
    /// </summary>
    Button3 = 0x00000200,

    /// <summary>
    /// The fourth button is the default button.
    /// </summary>
    Button4 = 0x00000300,
}

[SuppressMessage("Design", "CA1027:Mark enums with FlagsAttribute")]
public enum MessageBoxModality : uint
{
    /// <summary>
    /// <para>
    /// The user must respond to the message box before continuing work in the
    /// window identified by the <c>hWnd</c> parameter. However, the user can move to
    /// the windows of other threads and work in those windows.
    /// </para>
    /// <para>
    /// Depending on the hierarchy of windows in the application, the user may be
    /// able to move to other windows within the thread. All child windows of the
    /// parent of the message box are automatically disabled, but pop-up windows are not.
    /// </para>
    /// <para>
    /// <see cref="Application"/> is the default if neither <see cref="System"/>
    /// nor <see cref="Task"/> is specified.
    /// </para>
    /// </summary>
    Application = 0x00000000,

    /// <summary>
    /// Same as <see cref="Application"/> except that the message box has the
    /// <c>WS_EX_TOPMOST</c> style. Use system-modal message boxes to notify the user
    /// of serious, potentially damaging errors that require immediate attention
    /// (for example, running out of memory). This flag has no effect on the user's
    /// ability to interact with windows other than those associated with <c>hWnd</c>.
    /// </summary>
    System = 0x00001000,

    /// <summary>
    /// Same as <see cref="Application"/> except that all the top-level windows
    /// belonging to the current thread are disabled if the <c>hWnd</c> parameter is <see langword="NULL"/>.
    /// Use this flag when the calling application or library does not have a
    /// window handle available but still needs to prevent input to other windows
    /// in the calling thread without suspending other threads.
    /// </summary>
    Task = 0x00002000,
}

public enum MessageBoxResult : int
{
    /// <summary>
    /// The OK button was selected.
    /// </summary>
    Ok = 1,

    /// <summary>
    /// The Cancel button was selected.
    /// </summary>
    Cancel = 2,

    /// <summary>
    /// The Abort button was selected.
    /// </summary>
    Abort = 3,

    /// <summary>
    /// The Retry button was selected.
    /// </summary>
    Retry = 4,

    /// <summary>
    /// The Ignore button was selected.
    /// </summary>
    Ignore = 5,

    /// <summary>
    /// The Yes button was selected.
    /// </summary>
    Yes = 6,

    /// <summary>
    /// The No button was selected.
    /// </summary>
    No = 7,

    /// <summary>
    /// The Try Again button was selected.
    /// </summary>
    TryAgain = 10,

    /// <summary>
    /// The Continue button was selected.
    /// </summary>
    Continue = 11,
}
