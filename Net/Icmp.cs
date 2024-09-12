namespace Win32.Net;

[SupportedOSPlatform("windows")]
public static partial class Icmp
{
    [LibraryImport("Icmp.dll")]
    public static partial HANDLE IcmpCreateFile();

    [LibraryImport("Icmp.dll")]
    public static partial BOOL IcmpCloseHandle(HANDLE IcmpHandle);

    [LibraryImport("Icmp.dll")]
    public static unsafe partial DWORD IcmpSendEcho(
      HANDLE IcmpHandle,
      IpAddress DestinationAddress,
      void* RequestData,
      WORD RequestSize,
      [Optional] IpOptionInformation* RequestOptions,
      void* ReplyBuffer,
      DWORD ReplySize,
      DWORD Timeout
    );

    [LibraryImport("Icmp.dll")]
    public static unsafe partial DWORD IcmpSendEcho2(
      HANDLE IcmpHandle,
      [Optional] HANDLE Event,
      [Optional] FARPROC* ApcRoutine,
      [Optional] void* ApcContext,
      IpAddress DestinationAddress,
      void* RequestData,
      WORD RequestSize,
      [Optional] IpOptionInformation* RequestOptions,
      void* ReplyBuffer,
      DWORD ReplySize,
      DWORD Timeout
    );

    [LibraryImport("Icmp.dll")]
    public static unsafe partial DWORD IcmpSendEcho2Ex(
      HANDLE IcmpHandle,
      HANDLE Event,
      [Optional] FARPROC* ApcRoutine,
      [Optional] void* ApcContext,
      IpAddress SourceAddress,
      IpAddress DestinationAddress,
      void* RequestData,
      WORD RequestSize,
      [Optional] IpOptionInformation* RequestOptions,
      void* ReplyBuffer,
      DWORD ReplySize,
      DWORD Timeout
    );

    [LibraryImport("Icmp.dll")]
    public static unsafe partial DWORD IcmpParseReplies(
      void* ReplyBuffer,
      DWORD ReplySize
    );
}
