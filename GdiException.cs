namespace Win32
{

    [Serializable]
	public class GdiException : Exception
	{
		public GdiException() { }
		public GdiException(string message) : base(message) { }
		public GdiException(string message, Exception inner) : base(message, inner) { }
		protected GdiException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
