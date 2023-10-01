namespace Win32
{
    public static class VirtualKeyCodes
    {
        /*
         * Virtual Keys, Standard Set
         */
        public const ushort LBUTTON = 0x01;
        public const ushort RBUTTON = 0x02;
        public const ushort CANCEL = 0x03;
        /// <summary>
        /// NOT contiguous with L & RBUTTON
        /// </summary>
        public const ushort MBUTTON = 0x04;

        // #if (_WIN32_WINNT >= 0x0500)
        /// <summary>
        /// NOT contiguous with L & RBUTTON
        /// </summary>
        public const ushort XBUTTON1 = 0x05;
        /// <summary>
        /// NOT contiguous with L & RBUTTON
        /// </summary>
        public const ushort XBUTTON2 = 0x06;
        // #endif

        /*
         * 0x07 : reserved
         */

        public const ushort BACK = 0x08;
        public const ushort TAB = 0x09;

        /*
         * 0x0A - 0x0B : reserved
         */

        public const ushort CLEAR = 0x0C;
        public const ushort RETURN = 0x0D;

        /*
         * 0x0E - 0x0F : unassigned
         */

        public const ushort SHIFT = 0x10;
        public const ushort CONTROL = 0x11;
        public const ushort MENU = 0x12;
        public const ushort PAUSE = 0x13;
        public const ushort CAPITAL = 0x14;

        public const ushort KANA = 0x15;
        /// <summary>
        /// old name - should be here for compatibility
        /// </summary>
        public const ushort HANGEUL = 0x15;
        public const ushort HANGUL = 0x15;

        /*
         * 0x16 : unassigned
         */

        public const ushort JUNJA = 0x17;
        public const ushort FINAL = 0x18;
        public const ushort HANJA = 0x19;
        public const ushort KANJI = 0x19;

        /*
         * 0x1A : unassigned
         */

        public const ushort ESCAPE = 0x1B;

        public const ushort CONVERT = 0x1C;
        public const ushort NONCONVERT = 0x1D;
        public const ushort ACCEPT = 0x1E;
        public const ushort MODECHANGE = 0x1F;

        public const ushort SPACE = 0x20;
        public const ushort PRIOR = 0x21;
        public const ushort NEXT = 0x22;
        public const ushort END = 0x23;
        public const ushort HOME = 0x24;
        public const ushort LEFT = 0x25;
        public const ushort UP = 0x26;
        public const ushort RIGHT = 0x27;
        public const ushort DOWN = 0x28;
        public const ushort SELECT = 0x29;
        public const ushort PRINT = 0x2A;
        public const ushort EXECUTE = 0x2B;
        public const ushort SNAPSHOT = 0x2C;
        public const ushort INSERT = 0x2D;
        public const ushort DELETE = 0x2E;
        public const ushort HELP = 0x2F;

        /*
         * VK_0 - VK_9 are the same as ASCII '0' - '9' (0x30 - 0x39)
         * 0x3A - 0x40 : unassigned
         * VK_A - VK_Z are the same as ASCII 'A' - 'Z' (0x41 - 0x5A)
         */

        public const ushort LWIN = 0x5B;
        public const ushort RWIN = 0x5C;
        public const ushort APPS = 0x5D;

        /*
         * 0x5E : reserved
         */

        public const ushort SLEEP = 0x5F;

        public const ushort NUMPAD0 = 0x60;
        public const ushort NUMPAD1 = 0x61;
        public const ushort NUMPAD2 = 0x62;
        public const ushort NUMPAD3 = 0x63;
        public const ushort NUMPAD4 = 0x64;
        public const ushort NUMPAD5 = 0x65;
        public const ushort NUMPAD6 = 0x66;
        public const ushort NUMPAD7 = 0x67;
        public const ushort NUMPAD8 = 0x68;
        public const ushort NUMPAD9 = 0x69;
        public const ushort MULTIPLY = 0x6A;
        public const ushort ADD = 0x6B;
        public const ushort SEPARATOR = 0x6C;
        public const ushort SUBTRACT = 0x6D;
        public const ushort DECIMAL = 0x6E;
        public const ushort DIVIDE = 0x6F;
        public const ushort F1 = 0x70;
        public const ushort F2 = 0x71;
        public const ushort F3 = 0x72;
        public const ushort F4 = 0x73;
        public const ushort F5 = 0x74;
        public const ushort F6 = 0x75;
        public const ushort F7 = 0x76;
        public const ushort F8 = 0x77;
        public const ushort F9 = 0x78;
        public const ushort F10 = 0x79;
        public const ushort F11 = 0x7A;
        public const ushort F12 = 0x7B;
        public const ushort F13 = 0x7C;
        public const ushort F14 = 0x7D;
        public const ushort F15 = 0x7E;
        public const ushort F16 = 0x7F;
        public const ushort F17 = 0x80;
        public const ushort F18 = 0x81;
        public const ushort F19 = 0x82;
        public const ushort F20 = 0x83;
        public const ushort F21 = 0x84;
        public const ushort F22 = 0x85;
        public const ushort F23 = 0x86;
        public const ushort F24 = 0x87;

        // #if (_WIN32_WINNT >= 0x0604)

        /*
         * 0x88 - 0x8F : UI navigation
         */

        public const ushort NAVIGATION_VIEW = 0x88;
        public const ushort NAVIGATION_MENU = 0x89;
        public const ushort NAVIGATION_UP = 0x8A;
        public const ushort NAVIGATION_DOWN = 0x8B;
        public const ushort NAVIGATION_LEFT = 0x8C;
        public const ushort NAVIGATION_RIGHT = 0x8D;
        public const ushort NAVIGATION_ACCEPT = 0x8E;
        public const ushort NAVIGATION_CANCEL = 0x8F;

        // #endif

        public const ushort NUMLOCK = 0x90;
        public const ushort SCROLL = 0x91;

        /*
         * NEC PC-9800 kbd definitions
         */
        public const ushort OEM_NEC_EQUAL = 0x92;   // '=' key on numpad

        /*
            * Fujitsu/OASYS kbd definitions
            */
        public const ushort OEM_FJ_JISHO = 0x92;  // 'Dictionary' key
        public const ushort OEM_FJ_MASSHOU = 0x93;  // 'Unregister word' key
        public const ushort OEM_FJ_TOUROKU = 0x94;  // 'Register word' key
        public const ushort OEM_FJ_LOYA = 0x95; // 'Left OYAYUBI' key
        public const ushort OEM_FJ_ROYA = 0x96;  // 'Right OYAYUBI' key

        /*
         * 0x97 - 0x9F : unassigned
         */

        /*
         * VK_L* & VK_R* - left and right Alt, Ctrl and Shift virtual keys.
         * Used only as parameters to GetAsyncKeyState() and GetKeyState().
         * No other API or message will distinguish left and right keys in this way.
         */

        public const ushort LSHIFT = 0xA0;
        public const ushort RSHIFT = 0xA1;
        public const ushort LCONTROL = 0xA2;
        public const ushort RCONTROL = 0xA3;
        public const ushort LMENU = 0xA4;
        public const ushort RMENU = 0xA5;

        // #if (_WIN32_WINNT >= 0x0500)
        public const ushort BROWSER_BACK = 0xA6;
        public const ushort BROWSER_FORWARD = 0xA7;
        public const ushort BROWSER_REFRESH = 0xA8;
        public const ushort BROWSER_STOP = 0xA9;
        public const ushort BROWSER_SEARCH = 0xAA;
        public const ushort BROWSER_FAVORITES = 0xAB;
        public const ushort BROWSER_HOME = 0xAC;

        public const ushort VOLUME_MUTE = 0xAD;
        public const ushort VOLUME_DOWN = 0xAE;
        public const ushort VOLUME_UP = 0xAF;
        public const ushort MEDIA_NEXT_TRACK = 0xB0;
        public const ushort MEDIA_PREV_TRACK = 0xB1;
        public const ushort MEDIA_STOP = 0xB2;
        public const ushort MEDIA_PLAY_PAUSE = 0xB3;
        public const ushort LAUNCH_MAIL = 0xB4;
        public const ushort LAUNCH_MEDIA_SELECT = 0xB5;
        public const ushort LAUNCH_APP1 = 0xB6;
        public const ushort LAUNCH_APP2 = 0xB7;

        // #endif

        /*
         * 0xB8 - 0xB9 : reserved
         */

        public const ushort OEM_1 = 0xBA; // ';:' for US
        public const ushort OEM_PLUS = 0xBB; // '+' any country
        public const ushort OEM_COMMA = 0xBC; // ',' any country
        public const ushort OEM_MINUS = 0xBD; // '-' any country
        public const ushort OEM_PERIOD = 0xBE; // '.' any country
        public const ushort OEM_2 = 0xBF; // '/?' for US
        public const ushort OEM_3 = 0xC0; // '`~' for US

        /*
         * 0xC1 - 0xC2 : reserved
         */

        // #if (_WIN32_WINNT >= 0x0604)

        /*
         * 0xC3 - 0xDA : Gamepad input
         */

        public const ushort GAMEPAD_A = 0xC3;
        public const ushort GAMEPAD_B = 0xC4;
        public const ushort GAMEPAD_X = 0xC5;
        public const ushort GAMEPAD_Y = 0xC6;
        public const ushort GAMEPAD_RIGHT_SHOULDER = 0xC7;
        public const ushort GAMEPAD_LEFT_SHOULDER = 0xC8;
        public const ushort GAMEPAD_LEFT_TRIGGER = 0xC9;
        public const ushort GAMEPAD_RIGHT_TRIGGER = 0xCA;
        public const ushort GAMEPAD_DPAD_UP = 0xCB;
        public const ushort GAMEPAD_DPAD_DOWN = 0xCC;
        public const ushort GAMEPAD_DPAD_LEFT = 0xCD;
        public const ushort GAMEPAD_DPAD_RIGHT = 0xCE;
        public const ushort GAMEPAD_MENU = 0xCF;
        public const ushort GAMEPAD_VIEW = 0xD0;
        public const ushort GAMEPAD_LEFT_THUMBSTICK_BUTTON = 0xD1;
        public const ushort GAMEPAD_RIGHT_THUMBSTICK_BUTTON = 0xD2;
        public const ushort GAMEPAD_LEFT_THUMBSTICK_UP = 0xD3;
        public const ushort GAMEPAD_LEFT_THUMBSTICK_DOWN = 0xD4;
        public const ushort GAMEPAD_LEFT_THUMBSTICK_RIGHT = 0xD5;
        public const ushort GAMEPAD_LEFT_THUMBSTICK_LEFT = 0xD6;
        public const ushort GAMEPAD_RIGHT_THUMBSTICK_UP = 0xD7;
        public const ushort GAMEPAD_RIGHT_THUMBSTICK_DOWN = 0xD8;
        public const ushort GAMEPAD_RIGHT_THUMBSTICK_RIGHT = 0xD9;
        public const ushort GAMEPAD_RIGHT_THUMBSTICK_LEFT = 0xDA;

        // #endif

        public const ushort OEM_4 = 0xDB;  //  '[{' for US
        public const ushort OEM_5 = 0xDC;  //  '\|' for US
        public const ushort OEM_6 = 0xDD;  //  ']}' for US
        public const ushort OEM_7 = 0xDE;  //  ''"' for US
        public const ushort OEM_8 = 0xDF;

        /*
         * 0xE0 : reserved
         */

        /*
         * Various extended or enhanced keyboards
         */
        public const ushort OEM_AX = 0xE1; //  'AX' key on Japanese AX kbd
        public const ushort OEM_102 = 0xE2; //  "<>" or "\|" on RT 102-key kbd.
        public const ushort ICO_HELP = 0xE3; //  Help key on ICO
        public const ushort ICO_00 = 0xE4; //  00 key on ICO

        // #if (WINVER >= 0x0400)
        public const ushort PROCESSKEY = 0xE5;
        // #endif

        public const ushort ICO_CLEAR = 0xE6;


        // #if (_WIN32_WINNT >= 0x0500)
        public const ushort PACKET = 0xE7;
        // #endif

        /*
         * 0xE8 : unassigned
         */

        /*
         * Nokia/Ericsson definitions
         */
        public const ushort OEM_RESET = 0xE9;
        public const ushort OEM_JUMP = 0xEA;
        public const ushort OEM_PA1 = 0xEB;
        public const ushort OEM_PA2 = 0xEC;
        public const ushort OEM_PA3 = 0xED;
        public const ushort OEM_WSCTRL = 0xEE;
        public const ushort OEM_CUSEL = 0xEF;
        public const ushort OEM_ATTN = 0xF0;
        public const ushort OEM_FINISH = 0xF1;
        public const ushort OEM_COPY = 0xF2;
        public const ushort OEM_AUTO = 0xF3;
        public const ushort OEM_ENLW = 0xF4;
        public const ushort OEM_BACKTAB = 0xF5;

        public const ushort ATTN = 0xF6;
        public const ushort CRSEL = 0xF7;
        public const ushort EXSEL = 0xF8;
        public const ushort EREOF = 0xF9;
        public const ushort PLAY = 0xFA;
        public const ushort ZOOM = 0xFB;
        public const ushort NONAME = 0xFC;
        public const ushort PA1 = 0xFD;
        public const ushort OEM_CLEAR = 0xFE;
    }
}
