using System.ComponentModel;
using System.Diagnostics;

namespace Win32
{
    public enum MenuItemType : uint
    {
        Text = MFT.STRING,
        Bitmap = MFT.BITMAP,
        Separator = MFT.SEPARATOR,
    }

    public class MenuItem
    {
        readonly HMENU _parentHandle;
        readonly int _index;

        public MenuItem(HWND parentHandle, int index)
        {
            _parentHandle = parentHandle;
            _index = index;
        }

        /// <exception cref="InvalidEnumArgumentException"/>
        /// <exception cref="GeneralException"/>
        public void SetState(MenuItemState state)
        {
            uint state_ = state switch
            {
                MenuItemState.Enabled => 0x00000000,
                MenuItemState.Disabled => 0x00000002,
                MenuItemState.Grayed => 0x00000001,
                _ => throw new InvalidEnumArgumentException(nameof(state), (int)(uint)state, typeof(MenuItemState)),
            };
            int prevState = User32.EnableMenuItem(_parentHandle, (uint)_index, 0x00000400 | state_);
            if (prevState == -1)
            { throw new GeneralException($"Menu item at index {_index} does not exists in menu {_parentHandle}"); }
        }

        /// <exception cref="WindowsException"/>
        unsafe public string Text
        {
            get
            {
                MENUITEMINFOW info = MENUITEMINFOW.Create();
                info.Mask = MenuItemInfoMasks.STRING;
                info.TypeData = null;
                if (User32.GetMenuItemInfoW(_parentHandle, (uint)_index, TRUE, &info) == 0)
                { throw WindowsException.Get(); }
                uint bufferSize = info.cch + 1;
                fixed (WCHAR* buffer = new string('\0', (int)bufferSize))
                {
                    info = MENUITEMINFOW.Create();
                    info.Mask = MenuItemInfoMasks.STRING;
                    info.TypeData = buffer;
                    info.cch = bufferSize;
                    if (User32.GetMenuItemInfoW(_parentHandle, (uint)_index, TRUE, &info) == 0)
                    { throw WindowsException.Get(); }
                    return new string(buffer);
                }
            }
            set
            {
                fixed (WCHAR* textPtr = value)
                {
                    MENUITEMINFOW info = MENUITEMINFOW.Create();
                    info.Mask = MenuItemInfoMasks.STRING;
                    info.TypeData = textPtr;
                    if (User32.SetMenuItemInfoW(_parentHandle, (uint)_index, TRUE, &info) == 0)
                    { throw WindowsException.Get(); }
                }
            }
        }

        /// <exception cref="WindowsException"/>
        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        unsafe public MenuItemType Type
        {
            get
            {
                MENUITEMINFOW info = MENUITEMINFOW.Create();
                info.Mask = MenuItemInfoMasks.FTYPE;
                if (User32.GetMenuItemInfoW(_parentHandle, (uint)_index, TRUE, &info) == 0)
                { throw WindowsException.Get(); }
                return (MenuItemType)info.Type;
            }
            set
            {
                MENUITEMINFOW info = MENUITEMINFOW.Create();
                info.Mask = MenuItemInfoMasks.FTYPE;
                info.Type = (uint)value;
                if (User32.SetMenuItemInfoW(_parentHandle, (uint)_index, TRUE, &info) == 0)
                { throw WindowsException.Get(); }
            }
        }

        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public int Id
        {
            get
            {
                uint id = User32.GetMenuItemID(_parentHandle, _index);
                if (id == unchecked((uint)-1))
                { return -1; }
                return (int)id;
            }
        }

        /// <exception cref="WindowsException"/>
        unsafe public MENUITEMINFOW Info
        {
            get
            {
                MENUITEMINFOW info = MENUITEMINFOW.Create();
                if (User32.GetMenuItemInfoW(_parentHandle, (uint)_index, TRUE, &info) == 0)
                { throw WindowsException.Get(); }
                return info;
            }
            set
            {
                if (User32.SetMenuItemInfoW(_parentHandle, (uint)_index, TRUE, &value) == 0)
                { throw WindowsException.Get(); }
            }
        }

        /// <exception cref="WindowsException"/>
        public void SetDefault()
        {
            if (User32.SetMenuDefaultItem(_parentHandle, (uint)_index, TRUE) == 0)
            { throw WindowsException.Get(); }
        }

        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        unsafe public Menu? SubMenu
        {
            get
            {
                HMENU subMenuHandle = User32.GetSubMenu(_parentHandle, _index);
                if (subMenuHandle == HMENU.Zero)
                { return null; }
                return new Menu(subMenuHandle);
            }
            set
            {
                MENUITEMINFOW info = MENUITEMINFOW.Create();
                info.Mask = MenuItemInfoMasks.SUBMENU;
                info.SubMenuHandle = (value == null) ? HMENU.Zero : value.Handle;
                if (User32.SetMenuItemInfoW(_parentHandle, (uint)_index, TRUE, &info) == 0)
                { throw WindowsException.Get(); }
            }
        }

        /// <exception cref="WindowsException"/>
        public void Remove()
        {
            if (User32.RemoveMenu(_parentHandle, (uint)_index, MenuFlags.BYPOSITION) == 0)
            { throw WindowsException.Get(); }
        }

        public bool Hilite(HWND window, bool hilite)
        {
            int isChanged = User32.HiliteMenuItem(window, _parentHandle, (uint)_index, MenuFlags.BYPOSITION | (hilite ? MenuFlags.HILITE : MenuFlags.UNHILITE));
            return isChanged != 0;
        }
    }
}
