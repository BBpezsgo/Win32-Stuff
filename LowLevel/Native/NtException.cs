namespace Win32.LowLevel
{
    [Serializable]
    public class NtException : Exception
    {
        public NtException(NTSTATUS ntStatus) : base($"NT Error: {ntStatus}") { }

        protected NtException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        /// <exception cref="NtException"/>
        public static void EnsureSuccess(NTSTATUS ntStatus)
        {
            if (ntStatus != NtStatuses.Success)
            { throw new NtException(ntStatus); }
        }
    }
}
