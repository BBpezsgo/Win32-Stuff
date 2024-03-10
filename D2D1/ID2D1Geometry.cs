namespace Win32.D2D1;

/// <summary>
/// Represents a geometry resource and defines a set of helper methods for
/// manipulating and measuring geometric shapes. Interfaces that inherit from
/// <see cref="ID2D1Geometry"/> define specific shapes.
/// </summary>
[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("2cd906a1-12e2-11dc-9fed-001143a055f9")]
[SupportedOSPlatform("windows")]
public interface ID2D1Geometry : ID2D1Resource;
