namespace Win32.Forms;

#pragma warning disable CA1707 // Identifiers should not contain underscores
public static partial class WindowMessage
{
    public const uint WM_NULL = 0x0000;
    public const uint WM_CREATE = 0x0001;
    public const uint WM_DESTROY = 0x0002;
    public const uint WM_MOVE = 0x0003;
    public const uint WM_SIZE = 0x0005;

    public const uint WM_ACTIVATE = 0x0006;

    public const uint WM_SETFOCUS = 0x0007;
    public const uint WM_KILLFOCUS = 0x0008;
    public const uint WM_ENABLE = 0x000A;
    public const uint WM_SETREDRAW = 0x000B;
    public const uint WM_SETTEXT = 0x000C;
    public const uint WM_GETTEXT = 0x000D;
    public const uint WM_GETTEXTLENGTH = 0x000E;
    public const uint WM_PAINT = 0x000F;
    public const uint WM_CLOSE = 0x0010;

    // ifndef _WIN32_WCE
    public const uint WM_QUERYENDSESSION = 0x0011;
    public const uint WM_QUERYOPEN = 0x0013;
    public const uint WM_ENDSESSION = 0x0016;

    public const uint WM_QUIT = 0x0012;
    public const uint WM_ERASEBKGND = 0x0014;
    public const uint WM_SYSCOLORCHANGE = 0x0015;
    public const uint WM_SHOWWINDOW = 0x0018;
    public const uint WM_WININICHANGE = 0x001A;

    // if (WINVER >= 0x0400)
    public const uint WM_SETTINGCHANGE = WM_WININICHANGE;

    public const uint WM_DEVMODECHANGE = 0x001B;
    public const uint WM_ACTIVATEAPP = 0x001C;
    public const uint WM_FONTCHANGE = 0x001D;
    public const uint WM_TIMECHANGE = 0x001E;
    public const uint WM_CANCELMODE = 0x001F;
    public const uint WM_SETCURSOR = 0x0020;
    public const uint WM_MOUSEACTIVATE = 0x0021;
    public const uint WM_CHILDACTIVATE = 0x0022;
    public const uint WM_QUEUESYNC = 0x0023;

    public const uint WM_GETMINMAXINFO = 0x0024;

    public const uint WM_PAINTICON = 0x0026;
    public const uint WM_ICONERASEBKGND = 0x0027;
    public const uint WM_NEXTDLGCTL = 0x0028;
    public const uint WM_SPOOLERSTATUS = 0x002A;
    public const uint WM_DRAWITEM = 0x002B;
    public const uint WM_MEASUREITEM = 0x002C;
    public const uint WM_DELETEITEM = 0x002D;
    public const uint WM_VKEYTOITEM = 0x002E;
    public const uint WM_CHARTOITEM = 0x002F;
    public const uint WM_SETFONT = 0x0030;
    public const uint WM_GETFONT = 0x0031;
    public const uint WM_SETHOTKEY = 0x0032;
    public const uint WM_GETHOTKEY = 0x0033;
    public const uint WM_QUERYDRAGICON = 0x0037;
    public const uint WM_COMPAREITEM = 0x0039;

    // #if (WINVER >= 0x0500)
    // # ifndef _WIN32_WCE
    public const uint WM_GETOBJECT = 0x003D;
    // #endif
    // #endif /* WINVER >= 0x0500 */

    public const uint WM_COMPACTING = 0x0041;
    /// <summary>
    /// <b>No longer supported</b>
    /// </summary>
    public const uint WM_COMMNOTIFY = 0x0044;
    public const uint WM_WINDOWPOSCHANGING = 0x0046;
    public const uint WM_WINDOWPOSCHANGED = 0x0047;

    public const uint WM_POWER = 0x0048;

    public const uint WM_COPYDATA = 0x004A;
    public const uint WM_CANCELJOURNAL = 0x004B;

    // #if (WINVER >= 0x0400)
    public const uint WM_NOTIFY = 0x004E;
    public const uint WM_INPUTLANGCHANGEREQUEST = 0x0050;
    public const uint WM_INPUTLANGCHANGE = 0x0051;
    public const uint WM_TCARD = 0x0052;
    public const uint WM_HELP = 0x0053;
    public const uint WM_USERCHANGED = 0x0054;
    public const uint WM_NOTIFYFORMAT = 0x0055;

    public const uint NFR_ANSI = 1;
    public const uint NFR_UNICODE = 2;
    public const uint NF_QUERY = 3;
    public const uint NF_REQUERY = 4;

    /// <summary>
    /// Notifies a window that the user desires a context menu to appear.
    /// The user may have clicked the right mouse button (right-clicked)
    /// in the window, pressed Shift+F10 or pressed the applications key
    /// (context menu key) available on some keyboards.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <b>wParam:</b><br/>
    /// A handle to the window in which the user right-clicked the mouse.
    /// This can be a child window of the window receiving the message.
    /// For more information about processing this message,
    /// see the Remarks section.
    /// </para>
    /// <b>lParam:</b><br/>
    /// <para>
    /// The low-order word specifies the horizontal position of the cursor,
    /// in screen coordinates, at the time of the mouse click.
    /// </para>
    /// <para>
    /// The high-order word specifies the vertical position of the cursor,
    /// in screen coordinates, at the time of the mouse click.
    /// </para>
    /// </remarks>
    public const uint WM_CONTEXTMENU = 0x007B;
    public const uint WM_STYLECHANGING = 0x007C;
    public const uint WM_STYLECHANGED = 0x007D;
    public const uint WM_DISPLAYCHANGE = 0x007E;
    public const uint WM_GETICON = 0x007F;
    public const uint WM_SETICON = 0x0080;
    // #endif /* WINVER >= 0x0400 */

    public const uint WM_NCCREATE = 0x0081;
    public const uint WM_NCDESTROY = 0x0082;
    public const uint WM_NCCALCSIZE = 0x0083;
    public const uint WM_NCHITTEST = 0x0084;
    public const uint WM_NCPAINT = 0x0085;
    public const uint WM_NCACTIVATE = 0x0086;
    public const uint WM_GETDLGCODE = 0x0087;

    // ifndef _WIN32_WCE
    public const uint WM_SYNCPAINT = 0x0088;
    // endif

    public const uint WM_NCMOUSEMOVE = 0x00A0;
    public const uint WM_NCLBUTTONDOWN = 0x00A1;
    public const uint WM_NCLBUTTONUP = 0x00A2;
    public const uint WM_NCLBUTTONDBLCLK = 0x00A3;
    public const uint WM_NCRBUTTONDOWN = 0x00A4;
    public const uint WM_NCRBUTTONUP = 0x00A5;
    public const uint WM_NCRBUTTONDBLCLK = 0x00A6;
    public const uint WM_NCMBUTTONDOWN = 0x00A7;
    public const uint WM_NCMBUTTONUP = 0x00A8;
    public const uint WM_NCMBUTTONDBLCLK = 0x00A9;

    // #if (_WIN32_WINNT >= 0x0500)
    public const uint WM_NCXBUTTONDOWN = 0x00AB;
    public const uint WM_NCXBUTTONUP = 0x00AC;
    public const uint WM_NCXBUTTONDBLCLK = 0x00AD;
    // #endif /* _WIN32_WINNT >= 0x0500 */

    // #if (_WIN32_WINNT >= 0x0501)
    public const uint WM_INPUT_DEVICE_CHANGE = 0x00FE;
    // #endif /* _WIN32_WINNT >= 0x0501 */

    // #if (_WIN32_WINNT >= 0x0501)
    public const uint WM_INPUT = 0x00FF;
    // #endif /* _WIN32_WINNT >= 0x0501 */

    public const uint WM_KEYFIRST = 0x0100;
    public const uint WM_KEYDOWN = 0x0100;
    public const uint WM_KEYUP = 0x0101;
    public const uint WM_CHAR = 0x0102;
    public const uint WM_DEADCHAR = 0x0103;
    public const uint WM_SYSKEYDOWN = 0x0104;
    public const uint WM_SYSKEYUP = 0x0105;
    public const uint WM_SYSCHAR = 0x0106;
    public const uint WM_SYSDEADCHAR = 0x0107;
    /*
    #if (_WIN32_WINNT >= 0x0501)
    public const uint WM_UNICHAR = 0x0109;
    public const uint WM_KEYLAST = 0x0109;
    public const uint UNICODE_NOCHAR = 0xFFFF;
    #else
    public const uint WM_KEYLAST = 0x0108;
    #endif
    */

    // #if (WINVER >= 0x0400)
    public const uint WM_IME_STARTCOMPOSITION = 0x010D;
    public const uint WM_IME_ENDCOMPOSITION = 0x010E;
    public const uint WM_IME_COMPOSITION = 0x010F;
    public const uint WM_IME_KEYLAST = 0x010F;
    // #endif /* WINVER >= 0x0400 */

    public const uint WM_INITDIALOG = 0x0110;

    /// <summary>
    /// Sent when the user selects a command item from a menu, when
    /// a control sends a notification message to its parent window,
    /// or when an accelerator keystroke is translated.
    /// </summary>
    /// <returns>
    /// If an application processes this message, it should return zero.
    /// </returns>
    /// <remarks>
    /// Use of the <c>wParam</c> and <c>lParam</c> parameters are summarized here.
    /// <list type="bullet">
    ///
    /// <item>
    /// <term>Menu</term>
    /// <description><br/>
    /// <c>wParam (high word)</c>: 0<br/>
    /// <c>wParam (low word)</c>: Menu identifier(IDM_*)<br/>
    /// <c>lParam</c>: 0<br/>
    /// </description>
    /// </item>
    ///
    /// <item>
    /// <term>Accelerator</term>
    /// <description><br/>
    /// <c>wParam (high word)</c>: 1<br/>
    /// <c>wParam (low word)</c>: Accelerator identifier(IDM_*)<br/>
    /// <c>lParam</c>: 0<br/>
    /// </description>
    /// </item>
    ///
    /// <item>
    /// <term>Control</term>
    /// <description><br/>
    /// <c>wParam (high word)</c>: Control-defined notification code<br/>
    /// <c>wParam (low word)</c>: Control identifier<br/>
    /// <c>lParam</c>: Handle to the control window<br/>
    /// </description>
    /// </item>
    /// </list>
    ///
    /// </remarks>
    public const uint WM_COMMAND = 0x0111;
    public const uint WM_SYSCOMMAND = 0x0112;
    public const uint WM_TIMER = 0x0113;
    public const uint WM_HSCROLL = 0x0114;
    public const uint WM_VSCROLL = 0x0115;
    public const uint WM_INITMENU = 0x0116;
    public const uint WM_INITMENUPOPUP = 0x0117;
    // #if (WINVER >= 0x0601)
    public const uint WM_GESTURE = 0x0119;
    public const uint WM_GESTURENOTIFY = 0x011A;
    // #endif /* WINVER >= 0x0601 */
    public const uint WM_MENUSELECT = 0x011F;
    public const uint WM_MENUCHAR = 0x0120;
    public const uint WM_ENTERIDLE = 0x0121;
    // #if (WINVER >= 0x0500)
    // # ifndef _WIN32_WCE
    public const uint WM_MENURBUTTONUP = 0x0122;
    public const uint WM_MENUDRAG = 0x0123;
    public const uint WM_MENUGETOBJECT = 0x0124;
    public const uint WM_UNINITMENUPOPUP = 0x0125;
    public const uint WM_MENUCOMMAND = 0x0126;

    // # ifndef _WIN32_WCE
    // #if (_WIN32_WINNT >= 0x0500)
    public const uint WM_CHANGEUISTATE = 0x0127;
    public const uint WM_UPDATEUISTATE = 0x0128;
    public const uint WM_QUERYUISTATE = 0x0129;

    /// <summary>
    /// <c>LOWORD(wParam)</c> values in <c>WM_UISTATE</c>
    /// </summary>
    public const uint UIS_SET = 1;
    public const uint UIS_CLEAR = 2;
    public const uint UIS_INITIALIZE = 3;

    /// <summary>
    /// <c>HIWORD(wParam)</c> values in <c>WM_UISTATE</c>
    /// </summary>
    public const uint UISF_HIDEFOCUS = 0x1;
    public const uint UISF_HIDEACCEL = 0x2;
    // #if (_WIN32_WINNT >= 0x0501)
    public const uint UISF_ACTIVE = 0x4;
    // #endif /* _WIN32_WINNT >= 0x0501 */
    // #endif /* _WIN32_WINNT >= 0x0500 */
    // #endif

    // #endif
    // #endif /* WINVER >= 0x0500 */

    public const uint WM_CTLCOLORMSGBOX = 0x0132;
    public const uint WM_CTLCOLOREDIT = 0x0133;
    public const uint WM_CTLCOLORLISTBOX = 0x0134;
    public const uint WM_CTLCOLORBTN = 0x0135;
    public const uint WM_CTLCOLORDLG = 0x0136;
    public const uint WM_CTLCOLORSCROLLBAR = 0x0137;
    public const uint WM_CTLCOLORSTATIC = 0x0138;
    public const uint MN_GETHMENU = 0x01E1;

    public const uint WM_MOUSEFIRST = 0x0200;
    public const uint WM_MOUSEMOVE = 0x0200;
    public const uint WM_LBUTTONDOWN = 0x0201;
    public const uint WM_LBUTTONUP = 0x0202;
    public const uint WM_LBUTTONDBLCLK = 0x0203;
    public const uint WM_RBUTTONDOWN = 0x0204;
    public const uint WM_RBUTTONUP = 0x0205;
    public const uint WM_RBUTTONDBLCLK = 0x0206;
    public const uint WM_MBUTTONDOWN = 0x0207;
    public const uint WM_MBUTTONUP = 0x0208;
    public const uint WM_MBUTTONDBLCLK = 0x0209;
    // #if (_WIN32_WINNT >= 0x0400) || (_WIN32_WINDOWS > 0x0400)
    public const uint WM_MOUSEWHEEL = 0x020A;
    // #endif
    // #if (_WIN32_WINNT >= 0x0500)
    public const uint WM_XBUTTONDOWN = 0x020B;
    public const uint WM_XBUTTONUP = 0x020C;
    public const uint WM_XBUTTONDBLCLK = 0x020D;
    // #endif
    // #if (_WIN32_WINNT >= 0x0600)
    public const uint WM_MOUSEHWHEEL = 0x020E;
    // #endif

    /*
    #if (_WIN32_WINNT >= 0x0600)
    public const uint WM_MOUSELAST = 0x020E;
    #elif (_WIN32_WINNT >= 0x0500)
    public const uint WM_MOUSELAST = 0x020D;
    #elif (_WIN32_WINNT >= 0x0400) || (_WIN32_WINDOWS > 0x0400)
    public const uint WM_MOUSELAST = 0x020A;
    #else
    public const uint WM_MOUSELAST = 0x0209;
    #endif
    */

    // #if (_WIN32_WINNT >= 0x0400)
    /// <summary>
    /// Value for rolling one detent
    /// </summary>
    public const uint WHEEL_DELTA = 120;

    /// <summary>
    /// Setting to scroll one page for SPI_GET/SETWHEELSCROLLLINES
    /// </summary>
    public const uint WHEEL_PAGESCROLL = uint.MaxValue;
    // #endif /* _WIN32_WINNT >= 0x0400 */

    // #if (_WIN32_WINNT >= 0x0500)

    /* XButton values are WORD flags */
    public const uint XBUTTON1 = 0x0001;
    public const uint XBUTTON2 = 0x0002;
    /* Were there to be an XBUTTON3, its value would be 0x0004 */
    // #endif /* _WIN32_WINNT >= 0x0500 */

    public const uint WM_PARENTNOTIFY = 0x0210;
    public const uint WM_ENTERMENULOOP = 0x0211;
    public const uint WM_EXITMENULOOP = 0x0212;

    // #if (WINVER >= 0x0400)
    public const uint WM_NEXTMENU = 0x0213;
    public const uint WM_SIZING = 0x0214;
    public const uint WM_CAPTURECHANGED = 0x0215;
    public const uint WM_MOVING = 0x0216;
    // #endif /* WINVER >= 0x0400 */

    // #if (WINVER >= 0x0400)

    public const uint WM_POWERBROADCAST = 0x0218;

    // # ifndef _WIN32_WCE
    public const uint PBT_APMQUERYSUSPEND = 0x0000;
    public const uint PBT_APMQUERYSTANDBY = 0x0001;

    public const uint PBT_APMQUERYSUSPENDFAILED = 0x0002;
    public const uint PBT_APMQUERYSTANDBYFAILED = 0x0003;

    public const uint PBT_APMSUSPEND = 0x0004;
    public const uint PBT_APMSTANDBY = 0x0005;

    public const uint PBT_APMRESUMECRITICAL = 0x0006;
    public const uint PBT_APMRESUMESUSPEND = 0x0007;
    public const uint PBT_APMRESUMESTANDBY = 0x0008;

    public const uint PBTF_APMRESUMEFROMFAILURE = 0x00000001;

    public const uint PBT_APMBATTERYLOW = 0x0009;
    public const uint PBT_APMPOWERSTATUSCHANGE = 0x000A;

    public const uint PBT_APMOEMEVENT = 0x000B;

    public const uint PBT_APMRESUMEAUTOMATIC = 0x0012;
    // #if (_WIN32_WINNT >= 0x0502)
    // # ifndef PBT_POWERSETTINGCHANGE
    public const uint PBT_POWERSETTINGCHANGE = 0x8013;

    #region Desktop Family
    // #if WINAPI_FAMILY_PARTITION(WINAPI_PARTITION_DESKTOP)

    // #endif /* WINAPI_FAMILY_PARTITION(WINAPI_PARTITION_DESKTOP) */
    #endregion

    // #endif // PBT_POWERSETTINGCHANGE

    // #endif // (_WIN32_WINNT >= 0x0502)
    // #endif

    // #endif /* WINVER >= 0x0400 */

    // #if (WINVER >= 0x0400)
    public const uint WM_DEVICECHANGE = 0x0219;
    // #endif /* WINVER >= 0x0400 */

    public const uint WM_MDICREATE = 0x0220;
    public const uint WM_MDIDESTROY = 0x0221;
    public const uint WM_MDIACTIVATE = 0x0222;
    public const uint WM_MDIRESTORE = 0x0223;
    public const uint WM_MDINEXT = 0x0224;
    public const uint WM_MDIMAXIMIZE = 0x0225;
    public const uint WM_MDITILE = 0x0226;
    public const uint WM_MDICASCADE = 0x0227;
    public const uint WM_MDIICONARRANGE = 0x0228;
    public const uint WM_MDIGETACTIVE = 0x0229;

    public const uint WM_MDISETMENU = 0x0230;
    public const uint WM_ENTERSIZEMOVE = 0x0231;
    public const uint WM_EXITSIZEMOVE = 0x0232;
    public const uint WM_DROPFILES = 0x0233;
    public const uint WM_MDIREFRESHMENU = 0x0234;

    // #if (WINVER >= 0x0602)
    public const uint WM_POINTERDEVICECHANGE = 0x238;
    public const uint WM_POINTERDEVICEINRANGE = 0x239;
    public const uint WM_POINTERDEVICEOUTOFRANGE = 0x23A;
    // #endif /* WINVER >= 0x0602 */

    // #if (WINVER >= 0x0601)
    public const uint WM_TOUCH = 0x0240;
    // #endif /* WINVER >= 0x0601 */

    // #if (WINVER >= 0x0602)
    public const uint WM_NCPOINTERUPDATE = 0x0241;
    public const uint WM_NCPOINTERDOWN = 0x0242;
    public const uint WM_NCPOINTERUP = 0x0243;
    public const uint WM_POINTERUPDATE = 0x0245;
    public const uint WM_POINTERDOWN = 0x0246;
    public const uint WM_POINTERUP = 0x0247;
    public const uint WM_POINTERENTER = 0x0249;
    public const uint WM_POINTERLEAVE = 0x024A;
    public const uint WM_POINTERACTIVATE = 0x024B;
    public const uint WM_POINTERCAPTURECHANGED = 0x024C;
    public const uint WM_TOUCHHITTESTING = 0x024D;
    public const uint WM_POINTERWHEEL = 0x024E;
    public const uint WM_POINTERHWHEEL = 0x024F;
    public const uint DM_POINTERHITTEST = 0x0250;
    // #endif /* WINVER >= 0x0602 */

    // #if (WINVER >= 0x0400)
    public const uint WM_IME_SETCONTEXT = 0x0281;
    public const uint WM_IME_NOTIFY = 0x0282;
    public const uint WM_IME_CONTROL = 0x0283;
    public const uint WM_IME_COMPOSITIONFULL = 0x0284;
    public const uint WM_IME_SELECT = 0x0285;
    public const uint WM_IME_CHAR = 0x0286;
    // #endif /* WINVER >= 0x0400 */
    // #if (WINVER >= 0x0500)
    public const uint WM_IME_REQUEST = 0x0288;
    // #endif /* WINVER >= 0x0500 */
    // #if (WINVER >= 0x0400)
    public const uint WM_IME_KEYDOWN = 0x0290;
    public const uint WM_IME_KEYUP = 0x0291;
    // #endif /* WINVER >= 0x0400 */

    // #if ((_WIN32_WINNT >= 0x0400) || (WINVER >= 0x0500))
    public const uint WM_MOUSEHOVER = 0x02A1;
    public const uint WM_MOUSELEAVE = 0x02A3;
    // #endif
    // #if (WINVER >= 0x0500)
    public const uint WM_NCMOUSEHOVER = 0x02A0;
    public const uint WM_NCMOUSELEAVE = 0x02A2;
    // #endif /* WINVER >= 0x0500 */

    // #if (_WIN32_WINNT >= 0x0501)
    public const uint WM_WTSSESSION_CHANGE = 0x02B1;

    public const uint WM_TABLET_FIRST = 0x02c0;
    public const uint WM_TABLET_LAST = 0x02df;
    // #endif /* _WIN32_WINNT >= 0x0501 */

    // #if (WINVER >= 0x0601)
    public const uint WM_DPICHANGED = 0x02E0;
    // #endif /* WINVER >= 0x0601 */

    public const uint WM_CUT = 0x0300;
    public const uint WM_COPY = 0x0301;
    public const uint WM_PASTE = 0x0302;
    public const uint WM_CLEAR = 0x0303;
    public const uint WM_UNDO = 0x0304;
    public const uint WM_RENDERFORMAT = 0x0305;
    public const uint WM_RENDERALLFORMATS = 0x0306;
    public const uint WM_DESTROYCLIPBOARD = 0x0307;
    public const uint WM_DRAWCLIPBOARD = 0x0308;
    public const uint WM_PAINTCLIPBOARD = 0x0309;
    public const uint WM_VSCROLLCLIPBOARD = 0x030A;
    public const uint WM_SIZECLIPBOARD = 0x030B;
    public const uint WM_ASKCBFORMATNAME = 0x030C;
    public const uint WM_CHANGECBCHAIN = 0x030D;
    public const uint WM_HSCROLLCLIPBOARD = 0x030E;
    public const uint WM_QUERYNEWPALETTE = 0x030F;
    public const uint WM_PALETTEISCHANGING = 0x0310;
    public const uint WM_PALETTECHANGED = 0x0311;
    public const uint WM_HOTKEY = 0x0312;

    // #if (WINVER >= 0x0400)
    public const uint WM_PRINT = 0x0317;
    public const uint WM_PRINTCLIENT = 0x0318;
    // #endif /* WINVER >= 0x0400 */

    // #if (_WIN32_WINNT >= 0x0500)
    public const uint WM_APPCOMMAND = 0x0319;
    // #endif /* _WIN32_WINNT >= 0x0500 */

    // #if (_WIN32_WINNT >= 0x0501)
    public const uint WM_THEMECHANGED = 0x031A;
    // #endif /* _WIN32_WINNT >= 0x0501 */

    // #if (_WIN32_WINNT >= 0x0501)
    public const uint WM_CLIPBOARDUPDATE = 0x031D;
    // #endif /* _WIN32_WINNT >= 0x0501 */

    // #if (_WIN32_WINNT >= 0x0600)
    public const uint WM_DWMCOMPOSITIONCHANGED = 0x031E;
    public const uint WM_DWMNCRENDERINGCHANGED = 0x031F;
    public const uint WM_DWMCOLORIZATIONCOLORCHANGED = 0x0320;
    public const uint WM_DWMWINDOWMAXIMIZEDCHANGE = 0x0321;
    // #endif /* _WIN32_WINNT >= 0x0600 */

    // #if (_WIN32_WINNT >= 0x0601)
    public const uint WM_DWMSENDICONICTHUMBNAIL = 0x0323;
    public const uint WM_DWMSENDICONICLIVEPREVIEWBITMAP = 0x0326;
    // #endif /* _WIN32_WINNT >= 0x0601 */

    // #if (WINVER >= 0x0600)
    public const uint WM_GETTITLEBARINFOEX = 0x033F;
    // #endif /* WINVER >= 0x0600 */

    // #if (WINVER >= 0x0400)
    // #endif /* WINVER >= 0x0400 */

    // #if (WINVER >= 0x0400)
    public const uint WM_HANDHELDFIRST = 0x0358;
    public const uint WM_HANDHELDLAST = 0x035F;

    public const uint WM_AFXFIRST = 0x0360;
    public const uint WM_AFXLAST = 0x037F;
    // #endif /* WINVER >= 0x0400 */

    public const uint WM_PENWINFIRST = 0x0380;
    public const uint WM_PENWINLAST = 0x038F;

    // #if (WINVER >= 0x0400)
    public const uint WM_APP = 0x8000;
    // #endif /* WINVER >= 0x0400 */

    /*
     * NOTE: All Message Numbers below 0x0400 are RESERVED.
     *
     * Private Window Messages Start Here:
     */

    public const uint WM_USER = 0x0400;
}

// IDK?
public static partial class WindowMessage
{
    public const uint WM_CTLCOLOR = 0x0019;
    public const uint WM_COPYGLOBALDATA = 0x0049;

    public const uint WM_WNT_CONVERTREQUESTEX = 0x0109;
    public const uint WM_CONVERTREQUEST = 0x010a;
    public const uint WM_CONVERTRESULT = 0x010b;
    public const uint WM_INTERIM = 0x010c;

    public const uint WM_SYSTIMER = 0x0118;
    public const uint WM_LBTRACKPOINT = 0x0131;
}

/// <summary>
/// <c>WM_ACTIVATE</c> state values
/// </summary>
public static class WA
{
    public const uint WA_INACTIVE = 0;
    public const uint WA_ACTIVE = 1;
    public const uint WA_CLICKACTIVE = 2;
}

/// <summary>
/// <c>wParam</c> for <c>WM_POWER</c> window message and <c>DRV_POWER</c> driver notification
/// </summary>
public static class PWR
{
    public const uint PWR_OK = 1;
    public const uint PWR_FAIL = uint.MaxValue;
    public const uint PWR_SUSPENDREQUEST = 1;
    public const uint PWR_SUSPENDRESUME = 2;
    public const uint PWR_CRITICALRESUME = 3;
}
