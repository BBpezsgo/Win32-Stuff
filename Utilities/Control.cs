namespace Win32.Utilities
{
    public partial class Control : Window
    {
        public delegate LRESULT? EventHandler(HWND sender, HWND parent, ushort code);

        public EventHandler? OnEvent;

        public Control()
            : base()
        {

        }

        public Control(HWND handle)
            : base(handle)
        {

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
