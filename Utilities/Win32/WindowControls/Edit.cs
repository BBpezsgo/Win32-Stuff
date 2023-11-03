namespace Win32.Utilities
{
    public class EditControl : Control
    {
        public EditControl(
            HWND parent,
            string label,
            int x, int y,
            int width, int height,
            ushort id
        ) : base(Control.AnyHandle(
            parent,
            label,
            ClassName.EDIT,
            WS.TABSTOP | WS.VISIBLE | WS.CHILD | ES.LEFT,
            x, y,
            width, height,
            id)
        )
        { }

        public override void HandleNotification(Window parent, ushort code)
        {
            switch (code)
            {
                case EN.SETFOCUS:
                    break;
                case EN.KILLFOCUS:
                    break;
                case EN.CHANGE:
                    break;
                case EN.UPDATE:
                    break;
                case EN.ERRSPACE:
                    break;
                case EN.MAXTEXT:
                    break;
                case EN.HSCROLL:
                    break;
                case EN.VSCROLL:
                    break;
                case EN.ALIGN_LTR_EC:
                    break;
                case EN.ALIGN_RTL_EC:
                    break;
            }
        }
    }
}
