namespace Win32.Forms;

[Flags]
public enum IIC : DWORD
{
    /// <summary>
    /// Load list-view and header control classes.
    /// </summary>
    ListViewClasses = 0x00000001,
    /// <summary>
    /// Load tree-view and tooltip control classes.
    /// </summary>
    TreeViewClasses = 0x00000002,
    /// <summary>
    /// Load toolbar, status bar, trackbar, and tooltip control classes.
    /// </summary>
    BarClasses = 0x00000004,
    /// <summary>
    /// Load tab and tooltip control classes.
    /// </summary>
    TabClasses = 0x00000008,
    /// <summary>
    /// Load up-down control class.
    /// </summary>
    UpDownClass = 0x00000010,
    /// <summary>
    /// Load progress bar control class.
    /// </summary>
    ProgressClass = 0x00000020,
    /// <summary>
    /// Load hot key control class.
    /// </summary>
    HotkeyClass = 0x00000040,
    /// <summary>
    /// Load animate control class.
    /// </summary>
    AnimateClass = 0x00000080,
    /// <summary>
    /// Load animate control, header, hot key, list-view, progress bar, status bar,
    /// tab, tooltip, toolbar, trackbar, tree-view, and up-down control classes.
    /// </summary>
#pragma warning disable RCS1191
    Win95Classes = 0x000000FF,
#pragma warning restore RCS1191 
    /// <summary>
    /// Load date and time picker control class.
    /// </summary>
    DateClasses = 0x00000100,
    /// <summary>
    /// Load ComboBoxEx class.
    /// </summary>
    UserExClasses = 0x00000200,
    /// <summary>
    /// Load rebar control class.
    /// </summary>
    CoolClasses = 0x00000400,
    /// <summary>
    /// Load IP address class.
    /// </summary>
    InternetClasses = 0x00000800,
    /// <summary>
    /// Load pager control class.
    /// </summary>
    PageScrollerClass = 0x00001000,
    /// <summary>
    /// Load a native font control class.
    /// </summary>
    NativeFontControlClass = 0x00002000,
    /// <summary>
    /// Load one of the intrinsic User32 control classes.The user controls
    /// include button, edit, static, listbox, combobox, and scroll bar.
    /// </summary>
    StandardClasses = 0x00004000,
    /// <summary>
    /// Load a hyperlink control class.
    /// </summary>
    LinkClass = 0x00008000,
}
