namespace Win32
{
    [Serializable]
	public class GeneralException : Exception
	{
		public GeneralException() { }
		public GeneralException(string message) : base(message) { }
		public GeneralException(string message, Exception inner) : base(message, inner) { }
		protected GeneralException(
          System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
