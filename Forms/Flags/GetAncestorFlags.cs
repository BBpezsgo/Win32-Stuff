namespace Win32.Forms;

public enum GetAncestorFlags : UINT
{
    /// <summary>
    /// Retrieves the parent window.This does not include the owner,
    /// as it does with the <see cref="User32.GetParent"/> function.
    /// </summary>
    Parent = 1,
    /// <summary>
    /// Retrieves the root window by walking the chain of parent windows.
    /// </summary>
    Root = 2,
    /// <summary>
    /// Retrieves the owned root window by walking the chain of parent
    /// and owner windows returned by <see cref="User32.GetParent"/>.
    /// </summary>
    RootOwner = 3,
}
