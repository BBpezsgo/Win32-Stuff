namespace Win32.Gdi32;

public static class PenStyle
{
    public const int Mask = 0x0000000F;

    public const int Solid = 0;
    /// <summary>
    /// -------
    /// </summary>
    public const int Dash = 1;
    /// <summary>
    /// .......
    /// </summary>
    public const int Dot = 2;
    /// <summary>
    /// _._._._
    /// </summary>
    public const int DashDot = 3;
    /// <summary>
    /// _.._.._
    /// </summary>
    public const int DashDotDot = 4;
    public const int Null = 5;
    public const int InsideFrame = 6;
    public const int UserStyle = 7;
    public const int Alternate = 8;
}

public static class PenEndcap
{
    public const int Mask = 0x00000F00;

    public const int Round = 0x00000000;
    public const int Square = 0x00000100;
    public const int Flat = 0x00000200;
}

public static class PenJoin
{
    public const int Mask = 0x0000F000;

    public const int Round = 0x00000000;
    public const int Bevel = 0x00001000;
    public const int Miter = 0x00002000;
}

public static class PenType
{
    public const int Mask = 0x000F0000;

    public const int Cosmetric = 0x00000000;
    public const int Geometric = 0x00010000;
}
