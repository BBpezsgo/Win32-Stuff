namespace Win32.Forms;

public partial class ComboBox
{
    public static int GetSelectedIndex(HWND handle)
        => User32.SendMessage(handle, ComboBoxControlMessage.GETCURSEL, WPARAM.Zero, LPARAM.Zero).ToInt32();

    public static void SetSelectedIndex(HWND handle, int index)
        => User32.SendMessage(handle, ComboBoxControlMessage.SETCURSEL, (WPARAM)index, LPARAM.Zero);

    public static unsafe int AddString(HWND handle, string text)
    {
        fixed (char* newElementText = text)
        {
            return User32.SendMessage(handle, ComboBoxControlMessage.ADDSTRING, WPARAM.Zero, (LPARAM)newElementText).ToInt32();
        }
    }

    public static unsafe string GetString(HWND handle, int index)
    {
        int length = User32.SendMessage(handle, ComboBoxControlMessage.GETLBTEXTLEN, (WPARAM)index, (LPARAM)0).ToInt32();
        fixed (char* text = new string(' ', length))
        {
            User32.SendMessage(handle, ComboBoxControlMessage.GETLBTEXT, (WPARAM)index, (LPARAM)text).ToInt32();

            return new string(text);
        }
    }
}
