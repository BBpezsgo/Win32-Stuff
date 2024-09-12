namespace Win32.Native;

[StructLayout(LayoutKind.Sequential)]
public struct ClientId : IEquatable<ClientId>
{
    public HANDLE UniqueProcess;
    public HANDLE UniqueThread;

    public override readonly bool Equals(object? obj) => obj is ClientId id && Equals(id);
    public readonly bool Equals(ClientId other) =>
        UniqueProcess == other.UniqueProcess &&
        UniqueThread == other.UniqueThread;
    public override readonly int GetHashCode() => HashCode.Combine(UniqueProcess, UniqueThread);
    public override readonly string ToString() => $"(Process: {UniqueProcess} Thread: {UniqueThread})";

    public static bool operator ==(ClientId left, ClientId right) => left.Equals(right);
    public static bool operator !=(ClientId left, ClientId right) => !left.Equals(right);
}
