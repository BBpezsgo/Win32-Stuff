namespace Win32.Forms;

[Flags]
public enum LWA : DWORD
{
    /// <summary>
    /// Use <c>crKey</c> as the transparency color.
    /// </summary>
    ColorKey = 0x00000001,
    /// <summary>
    /// Use <c>bAlpha</c> to determine the opacity of the layered window.
    /// </summary>
    Alpha = 0x00000002,
}
