namespace Win32
{
    [Serializable]
    public class MciException : Exception
    {
        public MciException(MCIERROR error) : base($"MCI Error {error}")
        {

        }
    }
}
