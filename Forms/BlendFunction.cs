namespace Win32.Forms;

[StructLayout(LayoutKind.Sequential)]
public struct BlendFunction
{
    public BYTE BlendOp;
    public BYTE BlendFlags;
    public BYTE SourceConstantAlpha;
    public BYTE AlphaFormat;
}
