namespace Win32.Utilities
{
    public partial class ComboBox : Control
    {
        public delegate LRESULT? SelectionChangedHandler(ComboBox sender, HWND parent);

        public event SelectionChangedHandler? OnSelectionChanged;

        public ComboBox(
            HWND parent,
            string label,
            int x,
            int y,
            int width,
            int height,
            ushort id)
        {
            Handle = Control.AnyHandle(
                parent,
                label,
                ClassName.COMBOBOX,
                WS.CHILD | WS.OVERLAPPED | WS.VISIBLE | CBS.CBS_DROPDOWN | CBS.CBS_HASSTRINGS,
                x,
                y,
                width,
                height,
                id);
        }

        public ComboBox(HWND handle) : base(handle) { }

        public int SelectedIndex
        {
            get => ComboBox.GetSelectedIndex(Handle);
            set => ComboBox.SetSelectedIndex(Handle, value);
        }

        public int AddString(string text)
            => ComboBox.AddString(Handle, text);

        public string GetString(int index)
            => ComboBox.GetString(Handle, index);

        protected override HWND? HandleEvent(HWND sender, HWND parent, ushort code)
        {
            if (sender != Handle) return null;

            if (code == CBN.CBN_SELCHANGE)
            { return OnSelectionChanged?.Invoke(this, parent); }

            return null;
        }
    }
}
