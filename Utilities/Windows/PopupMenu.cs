﻿namespace Win32
{
    public class PopupMenu : Menu
    {
        public PopupMenu(HMENU handle) : base(handle) { }

        public static explicit operator PopupMenu(HMENU handle) => new(handle);

        /// <exception cref="WindowsException"/>
        public static PopupMenu CreatePopup()
        {
            HMENU handle = User32.CreatePopupMenu();
            if (handle == HMENU.Zero)
            { throw WindowsException.Get(); }
            return new PopupMenu(handle);
        }

        /// <exception cref="WindowsException"/>
        unsafe public void Show(HWND window, int x, int y)
        {
            if (User32.TrackPopupMenu(Handle, 0, x, y, 0, window, null) == 0)
            {
                uint errorCode = Kernel32.GetLastError();
                if (errorCode != 1446)
                { throw WindowsException.Get(errorCode); }
            }
        }

        /// <exception cref="WindowsException"/>
        unsafe public bool Show(HWND window, int x, int y, out int selectedId)
        {
            selectedId = User32.TrackPopupMenu(Handle, TPM.RETURNCMD, x, y, 0, window, null);
            return selectedId != 0;
        }

        /// <exception cref="WindowsException"/>
        unsafe public void Show(HWND window, int x, int y, RECT exclude)
        {
            TPMPARAMS @params = TPMPARAMS.Create();
            @params.rcExclude = exclude;
            if (User32.TrackPopupMenuEx(Handle, 0, x, y, window, &@params) == 0)
            { throw WindowsException.Get(); }
        }

        /// <exception cref="WindowsException"/>
        unsafe public bool Show(HWND window, int x, int y, RECT exclude, out int selectedId)
        {
            TPMPARAMS @params = TPMPARAMS.Create();
            @params.rcExclude = exclude;
            selectedId = User32.TrackPopupMenuEx(Handle, TPM.RETURNCMD, x, y, window, &@params);
            return selectedId != 0;
        }
    }
}