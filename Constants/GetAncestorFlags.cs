namespace Win32
{
    public static class GA
    {
        /// <summary>
        /// Retrieves the parent window.This does not include the owner,
        /// as it does with the <see cref="User32.GetParent"/> function.
        /// </summary>
        public const UINT GA_PARENT = 1;
        /// <summary>
        /// Retrieves the root window by walking the chain of parent windows.
        /// </summary>
        public const UINT GA_ROOT = 2;
        /// <summary>
        /// Retrieves the owned root window by walking the chain of parent
        /// and owner windows returned by <see cref="User32.GetParent"/>.
        /// </summary>
        public const UINT GA_ROOTOWNER = 3;
    }
}
