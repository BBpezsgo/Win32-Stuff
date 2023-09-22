﻿namespace Win32
{
    // Button Control Messages
    public static class BM
    {
        public const uint BM_GETCHECK = 0x00F0;
        public const uint BM_SETCHECK = 0x00F1;
        public const uint BM_GETSTATE = 0x00F2;
        public const uint BM_SETSTATE = 0x00F3;
        public const uint BM_SETSTYLE = 0x00F4;

        // if (WINVER >= 0x0400)
        public const uint BM_CLICK = 0x00F5;
        public const uint BM_GETIMAGE = 0x00F6;
        public const uint BM_SETIMAGE = 0x00F7;

        // if (WINVER >= 0x0600)
        public const uint BM_SETDONTCLICK = 0x00F8;

        // if (WINVER >= 0x0400)
        public const uint BST_UNCHECKED = 0x0000;
        public const uint BST_CHECKED = 0x0001;
        public const uint BST_INDETERMINATE = 0x0002;
        public const uint BST_PUSHED = 0x0004;
        public const uint BST_FOCUS = 0x0008;
    }
}
