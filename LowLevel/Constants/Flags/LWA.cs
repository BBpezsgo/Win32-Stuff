namespace Win32
{
    [Flags]
    public enum LWA : DWORD
    {
        /// <summary>
        /// Use <c>crKey</c> as the transparency color.
        /// </summary>
        COLORKEY = 0x00000001,
        /// <summary>
        /// Use <c>bAlpha</c> to determine the opacity of the layered window.
        /// </summary>
        ALPHA = 0x00000002,
    }
}
