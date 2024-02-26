namespace Win32.Gdi32
{
    public class GdiException : Exception
    {
        public GdiException(string message) : base(message) { }
        public GdiException(string message, Exception inner) : base(message, inner) { }
    }
}
