namespace Win32.D2D1;

/// <summary>
/// Stores an ordered pair of integers, typically the width and height of a
/// rectangle.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SizeU
{
    public UINT32 Width;
    public UINT32 Height;
}
