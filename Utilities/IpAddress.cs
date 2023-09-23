using Win32.Constants.Notification_Codes;

namespace Win32.Utilities
{
    public class IpAddress : Control
    {
        unsafe public IpAddress(
            HWND parent,
            int x,
            int y,
            int width,
            int height,
            ushort id)
        {
            Handle = Control.AnyHandle(
                        parent,
                        "",
                        ClassName.IP_ADDRESS,
                        WS.WS_TABSTOP | WS.WS_VISIBLE | WS.WS_CHILD,
                        x,
                        y,
                        width,
                        height,
                        id);
        }

        protected override void HandleEvent(HWND parent, ushort code)
        {
            if (code == IPN.IPN_FIELDCHANGED)
            {

            }
        }
    }
}
