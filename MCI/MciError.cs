namespace Win32.MCI;

public class MciException : Exception
{
    public MciException(MCIERROR error) : base($"MCI Error {error}") { }
}
