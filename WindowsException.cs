namespace Win32
{
    public class WindowsException : Exception
    {
        public readonly uint Code;
        readonly string? message;
        public bool IsUserDefined => (Code & WindowsException.APP_CODE_MASK) != 0;

        public const DWORD APP_CODE_MASK = 0b_0010_0000_0000_0000_0000_0000_0000_0000;

        public override string Message
        {
            get
            {
                if (message == null)
                {
                    if (IsUserDefined)
                    {
                        return $"User defined exception {Code ^ APP_CODE_MASK} (error code {Code})";
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

        unsafe public static WindowsException Get()
        {
            uint errorCode = Kernel32.GetLastError();
            return WindowsException.Get(errorCode);
        }

        unsafe public static WindowsException Get(uint errorCode)
        {
            string? result = WindowsException.GetMessage(errorCode);
            return new WindowsException(result, errorCode);
        }

        unsafe public static WindowsException Get(IReadOnlyDictionary<uint, string> messages)
        {
            uint errorCode = Kernel32.GetLastError();
            string? result = WindowsException.GetMessage(errorCode);
            if (result == null && (errorCode & APP_CODE_MASK) != 0 && messages.TryGetValue(errorCode ^ APP_CODE_MASK, out string? userDefinedMessage))
            { result = userDefinedMessage; }
            return new WindowsException(result, errorCode);
        }

        const int MESSAGE_BUFFER_SIZE = 255;

        unsafe public static string? GetMessage(uint errorCode)
        {
            fixed (char* buffer = new string('\0', MESSAGE_BUFFER_SIZE))
            {
                uint messageLength = Kernel32.FormatMessage(
                    FormatMessageAttributes.FORMAT_MESSAGE_FROM_SYSTEM |
                    FormatMessageAttributes.FORMAT_MESSAGE_IGNORE_INSERTS,
                    IntPtr.Zero,
                    errorCode,
                    LanguageMacros.LANG_SYSTEM_DEFAULT,
                    buffer,
                    MESSAGE_BUFFER_SIZE,
                    IntPtr.Zero);

                if (messageLength == 0)
                { return null; }

                string result = new(buffer, 0, (int)messageLength);
                return result.Trim();
            }
        }

        public void ShowMessageBox() => User32.MessageBox(HWND.Zero, Message, $"Win32 Exception", (uint)MessageBoxIcon.MB_ICONERROR | (uint)MessageBoxButton.MB_OK);
    }
}
