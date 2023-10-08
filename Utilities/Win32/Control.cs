using System.Diagnostics;

namespace Win32.Utilities
{
    public partial class Control : Window
    {
        public delegate void EventHandler(HWND parent, ushort code);

        public event EventHandler? OnEvent;

        public Control() : base() { }
        public Control(HWND handle) : base(handle) { }

        public virtual void DispatchEvent(HWND parent, uint message, WPARAM wParam, LPARAM lParam)
        {
            if (lParam != Handle) return;

            ushort code = Macros.HIWORD(wParam);

            OnEvent?.Invoke(parent, code);

            HandleEvent(parent, code);
        }

        protected virtual void HandleEvent(HWND parent, ushort code) { }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool Enabled
        {
            get => User32.IsWindowEnabled(Handle) != FALSE;
            set => _ = User32.EnableWindow(Handle, value ? TRUE : FALSE);
        }
    }
}
