namespace Win32.LowLevel
{
    public static class ButtonControlMessage
    {
        public const uint GETCHECK = 0x00F0;
        public const uint SETCHECK = 0x00F1;
        public const uint GETSTATE = 0x00F2;
        public const uint SETSTATE = 0x00F3;
        public const uint SETSTYLE = 0x00F4;

        // if (WINVER >= 0x0400)
        public const uint CLICK = 0x00F5;
        public const uint GETIMAGE = 0x00F6;
        public const uint SETIMAGE = 0x00F7;

        // if (WINVER >= 0x0600)
        public const uint SETDONTCLICK = 0x00F8;

        // if (WINVER >= 0x0400)
        public const uint BST_UNCHECKED = 0x0000;
        public const uint BST_CHECKED = 0x0001;
        public const uint BST_INDETERMINATE = 0x0002;
        public const uint BST_PUSHED = 0x0004;
        public const uint BST_FOCUS = 0x0008;
    }
}
