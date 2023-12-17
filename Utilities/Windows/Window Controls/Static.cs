namespace Win32
{
    [SupportedOSPlatform("windows")]
    public class StaticControl : Control
    {
        public SimpleEventHandler<StaticControl>? OnClick;

        public StaticControl(
            Form parent,
            string label,
            RECT rect,
            out ushort id
        ) : base(
            parent,
            label,
            LowLevel.ClassName.STATIC,
            WindowStyles.VISIBLE | WindowStyles.CHILD | StaticControlConstants.NOTIFY,
            rect,
            parent.GenerateControlId(out id)
        )
        { }

        public StaticControl(HWND handle) : base(handle) { }

        public void SetImage(HANDLE image, int type) => SendMessage(StaticControlMessage.SETIMAGE, (WPARAM)type, image);
        public HANDLE GetImage(int type) => SendMessage(StaticControlMessage.SETIMAGE, (WPARAM)type, LPARAM.Zero);

        unsafe public void SetIcon(HICON icon) => SendMessage(StaticControlMessage.SETICON, (WPARAM)(void*)icon, LPARAM.Zero);
        unsafe public HICON GetIcon() => SendMessage(StaticControlMessage.SETICON, WPARAM.Zero, LPARAM.Zero);

        public override void HandleNotification(Window parent, ushort code)
        {
            switch (code)
            {
                case StaticControlNotification.CLICKED:
                    OnClick?.Invoke(this);
                    break;
                case StaticControlNotification.DBLCLK:
                    break;
                case StaticControlNotification.ENABLE:
                    break;
                case StaticControlNotification.DISABLE:
                    break;
                default:
                    break;
            }
        }
    }
}
