namespace Win32.Forms;

[SupportedOSPlatform("windows")]
public class ListBox : Control
{
    public ListBox(
        Form parent,
        string label,
        RECT rect
    ) : base(
        parent,
        label,
        Forms.ClassName.ListBox,
        WindowStyles.TABSTOP | WindowStyles.VISIBLE | WindowStyles.CHILD | WindowStyles.BORDER,
        rect,
        parent.GenerateControlId()
    )
    { }

    public ListBox(HWND handle) : base(handle) { }

    const int ERR = -1;
    const int ERRSPACE = -2;

    /// <exception cref="GeneralException"/>
    public unsafe int AddItem(string item)
    {
        fixed (char* itemPtr = item)
        {
            int res = (int)SendMessage(ListBoxMessages.ADDSTRING, default, (nint)itemPtr);
            return res switch
            {
                ERR => throw new GeneralException("Failed to add item"),
                ERRSPACE => throw new GeneralException("There is insufficient space to store the new item"),
                _ => res,
            };
        }
    }

    /// <exception cref="GeneralException"/>
    public unsafe int InsertItem(int index, string item)
    {
        fixed (char* itemPtr = item)
        {
            int res = (int)SendMessage(ListBoxMessages.INSERTSTRING, (nuint)index, (nint)itemPtr);
            return res switch
            {
                ERR => throw new GeneralException("Failed to insert item"),
                ERRSPACE => throw new GeneralException("There is insufficient space to store the new item"),
                _ => res,
            };
        }
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public unsafe void DeleteItem(int index)
    {
        int res = (int)SendMessage(ListBoxMessages.DELETESTRING, (nuint)index, default);
        if (res == ERR) throw new ArgumentOutOfRangeException(nameof(index), index, "Index greater than the number of items in the list");
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public unsafe string GetItem(int index)
    {
        int textLength = (int)SendMessage(ListBoxMessages.GETTEXTLEN, (nuint)index, default);
        if (textLength == ERR) throw new ArgumentOutOfRangeException(nameof(index), index, "Invalid index");
        char[] res = new char[textLength + 1];
        fixed (char* resPtr = res)
        {
            int res2 = (int)SendMessage(ListBoxMessages.GETTEXT, (nuint)index, (nint)resPtr);
            if (res2 == ERR) throw new ArgumentOutOfRangeException(nameof(index), index, "Invalid index");
            return new string(resPtr, 0, res2);
        }
    }

    /// <exception cref="GeneralException"/>
    public unsafe int ItemCount
    {
        get
        {
            int res = (int)SendMessage(ListBoxMessages.GETCOUNT, default, default);
            if (res == ERR) throw new GeneralException("Error");
            return res;
        }
    }

    /// <exception cref="GeneralException"/>
    public unsafe int SelectedIndex
    {
        get
        {
            int res = (int)SendMessage(ListBoxMessages.GETCURSEL, default, default);
            if (res == ERR) throw new GeneralException("Error");
            return res;
        }
    }
}
