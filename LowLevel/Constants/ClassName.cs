namespace Win32.LowLevel
{
    public static class ClassName
    {
        /// <summary>
        /// <para>
        /// Designates a small rectangular child window that represents a button the
        /// user can click to turn it on or off. Button controls can be used alone or
        /// in groups, and they can either be labeled or appear without text. Button
        /// controls typically change appearance when the user clicks them. For more information,
        /// see Buttons.
        /// </para>
        /// <para>
        /// For a table of the button styles you can specify in the <c>dwStyle</c> parameter,
        /// see Button Styles.
        /// </para>
        /// </summary>
        public const string BUTTON = "BUTTON";

        /// <summary>
        /// <para>
        /// Designates a control consisting of a list box and a selection
        /// field similar to an edit control. When using this style, an application
        /// should either display the list box at all times or enable a drop-down
        /// list box. If the list box is visible, typing characters into the selection
        /// field highlights the first list box entry that matches the characters typed. Conversely,
        /// selecting an item in the list box displays the selected text in the selection field.
        /// For more information, see Combo Boxes.
        /// </para>
        /// <para>
        /// For a table of the combo box styles you can specify in the
        /// <c>dwStyle</c> parameter, see Combo Box Styles.
        /// </para>
        /// </summary>
        public const string COMBOBOX = "COMBOBOX";

        /// <summary>
        /// <para>
        /// Designates a rectangular child window into which the user can
        /// type text from the keyboard. The user selects the control and gives it
        /// the keyboard focus by clicking it or moving to it by pressing the TAB key.
        /// The user can type text when the edit control displays a flashing caret;
        /// use the mouse to move the cursor, select characters to be replaced,
        /// or position the cursor for inserting characters; or use the key to
        /// delete characters. For more information, see Edit Controls.
        /// </para>
        /// <para>
        /// For a table of the edit control styles you can specify in the
        /// <c>dwStyle</c> parameter, see Edit Control Styles.
        /// </para>
        /// </summary>
        public const string EDIT = "EDIT";

        /// <summary>
        /// <para>
        /// Designates a list of character strings. Specify this control
        /// whenever an application must present a list of names, such as filenames,
        /// from which the user can choose. The user can select a string by clicking it.
        /// A selected string is highlighted, and a notification message is passed
        /// to the parent window. For more information, see List Boxes.
        /// </para>
        /// <para>
        /// For a table of the list box styles you can specify in the
        /// <c>dwStyle</c> parameter, see List Box Styles.
        /// </para>
        /// </summary>
        public const string LISTBOX = "LISTBOX";

        /// <summary>
        /// <para>
        /// Designates an MDI client window. This window receives messages
        /// that control the MDI application's child windows. The recommended style
        /// bits are <c>WS_CLIPCHILDREN</c> and <c>WS_CHILD</c>.
        /// Specify the <c>WS_HSCROLL</c> and <c>WS_VSCROLL</c>
        /// styles to create an MDI client window that allows the user to scroll
        /// MDI child windows into view. For more information, see Multiple Document
        /// Interface.
        /// </para>
        /// <para>
        /// RichEdit Designates a Microsoft Rich Edit 1.0 control.
        /// This window lets the user view and edit text with character and paragraph
        /// formatting, and can include embedded Component Object Model (COM) objects.
        /// For more information, see Rich Edit Controls.
        /// </para>
        /// <para>
        /// For a table of the rich edit control styles you can specify
        /// in the <c>dwStyle</c> parameter, see Rich Edit Control Styles.
        /// </para>
        /// </summary>
        public const string MDICLIENT = "MDICLIENT";

        /// <summary>
        /// <para>
        /// Designates a Microsoft Rich Edit 2.0 control. This controls
        /// let the user view and edit text with character and paragraph formatting,
        /// and can include embedded COM objects. For more information, see Rich Edit Controls.
        /// </para>
        /// <para>
        /// For a table of the rich edit control styles you can specify
        /// in the dwStyle parameter, see Rich Edit Control Styles.
        /// </para>
        /// </summary>
        public const string RICHEDIT_CLASS = "RICHEDIT_CLASS";

        /// <summary>
        /// <para>
        /// Designates a rectangle that contains a scroll box and has direction
        /// arrows at both ends. The scroll bar sends a notification message to its parent window whenever the user clicks the control. The parent window is responsible for updating the position of the scroll box, if necessary. For more information, see Scroll Bars.</para>
        /// <para>
        /// For a table of the scroll bar control styles you can specify
        /// in the dwStyle parameter, see Scroll Bar Control Styles.
        /// </para>
        /// </summary>
        public const string SCROLLBAR = "SCROLLBAR";

        /// <summary>
        /// <para>
        /// Designates a simple text field, box, or rectangle used to label,
        /// box, or separate other controls. Static controls take no input and provide no output.
        /// For more information, see Static Controls.
        /// <para>
        /// For a table of the static control styles you can specify in the dwStyle parameter,
        /// see Static Control Styles.
        /// </para>
        /// </para>
        /// </summary>
        public const string STATIC = "STATIC";

        public const string PROGRESS_BAR = "msctls_progress32";

        public const string IP_ADDRESS = "SysIPAddress32";
    }
}
