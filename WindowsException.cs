namespace Win32.Common
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
            if (result == null && (errorCode & APP_CODE_MASK) != 0 && messages.TryGetValue(errorCode ^ APP_CODE_MASK, out string? userDefinedMessage))
            { result = userDefinedMessage; }
            return new WindowsException(result, errorCode);
        }

        const int MESSAGE_BUFFER_SIZE = 255;

        [SupportedOSPlatform("windows")]
        public static unsafe string? GetMessage(uint errorCode)
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

        [SupportedOSPlatform("windows")]
        public void ShowMessageBox() => User32.MessageBox(HWND.Zero, Message, $"Win32 Exception", (uint)MessageBoxIcon.MB_ICONERROR | (uint)MessageBoxButton.MB_OK);
    }

    public struct FormatMessageAttributes
    {
        /// <remarks>
        /// <para>
        /// The function allocates a buffer large enough to hold the formatted message,
        /// and places a pointer to the allocated buffer at the address specified
        /// by <c>lpBuffer</c>. The <c>lpBuffer</c> parameter is a pointer to an <see cref="WCHAR"/>*;
        /// you must cast the pointer to an <see cref="WCHAR"/>*.
        /// The <c>nSize</c> parameter specifies the minimum number of <see cref="WCHAR"/>s to allocate
        /// for an output message buffer. The caller should use the <see cref="Kernel32.LocalFree"/> function
        /// to free the buffer when it is no longer needed.
        /// </para>
        /// <para>
        /// If the length of the formatted message exceeds 128K bytes,
        /// then <see cref="Kernel32.FormatMessage"/> will fail and a subsequent
        /// call to <see cref="Kernel32.GetLastError"/> will return <c>ERROR_MORE_DATA</c>.
        /// </para>
        /// <para>
        /// In previous versions of Windows, this value was not available
        /// for use when compiling Windows Store apps. As of Windows 10 this value can be used.
        /// </para>
        /// <para>
        /// <b>Windows Server 2003 and Windows XP:</b><br/>
        /// If the length of the formatted message exceeds 128K bytes,
        /// then <see cref="Kernel32.FormatMessage"/> will not automatically
        /// fail with an error of <c>ERROR_MORE_DATA</c>.
        /// </para>
        /// </remarks>
        public const int FORMAT_MESSAGE_ALLOCATE_BUFFER = 0x100;
        public const int FORMAT_MESSAGE_IGNORE_INSERTS = 0x200;
        /// <remarks>
        /// <para>
        /// The <c>lpSource</c> parameter is a pointer to a null-terminated
        /// string that contains a message definition. The message definition
        /// may contain insert sequences, just as the message text in a message
        /// table resource may. This flag cannot be used with
        /// <see cref="FORMAT_MESSAGE_FROM_HMODULE"/> or <see cref="FORMAT_MESSAGE_FROM_SYSTEM"/>.
        /// </para>
        /// </remarks>
        public const int FORMAT_MESSAGE_FROM_STRING = 0x400;
        /// <remarks>
        /// <para>
        /// The <c>lpSource</c> parameter is a module handle containing
        /// the message-table resource(s) to search. If this <c>lpSource</c>
        /// handle is <see langword="null"/>, the current process's application
        /// image file will be searched. This flag cannot be used with <see cref="FORMAT_MESSAGE_FROM_STRING"/>.
        /// </para>
        /// <para>
        /// If the module has no message table resource, the function
        /// fails with <c>ERROR_RESOURCE_TYPE_NOT_FOUND</c>.
        /// </para>
        /// </remarks>
        public const int FORMAT_MESSAGE_FROM_HMODULE = 0x800;
        /// <remarks>
        /// <para>
        /// The function should search the system message-table resource(s)
        /// for the requested message. If this flag is specified with
        /// <see cref="FORMAT_MESSAGE_FROM_HMODULE"/>, the function
        /// searches the system message table if the message is not
        /// found in the module specified by <c>lpSource</c>.
        /// This flag cannot be used with <see cref="FORMAT_MESSAGE_FROM_STRING"/>.
        /// </para>
        /// <para>
        /// If this flag is specified, an application can pass the result
        /// of the <see cref="Kernel32.GetLastError"/> function to retrieve
        /// the message text for a system-defined error.
        /// </para>
        /// </remarks>
        public const int FORMAT_MESSAGE_FROM_SYSTEM = 0x1000;
        /// <remarks>
        /// <para>
        /// The <i>Arguments</i> parameter is not a <c>va_list</c>
        /// structure, but is a pointer to an array of values that represent the arguments.
        /// </para>
        /// <para>
        /// This flag cannot be used with 64-bit integer values.
        /// If you are using a 64-bit integer, you must use the <c>va_list</c> structure.
        /// </para>
        /// </remarks>
        public const int FORMAT_MESSAGE_ARGUMENT_ARRAY = 0x2000;
        public const int FORMAT_MESSAGE_MAX_WIDTH_MASK = 0xff;
    }
}
