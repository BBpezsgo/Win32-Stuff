namespace Win32
{
    public class Button : Control
    {
        public delegate void ClickEventHandler(Button sender);

        public event ClickEventHandler? OnClick;

        public Button(
            HWND parent,
            string label,
            RECT rect,
            ushort id
        ) : base(
            parent,
            label,
            ClassName.BUTTON,
            WS.TABSTOP | WS.VISIBLE | WS.CHILD | BS.DEFPUSHBUTTON,
            rect,
            id
        )
        { }

        public Button(
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

        public Button(HWND handle) : base(handle) { }

        public override void HandleNotification(Window parent, ushort code)
        {
            if (code == BN.CLICKED)
            {
                OnClick?.Invoke(this);
                return;
            }
        }
    }
}
