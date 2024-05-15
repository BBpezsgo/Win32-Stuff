namespace Win32;

public class WindowsException : Exception
{
    public readonly uint Code;
    readonly string? message;
    public bool IsUserDefined => (Code & WindowsException.AppCodeMask) != 0;

    public const DWORD AppCodeMask = 0b_0010_0000_0000_0000_0000_0000_0000_0000;

    public override string Message
    {
        get
        {
            if (message == null)
            {
                if (IsUserDefined)
                {
                    return $"User defined exception {Code ^ AppCodeMask} (error code {Code})";
                }
                else
                {
                    return $"Unknown exception (error code {Code})";
                }
            }
            else
            {
                return $"{message} (error code {Code})";
            }
        }
    }

    public WindowsException(string? message, uint code) : base()
    {
        this.Code = code;
        this.message = message;
    }

    [SupportedOSPlatform("windows")]
    public static unsafe WindowsException Get()
    {
        uint errorCode = Kernel32.GetLastError();
        return WindowsException.Get(errorCode);
    }

    [SupportedOSPlatform("windows")]
    public static unsafe WindowsException Get(uint errorCode)
    {
        string? result = WindowsException.GetMessage(errorCode);
        return new WindowsException(result, errorCode);
    }

    [SupportedOSPlatform("windows")]
    public static unsafe WindowsException Get(IReadOnlyDictionary<uint, string> messages)
    {
        uint errorCode = Kernel32.GetLastError();
        string? result = WindowsException.GetMessage(errorCode);
        if (result == null && (errorCode & AppCodeMask) != 0 && messages.TryGetValue(errorCode ^ AppCodeMask, out string? userDefinedMessage))
        { result = userDefinedMessage; }
        return new WindowsException(result, errorCode);
    }

    const int MESSAGE_BUFFER_SIZE = 255;

    [SupportedOSPlatform("windows")]
    public static unsafe string? GetMessage(uint errorCode)
    {
        fixed (char* buffer = new string('\0', MESSAGE_BUFFER_SIZE))
        {
            uint messageLength = Kernel32.FormatMessageW(
                FormatMessageFlags.FromSystem |
                FormatMessageFlags.IgnoreInserts,
                IntPtr.Zero,
                errorCode,
                PrimaryLanguage.SystemDefault,
                buffer,
                MESSAGE_BUFFER_SIZE,
                IntPtr.Zero);

            if (messageLength == 0)
            { return null; }

            string result = new(buffer, 0, (int)messageLength);
            return result.Trim();
        }
    }

    [SupportedOSPlatform("windows")]
    public void ShowMessageBox() => User32.MessageBox(HWND.Zero, Message, "Win32 Exception", (uint)MessageBoxIcon.Error | (uint)MessageBoxButton.Ok);
}
