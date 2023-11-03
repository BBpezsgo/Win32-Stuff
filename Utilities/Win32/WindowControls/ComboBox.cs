namespace Win32.Utilities
{
    public partial class ComboBox : Control
    {
        public delegate LRESULT? SelectionChangedHandler(ComboBox sender, Window parent);

        public event SelectionChangedHandler? OnSelectionChanged;

        public ComboBox(
            HWND parent,
            string label,
            int x, int y,
            int width, int height,
            ushort id
        ) : base(Control.AnyHandle(
            parent,
            label,
            ClassName.COMBOBOX,
            WS.OVERLAPPED | WS.VISIBLE | WS.CHILD | CBS.DROPDOWNLIST | CBS.DROPDOWN | CBS.HASSTRINGS | WS.VSCROLL | BS.DEFSPLITBUTTON,
            x, y,
            width, height,
            id)
        )
        { }

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

        public override void HandleNotification(Window parent, ushort code)
        {
            if (code == CBN.SELCHANGE)
            {
                OnSelectionChanged?.Invoke(this, parent);
                return;
            }
            else if (code == CBN.SETFOCUS)
            {
                return;
            }
            else if (code == CBN.DROPDOWN)
            {
                return;
            }
            else
            {

            }
        }
    }
}
