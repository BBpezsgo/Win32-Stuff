namespace Win32.Net;

[SupportedOSPlatform("windows")]
public readonly struct IcmpHandle : IDisposable, IEquatable<IcmpHandle>
{
    readonly HANDLE Handle;

    IcmpHandle(HANDLE handle) => Handle = handle;

    /// <exception cref="WindowsException"/>
    public static IcmpHandle Create()
    {
        HANDLE handle = Icmp.IcmpCreateFile();
        if (handle == Kernel32.InvalidHandle)
        { throw WindowsException.Get(); }
        return new IcmpHandle(handle);
    }

    /// <exception cref="WindowsException"/>
    public void Dispose()
    {
        if (Icmp.IcmpCloseHandle(Handle) != TRUE)
        { throw WindowsException.Get(); }
    }

    /// <exception cref="WindowsException"/>
    public static unsafe void ParseReplies(void* replyBuffer, uint replySize)
    {
        uint n = Icmp.IcmpParseReplies(replyBuffer, replySize);
        if (n == 0)
        { throw WindowsException.Get(); }
    }

    /// <exception cref="WindowsException"/>
    public unsafe int SendEcho(
        IpAddress destinationAddress,
        void* requestData,
        int requestSize,
        void* replyBuffer,
        int replySize,
        DWORD timeout,
        IpOptionInformation* requestOptions = null)
    {
        uint n = Icmp.IcmpSendEcho(
            Handle,
            destinationAddress,
            requestData,
            (ushort)requestSize,
            requestOptions,
            replyBuffer,
            (uint)replySize,
            timeout);
        if (n == 0)
        { throw WindowsException.Get(); }
        return (int)n;
    }

    /// <exception cref="WindowsException"/>
    public unsafe int SendEcho(
        IpAddress destinationAddress,
        ReadOnlySpan<byte> requestBuffer,
        ReadOnlySpan<byte> replyBuffer,
        DWORD timeout,
        IpOptionInformation* requestOptions = null)
    {
        fixed (byte* requestBufferPtr = &requestBuffer.GetPinnableReference())
        fixed (byte* replyBufferPtr = &replyBuffer.GetPinnableReference())
        {
            return SendEcho(
                destinationAddress,
                requestBufferPtr,
                requestBuffer.Length,
                replyBufferPtr,
                replyBuffer.Length,
                timeout,
                requestOptions);
        }
    }

    /// <exception cref="WindowsException"/>
    public unsafe void SendEcho(
        IpAddress destinationAddress,
        ReadOnlySpan<byte> requestBuffer,
        out IcmpEchoReply reply,
        DWORD timeout,
        IpOptionInformation* requestOptions = null)
    {
        int replyBufferSize = sizeof(IcmpEchoReply) + requestBuffer.Length + 8;
        byte* replyBufferPtr = stackalloc byte[replyBufferSize];
        int n;
        fixed (byte* requestBufferPtr = &requestBuffer.GetPinnableReference())
        {
            n = SendEcho(
                destinationAddress,
                requestBufferPtr,
                requestBuffer.Length,
                replyBufferPtr,
                replyBufferSize,
                timeout,
                requestOptions);
        }
        reply = ((IcmpEchoReply*)replyBufferPtr)[0];
    }

    public override bool Equals(object? obj) => obj is IcmpHandle handle && Equals(handle);
    public bool Equals(IcmpHandle other) => Handle == other.Handle;
    public override int GetHashCode() => Handle.GetHashCode();

    public static bool operator ==(IcmpHandle left, IcmpHandle right) => left.Equals(right);
    public static bool operator !=(IcmpHandle left, IcmpHandle right) => !left.Equals(right);
}
