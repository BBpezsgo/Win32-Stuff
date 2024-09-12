namespace Win32.D2D1;

/// <summary>
/// Specifies whether Direct2D provides synchronization for an <see cref="ID2D1Factory"/>
/// and the resources it creates, so that they may be safely accessed
/// from multiple threads.
/// </summary>
public enum FactoryType : DWORD
{
    /// <summary>
    /// No synchronization is provided for accessing or writing to the
    /// factory or the objects it creates. If the factory or the objects
    /// are called from multiple threads, it is up to the application to
    /// provide access locking.
    /// </summary>
    SingleThreaded = 0,
    /// <summary>
    /// Direct2D provides synchronization for accessing and writing to the
    /// factory and the objects it creates, enabling safe access
    /// from multiple threads.
    /// </summary>
    MultiThreaded = 1,
}
