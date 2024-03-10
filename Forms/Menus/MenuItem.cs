using System.ComponentModel;

namespace Win32.Forms;

[SuppressMessage("Design", "CA1027")]
public enum MenuItemType : uint
{
    Text = MFT.String,
    Bitmap = MFT.Bitmap,
    Separator = MFT.Separator,
}

[SupportedOSPlatform("windows")]
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
    public unsafe string Text
    {
        get
        {
            MenuItemInfo info = MenuItemInfo.Create();
            info.Mask = MenuItemInfoMasks.String;
            info.TypeData = null;
            if (User32.GetMenuItemInfoW(_parentHandle, (uint)_index, TRUE, &info) == 0)
            { throw WindowsException.Get(); }
            uint bufferSize = info.cch + 1;
            fixed (WCHAR* buffer = new string('\0', (int)bufferSize))
            {
                info = MenuItemInfo.Create();
                info.Mask = MenuItemInfoMasks.String;
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
                MenuItemInfo info = MenuItemInfo.Create();
                info.Mask = MenuItemInfoMasks.String;
                info.TypeData = textPtr;
                if (User32.SetMenuItemInfoW(_parentHandle, (uint)_index, TRUE, &info) == 0)
                { throw WindowsException.Get(); }
            }
        }
    }

    /// <exception cref="WindowsException"/>
    [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
    public unsafe MenuItemType Type
    {
        get
        {
            MenuItemInfo info = MenuItemInfo.Create();
            info.Mask = MenuItemInfoMasks.FType;
            if (User32.GetMenuItemInfoW(_parentHandle, (uint)_index, TRUE, &info) == 0)
            { throw WindowsException.Get(); }
            return (MenuItemType)info.Type;
        }
        set
        {
            MenuItemInfo info = MenuItemInfo.Create();
            info.Mask = MenuItemInfoMasks.FType;
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
    public unsafe MenuItemInfo Info
    {
        get
        {
            MenuItemInfo info = MenuItemInfo.Create();
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
    public unsafe Menu? SubMenu
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
            MenuItemInfo info = MenuItemInfo.Create();
            info.Mask = MenuItemInfoMasks.SubMenu;
            info.SubMenuHandle = (value == null) ? HMENU.Zero : value.Handle;
            if (User32.SetMenuItemInfoW(_parentHandle, (uint)_index, TRUE, &info) == 0)
            { throw WindowsException.Get(); }
        }
    }

    /// <exception cref="WindowsException"/>
    public void Remove()
    {
        if (User32.RemoveMenu(_parentHandle, (uint)_index, MenuFlags.ByPosition) == 0)
        { throw WindowsException.Get(); }
    }

    public bool Hilite(HWND window, bool hilite)
    {
        int isChanged = User32.HiliteMenuItem(window, _parentHandle, (uint)_index, MenuFlags.ByPosition | (hilite ? MenuFlags.Hilite : MenuFlags.Unhilite));
        return isChanged != 0;
    }
}
