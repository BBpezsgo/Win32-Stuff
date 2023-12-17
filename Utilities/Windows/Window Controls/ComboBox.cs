namespace Win32
{
    [SupportedOSPlatform("windows")]
    public partial class ComboBox : Control
    {
        public delegate LRESULT? SelectionChangedHandler(ComboBox sender, Window parent);

        public event SelectionChangedHandler? OnSelectionChanged;

        public ComboBox(
            Form parent,
            string label,
            RECT rect,
            out ushort id
        ) : base(
            parent,
            label,
            LowLevel.ClassName.COMBOBOX,
            WindowStyles.OVERLAPPED | WindowStyles.VISIBLE | WindowStyles.CHILD | ComboBoxControlStyles.DROPDOWNLIST | ComboBoxControlStyles.DROPDOWN | ComboBoxControlStyles.HASSTRINGS | WindowStyles.VSCROLL | ButtonControlStyles.DEFSPLITBUTTON,
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
            if (code == ComboBoxControlNotification.SELCHANGE)
            {
                OnSelectionChanged?.Invoke(this, parent);
                return;
            }
            else if (code == ComboBoxControlNotification.SETFOCUS)
            {
                return;
            }
            else if (code == ComboBoxControlNotification.DROPDOWN)
            {
                return;
            }
            else
            {

            }
        }
    }
}
