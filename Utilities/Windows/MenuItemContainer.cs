using System.Collections;
using System.Diagnostics;

namespace Win32
{
    public class MenuItemContainer : IEnumerable<MenuItem>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        readonly HMENU MenuHandle;

        public MenuItemContainer(HWND menuHandle) => MenuHandle = menuHandle;

        [DebuggerBrowsable(Utils.GlobalDebuggerBrowsable)]
        public int Count
        {
            get
            {
                int result = User32.GetMenuItemCount(MenuHandle);
                if (result == -1)
                { throw WindowsException.Get(); }
                return result;
            }
        }

        public MenuItem this[int index] => new(MenuHandle, index);

        public IEnumerator<MenuItem> GetEnumerator()
        {
            int menuItemCount = Count;
            for (int i = 0; i < menuItemCount; i++)
            {
                yield return new MenuItem(MenuHandle, i);
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public MenuItem[] ToArray()
        {
            int menuItemCount = Count;
            MenuItem[] result = new MenuItem[menuItemCount];
            for (int i = 0; i < menuItemCount; i++)
            {
                result[i] = new MenuItem(MenuHandle, i);
            }
            return result;
        }
    }
}
