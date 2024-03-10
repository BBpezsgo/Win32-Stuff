namespace Win32;

[SuppressMessage("Roslynator", "RCS1234")]
public enum VirtualKeyCode : WORD
{
    None = 0x00,

    LButton = 0x01,
    RButton = 0x02,
    /// <summary>
    /// Control-break processing
    /// </summary>
    Cancel = 0x03,
    /// <summary>
    /// NOT contiguous with L &#38; <see cref="RButton"/>
    /// </summary>
    MButton = 0x04,

    /// <summary>
    /// NOT contiguous with L &#38; <see cref="RButton"/>
    /// </summary>
    XButton1 = 0x05,
    /// <summary>
    /// NOT contiguous with L &#38; <see cref="RButton"/>
    /// </summary>
    XButton2 = 0x06,

    // 0x07 : reserved

    Back = 0x08,
    Tab = 0x09,

    // 0x0A - 0x0B : reserved

    Clear = 0x0C,
    Return = 0x0D,

    // 0x0E - 0x0F : unassigned

    Shift = 0x10,
    Control = 0x11,
    Menu = 0x12,
    Pause = 0x13,
    CapsLock = 0x14,

    /// <summary>
    /// IME Kana mode
    /// </summary>
    Kana = 0x15,
    /// <summary>
    /// IME Hangul mode
    /// </summary>
    Hangul = 0x15,

    /// <summary>
    /// IME On
    /// </summary>
    ImeOn = 0x16,

    /// <summary>
    /// IME Junja mode
    /// </summary>
    Junja = 0x17,
    /// <summary>
    /// IME final mode
    /// </summary>
    Final = 0x18,
    /// <summary>
    /// IME Hanja mode
    /// </summary>
    Hanja = 0x19,
    /// <summary>
    /// IME Kanji mode
    /// </summary>
    Kanji = 0x19,

    /// <summary>
    /// IME Off
    /// </summary>
    ImeOff = 0x1A,

    Escape = 0x1B,

    /// <summary>
    /// IME convert
    /// </summary>
    Convert = 0x1C,
    /// <summary>
    /// IME nonconvert
    /// </summary>
    NonConvert = 0x1D,
    /// <summary>
    /// IME accept
    /// </summary>
    Accept = 0x1E,
    /// <summary>
    /// IME mode change request
    /// </summary>
    ModeChange = 0x1F,

    Space = 0x20,
    Prior = 0x21,
    Next = 0x22,
    End = 0x23,
    Home = 0x24,
    Left = 0x25,
    Up = 0x26,
    Right = 0x27,
    Down = 0x28,
    Select = 0x29,
    Print = 0x2A,
    Execute = 0x2B,
    /// <summary>
    /// PRINT SCREEN key
    /// </summary>
    Snapshot = 0x2C,
    Insert = 0x2D,
    Delete = 0x2E,
    Help = 0x2F,

    Number0 = '0',
    Number1 = '1',
    Number2 = '2',
    Number3 = '3',
    Number4 = '4',
    Number5 = '5',
    Number6 = '6',
    Number7 = '7',
    Number8 = '8',
    Number9 = '9',

    // 0x3A - 0x40 : unassigned

    A = 'A',
    B = 'B',
    C = 'C',
    D = 'D',
    E = 'E',
    F = 'F',
    G = 'G',
    H = 'H',
    I = 'I',
    J = 'J',
    K = 'K',
    L = 'L',
    M = 'M',
    N = 'N',
    O = 'O',
    P = 'P',
    Q = 'Q',
    R = 'R',
    S = 'S',
    T = 'T',
    U = 'U',
    V = 'V',
    W = 'W',
    X = 'X',
    Y = 'Y',
    Z = 'Z',

    LWindows = 0x5B,
    RWindows = 0x5C,
    /// <summary>
    /// Applications key
    /// </summary>
    Apps = 0x5D,

    // 0x5E : reserved

    /// <summary>
    /// Computer Sleep key
    /// </summary>
    Sleep = 0x5F,

    Numpad0 = 0x60,
    Numpad1 = 0x61,
    Numpad2 = 0x62,
    Numpad3 = 0x63,
    Numpad4 = 0x64,
    Numpad5 = 0x65,
    Numpad6 = 0x66,
    Numpad7 = 0x67,
    Numpad8 = 0x68,
    Numpad9 = 0x69,

    Multiply = 0x6A,
    Add = 0x6B,
    Separator = 0x6C,
    Subtract = 0x6D,
    Decimal = 0x6E,
    Divide = 0x6F,

    F1 = 0x70,
    F2 = 0x71,
    F3 = 0x72,
    F4 = 0x73,
    F5 = 0x74,
    F6 = 0x75,
    F7 = 0x76,
    F8 = 0x77,
    F9 = 0x78,
    F10 = 0x79,
    F11 = 0x7A,
    F12 = 0x7B,
    F13 = 0x7C,
    F14 = 0x7D,
    F15 = 0x7E,
    F16 = 0x7F,
    F17 = 0x80,
    F18 = 0x81,
    F19 = 0x82,
    F20 = 0x83,
    F21 = 0x84,
    F22 = 0x85,
    F23 = 0x86,
    F24 = 0x87,

    #region UI navigation
    NavigationView = 0x88,
    NavigationMenu = 0x89,
    NavigationUp = 0x8A,
    NavigationDown = 0x8B,
    NavigationLeft = 0x8C,
    NavigationRight = 0x8D,
    NavigationAccept = 0x8E,
    NavigationCancel = 0x8F,
    #endregion

    NumLock = 0x90,
    ScrollLock = 0x91,

    #region NEC PC-9800 kbd definitions
    OemNecEqual = 0x92,   // '=' key on numpad
    #endregion

    #region Fujitsu/OASYS kbd definitions
    OemFjJisho = 0x92,  // 'Dictionary' key
    OemFjMasshou = 0x93,  // 'Unregister word' key
    OemFjTouroku = 0x94,  // 'Register word' key
    OemFjLoya = 0x95, // 'Left OYAYUBI' key
    OemFjRoya = 0x96,  // 'Right OYAYUBI' key
    #endregion

    // 0x97 - 0x9F : unassigned

    /*
     * VK_L* & VK_R* - left and right Alt, Ctrl and Shift virtual keys.
     * Used only as parameters to GetAsyncKeyState() and GetKeyState().
     * No other API or message will distinguish left and right keys in this way.
     */

    LShift = 0xA0,
    RShift = 0xA1,
    LControl = 0xA2,
    RControl = 0xA3,
    LMenu = 0xA4,
    RMenu = 0xA5,

    BrowserBack = 0xA6,
    BrowserForward = 0xA7,
    BrowserRefresh = 0xA8,
    BrowserStop = 0xA9,
    BrowserSearch = 0xAA,
    BrowserFavorites = 0xAB,
    BrowserHome = 0xAC,

    VolumeMute = 0xAD,
    VolumeDown = 0xAE,
    VolumeUp = 0xAF,

    MediaNextTrack = 0xB0,
    MediaPrevTrack = 0xB1,
    MediaStop = 0xB2,
    MediaPlayPause = 0xB3,

    LaunchMail = 0xB4,
    LaunchMediaSelect = 0xB5,
    LaunchApp1 = 0xB6,
    LaunchApp2 = 0xB7,

    // 0xB8 - 0xB9 : reserved

    Oem1 = 0xBA, // ';:' for US
    OemPlus = 0xBB, // '+' any country
    OemComma = 0xBC, // ',' any country
    OemMinus = 0xBD, // '-' any country
    OemPeriod = 0xBE, // '.' any country
    Oem2 = 0xBF, // '/?' for US
    Oem3 = 0xC0, // '`~' for US

    // 0xC1 - 0xC2 : reserved

    #region Gamepad input
    GamepadA = 0xC3,
    GamepadB = 0xC4,
    GamepadX = 0xC5,
    GamepadY = 0xC6,
    GamepadRightShoulder = 0xC7,
    GamepadLeftShoulder = 0xC8,
    GamepadLeftTrigger = 0xC9,
    GamepadRightTrigger = 0xCA,
    GamepadDPadUp = 0xCB,
    GamepadDPadDown = 0xCC,
    GamepadDPadLeft = 0xCD,
    GamepadDPadRight = 0xCE,
    GamepadMenu = 0xCF,
    GamepadView = 0xD0,
    GamepadLeftThumbStickButton = 0xD1,
    GamepadRightThumbStickButton = 0xD2,
    GamepadLeftThumbStickUp = 0xD3,
    GamepadLeftThumbStickDown = 0xD4,
    GamepadLeftThumbStickRight = 0xD5,
    GamepadLeftThumbStickLeft = 0xD6,
    GamepadRightThumbStickUp = 0xD7,
    GamepadRightThumbStickDown = 0xD8,
    GamepadRightThumbStickRight = 0xD9,
    GamepadRightThumbStickLeft = 0xDA,
    #endregion

    /// <summary>
    /// '[{' for US
    /// </summary>
    Oem4 = 0xDB,
    /// <summary>
    /// '\|' for US
    /// </summary>
    Oem5 = 0xDC,
    /// <summary>
    /// ']}' for US
    /// </summary>
    Oem6 = 0xDD,
    /// <summary>
    ///  ''"' for US
    /// </summary>
    Oem7 = 0xDE,
    Oem8 = 0xDF,

    // 0xE0 : reserved

    #region Various extended or enhanced keyboards
    /// <summary>
    /// 'AX' key on Japanese AX kbd
    /// </summary>
    OemAX = 0xE1,
    /// <summary>
    /// "<>" or "\|" on RT 102-key kbd
    /// </summary>
    Oem102 = 0xE2,
    /// <summary>
    /// Help key on ICO
    /// </summary>
    IcoHelp = 0xE3,
    /// <summary>
    /// 00 key on ICO
    /// </summary>
    Ico00 = 0xE4,

    /// <summary>
    /// IME PROCESS key
    /// </summary>
    ProcessKey = 0xE5,

    IcoClear = 0xE6,

    /// <summary>
    /// Used to pass Unicode characters as if they were keystrokes.
    /// The <see cref="Packet"/> key is the low word of a 32-bit
    /// Virtual Key value used for non-keyboard input
    /// methods. For more information, see
    /// Remark in <c>KEYBDINPUT</c>, <see cref="User32.SendInput"/>, <see cref="WindowMessage.WM_KEYDOWN"/>, and <see cref="WindowMessage.WM_KEYUP"/>
    /// </summary>
    Packet = 0xE7,
    #endregion

    // 0xE8 : unassigned

    #region Nokia/Ericsson definitions

    OemReset = 0xE9,
    OemJump = 0xEA,
    OemPA1 = 0xEB,
    OemPA2 = 0xEC,
    OemPA3 = 0xED,
    OemWSCTRL = 0xEE,
    OemCUSEL = 0xEF,
    OemATTN = 0xF0,
    OemFinish = 0xF1,
    OemCopy = 0xF2,
    OemAuto = 0xF3,
    OemENLW = 0xF4,
    OemBackTab = 0xF5,
    #endregion

    /// <summary>
    /// Attn key
    /// </summary>
    Attn = 0xF6,
    /// <summary>
    /// CrSel key
    /// </summary>
    CrSel = 0xF7,
    /// <summary>
    /// ExSel key
    /// </summary>
    ExSel = 0xF8,
    /// <summary>
    /// Erase EOF key
    /// </summary>
    EraseEOF = 0xF9,
    /// <summary>
    /// Play key
    /// </summary>
    Play = 0xFA,
    /// <summary>
    /// Zoom key
    /// </summary>
    Zoom = 0xFB,
    /// <summary>
    /// Reserved
    /// </summary>
    NoName = 0xFC,
    /// <summary>
    /// PA1 key
    /// </summary>
    PA1 = 0xFD,
    /// <summary>
    /// Clear key
    /// </summary>
    OemClear = 0xFE,
}
