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
        internal int CursorPosition;

        public ConsoleInputField(string? value)
        {
            value ??= string.Empty;
            Value = new(value);
            IsActive = false;
            CursorPosition = value.Length;
        }
    }

    public class ConsolePanel
    {
        public SMALL_RECT Rect;
        public bool IsActive;
        public COORD PressedOffset;

        public ConsolePanel(SMALL_RECT rect) => Rect = rect;
    }

    public partial class ConsoleRenderer
    {
        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public void Text(COORD point, ReadOnlySpan<char> text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
            => Text(point.X, point.Y, text, foreground, background);

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public void Text(Vector2 point, ReadOnlySpan<char> text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
            => Text((int)MathF.Round(point.X), (int)MathF.Round(point.Y), text, foreground, background);

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public void Text(int x, int y, ReadOnlySpan<char> text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
        {
            if (text.IsEmpty) return;
            if (y < 0 || y >= BufferHeight) return;

            for (int i = 0; i < text.Length; i++)
            {
                int x_ = x + i;
                if (x_ < 0) continue;
                if (x_ >= BufferWidth) return;

                this[x_, y] = new ConsoleChar(text[i], foreground, background);
            }
        }

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public void Text(COORD point, ReadOnlySpan<byte> text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
            => Text(point.X, point.Y, text, foreground, background);

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public void Text(Vector2 point, ReadOnlySpan<byte> text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
            => Text((int)MathF.Round(point.X), (int)MathF.Round(point.Y), text, foreground, background);

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public void Text(int x, int y, ReadOnlySpan<byte> text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
        {
            if (text.IsEmpty) return;
            if (y < 0 || y >= BufferHeight) return;

            for (int i = 0; i < text.Length; i++)
            {
                int x_ = x + i;
                if (x_ < 0) continue;
                if (x_ >= BufferWidth) return;

                this[x_, y] = new ConsoleChar((char)text[i], foreground, background);
            }
        }

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

            int labelOffsetX = (int)((rect.Width / 2f) - (text.Length / 2f));
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
        public void InputField(SmallRect rect, ConsoleInputFieldStyle style, ref bool active, StringBuilder value)
        {
            WORD attributes = style.Normal;

            if (!Mouse.WasUsed && Mouse.IsPressed(MouseButton.Left))
            {
                if (active = rect.Contains(Mouse.LeftPressedAt))
                {
                    Mouse.Use();
                }
            }

            // if (!Mouse.WasUsed)
            // {
            //     if (active || rect.Contains(Mouse.RecordedConsolePosition))
            //     {
            //         attributes = style.Active;
            //         Mouse.Use();
            //     }
            // 
            //     if (Mouse.IsPressed(MouseButton.Left) && rect.Contains(Mouse.LeftPressedAt))
            //     {
            //         active = true;
            //         Mouse.Use();
            //     }
            // }

            int labelOffsetY = rect.Top + (rect.Height / 2) - 1;

            if (active)
            {
                if (Keyboard.IsKeyDown(VirtualKeyCode.BACK))
                {
                    if (value.Length > 0)
                    { value.Remove(value.Length - 1, 1); }
                }
                else
                {
                    for (int i = 0; i < Keys.Length; i++)
                    {
                        if (!Keyboard.IsKeyDown(Keys[i]))
                        { continue; }

                        if (Keyboard.IsKeyPressed(VirtualKeyCode.SHIFT))
                        { value.Append(ShiftedChars[i]); }
                        else
                        { value.Append(Chars[i]); }
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

                    if (i >= 0 && i < value.Length && y == labelOffsetY)
                    { c = value[i]; }

                    this[x, y] = new ConsoleChar(c, attributes);
                }
            }
        }

        /// <remarks>
        /// <para>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </para>
        /// <para>
        /// This uses <see cref="Mouse"/> and <see cref="Keyboard"/>
        /// </para>
        /// </remarks>
        public void InputField(SmallRect rect, ConsoleInputFieldStyle style, ref ConsoleInputField textField)
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
                }
                else
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
                }
                else if (Keyboard.IsKeyDown(VirtualKeyCode.DELETE))
                {
                    if (textField.Value.Length > 0 && textField.CursorPosition < textField.Value.Length)
                    {
                        textField.CursorPosition = Math.Clamp(textField.CursorPosition, 0, textField.Value.Length);
                        textField.Value.Remove(textField.CursorPosition, 1);
                    }
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
                    }

                    if (Keyboard.IsKeyDown(VirtualKeyCode.LEFT))
                    { textField.CursorPosition = Math.Clamp(textField.CursorPosition - 1, 0, textField.Value.Length); }
                    if (Keyboard.IsKeyDown(VirtualKeyCode.RIGHT))
                    { textField.CursorPosition = Math.Clamp(textField.CursorPosition + 1, 0, textField.Value.Length); }
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

                    if (i == textField.CursorPosition && textField.IsActive)
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


        public void Box(SMALL_RECT box, char[] sideCharacters)
        {
            for (int _y = 0; _y < box.Height; _y++)
            {
                int actualY = box.Y + _y;
                if (actualY >= Height) break;

                for (int _x = 0; _x < box.Width; _x++)
                {
                    int actualX = box.X + _x;

                    if (actualX >= Width) break;

                    int size = 0b_0000;

                    if (_y == 0) size |= 0b_1000; // Top
                    if (_x == 0) size |= 0b_0100; // Left
                    if (_y == box.Height - 1) size |= 0b_0010; // Bottom
                    if (_x == box.Width - 1) size |= 0b_0001; // Right

                    char c = sideCharacters[size];

                    this[actualX, actualY].Char = c;
                }
            }
        }
        public void Box(SMALL_RECT box, byte background, byte foreground, char[] sideCharacters) => Box(box, CharColor.Make(background, foreground), sideCharacters);
        public void Box(SMALL_RECT box, ushort attributes, char[] sideCharacters)
        {
            int top = box.Top;
            int left = box.Left;
            int bottom = box.Bottom;
            int right = box.Right;

            for (int x = left + 1; x < right; x++)
            {
                if (x < 0 || x >= BufferWidth) continue;

                if (top >= 0 && top < BufferHeight)
                { this[x, top] = new ConsoleChar(sideCharacters[0b_1000], attributes); }

                if (bottom >= 0 && bottom < BufferHeight)
                { this[x, bottom] = new ConsoleChar(sideCharacters[0b_0010], attributes); }
            }

            for (int y = top + 1; y < bottom; y++)
            {
                if (y < 0 || y >= BufferHeight) continue;

                if (left >= 0 && left < BufferWidth)
                { this[left, y] = new ConsoleChar(sideCharacters[0b_0100], attributes); }

                if (right >= 0 && right < BufferWidth)
                { this[right, y] = new ConsoleChar(sideCharacters[0b_0001], attributes); }
            }

            if (left >= 0 && left < BufferWidth && top >= 0 && top < BufferHeight)
            { this[left, top] = new ConsoleChar(sideCharacters[0b_1100], attributes); }

            if (right >= 0 && right < BufferWidth && top >= 0 && top < BufferHeight)
            { this[right, top] = new ConsoleChar(sideCharacters[0b_1001], attributes); }

            if (left >= 0 && left < BufferWidth && bottom >= 0 && bottom < BufferHeight)
            { this[left, bottom] = new ConsoleChar(sideCharacters[0b_0110], attributes); }

            if (right >= 0 && right < BufferWidth && bottom >= 0 && bottom < BufferHeight)
            { this[right, bottom] = new ConsoleChar(sideCharacters[0b_0011], attributes); }
        }

        public void Panel(ConsolePanel panel, ushort attributes, char[] sideCharacters)
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

                        if (newPosition.Y < 0) newPosition.Y = 0;
                        if (newPosition.X < 0) newPosition.X = 0;
                        if (newPosition.Y > BufferHeight - panelHeight) newPosition.Y = (short)(BufferHeight - panelHeight);
                        if (newPosition.X >= BufferWidth - panelWidth) newPosition.X = (short)(BufferWidth - panelWidth - 1);

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
                if (x < 0 || x >= BufferWidth) continue;

                if (top >= 0 && top < BufferHeight)
                { this[x, top] = new ConsoleChar(sideCharacters[0b_1000], attributes); }

                if (bottom >= 0 && bottom < BufferHeight)
                { this[x, bottom] = new ConsoleChar(sideCharacters[0b_0010], attributes); }
            }

            for (int y = top + 1; y < bottom; y++)
            {
                if (y < 0 || y >= BufferHeight) continue;

                if (left >= 0 && left < BufferWidth)
                { this[left, y] = new ConsoleChar(sideCharacters[0b_0100], attributes); }

                if (right >= 0 && right < BufferWidth)
                { this[right, y] = new ConsoleChar(sideCharacters[0b_0001], attributes); }
            }

            if (left >= 0 && left < BufferWidth && top >= 0 && top < BufferHeight)
            { this[left, top] = new ConsoleChar(sideCharacters[0b_1100], attributes); }

            if (right >= 0 && right < BufferWidth && top >= 0 && top < BufferHeight)
            { this[right, top] = new ConsoleChar(sideCharacters[0b_1001], attributes); }

            if (left >= 0 && left < BufferWidth && bottom >= 0 && bottom < BufferHeight)
            { this[left, bottom] = new ConsoleChar(sideCharacters[0b_0110], attributes); }

            if (right >= 0 && right < BufferWidth && bottom >= 0 && bottom < BufferHeight)
            { this[right, bottom] = new ConsoleChar(sideCharacters[0b_0011], attributes); }
        }

        public void Fill(SMALL_RECT box, byte background, byte foreground, char character) => Fill(box, CharColor.Make(background, foreground), character);
        public void Fill(SMALL_RECT box, ushort attributes, char character)
        {
            for (int _y = 0; _y < box.Height; _y++)
            {
                int actualY = box.Y + _y;
                if (actualY >= Height) break;
                if (actualY < 0) continue;

                int startIndex = (actualY * BufferWidth) + Math.Max((short)0, box.Left);
                int endIndex = (actualY * BufferWidth) + Math.Min(BufferWidth - 1, box.Right);
                int length = Math.Max(0, endIndex - startIndex);

                Array.Fill(ConsoleBuffer, new ConsoleChar(character, attributes), startIndex, length);

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

        public void Clear(SMALL_RECT box)
        {
            for (int _y = 0; _y < box.Height; _y++)
            {
                int actualY = box.Y + _y;
                if (actualY >= Height) break;
                if (actualY < 0) continue;

                int startIndex = (actualY * BufferWidth) + Math.Max((short)0, box.Left);
                int endIndex = (actualY * BufferWidth) + Math.Min(BufferWidth - 1, box.Right);
                int length = Math.Max(0, endIndex - startIndex);

                Array.Clear(ConsoleBuffer, startIndex, length);
            }
        }
    }
}
