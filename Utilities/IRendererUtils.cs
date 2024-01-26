using System.Numerics;

namespace Win32
{
    public static class IRendererUtils
    {
        #region Text()

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Text(this IRenderer<ConsoleChar> self, COORD point, ReadOnlySpan<byte> text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
            => self.Text(point.X, point.Y, text, CharColor.Make(background, foreground));

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Text(this IRenderer<ConsoleChar> self, Vector2 point, ReadOnlySpan<byte> text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
            => self.Text((int)MathF.Round(point.X), (int)MathF.Round(point.Y), text, CharColor.Make(background, foreground));

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Text(this IRenderer<ConsoleChar> self, int x, int y, ReadOnlySpan<byte> text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
            => self.Text(x, y, text, CharColor.Make(background, foreground));

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Text(this IRenderer<ConsoleChar> self, COORD point, ReadOnlySpan<byte> text, ushort attributes)
            => self.Text(point.X, point.Y, text, attributes);

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Text(this IRenderer<ConsoleChar> self, Vector2 point, ReadOnlySpan<byte> text, ushort attributes)
            => self.Text((int)MathF.Round(point.X), (int)MathF.Round(point.Y), text, attributes);

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Text(this IRenderer<ConsoleChar> self, int x, int y, ReadOnlySpan<byte> text, ushort attributes)
        {
            if (text.IsEmpty) return;
            if (y < 0 || y >= self.Height) return;

            for (int i = 0; i < text.Length; i++)
            {
                int x_ = x + i;
                if (x_ < 0) continue;
                if (x_ >= self.Width) return;

                self[x_, y] = new ConsoleChar((char)text[i], attributes);
            }
        }

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Text(this IRenderer<ConsoleChar> self, COORD point, ReadOnlySpan<char> text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
            => self.Text(point.X, point.Y, text, CharColor.Make(background, foreground));

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Text(this IRenderer<ConsoleChar> self, Vector2 point, ReadOnlySpan<char> text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
            => self.Text((int)MathF.Round(point.X), (int)MathF.Round(point.Y), text, CharColor.Make(background, foreground));

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Text(this IRenderer<ConsoleChar> self, int x, int y, ReadOnlySpan<char> text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
            => self.Text(x, y, text, CharColor.Make(background, foreground));

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Text(this IRenderer<ConsoleChar> self, COORD point, ReadOnlySpan<char> text, ushort attributes)
            => self.Text(point.X, point.Y, text, attributes);

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Text(this IRenderer<ConsoleChar> self, Vector2 point, ReadOnlySpan<char> text, ushort attributes)
            => self.Text((int)MathF.Round(point.X), (int)MathF.Round(point.Y), text, attributes);

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Text(this IRenderer<ConsoleChar> self, int x, int y, ReadOnlySpan<char> text, ushort attributes)
        {
            if (text.IsEmpty) return;
            if (y < 0 || y >= self.Height) return;

            for (int i = 0; i < text.Length; i++)
            {
                int x_ = x + i;
                if (x_ < 0) continue;
                if (x_ >= self.Width) return;

                self[x_, y] = new ConsoleChar(text[i], attributes);
            }
        }

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Text(this IRenderer<ConsoleChar> self, ref int x, int y, ReadOnlySpan<char> text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
        {
            if (text.IsEmpty) return;
            if (y < 0 || y >= self.Height) return;

            for (int i = 0; i < text.Length; i++)
            {
                if (x < 0) { x++; continue; }
                if (x >= self.Width) return;

                self[x, y] = new ConsoleChar(text[i], foreground, background);

                x++;
            }
        }

        #endregion

        #region Dropdown()

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Dropdown(this IRenderer<ConsoleChar> self, COORD coord, ConsoleDropdown dropdown, ReadOnlySpan<char> text, ConsoleDropdownStyle style)
            => self.Dropdown(coord.X, coord.Y, dropdown, text, style);

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Dropdown(this IRenderer<ConsoleChar> self, int x, int y, ConsoleDropdown dropdown, ReadOnlySpan<char> text, ConsoleDropdownStyle style)
        {
            if (text.IsEmpty) return;
            if (y < 0 || y >= self.Height) return;

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

            if (x >= 0 && x < self.Width)
            { self[x, y] = new ConsoleChar(dropdown.IsActive ? '▼' : '►', attributes); }

            for (int i = 0; i < text.Length; i++)
            {
                if (x + i + 2 < 0) { continue; }
                if (x + i + 2 >= self.Width) return;

                self[x + i + 2, y] = new ConsoleChar(text[i], attributes);
            }
        }

        #endregion

        #region Textbox()

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Textbox(this IRenderer<ConsoleChar> self, SMALL_RECT rect, string text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
        {
            if (string.IsNullOrWhiteSpace(text)) return;
            if (rect.Top >= self.Height) return;

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
                        actualX < self.Width &&
                        actualY >= 0 &&
                        actualY < self.Height)
                    {
                        self[actualX, actualY] = new ConsoleChar(words[i][j], foreground, background);
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
        public static bool Button(this IRenderer<ConsoleChar> self, SMALL_RECT rect, ReadOnlySpan<char> text, ConsoleButtonStyle style)
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
                if (y >= self.Height) break;
                if (y < 0) continue;

                for (int x = rect.Left; x < rect.Right; x++)
                {
                    if (x >= self.Width) break;
                    if (x < 0) continue;

                    char c = ' ';

                    int i = x - rect.Left - labelOffsetX;

                    if (i >= 0 && i < text.Length && labelOffsetY == y)
                    { c = text[i]; }

                    self[x, y] = new ConsoleChar(c, attributes);
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
        public static void InputField(this IRenderer<ConsoleChar> self, SmallRect rect, ConsoleInputFieldStyle style, ConsoleInputField textField)
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
                if (y >= self.Height) break;
                if (y < 0) continue;

                for (int x = rect.Left; x < rect.Right; x++)
                {
                    if (x >= self.Width) break;
                    if (x < 0) continue;

                    char c = ' ';

                    int i = x - rect.Left;

                    if (i >= 0 && i < textField.Value.Length && y == labelOffsetY)
                    { c = textField.Value[i]; }

                    if (i == textField.CursorPosition && textField.IsActive && (int)((DateTime.UtcNow.TimeOfDay.TotalSeconds - textField.CursorBlinker) * 2) % 2 == 0)
                    {
                        byte fg = (byte)((attributes) & CharColor.MASK_FG);
                        byte bg = (byte)((attributes >> 4) & CharColor.MASK_FG);
                        self[x, y] = new ConsoleChar(c, CharColor.Make(fg, bg));
                    }
                    else
                    {
                        self[x, y] = new ConsoleChar(c, attributes);
                    }
                }
            }
        }

        #endregion

        #region Box()

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Box(this IRenderer<ConsoleChar> self, SMALL_RECT box, in SideCharacters<char> sideCharacters)
        {
            int top = box.Top;
            int left = box.Left;
            int bottom = box.Bottom;
            int right = box.Right;

            for (int x = left + 1; x < right; x++)
            {
                if (x < 0) continue;
                if (x >= self.Width) break;

                if (top >= 0 && top < self.Height)
                { self[x, top].Char = sideCharacters.Top; }

                if (bottom >= 0 && bottom < self.Height)
                { self[x, bottom].Char = sideCharacters.Bottom; }
            }

            for (int y = top + 1; y < bottom; y++)
            {
                if (y < 0) continue;
                if (y >= self.Height) break;

                if (left >= 0 && left < self.Width)
                { self[left, y].Char = sideCharacters.Left; }

                if (right >= 0 && right < self.Width)
                { self[right, y].Char = sideCharacters.Right; }
            }

            if (self.IsVisible(left, top))
            { self[left, top].Char = sideCharacters.TopLeft; }

            if (self.IsVisible(right, top))
            { self[right, top].Char = sideCharacters.TopRight; }

            if (self.IsVisible(left, bottom))
            { self[left, bottom].Char = sideCharacters.BottomLeft; }

            if (self.IsVisible(right, bottom))
            { self[right, bottom].Char = sideCharacters.BottomRight; }
        }
        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Box(this IRenderer<ConsoleChar> self, SMALL_RECT box, byte background, byte foreground, in SideCharacters<char> sideCharacters) => self.Box(box, CharColor.Make(background, foreground), in sideCharacters);

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Box(this IRenderer<ConsoleChar> self, SMALL_RECT box, ushort attributes, in SideCharacters<char> sideCharacters)
        {
            int top = box.Top;
            int left = box.Left;
            int bottom = box.Bottom;
            int right = box.Right;

            for (int x = left + 1; x < right; x++)
            {
                if (x < 0) continue;
                if (x >= self.Width) break;

                if (top >= 0 && top < self.Height)
                { self[x, top] = new ConsoleChar(sideCharacters.Top, attributes); }

                if (bottom >= 0 && bottom < self.Height)
                { self[x, bottom] = new ConsoleChar(sideCharacters.Bottom, attributes); }
            }

            for (int y = top + 1; y < bottom; y++)
            {
                if (y < 0) continue;
                if (y >= self.Height) break;

                if (left >= 0 && left < self.Width)
                { self[left, y] = new ConsoleChar(sideCharacters.Left, attributes); }

                if (right >= 0 && right < self.Width)
                { self[right, y] = new ConsoleChar(sideCharacters.Right, attributes); }
            }

            if (self.IsVisible(left, top))
            { self[left, top] = new ConsoleChar(sideCharacters.TopLeft, attributes); }

            if (self.IsVisible(right, top))
            { self[right, top] = new ConsoleChar(sideCharacters.TopRight, attributes); }

            if (self.IsVisible(left, bottom))
            { self[left, bottom] = new ConsoleChar(sideCharacters.BottomLeft, attributes); }

            if (self.IsVisible(right, bottom))
            { self[right, bottom] = new ConsoleChar(sideCharacters.BottomRight, attributes); }
        }

        #endregion

        #region Panel()

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Panel(this IRenderer<ConsoleChar> self, ConsolePanel panel, ushort attributes, in SideCharacters<char> sideCharacters)
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
                        if (newPosition.Y > self.Height - panelHeight) newPosition.Y = (short)(self.Height - panelHeight);
                        if (newPosition.X > self.Width - panelWidth) newPosition.X = (short)(self.Width - panelWidth);

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
                if (x >= self.Width) break;

                if (top >= 0 && top < self.Height)
                { self[x, top] = new ConsoleChar(sideCharacters.Top, attributes); }

                if (bottom >= 0 && bottom < self.Height)
                { self[x, bottom] = new ConsoleChar(sideCharacters.Bottom, attributes); }
            }

            for (int y = top + 1; y < bottom; y++)
            {
                if (y < 0) continue;
                if (y >= self.Height) break;

                if (left >= 0 && left < self.Width)
                { self[left, y] = new ConsoleChar(sideCharacters.Left, attributes); }

                if (right >= 0 && right < self.Width)
                { self[right, y] = new ConsoleChar(sideCharacters.Right, attributes); }
            }

            if (self.IsVisible(left, top))
            { self[left, top] = new ConsoleChar(sideCharacters.TopLeft, attributes); }

            if (self.IsVisible(right, top))
            { self[right, top] = new ConsoleChar(sideCharacters.TopRight, attributes); }

            if (self.IsVisible(left, bottom))
            { self[left, bottom] = new ConsoleChar(sideCharacters.BottomLeft, attributes); }

            if (self.IsVisible(right, bottom))
            { self[right, bottom] = new ConsoleChar(sideCharacters.BottomRight, attributes); }
        }

        #endregion

        #region Fill()

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Fill(this IRenderer<ConsoleChar> self, SMALL_RECT rect, byte background, byte foreground, char character) => self.Fill(rect, CharColor.Make(background, foreground), character);
        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Fill(this IRenderer<ConsoleChar> self, SMALL_RECT rect, ushort attributes, char character) => self.Fill(rect, new ConsoleChar(character, attributes));

        public static void Fill(this IRenderer<ConsoleChar> self, byte background, byte foreground, char character) => self.Fill(new ConsoleChar(character, foreground, background));
        public static void Fill(this IRenderer<ConsoleChar> self, ushort attributes, char character) => self.Fill(new ConsoleChar(character, attributes));

        #endregion
    }
}
