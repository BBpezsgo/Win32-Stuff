namespace Win32.Forms;

[SupportedOSPlatform("windows")]
public class EditControl : Control
{
    /// <summary>
    /// Sent when the user has changed the edit control direction to left-to-right.
    /// </summary>
    public event ControlNotification<EditControl>? OnDirectionLTR;

    /// <summary>
    /// Sent when the user has changed the edit control direction to right-to-left.
    /// </summary>
    public event ControlNotification<EditControl>? OnDirectionRTL;

    /// <summary>
    /// Sent when the user has taken an action that may
    /// have altered text in an edit control. Unlike
    /// the <see cref="OnUpdate"/> notification code, this
    /// notification code is sent after the
    /// system updates the screen.
    /// </summary>
    public event ControlNotification<EditControl>? OnChanged;

    /// <summary>
    /// Sent when an edit control cannot allocate enough memory to meet a specific request.
    /// </summary>
    public event ControlNotification<EditControl>? OnErrorSpace;

    /// <summary>
    /// Sent when the user clicks an edit control's horizontal scroll bar.
    /// </summary>
    public event ControlNotification<EditControl>? OnHScroll;

    /// <summary>
    /// Sent when an edit control loses the keyboard focus.
    /// </summary>
    public event ControlNotification<EditControl>? OnKillFocus;

    /// <summary>
    /// <para>
    /// Sent when the current text insertion has exceeded the
    /// specified number of characters for the edit control.
    /// The text insertion has been truncated.
    /// </para>
    /// <para>
    /// This notification code is also sent when an edit control
    /// does not have the <see cref="EditControlStyles.AUTOHSCROLL"/> style and the number of
    /// characters to be inserted would exceed the width of
    /// the edit control.
    /// </para>
    /// <para>
    /// This notification code is also sent when an edit control
    /// does not have the <see cref="EditControlStyles.AUTOVSCROLL"/> style and the total number
    /// of lines resulting from a text insertion would exceed
    /// the height of the edit control.
    /// </para>
    /// </summary>
    public event ControlNotification<EditControl>? OnMaxText;

    /// <summary>
    /// Sent when an edit control receives the keyboard focus.
    /// </summary>
    public event ControlNotification<EditControl>? OnSetFocus;

    /// <summary>
    /// Sent when an edit control is about to redraw itself.
    /// This notification code is sent after the
    /// control has formatted the text, but before
    /// it displays the text. This makes it possible
    /// to resize the edit control window, if necessary.
    /// </summary>
    public event ControlNotification<EditControl>? OnUpdate;

    /// <summary>
    /// Sent when the user clicks an edit control's vertical
    /// scroll bar or when the user scrolls the mouse
    /// wheel over the edit control.
    /// </summary>
    public event ControlNotification<EditControl>? OnVScroll;

    public EditControl(
        Form parent,
        string label,
        RECT rect
    ) : base(
        parent,
        label,
        Forms.ClassName.Edit,
        WindowStyles.BORDER | WindowStyles.TABSTOP | WindowStyles.VISIBLE | WindowStyles.CHILD | EditControlStyles.LEFT,
        rect,
        parent.GenerateControlId()
    )
    { }

    public EditControl(HWND handle) : base(handle) { }

    public override void HandleNotification(Window parent, ushort code)
    {
        switch (code)
        {
            case EditControlNotification.ALIGN_LTR_EC: OnDirectionLTR?.Invoke(this); break;
            case EditControlNotification.ALIGN_RTL_EC: OnDirectionRTL?.Invoke(this); break;
            case EditControlNotification.CHANGE: OnChanged?.Invoke(this); break;
            case EditControlNotification.ERRSPACE: OnErrorSpace?.Invoke(this); break;
            case EditControlNotification.HSCROLL: OnHScroll?.Invoke(this); break;
            case EditControlNotification.KILLFOCUS: OnKillFocus?.Invoke(this); break;
            case EditControlNotification.MAXTEXT: OnMaxText?.Invoke(this); break;
            case EditControlNotification.SETFOCUS: OnSetFocus?.Invoke(this); break;
            case EditControlNotification.UPDATE: OnUpdate?.Invoke(this); break;
            case EditControlNotification.VSCROLL: OnVScroll?.Invoke(this); break;
        }
    }
}
