﻿namespace Win32.Forms;

[SupportedOSPlatform("windows")]
public class Control : Window
{
    public ushort Id { get; }

    public Control(HWND handle) : base(handle) { }

    public unsafe Control(
        Form parent,
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
        parent.Controls.Add(id, this);
        Id = id;
    }

    public virtual void HandleNotification(Window parent, ushort code) { }
}
