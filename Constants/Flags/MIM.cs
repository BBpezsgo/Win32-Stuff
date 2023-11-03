namespace Win32
{
    public static class MIM
    {
        /// <summary>
        /// Settings apply to the menu and all of its submenus.
        /// <see cref="User32.SetMenuInfo"/> uses this flag and <see cref="User32.GetMenuInfo"/> ignores this flag
        /// </summary>
        public const UINT APPLYTOSUBMENUS = 0x80000000;
        /// <summary>
        /// Retrieves or sets the <c>hbrBack</c> member.
        /// </summary>
        public const UINT BACKGROUND = 0x00000002;
        /// <summary>
        /// Retrieves or sets the <c>dwContextHelpID</c> member.
        /// </summary>
        public const UINT HELPID = 0x00000004;
        /// <summary>
        /// Retrieves or sets the <c>cyMax</c> member.
        /// </summary>
        public const UINT MAXHEIGHT = 0x00000001;
        /// <summary>
        /// Retrieves or sets the <c>dwMenuData</c> member.
        /// </summary>
        public const UINT MENUDATA = 0x00000008;
        /// <summary>
        /// Retrieves or sets the <c>dwStyle</c> member.
        /// </summary>
        public const UINT STYLE = 0x00000010;
    }
}
