namespace Win32
{
    public class WindowsException : Exception
    {
        public readonly uint Code;
        readonly string? _message;

        public override string Message => $"{_message ?? "Unknown exception"} (error code {Code})";

        public WindowsException(string message, uint code) : base()
        {
            Code = code;
            _message = message;
        }

        public WindowsException(uint code) : base()
        {
            Code = code;
            _message = null;
        }

        unsafe public static WindowsException Get()
        {
            uint errorCode = Kernel32.GetLastError();

            string? result = WindowsException.GetMessage(errorCode);

            if (!string.IsNullOrWhiteSpace(result))
            { return new WindowsException(result, errorCode); }

            return new WindowsException($"Unknown exception {errorCode}", errorCode);
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
                { throw new WindowsException($"Failed to format error message", Kernel32.GetLastError()); }

                string result = new(buffer, 0, (int)messageLength);
                return result.Trim();
            }
        }

        public void ShowMessageBox() => User32.MessageBox(HWND.Zero, Message, $"Win32 Exception", (uint)MessageBoxIcon.MB_ICONERROR | (uint)MessageBoxButton.MB_OK);
    }
}
