namespace Win32.Utilities
{
    public partial class Control
    {
    public delegate LRESULT? EventHandler(HWND sender, HWND parent, ushort code);

        HWND _handle;
        public EventHandler? OnEvent;

        public HWND Handle
        {
            get => _handle;
            protected set => _handle = value;
        }

        public Control()
        {
            _handle = HWND.Zero;
        }

        public Control(HWND handle)
        {
            _handle = handle;
        }

        public virtual LRESULT DispatchEvent(HWND parent, uint message, WPARAM wParam, LPARAM lParam)
        {
            ushort code = Macros.HIWORD(wParam);

            OnEvent?.Invoke(lParam, parent, code);

            HandleEvent(lParam, parent, code);

            return User32.DefWindowProcW(parent, message, wParam, lParam);
        }

        protected virtual LRESULT? HandleEvent(HWND sender, HWND parent, ushort code) => null;
    }
}
