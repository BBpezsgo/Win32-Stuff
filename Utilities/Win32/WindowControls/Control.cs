namespace Win32.Utilities
{
    public partial class Control : Window
    {
        public Control() : base() { }
        public Control(HWND handle) : base(handle) { }

        public virtual void HandleNotification(Window parent, ushort code) { }
    }
}
