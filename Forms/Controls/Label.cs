namespace Win32.Forms;

[SupportedOSPlatform("windows")]
public class StaticControl : Control
{
    /// <summary>
    /// The <see cref="OnClick"/> notification code is sent when
    /// the user clicks a static control that has the <see cref="StaticControlConstants.NOTIFY"/> style.
    /// </summary>
    public ControlNotification<StaticControl>? OnClick;

    /// <summary>
    /// The <see cref="OnDoubleClick"/> notification code is sent when the user
    /// double-clicks a static control that has
    /// the <see cref="StaticControlConstants.NOTIFY"/> style.
    /// </summary>
    public ControlNotification<StaticControl>? OnDoubleClick;

    /// <summary>
    /// The <see cref="OnDisable"/> notification code is sent when a static control is disabled.
    /// The static control must have the
    /// <see cref="StaticControlConstants.NOTIFY"/> style to receive this notification code.
    /// </summary>
    public ControlNotification<StaticControl>? OnDisable;

    /// <summary>
    /// The <see cref="OnEnable"/> notification code is sent when a static control is enabled.
    /// The static control must have the
    /// <see cref="StaticControlConstants.NOTIFY"/> style to receive this notification code.
    /// </summary>
    public ControlNotification<StaticControl>? OnEnable;

    public StaticControl(
        Form parent,
        string label,
        RECT rect
    ) : base(
        parent,
        label,
        Forms.ClassName.Static,
        WindowStyles.VISIBLE | WindowStyles.CHILD | StaticControlConstants.NOTIFY,
        rect,
        parent.GenerateControlId()
    )
    { }

    public StaticControl(HWND handle) : base(handle) { }

    public void SetImage(HANDLE image, int type) => SendMessage(StaticControlMessage.SETIMAGE, (WPARAM)type, image);
    public HANDLE GetImage(int type) => SendMessage(StaticControlMessage.SETIMAGE, (WPARAM)type, LPARAM.Zero);

    public unsafe void SetIcon(HICON icon) => SendMessage(StaticControlMessage.SETICON, (WPARAM)(void*)icon, LPARAM.Zero);
    public unsafe HICON GetIcon() => SendMessage(StaticControlMessage.SETICON, WPARAM.Zero, LPARAM.Zero);

    public override void HandleNotification(Window parent, ushort code)
    {
        switch (code)
        {
            case StaticControlNotification.CLICKED: OnClick?.Invoke(this); break;
            case StaticControlNotification.DBLCLK: OnDoubleClick?.Invoke(this); break;
            case StaticControlNotification.ENABLE: OnEnable?.Invoke(this); break;
            case StaticControlNotification.DISABLE: OnDisable?.Invoke(this); break;
        }
    }
}
