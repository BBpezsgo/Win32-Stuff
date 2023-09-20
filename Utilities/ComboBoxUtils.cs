namespace Win32.Utilities
{
    public partial class ComboBox
    {
        public static int GetSelectedIndex(HWND handle)
        {
            return User32.SendMessage(handle, CB.CB_GETCURSEL, (WPARAM)0, (LPARAM)0).ToInt32();
        }

        public static void SetSelectedIndex(HWND handle, int index)
        {
            User32.SendMessage(handle, CB.CB_SETCURSEL, (WPARAM)index, (LPARAM)0);
        }

        unsafe public static int AddString(HWND handle, string text)
        {
            fixed (char* newElementText = text)
            {
                return User32.SendMessage(handle, CB.CB_ADDSTRING, (WPARAM)0, (LPARAM)newElementText).ToInt32();
            }
        }

        unsafe public static string GetString(HWND handle, int index)
        {
            int length = User32.SendMessage(handle, CB.CB_GETLBTEXTLEN, (WPARAM)index, (LPARAM)0).ToInt32();
            fixed (char* text = new string(' ', length))
            {
                User32.SendMessage(handle, CB.CB_GETLBTEXT, (WPARAM)index, (LPARAM)text).ToInt32();

                return new string(text);
            }
        }
    }
}
