namespace Win32
{
    [Serializable]
	public class NotWindowsException : Exception
	{
		public NotWindowsException() { }
		public NotWindowsException(string message) : base(message) { }
		public NotWindowsException(string message, Exception inner) : base(message, inner) { }
		protected NotWindowsException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
