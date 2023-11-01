namespace Win32.Utilities
{
    public class Button : Control
    {
        public delegate void SimpleEventHandler(Button sender);

        public event SimpleEventHandler? OnClick;

        unsafe public Button(
            HWND parent,
            string label,
            int x,
            int y,
            int width,
            int height,
            ushort id)
        {
            Handle = Control.AnyHandle(
                        parent,
                        label,
                        ClassName.BUTTON,
                        WS.TABSTOP | WS.VISIBLE | WS.CHILD | BS.DEFPUSHBUTTON,
                        x,
                        y,
                        width,
                        height,
                        id);
        }

        protected override void HandleEvent(HWND parent, ushort code)
        {
            if (code == BN.CLICKED)
            {
                OnClick?.Invoke(this);
                return;
            }
        }
    }
}
