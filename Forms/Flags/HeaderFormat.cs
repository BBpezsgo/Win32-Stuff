namespace Win32.Forms;

public static class HeaderFormat
{
    public const int Left = 0x0000;// Same as LVCFMT_LEFT
    public const int Right = 0x0001;// Same as LVCFMT_RIGHT
    public const int Center = 0x0002;// Same as LVCFMT_CENTER
    public const int JustifyMask = 0x0003;// Same as LVCFMT_JUSTIFYMASK
    public const int RTLReading = 0x0004;// Same as LVCFMT_LEFT
    public const int Bitmap = 0x2000;
    public const int String = 0x4000;
    public const int OwnerDraw = 0x8000;// Same as LVCFMT_COL_HAS_IMAGES
    public const int Image = 0x0800;// Same as LVCFMT_IMAGE
    public const int BitmapOnRight = 0x1000;// Same as LVCFMT_BITMAP_ON_RIGHT
    public const int SortUp = 0x0400;
    public const int SortDown = 0x0200;
    public const int CheckBox = 0x0040;
    public const int Checked = 0x0080;
    /// <summary>
    /// Can't resize the column; same as LVCFMT_FIXED_WIDTH
    /// </summary>
    public const int FixedWidth = 0x0100;
    /// <summary>
    /// Column is a split button; same as LVCFMT_SPLITBUTTON
    /// </summary>
    public const int SplitButton = 0x1000000;
}
