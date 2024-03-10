namespace Win32.Net;

public enum IpStatus : ULONG
{
    /// <summary>
    /// The status was success.
    /// </summary>
    Success = 0,
    /// <summary>
    /// The reply buffer was too small.
    /// </summary>
    BufferTooSmall = 11001,
    /// <summary>
    /// The destination network was unreachable.
    /// </summary>
    DestinationNetUnreachable = 11002,
    /// <summary>
    /// The destination host was unreachable.
    /// </summary>
    DestinationHostUnreachable = 11003,
    /// <summary>
    /// The destination protocol was unreachable.
    /// </summary>
    DestinationProtocolUnreachable = 11004,
    /// <summary>
    /// The destination port was unreachable.
    /// </summary>
    DestinationPortUnreachable = 11005,
    /// <summary>
    /// Insufficient IP resources were available.
    /// </summary>
    NoResources = 11006,
    /// <summary>
    /// A bad IP option was specified.
    /// </summary>
    BadOption = 11007,
    /// <summary>
    /// A hardware error occurred.
    /// </summary>
    HardwareError = 11008,
    /// <summary>
    /// The packet was too big.
    /// </summary>
    PacketTooLong = 11009,
    /// <summary>
    /// The request timed out.
    /// </summary>
    TimedOut = 11010,
    /// <summary>
    /// A bad request.
    /// </summary>
    BadRequest = 11011,
    /// <summary>
    /// A bad route.
    /// </summary>
    BadRoute = 11012,
    /// <summary>
    /// The time to live (TTL) expired in transit.
    /// </summary>
    TTLExpiredTransit = 11013,
    /// <summary>
    /// The time to live expired during fragment reassembly.
    /// </summary>
    TTLExpiredReassembly = 11014,
    /// <summary>
    /// A parameter problem.
    /// </summary>
    ParameterProblem = 11015,
    /// <summary>
    /// Datagrams are arriving too fast to be processed and datagrams may have been discarded.
    /// </summary>
    SourceQuench = 11016,
    /// <summary>
    /// An IP option was too big.
    /// </summary>
    OptionTooBig = 11017,
    /// <summary>
    /// A bad destination.
    /// </summary>
    BadDestination = 11018,
    /// <summary>
    /// A general failure. This error can be returned for some malformed ICMP packets.
    /// </summary>
    GeneralFailure = 11050,
}
