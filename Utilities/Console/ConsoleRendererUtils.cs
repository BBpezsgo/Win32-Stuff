using System.Numerics;
using System.Text;

namespace Win32
{
    public partial class ConsoleRenderer
    {
        public struct ButtonStyle
        {
            public ushort Normal;
            public ushort Hover;
            public ushort Down;

            public static ButtonStyle Default => new()
            {
                Normal = CharColor.Make(CharColor.Gray, CharColor.White),
                Hover = CharColor.Make(CharColor.Silver, CharColor.Black),
                Down = CharColor.Make(CharColor.White, CharColor.Black),
            };
        }

        public struct TextFieldStyle
        {
            public ushort Normal;
            public ushort Active;

            public static TextFieldStyle Default => new()
            {
                Normal = CharColor.Make(CharColor.Gray, CharColor.White),
                Active = CharColor.Make(CharColor.Silver, CharColor.Black),
            };
        }

        public struct TextField
        {
            public StringBuilder Value;
            public bool IsActive;
            internal int CursorPosition;

            public TextField(string? value)
            {
                value ??= string.Empty;
                Value = new(value);
                IsActive = false;
                CursorPosition = value.Length;
            }
        }

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public void Text(COORD point, string? text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
            => Text(point.X, point.Y, text, foreground, background);

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public void Text(Vector2 point, string? text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
            => Text((int)MathF.Round(point.X), (int)MathF.Round(point.Y), text, foreground, background);

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public void Text(int x, int y, string? text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
        {
            if (text is null) return;
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
        public void Text(ref int x, int y, string? text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
        {
            if (text is null) return;
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
        /// <para>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </para>
        /// <para>
        /// This uses <see cref="Mouse"/>
        /// </para>
        /// </remarks>
        public bool Button(SMALL_RECT rect, string text, ButtonStyle style)
        {
            WORD attributes = style.Normal;

            if (rect.Contains(Mouse.RecordedConsolePosition))
            { attributes = style.Hover; }

            if (Mouse.IsPressed(MouseButton.Left) && rect.Contains(Mouse.LeftPressedAt))
            { attributes = style.Down; }

            bool clicked = Mouse.IsUp(MouseButton.Left) && rect.Contains(Mouse.LeftPressedAt);

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
        public void InputField(SmallRect rect, TextFieldStyle style, ref bool active, StringBuilder value)
        {
            WORD attributes = style.Normal;

            if (active || rect.Contains(Mouse.RecordedConsolePosition))
            { attributes = style.Active; }

            if (Mouse.IsPressed(MouseButton.Left))
            { active = rect.Contains(Mouse.LeftPressedAt); }

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
        public void InputField(SmallRect rect, TextFieldStyle style, ref TextField textField)
        {
            WORD attributes = style.Normal;

            if (textField.IsActive || rect.Contains(Mouse.RecordedConsolePosition))
            { attributes = style.Active; }

            int labelOffsetY = rect.Top + (rect.Height / 2) - 1;

            if (Mouse.IsPressed(MouseButton.Left))
            {
                if (textField.IsActive = rect.Contains(Mouse.LeftPressedAt))
                {
                    textField.CursorPosition = Math.Clamp(Mouse.LeftPressedAt.X - rect.Left, 0, textField.Value.Length);
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
            for (int _y = 0; _y < box.Height; _y++)
            {
                int actualY = box.Y + _y;
                if (actualY >= Height) break;
                if (actualY < 0) continue;

                for (int _x = 0; _x < box.Width; _x++)
                {
                    int actualX = box.X + _x;

                    if (actualX >= Width) break;
                    if (actualX < 0) continue;

                    int size = 0b_0000;

                    if (_y == 0) size |= 0b_1000; // Top
                    if (_x == 0) size |= 0b_0100; // Left
                    if (_y == box.Height - 1) size |= 0b_0010; // Bottom
                    if (_x == box.Width - 1) size |= 0b_0001; // Right

                    char c = sideCharacters[size];

                    this[actualX, actualY].Char = c;
                    this[actualX, actualY].Attributes = attributes;
                }
            }
        }
    }
}
