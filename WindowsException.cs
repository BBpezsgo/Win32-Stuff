using System.Text;

namespace Win32
{
    public class WindowsException : Exception
    {
        public readonly uint Code;

        public WindowsException(string message, uint code) : base(message)
        {
            Code = code;
        }

        public static WindowsException GetException()
        {
            uint errorCode = Kernel32.GetLastError();

            uint formatResult = Kernel32.FormatMessage(
                FormatMessageAttributes.FORMAT_MESSAGE_ALLOCATE_BUFFER | FormatMessageAttributes.FORMAT_MESSAGE_FROM_SYSTEM | FormatMessageAttributes.FORMAT_MESSAGE_IGNORE_INSERTS,
                IntPtr.Zero,
                errorCode,
                0,
                out StringBuilder message,
                0,
                IntPtr.Zero);

            if (formatResult == 0)
            { return new WindowsException(message.ToString(), errorCode); }
            else
            { return new WindowsException($"Unknown exception {errorCode}", errorCode); }
        }
    }

}
