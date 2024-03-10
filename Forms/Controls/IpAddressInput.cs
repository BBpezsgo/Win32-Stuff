namespace Win32.Forms;

[SupportedOSPlatform("windows")]
public class IpAddressInput : Control
{
    public IpAddressInput(
        Form parent,
        RECT rect
    ) : base(
        parent,
        null,
        Forms.ClassName.IPAddress,
        WindowStyles.TABSTOP | WindowStyles.VISIBLE | WindowStyles.CHILD,
        rect,
        parent.GenerateControlId()
    )
    { }

    public IpAddressInput(HWND handle) : base(handle) { }
}
