namespace Win32.Common
{
    public class GeneralException : Exception
    {
        public GeneralException(string message) : base(message) { }
        public GeneralException(string message, Exception inner) : base(message, inner) { }
    }
}
