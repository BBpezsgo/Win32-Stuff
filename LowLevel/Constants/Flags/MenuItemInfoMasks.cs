namespace Win32.LowLevel
{
    public static class MenuItemInfoMasks
    {
        /// <summary>
        /// Retrieves or sets the <c>hbmpItem</c> member.
        /// </summary>
        public const int BITMAP = 0x00000080;
        /// <summary>
        /// Retrieves or sets the <c>hbmpChecked</c> and <c>hbmpUnchecked</c> members.
        /// </summary>
        public const int CHECKMARKS = 0x00000008;
        /// <summary>
        /// Retrieves or sets the <c>dwItemData</c> member.
        /// </summary>
        public const int DATA = 0x00000020;
        /// <summary>
        /// Retrieves or sets the <see cref="MENUITEMINFOW.Type"/> member.
        /// </summary>
        public const int FTYPE = 0x00000100;
        /// <summary>
        /// Retrieves or sets the <c>wID</c> member.
        /// </summary>
        public const int ID = 0x00000002;
        /// <summary>
        /// Retrieves or sets the <c>fState</c> member.
        /// </summary>
        public const int STATE = 0x00000001;
        /// <summary>
        /// Retrieves or sets the <see cref="MENUITEMINFOW.TypeData"/> member.
        /// </summary>
        public const int STRING = 0x00000040;
        /// <summary>
        /// Retrieves or sets the <see cref="MENUITEMINFOW.SubMenuHandle"/> member.
        /// </summary>
        public const int SUBMENU = 0x00000004;
        /// <summary>
        /// Retrieves or sets the <see cref="MENUITEMINFOW.Type"/> and <see cref="MENUITEMINFOW.TypeData"/> members.
        /// <see cref="TYPE"/> is replaced by <see cref="BITMAP"/>, <see cref="FTYPE"/>, and <see cref="STRING"/>.
        /// </summary>
        public const int TYPE = 0x00000010;
    }
}
