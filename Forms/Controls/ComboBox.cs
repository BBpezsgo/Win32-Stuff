namespace Win32.Forms;

[SupportedOSPlatform("windows")]
public partial class ComboBox : Control
{
    /// <summary>
    /// Sent when the list box of a combo box has been closed.
    /// </summary>
    public event ControlNotification<ComboBox>? OnCloseup;

    /// <summary>
    /// Sent when the user double-clicks a string in the list box of a combo box.
    /// </summary>
    public event ControlNotification<ComboBox>? OnDoubleClick;

    /// <summary>
    /// Sent when the list box of a combo box is about to be made visible.
    /// </summary>
    public event ControlNotification<ComboBox>? OnDropdown;

    /// <summary>
    /// Sent after the user has taken an action that may have altered the text
    /// in the edit control portion of a combo box. Unlike
    /// the <see cref="OnEditUpdate"/>, this
    /// notification code is sent after the
    /// system updates the screen.
    /// </summary>
    public event ControlNotification<ComboBox>? OnEditChange;

    /// <summary>
    /// Sent when the edit control portion of a combo box is about to
    /// display altered text. This notification code is sent after
    /// the control has formatted the text, but
    /// before it displays the text.
    /// </summary>
    public event ControlNotification<ComboBox>? OnEditUpdate;

    /// <summary>
    /// Sent when a combo box cannot allocate enough memory to meet a specific request.
    /// </summary>
    public event ControlNotification<ComboBox>? OnErrorSpace;

    /// <summary>
    /// Sent when a combo box loses the keyboard focus.
    /// </summary>
    public event ControlNotification<ComboBox>? OnKillFocus;

    /// <summary>
    /// Sent when the user changes the current selection in the
    /// list box of a combo box. The user can change the
    /// selection by clicking in the list box or
    /// by using the arrow keys.
    /// </summary>
    public event ControlNotification<ComboBox>? OnSelectionChanged;

    /// <summary>
    /// Sent when the user selects an item, but then selects
    /// another control or closes the dialog box.
    /// It indicates the user's initial selection is to be ignored.
    /// </summary>
    public event ControlNotification<ComboBox>? OnSelectionCanceled;

    /// <summary>
    /// Sent when the user selects a list item, or selects an item and
    /// then closes the list. It indicates that the
    /// user's selection is to be processed.
    /// </summary>
    /// <remarks>
    /// In a combo box with the <see cref="ComboBoxControlStyles.SIMPLE"/> style,
    /// this sent immediately before every <see cref="OnSelectionChanged"/> notification code.
    /// </remarks>
    public event ControlNotification<ComboBox>? OnSelectionOk;

    /// <summary>
    /// Sent when a combo box receives the keyboard focus.
    /// </summary>
    public event ControlNotification<ComboBox>? OnSetFocus;

    public ComboBox(
        Form parent,
        string label,
        RECT rect
    ) : base(
        parent,
        label,
        Forms.ClassName.ComboBox,
        WindowStyles.OVERLAPPED | WindowStyles.VISIBLE | WindowStyles.CHILD | ComboBoxControlStyles.DROPDOWNLIST | ComboBoxControlStyles.DROPDOWN | ComboBoxControlStyles.HASSTRINGS | WindowStyles.VSCROLL | ButtonControlStyles.DEFSPLITBUTTON,
        rect,
        parent.GenerateControlId()
    )
    { }

    public ComboBox(HWND handle) : base(handle) { }

    public int SelectedIndex
    {
        get => ComboBox.GetSelectedIndex(Handle);
        set => ComboBox.SetSelectedIndex(Handle, value);
    }

    public int AddString(string text) => ComboBox.AddString(Handle, text);

    public string GetString(int index) => ComboBox.GetString(Handle, index);

    public override void HandleNotification(Window parent, ushort code)
    {
        switch ((uint)code)
        {
            case ComboBoxControlNotification.CLOSEUP: OnCloseup?.Invoke(this); break;
            case ComboBoxControlNotification.DBLCLK: OnDoubleClick?.Invoke(this); break;
            case ComboBoxControlNotification.DROPDOWN: OnDropdown?.Invoke(this); break;
            case ComboBoxControlNotification.EDITCHANGE: OnEditChange?.Invoke(this); break;
            case ComboBoxControlNotification.EDITUPDATE: OnEditUpdate?.Invoke(this); break;
            case ComboBoxControlNotification.ERRSPACE: OnErrorSpace?.Invoke(this); break;
            case ComboBoxControlNotification.KILLFOCUS: OnKillFocus?.Invoke(this); break;
            case ComboBoxControlNotification.SELCHANGE: OnSelectionChanged?.Invoke(this); break;
            case ComboBoxControlNotification.SELENDCANCEL: OnSelectionCanceled?.Invoke(this); break;
            case ComboBoxControlNotification.SELENDOK: OnSelectionOk?.Invoke(this); break;
            case ComboBoxControlNotification.SETFOCUS: OnSetFocus?.Invoke(this);  break;
        }
    }
}
