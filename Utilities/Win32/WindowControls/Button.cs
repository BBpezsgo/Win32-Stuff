namespace Win32.Utilities
{
    public class Button : Control
    {
        public delegate void ClickEventHandler(Button sender);

        public event ClickEventHandler? OnClick;

        public Button(
            HWND parent,
            string label,
            int x, int y,
            int width, int height,
            ushort id
        ) : base(Control.AnyHandle(
            parent,
            label,
            ClassName.BUTTON,
            WS.TABSTOP | WS.VISIBLE | WS.CHILD | BS.DEFPUSHBUTTON,
            x, y,
            width, height,
            id)
        )
        { }

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
