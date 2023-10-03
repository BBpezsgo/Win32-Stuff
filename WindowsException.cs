namespace Win32
{
    public class WindowsException : Exception
    {
        public readonly uint Code;

        public WindowsException(string message, uint code) : base(message)
        {
            Code = code;
        }

        unsafe public static WindowsException Get()
        {
            uint errorCode = Kernel32.GetLastError();
            char* buffer = null;

            uint formatResult = Kernel32.FormatMessage(
                FormatMessageAttributes.FORMAT_MESSAGE_ALLOCATE_BUFFER | FormatMessageAttributes.FORMAT_MESSAGE_FROM_SYSTEM | FormatMessageAttributes.FORMAT_MESSAGE_IGNORE_INSERTS,
                IntPtr.Zero,
                errorCode,
                0,
                buffer,
                0,
                IntPtr.Zero);

            if (formatResult == 0)
            {
                string result = new(buffer);
                Kernel32.LocalFree((HLOCAL)buffer);

                if (!string.IsNullOrWhiteSpace(result)) {
                    return new WindowsException(result, errorCode);
                }
            }
            
            return new WindowsException($"Unknown exception {errorCode}", errorCode);
        }
    }
}
