namespace Win32
{
    public class Button : Control
    {
        public delegate void ClickEventHandler(Button sender);

        public event ClickEventHandler? OnClick;

        public Button(
            Form parent,
            string label,
            RECT rect,
            out ushort id
        ) : base(
            parent,
            label,
            LowLevel.ClassName.BUTTON,
            WindowStyles.TABSTOP | WindowStyles.VISIBLE | WindowStyles.CHILD | ButtonControlStyles.DEFPUSHBUTTON,
            rect,
            parent.GenerateControlId(out id)
        )
        { }

        public Button(HWND handle) : base(handle) { }

        public override void HandleNotification(Window parent, ushort code)
        {
            if (code == ButtonControlNotifications.CLICKED)
            {
                OnClick?.Invoke(this);
                return;
            }
        }
    }
}
