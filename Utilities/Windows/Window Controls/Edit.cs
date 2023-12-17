namespace Win32
{
    [SupportedOSPlatform("windows")]
    public class EditControl : Control
    {
        public EditControl(
            Form parent,
            string label,
            RECT rect,
            out ushort id
        ) : base(
            parent,
            label,
            LowLevel.ClassName.EDIT,
            WindowStyles.TABSTOP | WindowStyles.VISIBLE | WindowStyles.CHILD | EditControlStyles.LEFT,
            rect,
            parent.GenerateControlId(out id)
        )
        { }

        public EditControl(HWND handle) : base(handle) { }

        public override void HandleNotification(Window parent, ushort code)
        {
            switch (code)
            {
                case EditControlNotification.SETFOCUS:
                    break;
                case EditControlNotification.KILLFOCUS:
                    break;
                case EditControlNotification.CHANGE:
                    break;
                case EditControlNotification.UPDATE:
                    break;
                case EditControlNotification.ERRSPACE:
                    break;
                case EditControlNotification.MAXTEXT:
                    break;
                case EditControlNotification.HSCROLL:
                    break;
                case EditControlNotification.VSCROLL:
                    break;
                case EditControlNotification.ALIGN_LTR_EC:
                    break;
                case EditControlNotification.ALIGN_RTL_EC:
                    break;
            }
        }
    }
}
