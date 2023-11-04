namespace Win32
{
    public class EditControl : Control
    {
        public EditControl(
            HWND parent,
            string label,
            RECT rect,
            ushort id
        ) : base(
            parent,
            label,
            ClassName.EDIT,
            WS.TABSTOP | WS.VISIBLE | WS.CHILD | ES.LEFT,
            rect,
            id
        )
        { }

        public EditControl(
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

        public EditControl(HWND handle) : base(handle) { }

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
