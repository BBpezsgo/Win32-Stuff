namespace Win32
{
    public class IpAddress : Control
    {
        public IpAddress(
            HWND parent,
            RECT rect,
            ushort id
        ) : base(
            parent,
            string.Empty,
            ClassName.IP_ADDRESS,
            WS.TABSTOP | WS.VISIBLE | WS.CHILD,
            rect,
            id
        )
        { }

        public IpAddress(
            Form parent,
            RECT rect,
            out ushort id
        ) : this(
            parent.Handle,
            rect,
            parent.GenerateControlId(out id)
        )
        { }

        public IpAddress(HWND handle) : base(handle) { }

        public override void HandleNotification(Window parent, ushort code)
        {
            if (code == IPN.FIELDCHANGED)
            {

            }
        }
    }
}
