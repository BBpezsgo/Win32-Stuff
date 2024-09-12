namespace Win32.Forms;

public enum PeekMessageFlags : uint
{
    /// <summary>
    /// Messages are not removed from the queue after processing by PeekMessage.
    /// </summary>
    NOREMOVE = 0x0000,
    /// <summary>
    /// Messages are removed from the queue after processing by PeekMessage.
    /// </summary>
    REMOVE = 0x0001,
    /// <summary>
    /// <para>
    /// Prevents the system from releasing any thread that is waiting for the caller to go idle (see WaitForInputIdle).
    /// </para>
    /// <para>
    /// Combine this value with either PM_NOREMOVE or PM_REMOVE.
    /// </para>
    /// </summary>
    NOYIELD = 0x0002,
}
