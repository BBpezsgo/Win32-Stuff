namespace Win32
{
    public class StaticControl : Control
    {
        public SimpleEventHandler<StaticControl>? OnClick;

        public StaticControl(
            HWND parent,
            string? label,
            RECT rect,
            ushort id,
            uint styles = 0
        ) : base(
            parent,
            label,
            ClassName.STATIC,
            WS.VISIBLE | WS.CHILD | SS.NOTIFY | styles,
            rect,
            id
        )
        { }

        public StaticControl(
            Form parent,
            string label,
            RECT rect,
            out ushort id
        ) : this(
            parent.Handle,
            label,
            rect,
            parent.GenerateControlId(out id)
        )
        { }

        public StaticControl(HWND handle) : base(handle) { }

        public void SetImage(HANDLE image, int type) => SendMessage(STM.SETIMAGE, (WPARAM)type, image);
        public HANDLE GetImage(int type) => SendMessage(STM.SETIMAGE, (WPARAM)type, LPARAM.Zero);

        unsafe public void SetIcon(HICON icon) => SendMessage(STM.SETICON, (WPARAM)(void*)icon, LPARAM.Zero);
        unsafe public HICON GetIcon() => SendMessage(STM.SETICON, WPARAM.Zero, LPARAM.Zero);

        public override void HandleNotification(Window parent, ushort code)
        {
            switch (code)
            {
                case STN.CLICKED:
                    OnClick?.Invoke(this);
                    break;
                case STN.DBLCLK:
                    break;
                case STN.ENABLE:
                    break;
                case STN.DISABLE:
                    break;
                default:
                    break;
            }
        }
    }
}
