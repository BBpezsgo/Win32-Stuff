﻿namespace Win32
{
    public partial class Control : Window
    {
        public delegate void SimpleEventHandler<T>(T sender);

        public Control() : base() { }
        public Control(HWND handle) : base(handle) { }
        unsafe public Control(
            HWND parent,
            string? name,
            string @class,
            DWORD style,
            RECT rect,
            ushort id)
        {
            fixed (WCHAR* windowNamePtr = name)
            fixed (WCHAR* classNamePtr = @class)
            {
                Handle = User32.CreateWindowExW(
                    0,
                    classNamePtr,
                    windowNamePtr,
                    style,
                    rect.X,
                    rect.Y,
                    rect.Width,
                    rect.Height,
                    parent,
                    (HMENU)id,
                    User32.GetWindowLongPtrW(parent, GWLP.HINSTANCE));
            }
        }

        public virtual void HandleNotification(Window parent, ushort code) { }
    }
}