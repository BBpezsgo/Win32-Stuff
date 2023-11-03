namespace Win32.Utilities
{
    public class StaticControl : Control
    {
        public StaticControl(
            HWND parent,
            string? label,
            int x, int y,
            int width, int height,
            ushort id,
            uint styles = 0
        ) : base(Control.AnyHandle(
            parent,
            label,
            ClassName.STATIC,
            WS.VISIBLE | WS.CHILD | styles,
            x, y,
            width, height,
            id)
        )
        { }
    }
}
