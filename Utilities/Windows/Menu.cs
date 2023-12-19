﻿using System.Diagnostics;
using System.Globalization;

namespace Win32
{
    public enum MenuItemState
    {
        Enabled,
        Disabled,
        Grayed,
    }

    [SupportedOSPlatform("windows")]
    [DebuggerDisplay($"{{{nameof(ToString)}(),nq}}")]
    public class Menu : IDisposable
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        HMENU _handle;

        bool IsDisposed;

        public HMENU Handle => _handle;

        public Menu(HMENU handle)
        {
            _handle = handle;
            IsDisposed = false;
        }

        public static explicit operator Menu(HMENU handle) => new(handle);

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        ~Menu() { Dispose(disposing: false); }
        void Dispose(bool disposing)
        {
            if (IsDisposed) return;

            if (disposing)
            {
                if (User32.DestroyMenu(_handle) == 0)
                { throw WindowsException.Get(); }
            }

            IsDisposed = true;
            _handle = HWND.Zero;
        }

        /// <exception cref="WindowsException"/>
        public unsafe MENUINFO Info
        {
            get
            {
                MENUINFO result = MENUINFO.Create();
                if (User32.GetMenuInfo(_handle, &result) == 0)
                { throw WindowsException.Get(); }
                return result;
            }
            set
            {
                if (User32.SetMenuInfo(_handle, &value) == 0)
                { throw WindowsException.Get(); }
            }
        }

        /// <exception cref="WindowsException"/>
        public void Destroy()
        {
            if (User32.DestroyMenu(_handle) == 0)
            { throw WindowsException.Get(); }
        }

        /// <exception cref="WindowsException"/>
        public static Menu Create()
        {
            HMENU handle = User32.CreateMenu();
            if (handle == HMENU.Zero)
            { throw WindowsException.Get(); }
            return new Menu(handle);
        }

        /// <exception cref="WindowsException"/>
        public unsafe void AppendSeparator()
        {
            if (User32.AppendMenuW(_handle, MenuFlags.SEPARATOR, UINT_PTR.Zero, null) == 0)
            { throw WindowsException.Get(); }
        }

        /// <exception cref="WindowsException"/>
        public unsafe void AppendMenu(int id, string label)
        {
            fixed (char* labelPtr = label)
            {
                if (User32.AppendMenuW(_handle, MenuFlags.STRING, (UINT_PTR)id, labelPtr) == 0)
                { throw WindowsException.Get(); }
            }
        }

        /// <exception cref="WindowsException"/>
        public unsafe void AppendMenu(int id, HBITMAP bitmap)
        {
            if (User32.AppendMenuW(_handle, MenuFlags.BITMAP, (UINT_PTR)id, (char*)bitmap) == 0)
            { throw WindowsException.Get(); }
        }

        /// <exception cref="WindowsException"/>
        public unsafe void AppendMenu(int id, void* data)
        {
            if (User32.AppendMenuW(_handle, MenuFlags.OWNERDRAW, (UINT_PTR)id, (char*)data) == 0)
            { throw WindowsException.Get(); }
        }

        /// <exception cref="WindowsException"/>
        public unsafe void InsertMenuItem(uint id, MENUITEMINFOW info)
            => InsertMenuItem(id, &info);

        /// <exception cref="WindowsException"/>
        public unsafe void InsertMenuItem(uint id, MENUITEMINFOW* info)
        {
            if (User32.InsertMenuItemW(_handle, id, FALSE, info) == 0)
            { throw WindowsException.Get(); }
        }

        public MenuItemContainer MenuItems => new(_handle);

        public override string ToString() => "0x" + _handle.ToString("x", CultureInfo.InvariantCulture).PadLeft(16, '0');

        public int GetItemAtPosition(HWND window, POINT screenPosition)
            => User32.MenuItemFromPoint(window, _handle, screenPosition);
        public bool GetItemAtPosition(HWND window, POINT screenPosition, out int itemIndex)
        {
            itemIndex = GetItemAtPosition(window, screenPosition);
            return itemIndex != -1;
        }

        public static bool IsMenu(HMENU handle)
            => User32.IsMenu(handle) != 0;

        /// <exception cref="WindowsException"/>
        public void CheckRadioItem(int first, int last, int index)
        {
            if (User32.CheckMenuRadioItem(_handle, (uint)first, (uint)last, (uint)index, MenuFlags.BYPOSITION) == 0)
            { throw WindowsException.Get(); }
        }
    }
}
