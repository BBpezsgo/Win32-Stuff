using System.Text;

namespace Win32.Console;

#region Elements

public class ConsoleButtonStyle
{
    public ushort Normal;
    public ushort Hover;
    public ushort Down;

    public static ConsoleButtonStyle Default => new()
    {
        Normal = CharColor.Make(CharColor.Gray, CharColor.White),
        Hover = CharColor.Make(CharColor.Silver, CharColor.Black),
        Down = CharColor.Make(CharColor.White, CharColor.Black),
    };
}

public class ConsoleDropdownStyle
{
    public ushort Normal;
    public ushort Hover;
    public ushort Down;
    public char ActiveChar;
    public char InactiveChar;

    public static ConsoleDropdownStyle Default => new()
    {
        Normal = CharColor.Make(CharColor.Gray, CharColor.White),
        Hover = CharColor.Make(CharColor.Silver, CharColor.Black),
        Down = CharColor.Make(CharColor.White, CharColor.Black),
        ActiveChar = '▼',
        InactiveChar = '►',
    };
}

public class ConsoleSelectBoxStyle
{
    public ushort LabelNormal;
    public ushort LabelHover;
    public ushort LabelDown;
    public ushort LabelActive;

    public ushort ButtonNormal;
    public ushort ButtonHover;
    public ushort ButtonDown;

    public char LeftChar;
    public char RightChar;

    public ConsoleSelectBoxStyle() { }

    public ConsoleSelectBoxStyle(ConsoleSelectBoxStyle other)
    {
        this.LabelNormal = other.LabelNormal;
        this.LabelHover = other.LabelHover;
        this.LabelDown = other.LabelDown;
        this.LabelActive = other.LabelActive;

        this.ButtonNormal = other.ButtonNormal;
        this.ButtonHover = other.ButtonHover;
        this.ButtonDown = other.ButtonDown;

        this.LeftChar = other.LeftChar;
        this.RightChar = other.RightChar;
    }

    public static ConsoleSelectBoxStyle Default => new()
    {
        LabelNormal = CharColor.Make(CharColor.Gray, CharColor.White),
        LabelHover = CharColor.Make(CharColor.Silver, CharColor.Black),
        LabelDown = CharColor.Make(CharColor.White, CharColor.Black),
        LabelActive = CharColor.Make(CharColor.White, CharColor.Black),

        ButtonNormal = CharColor.Make(CharColor.Gray, CharColor.White),
        ButtonHover = CharColor.Make(CharColor.Silver, CharColor.Black),
        ButtonDown = CharColor.Make(CharColor.White, CharColor.Black),

        LeftChar = '<',
        RightChar = '>',
    };
}

public class ConsoleInputFieldStyle
{
    public ushort Normal;
    public ushort Active;

    public static ConsoleInputFieldStyle Default => new()
    {
        Normal = CharColor.Make(CharColor.Gray, CharColor.White),
        Active = CharColor.Make(CharColor.Silver, CharColor.Black),
    };
}

public class ConsoleSelectBox<T>
{
    public bool IsActive;
    public int SelectedIndex;
    public T[] Items;
    public T? SelectedItem
    {
        get => (SelectedIndex < 0 || SelectedIndex >= Items.Length) ? default : Items[SelectedIndex];
        set => SelectedIndex = Array.IndexOf(Items, value);
    }
    public ConsoleSelectBox(params T[] items)
    {
        IsActive = false;
        SelectedIndex = -1;
        Items = items;
    }

    public void ClampIndex()
    {
        if (SelectedIndex < 0) SelectedIndex = Items.Length - 1;
        if (SelectedIndex >= Items.Length) SelectedIndex = 0;
    }

    public static bool operator true(ConsoleSelectBox<T> consoleSelectBox) => consoleSelectBox.IsActive;
    public static bool operator false(ConsoleSelectBox<T> consoleSelectBox) => !consoleSelectBox.IsActive;
}

public class ConsoleDropdown
{
    public bool IsActive;

    public ConsoleDropdown()
    {
        IsActive = false;
    }

    public static bool operator true(ConsoleDropdown consoleDropdown) => consoleDropdown.IsActive;
    public static bool operator false(ConsoleDropdown consoleDropdown) => !consoleDropdown.IsActive;
}

public class ConsoleInputField
{
    public StringBuilder Value;
    public bool IsActive;
    public bool NeverLoseFocus;
    public string Label;

    internal int CursorPosition;
    internal float CursorBlinker;

    public ConsoleInputField(string? value)
    {
        value ??= string.Empty;

        Value = new(value);
        IsActive = false;
        NeverLoseFocus = false;
        Label = string.Empty;

        CursorPosition = value.Length;
    }

    public void Clear()
    {
        Value.Clear();
        CursorPosition = 0;
    }
}

public class ConsolePanel
{
    public const byte DockNone = 0b_0000;
    public const byte DockTop = 0b_1000;
    public const byte DockLeft = 0b_0100;
    public const byte DockBottom = 0b_0010;
    public const byte DockRight = 0b_0001;

    public SMALL_RECT Rect;
    public bool IsActive;
    public COORD PressedOffset;

    public byte Dock;

    public ConsolePanel(SMALL_RECT rect)
    {
        Rect = rect;
    }

    public ConsolePanel(SMALL_RECT rect, byte dock)
    {
        Rect = rect;
        Dock = dock;
    }

    public void RefreshPosition(SmallSize rendererSize)
    {
        int panelWidth = Rect.Width + 1;
        int panelHeight = Rect.Height + 1;

        if (Rect.Y < 0) Rect.Y = 0;
        if (Rect.X < 0) Rect.X = 0;
        if (Rect.Y > rendererSize.Height - panelHeight) Rect.Y = (short)(rendererSize.Height - panelHeight);
        if (Rect.X > rendererSize.Width - panelWidth) Rect.X = (short)(rendererSize.Width - panelWidth);

        if ((Dock & DockTop) != 0) Rect.Y = 0;
        if ((Dock & DockLeft) != 0) Rect.X = 0;
        if ((Dock & DockBottom) != 0) Rect.Y = (short)(rendererSize.Height - panelHeight);
        if ((Dock & DockRight) != 0) Rect.X = (short)(rendererSize.Width - panelWidth);
    }
}

#endregion
