namespace Win32.Utilities
{
    public class Button : Control
    {
        public Button(
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
                        WS.WS_TABSTOP | WS.WS_VISIBLE | WS.WS_CHILD | BS.BS_DEFPUSHBUTTON,
                        x,
                        y,
                        width,
                        height,
                        id);
        }
    }
}
