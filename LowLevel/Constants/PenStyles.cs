namespace Win32.LowLevel
{
    public static class PS
    {
        public const int SOLID = 0;
        /// <summary>
        /// ------- 
        /// </summary>
        public const int DASH = 1;
        /// <summary>
        /// ....... 
        /// </summary>
        public const int DOT = 2;
        /// <summary>
        /// _._._._ 
        /// </summary>
        public const int DASHDOT = 3;
        /// <summary>
        /// _.._.._ 
        /// </summary>
        public const int DASHDOTDOT = 4;
        public const int NULL = 5;
        public const int INSIDEFRAME = 6;
        public const int USERSTYLE = 7;
        public const int ALTERNATE = 8;
        public const int STYLE_MASK = 0x0000000F;
        public const int ENDCAP_ROUND = 0x00000000;
        public const int ENDCAP_SQUARE = 0x00000100;
        public const int ENDCAP_FLAT = 0x00000200;
        public const int ENDCAP_MASK = 0x00000F00;
        public const int JOIN_ROUND = 0x00000000;
        public const int JOIN_BEVEL = 0x00001000;
        public const int JOIN_MITER = 0x00002000;
        public const int JOIN_MASK = 0x0000F000;
        public const int COSMETIC = 0x00000000;
        public const int GEOMETRIC = 0x00010000;
        public const int TYPE_MASK = 0x000F0000;
    }
}
