namespace Win32.Forms;

[SupportedOSPlatform("windows")]
public class Header : Control
{
    public Header(
        Form parent,
        string label,
        RECT rect
    ) : base(
        parent,
        label,
        Forms.ClassName.Header,
        WindowStyles.TABSTOP | WindowStyles.VISIBLE | WindowStyles.CHILD | WindowStyles.BORDER | 2,
        rect,
        parent.GenerateControlId()
    )
    { }

    public Header(HWND handle) : base(handle) { }

    public unsafe int InsertItem(int iInsertAfter, int nWidth, string lpsz)
    {
        fixed (WCHAR* lpszPtr = lpsz)
        {
            HDItem hdi = new()
            {
                Mask = HeaderItemMask.Text | HeaderItemMask.Format | HeaderItemMask.Width,
                cxy = nWidth,
                Text = lpszPtr,
                cchTextMax = lpsz.Length / sizeof(char),
                fmt = HeaderFormat.Left | HeaderFormat.String
            };
            nint result = SendMessage(HeaderMessage.INSERTITEM, (WPARAM)iInsertAfter, (LPARAM)(&hdi));
            if (result == -1)
            { throw new GeneralException("Failed to insert item"); }
            return (int)result;
        }
    }

    public unsafe bool DeleteItem(int index)
    {
        return SendMessage(HeaderMessage.DELETEITEM, (nuint)index, 0) == TRUE;
    }

    public unsafe int ItemCount()
    {
        nint result = SendMessage(HeaderMessage.GETITEMCOUNT, 0, 0);
        if (result == -1)
        { throw new GeneralException("Failed to get the item count"); }
        return (int)result;
    }
}
