namespace Win32.Forms;

[SupportedOSPlatform("windows")]
public sealed class Button : Control
{
    /// <summary>
    /// Sent when the user clicks a button.
    /// </summary>
    public event ControlNotification<Button>? OnClick;

    /// <summary>
    /// Sent when the user double-clicks a button.
    /// This notification code is sent automatically for
    /// <see cref="ButtonControlStyles.USERBUTTON"/>, <see cref="ButtonControlStyles.RADIOBUTTON"/>, and <see cref="ButtonControlStyles.OWNERDRAW"/> buttons.
    /// Other button types send <see cref="ButtonControlNotifications.DOUBLECLICKED"/> only if
    /// they have the <see cref="ButtonControlStyles.NOTIFY"/> style.
    /// </summary>
    public event ControlNotification<Button>? OnDoubleClicked;

    /// <summary>
    /// Sent when a button receives the keyboard focus.
    /// The button must have the <see cref="ButtonControlStyles.NOTIFY"/> style to send this notification code.
    /// </summary>
    public event ControlNotification<Button>? OnSetFocus;

    /// <summary>
    /// Sent when a button loses the keyboard focus.
    /// The button must have the <see cref="ButtonControlStyles.NOTIFY"/> style to send this notification code.
    /// </summary>
    public event ControlNotification<Button>? OnKillFocus;

    public Button(
        Form parent,
        string label,
        RECT rect
    ) : base(
        parent,
        label,
        Forms.ClassName.Button,
        WindowStyles.TABSTOP | WindowStyles.VISIBLE | WindowStyles.CHILD | ButtonControlStyles.DEFPUSHBUTTON,
        rect,
        parent.GenerateControlId()
    )
    { }

    public Button(HWND handle) : base(handle) { }

    public override void HandleNotification(Window parent, ushort code)
    {
        switch (code)
        {
            case ButtonControlNotifications.CLICKED: OnClick?.Invoke(this); break;
            case ButtonControlNotifications.DOUBLECLICKED: OnDoubleClicked?.Invoke(this); break;
            case ButtonControlNotifications.SETFOCUS: OnSetFocus?.Invoke(this); break;
            case ButtonControlNotifications.KILLFOCUS: OnKillFocus?.Invoke(this); break;
            case ButtonControlNotifications.PAINT:
            case ButtonControlNotifications.HILITE:
            case ButtonControlNotifications.UNHILITE:
            case ButtonControlNotifications.DISABLE: break;
        }
    }
}
