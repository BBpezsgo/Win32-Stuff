namespace Win32
{
    public class MciException : Exception
    {
        public MciException(MCIERROR error) : base($"MCI Error {error}") { }
    }
}
