namespace Win32
{
    public class IpAddress : Control
    {
        public IpAddress(
            Form parent,
            RECT rect,
            out ushort id
        ) : base(
            parent,
            null,
            LowLevel.ClassName.IP_ADDRESS,
            WindowStyles.TABSTOP | WindowStyles.VISIBLE | WindowStyles.CHILD,
            rect,
            parent.GenerateControlId(out id)
        )
        { }

        public IpAddress(HWND handle) : base(handle) { }

        public override void HandleNotification(Window parent, ushort code)
        {
            if (code == IPAddressControlNotification.FIELDCHANGED)
            {

            }
        }
    }
}
