namespace Win32.D2D1;

/// <summary>
/// Allows the drawing state to be atomically created. This also specifies the
/// drawing state that is saved into an <see cref="IDrawingStateBlock"/> object.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct DrawingStateDescription
{
    public AntialiasMode AntialiasMode;
    public TextAntialiasMode TextAntialiasMode;
    public UINT64 Tag1;
    public UINT64 Tag2;
    public MatrixF3X2 Transform;
}
