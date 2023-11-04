namespace Win32
{
    public partial class ComboBox : Control
    {
        public delegate LRESULT? SelectionChangedHandler(ComboBox sender, Window parent);

        public event SelectionChangedHandler? OnSelectionChanged;

        public ComboBox(
            HWND parent,
            string label,
            RECT rect,
            ushort id
        ) : base(
            parent,
            label,
            ClassName.COMBOBOX,
            WS.OVERLAPPED | WS.VISIBLE | WS.CHILD | CBS.DROPDOWNLIST | CBS.DROPDOWN | CBS.HASSTRINGS | WS.VSCROLL | BS.DEFSPLITBUTTON,
            rect,
            id
        )
        { }

        public ComboBox(
            Form parent,
            string label,
            RECT rect,
            out ushort id
        ) : this(
            parent.Handle,
            label,
            rect,
            parent.GenerateControlId(out id)
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
