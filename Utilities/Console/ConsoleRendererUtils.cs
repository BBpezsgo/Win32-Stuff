using System.Numerics;
using System.Text;

namespace Win32
{
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

        internal int CursorPosition;
        internal float CursorBlinker;

        public ConsoleInputField(string? value)
        {
            value ??= string.Empty;

            Value = new(value);
            IsActive = false;
            NeverLoseFocus = false;

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

    public partial class ConsoleRenderer
    {
        #region Text()

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public void Text(COORD point, ReadOnlySpan<byte> text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
            => Text(point.X, point.Y, text, CharColor.Make(background, foreground));

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public void Text(Vector2 point, ReadOnlySpan<byte> text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
            => Text((int)MathF.Round(point.X), (int)MathF.Round(point.Y), text, CharColor.Make(background, foreground));

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public void Text(int x, int y, ReadOnlySpan<byte> text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
            => Text(x, y, text, CharColor.Make(background, foreground));

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public void Text(COORD point, ReadOnlySpan<byte> text, ushort attributes)
            => Text(point.X, point.Y, text, attributes);

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public void Text(Vector2 point, ReadOnlySpan<byte> text, ushort attributes)
            => Text((int)MathF.Round(point.X), (int)MathF.Round(point.Y), text, attributes);

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public void Text(int x, int y, ReadOnlySpan<byte> text, ushort attributes)
        {
            if (text.IsEmpty) return;
            if (y < 0 || y >= BufferHeight) return;

            for (int i = 0; i < text.Length; i++)
            {
                int x_ = x + i;
                if (x_ < 0) continue;
                if (x_ >= BufferWidth) return;

                this[x_, y] = new ConsoleChar((char)text[i], attributes);
            }
        }

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public void Text(COORD point, ReadOnlySpan<char> text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
            => Text(point.X, point.Y, text, CharColor.Make(background, foreground));

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public void Text(Vector2 point, ReadOnlySpan<char> text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
            => Text((int)MathF.Round(point.X), (int)MathF.Round(point.Y), text, CharColor.Make(background, foreground));

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public void Text(int x, int y, ReadOnlySpan<char> text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
            => Text(x, y, text, CharColor.Make(background, foreground));

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public void Text(COORD point, ReadOnlySpan<char> text, ushort attributes)
            => Text(point.X, point.Y, text, attributes);

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public void Text(Vector2 point, ReadOnlySpan<char> text, ushort attributes)
            => Text((int)MathF.Round(point.X), (int)MathF.Round(point.Y), text, attributes);

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public void Text(int x, int y, ReadOnlySpan<char> text, ushort attributes)
        {
            if (text.IsEmpty) return;
            if (y < 0 || y >= BufferHeight) return;

            for (int i = 0; i < text.Length; i++)
            {
                int x_ = x + i;
                if (x_ < 0) continue;
                if (x_ >= BufferWidth) return;

                this[x_, y] = new ConsoleChar(text[i], attributes);
            }
        }

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public void Text(ref int x, int y, ReadOnlySpan<char> text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
        {
            if (text.IsEmpty) return;
            if (y < 0 || y >= BufferHeight) return;

            for (int i = 0; i < text.Length; i++)
            {
                if (x < 0) { x++; continue; }
                if (x >= BufferWidth) return;

                this[x, y] = new ConsoleChar(text[i], foreground, background);

                x++;
            }
        }

        #endregion

        #region Dropdown()

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public void Dropdown(COORD coord, ConsoleDropdown dropdown, ReadOnlySpan<char> text, ConsoleDropdownStyle style)
            => Dropdown(coord.X, coord.Y, dropdown, text, style);

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public void Dropdown(int x, int y, ConsoleDropdown dropdown, ReadOnlySpan<char> text, ConsoleDropdownStyle style)
        {
            if (text.IsEmpty) return;
            if (y < 0 || y >= BufferHeight) return;

            short width = (short)(2 + text.Length);
            SmallRect rect = new(x, y, width, 1);

            WORD attributes = style.Normal;

            if (!Mouse.WasUsed)
            {
                if (rect.Contains(Mouse.RecordedConsolePosition))
                { attributes = style.Hover; }

                if (rect.Contains(Mouse.LeftPressedAt))
                {
                    if (Mouse.IsPressed(MouseButton.Left))
                    {
                        attributes = style.Down;
                        Mouse.Use();
                    }

                    if (Mouse.IsUp(MouseButton.Left) && rect.Contains(Mouse.RecordedConsolePosition))
                    {
                        dropdown.IsActive = !dropdown.IsActive;
                        Mouse.Use();
                    }
                }
            }

            if (x >= 0 && x < BufferWidth)
            { this[x, y] = new ConsoleChar(dropdown.IsActive ? '▼' : '►', attributes); }

            for (int i = 0; i < text.Length; i++)
            {
                if (x + i + 2 < 0) { continue; }
                if (x + i + 2 >= BufferWidth) return;

                this[x + i + 2, y] = new ConsoleChar(text[i], attributes);
            }
        }

        #endregion

        #region Textbox()

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public void Textbox(SMALL_RECT rect, string text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
        {
            if (string.IsNullOrWhiteSpace(text)) return;
            if (rect.Top >= BufferHeight) return;

            int x = 0;
            int y = 0;

            string[] words = text.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                if (x + words.Length > rect.Width)
                {
                    x = 0;
                    y++;
                }

                int actualY = rect.Top + y;

                for (int j = 0; j < words[i].Length; j++)
                {
                    int actualX = rect.Left + x;

                    if (actualX >= 0 &&
                        actualX < BufferWidth &&
                        actualY >= 0 &&
                        actualY < BufferHeight)
                    {
                        this[actualX, actualY] = new ConsoleChar(words[i][j], foreground, background);
                    }

                    x++;
                }
                x++;
            }
        }

        #endregion

        #region Button()

        /// <remarks>
        /// <para>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </para>
        /// <para>
        /// This uses <see cref="Mouse"/>
        /// </para>
        /// </remarks>
        public bool Button(SMALL_RECT rect, ReadOnlySpan<char> text, ConsoleButtonStyle style)
        {
            WORD attributes = style.Normal;
            bool clicked = false;

            if (!Mouse.WasUsed)
            {
                if (rect.Contains(Mouse.RecordedConsolePosition))
                { attributes = style.Hover; }

                if (rect.Contains(Mouse.LeftPressedAt))
                {
                    if (Mouse.IsPressed(MouseButton.Left))
                    {
                        attributes = style.Down;
                        Mouse.Use();
                    }

                    if (Mouse.IsUp(MouseButton.Left) && rect.Contains(Mouse.RecordedConsolePosition))
                    {
                        clicked = true;
                        Mouse.Use();
                    }
                }
            }

            int labelOffsetX = (int)MathF.Ceiling((rect.Width / 2f) - (text.Length / 2f));
            int labelOffsetY = rect.Top + (rect.Height / 2) - 1;

            for (int y = rect.Top; y < rect.Bottom; y++)
            {
                if (y >= BufferHeight) break;
                if (y < 0) continue;

                for (int x = rect.Left; x < rect.Right; x++)
                {
                    if (x >= BufferWidth) break;
                    if (x < 0) continue;

                    char c = ' ';

                    int i = x - rect.Left - labelOffsetX;

                    if (i >= 0 && i < text.Length && labelOffsetY == y)
                    { c = text[i]; }

                    this[x, y] = new ConsoleChar(c, attributes);
                }
            }

            return clicked;
        }

        #endregion

        #region InputField()

        const string ShiftedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ§'\"+!%/=()?:_-+";
        const string Chars = "abcdefghijklmnopqrstuvwxyz0123456789,.-+";
        const string Keys = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789,.-+";

        /// <remarks>
        /// <para>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </para>
        /// <para>
        /// This uses <see cref="Mouse"/> and <see cref="Keyboard"/>
        /// </para>
        /// </remarks>
        public void InputField(SmallRect rect, ConsoleInputFieldStyle style, ConsoleInputField textField)
        {
            WORD attributes = style.Normal;

            if (textField.IsActive || rect.Contains(Mouse.RecordedConsolePosition))
            { attributes = style.Active; }

            int labelOffsetY = rect.Top + (rect.Height / 2) - 1;

            if (Mouse.IsPressed(MouseButton.Left))
            {
                if (rect.Contains(Mouse.LeftPressedAt) && !Mouse.WasUsed)
                {
                    Mouse.Use();
                    textField.IsActive = true;
                    textField.CursorPosition = Math.Clamp(Mouse.LeftPressedAt.X - rect.Left, 0, textField.Value.Length);
                    textField.CursorBlinker = (float)DateTime.UtcNow.TimeOfDay.TotalSeconds;
                }
                else if (!textField.NeverLoseFocus)
                {
                    textField.IsActive = false;
                }
            }

            if (textField.IsActive)
            {
                if (Keyboard.IsKeyDown(VirtualKeyCode.BACK))
                {
                    if (textField.Value.Length > 0 && textField.CursorPosition > 0)
                    {
                        textField.CursorPosition = Math.Clamp(textField.CursorPosition, 0, textField.Value.Length);
                        textField.Value.Remove(textField.CursorPosition - 1, 1);
                        textField.CursorPosition--;
                    }
                    textField.CursorBlinker = (float)DateTime.UtcNow.TimeOfDay.TotalSeconds;
                }
                else if (Keyboard.IsKeyDown(VirtualKeyCode.DELETE))
                {
                    if (textField.Value.Length > 0 && textField.CursorPosition < textField.Value.Length)
                    {
                        textField.CursorPosition = Math.Clamp(textField.CursorPosition, 0, textField.Value.Length);
                        textField.Value.Remove(textField.CursorPosition, 1);
                    }
                    textField.CursorBlinker = (float)DateTime.UtcNow.TimeOfDay.TotalSeconds;
                }
                else
                {
                    for (int i = 0; i < Keys.Length; i++)
                    {
                        if (!Keyboard.IsKeyDown(Keys[i]))
                        { continue; }

                        char c = Keyboard.IsKeyPressed(VirtualKeyCode.SHIFT) ? ShiftedChars[i] : Chars[i];

                        if (textField.CursorPosition == textField.Value.Length)
                        {
                            textField.Value.Append(c);
                        }
                        else
                        {
                            textField.CursorPosition = Math.Clamp(textField.CursorPosition, 0, textField.Value.Length);
                            textField.Value.Insert(textField.CursorPosition, c);
                        }
                        textField.CursorPosition++;
                        textField.CursorBlinker = (float)DateTime.UtcNow.TimeOfDay.TotalSeconds;
                    }

                    if (Keyboard.IsKeyDown(VirtualKeyCode.LEFT))
                    {
                        textField.CursorPosition = Math.Clamp(textField.CursorPosition - 1, 0, textField.Value.Length);
                        textField.CursorBlinker = (float)DateTime.UtcNow.TimeOfDay.TotalSeconds;
                    }

                    if (Keyboard.IsKeyDown(VirtualKeyCode.RIGHT))
                    {
                        textField.CursorPosition = Math.Clamp(textField.CursorPosition + 1, 0, textField.Value.Length);
                        textField.CursorBlinker = (float)DateTime.UtcNow.TimeOfDay.TotalSeconds;
                    }
                }
            }

            for (int y = rect.Top; y < rect.Bottom; y++)
            {
                if (y >= BufferHeight) break;
                if (y < 0) continue;

                for (int x = rect.Left; x < rect.Right; x++)
                {
                    if (x >= BufferWidth) break;
                    if (x < 0) continue;

                    char c = ' ';

                    int i = x - rect.Left;

                    if (i >= 0 && i < textField.Value.Length && y == labelOffsetY)
                    { c = textField.Value[i]; }

                    if (i == textField.CursorPosition && textField.IsActive && (int)((DateTime.UtcNow.TimeOfDay.TotalSeconds - textField.CursorBlinker) * 2) % 2 == 0)
                    {
                        byte fg = (byte)((attributes) & CharColor.MASK_FG);
                        byte bg = (byte)((attributes >> 4) & CharColor.MASK_FG);
                        this[x, y] = new ConsoleChar(c, CharColor.Make(fg, bg));
                    }
                    else
                    {
                        this[x, y] = new ConsoleChar(c, attributes);
                    }
                }
            }
        }

        #endregion

        #region Box()

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public void Box(SMALL_RECT box, in SideCharacters<char> sideCharacters)
        {
            int top = box.Top;
            int left = box.Left;
            int bottom = box.Bottom;
            int right = box.Right;

            for (int x = left + 1; x < right; x++)
            {
                if (x < 0) continue;
                if (x >= BufferWidth) break;

                if (top >= 0 && top < BufferHeight)
                { this[x, top].Char = sideCharacters.Top; }

                if (bottom >= 0 && bottom < BufferHeight)
                { this[x, bottom].Char = sideCharacters.Bottom; }
            }

            for (int y = top + 1; y < bottom; y++)
            {
                if (y < 0) continue;
                if (y >= BufferHeight) break;

                if (left >= 0 && left < BufferWidth)
                { this[left, y].Char = sideCharacters.Left; }

                if (right >= 0 && right < BufferWidth)
                { this[right, y].Char = sideCharacters.Right; }
            }

            if (IsVisible(left, top))
            { this[left, top].Char = sideCharacters.TopLeft; }

            if (IsVisible(right, top))
            { this[right, top].Char = sideCharacters.TopRight; }

            if (IsVisible(left, bottom))
            { this[left, bottom].Char = sideCharacters.BottomLeft; }

            if (IsVisible(right, bottom))
            { this[right, bottom].Char = sideCharacters.BottomRight; }
        }
        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public void Box(SMALL_RECT box, byte background, byte foreground, in SideCharacters<char> sideCharacters) => Box(box, CharColor.Make(background, foreground), in sideCharacters);

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public void Box(SMALL_RECT box, ushort attributes, in SideCharacters<char> sideCharacters)
        {
            int top = box.Top;
            int left = box.Left;
            int bottom = box.Bottom;
            int right = box.Right;

            for (int x = left + 1; x < right; x++)
            {
                if (x < 0) continue;
                if (x >= BufferWidth) break;

                if (top >= 0 && top < BufferHeight)
                { this[x, top] = new ConsoleChar(sideCharacters.Top, attributes); }

                if (bottom >= 0 && bottom < BufferHeight)
                { this[x, bottom] = new ConsoleChar(sideCharacters.Bottom, attributes); }
            }

            for (int y = top + 1; y < bottom; y++)
            {
                if (y < 0) continue;
                if (y >= BufferHeight) break;

                if (left >= 0 && left < BufferWidth)
                { this[left, y] = new ConsoleChar(sideCharacters.Left, attributes); }

                if (right >= 0 && right < BufferWidth)
                { this[right, y] = new ConsoleChar(sideCharacters.Right, attributes); }
            }

            if (IsVisible(left, top))
            { this[left, top] = new ConsoleChar(sideCharacters.TopLeft, attributes); }

            if (IsVisible(right, top))
            { this[right, top] = new ConsoleChar(sideCharacters.TopRight, attributes); }

            if (IsVisible(left, bottom))
            { this[left, bottom] = new ConsoleChar(sideCharacters.BottomLeft, attributes); }

            if (IsVisible(right, bottom))
            { this[right, bottom] = new ConsoleChar(sideCharacters.BottomRight, attributes); }
        }

        #endregion

        #region Panel()

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public void Panel(ConsolePanel panel, ushort attributes, in SideCharacters<char> sideCharacters)
        {
            if (!Mouse.WasUsed)
            {
                if (Mouse.IsDown(MouseButton.Left) &&
                    new SmallRect(panel.Rect.X, panel.Rect.Y, panel.Rect.Width, (short)1).Contains(Mouse.RecordedConsolePosition))
                {
                    Mouse.Use();
                    panel.IsActive = true;
                    panel.PressedOffset = panel.Rect.Position - Mouse.RecordedConsolePosition;
                }
                else if (panel.IsActive)
                {
                    Mouse.Use();
                    if (Mouse.IsHold(MouseButton.Left))
                    {
                        int panelWidth = panel.Rect.Width;
                        int panelHeight = panel.Rect.Height;

                        COORD newPosition = Mouse.RecordedConsolePosition + panel.PressedOffset;

                        if (newPosition.Y <= 0)
                        {
                            newPosition.Y = 0;
                            BitUtils.SetMask(ref panel.Dock, ConsolePanel.DockTop);
                        }
                        else
                        {
                            BitUtils.ResetMask(ref panel.Dock, ConsolePanel.DockTop);
                        }

                        if (newPosition.X <= 0)
                        {
                            newPosition.X = 0;
                            BitUtils.SetMask(ref panel.Dock, ConsolePanel.DockLeft);
                        }
                        else
                        {
                            BitUtils.ResetMask(ref panel.Dock, ConsolePanel.DockLeft);
                        }

                        if (newPosition.Y >= BufferHeight - panelHeight)
                        {
                            newPosition.Y = (short)(BufferHeight - panelHeight);
                            BitUtils.SetMask(ref panel.Dock, ConsolePanel.DockBottom);
                        }
                        else
                        {
                            BitUtils.ResetMask(ref panel.Dock, ConsolePanel.DockBottom);
                        }

                        if (newPosition.X >= BufferWidth - panelWidth)
                        {
                            newPosition.X = (short)(BufferWidth - panelWidth);
                            BitUtils.SetMask(ref panel.Dock, ConsolePanel.DockRight);
                        }
                        else
                        {
                            BitUtils.ResetMask(ref panel.Dock, ConsolePanel.DockRight);
                        }

                        panel.Rect.Position = newPosition;
                    }
                    else
                    {
                        panel.IsActive = false;
                        panel.PressedOffset = default;
                    }
                }
            }

            int top = panel.Rect.Top;
            int left = panel.Rect.Left;
            int bottom = panel.Rect.Bottom;
            int right = panel.Rect.Right;

            for (int x = left + 1; x < right; x++)
            {
                if (x < 0) continue;
                if (x >= BufferWidth) break;

                if (top >= 0 && top < BufferHeight)
                { this[x, top] = new ConsoleChar(sideCharacters.Top, attributes); }

                if (bottom >= 0 && bottom < BufferHeight)
                { this[x, bottom] = new ConsoleChar(sideCharacters.Bottom, attributes); }
            }

            for (int y = top + 1; y < bottom; y++)
            {
                if (y < 0) continue;
                if (y >= BufferHeight) break;

                if (left >= 0 && left < BufferWidth)
                { this[left, y] = new ConsoleChar(sideCharacters.Left, attributes); }

                if (right >= 0 && right < BufferWidth)
                { this[right, y] = new ConsoleChar(sideCharacters.Right, attributes); }
            }

            if (IsVisible(left, top))
            { this[left, top] = new ConsoleChar(sideCharacters.TopLeft, attributes); }

            if (IsVisible(right, top))
            { this[right, top] = new ConsoleChar(sideCharacters.TopRight, attributes); }

            if (IsVisible(left, bottom))
            { this[left, bottom] = new ConsoleChar(sideCharacters.BottomLeft, attributes); }

            if (IsVisible(right, bottom))
            { this[right, bottom] = new ConsoleChar(sideCharacters.BottomRight, attributes); }
        }

        #endregion

        #region Fill()

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public void Fill(SMALL_RECT rect, byte background, byte foreground, char character) => Fill(rect, CharColor.Make(background, foreground), character);
        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public void Fill(SMALL_RECT rect, ushort attributes, char character) => Fill(rect, new ConsoleChar(character, attributes));
        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public void Fill(SMALL_RECT rect, ConsoleChar value)
        {
            for (int _y = 0; _y < rect.Height; _y++)
            {
                int actualY = rect.Y + _y;
                if (actualY >= Height) break;
                if (actualY < 0) continue;

                int startIndex = (actualY * BufferWidth) + Math.Max((short)0, rect.Left);
                int endIndex = (actualY * BufferWidth) + Math.Min(BufferWidth - 1, rect.Right);
                int length = Math.Max(0, endIndex - startIndex);

                Array.Fill(ConsoleBuffer, value, startIndex, length);

                // for (int _x = 0; _x < box.Width; _x++)
                // {
                //     int actualX = box.X + _x;
                // 
                //     if (actualX >= Width) break;
                //     if (actualX < 0) continue;
                // 
                //     this[actualX, actualY] = new ConsoleChar(character, attributes);
                // }
            }
        }

        public void Fill(byte background, byte foreground, char character) => Array.Fill(ConsoleBuffer, new ConsoleChar(character, foreground, background));
        public void Fill(ushort attributes, char character) => Array.Fill(ConsoleBuffer, new ConsoleChar(character, attributes));
        public void Fill(ConsoleChar value) => Array.Fill(ConsoleBuffer, value);

        #endregion

        #region Clear()

        public void Clear(SMALL_RECT rect)
        {
            for (int _y = 0; _y < rect.Height; _y++)
            {
                int actualY = rect.Y + _y;
                if (actualY >= Height) break;
                if (actualY < 0) continue;

                int startIndex = (actualY * BufferWidth) + Math.Max((short)0, rect.Left);
                int endIndex = (actualY * BufferWidth) + Math.Min(BufferWidth - 1, rect.Right);
                int length = Math.Max(0, endIndex - startIndex);

                Array.Clear(ConsoleBuffer, startIndex, length);
            }
        }

        #endregion
    }
}
