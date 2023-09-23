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
                WS.WS_OVERLAPPED | WS.WS_VISIBLE | WS.WS_CHILD | CBS.CBS_DROPDOWNLIST | CBS.CBS_DROPDOWN | CBS.CBS_HASSTRINGS | WS.WS_VSCROLL | BS.BS_DEFSPLITBUTTON,
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

        protected override void HandleEvent(HWND parent, ushort code)
        {
            if (code == CBN.CBN_SELCHANGE)
            {
                OnSelectionChanged?.Invoke(this, parent);
                return;
            }
            else if (code == CBN.CBN_SETFOCUS)
            {
                return;
            }
            else if (code == CBN.CBN_DROPDOWN)
            {
                return;
            }
            else
            {

            }
        }
    }
}
