namespace Win32.LowLevel
{
    public static class GA
    {
        /// <summary>
        /// Retrieves the parent window.This does not include the owner,
        /// as it does with the <see cref="User32.GetParent"/> function.
        /// </summary>
        public const UINT PARENT = 1;
        /// <summary>
        /// Retrieves the root window by walking the chain of parent windows.
        /// </summary>
        public const UINT ROOT = 2;
        /// <summary>
        /// Retrieves the owned root window by walking the chain of parent
        /// and owner windows returned by <see cref="User32.GetParent"/>.
        /// </summary>
        public const UINT ROOTOWNER = 3;
    }
}
