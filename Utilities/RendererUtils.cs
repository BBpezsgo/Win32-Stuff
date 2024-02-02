using System.Numerics;

namespace Win32
{
    public static class RendererUtils
    {
        #region Text()

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Text(this Renderer<ConsoleChar> self, COORD position, ReadOnlySpan<byte> text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
            => self.Text(position.X, position.Y, text, CharColor.Make(background, foreground));

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Text(this Renderer<ConsoleChar> self, Vector2 position, ReadOnlySpan<byte> text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
            => self.Text((int)MathF.Round(position.X), (int)MathF.Round(position.Y), text, CharColor.Make(background, foreground));

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Text(this Renderer<ConsoleChar> self, int x, int y, ReadOnlySpan<byte> text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
            => self.Text(x, y, text, CharColor.Make(background, foreground));

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Text(this Renderer<ConsoleChar> self, COORD position, ReadOnlySpan<byte> text, ushort attributes)
            => self.Text(position.X, position.Y, text, attributes);

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Text(this Renderer<ConsoleChar> self, Vector2 position, ReadOnlySpan<byte> text, ushort attributes)
            => self.Text((int)MathF.Round(position.X), (int)MathF.Round(position.Y), text, attributes);

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Text(this Renderer<ConsoleChar> self, int x, int y, ReadOnlySpan<byte> text, ushort attributes)
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
        public static void Text(this Renderer<ConsoleChar> self, COORD position, ReadOnlySpan<char> text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
            => self.Text(position.X, position.Y, text, CharColor.Make(background, foreground));

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Text(this Renderer<ConsoleChar> self, Vector2 position, ReadOnlySpan<char> text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
            => self.Text((int)MathF.Round(position.X), (int)MathF.Round(position.Y), text, CharColor.Make(background, foreground));

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Text(this Renderer<ConsoleChar> self, int x, int y, ReadOnlySpan<char> text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
            => self.Text(x, y, text, CharColor.Make(background, foreground));

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Text(this Renderer<ConsoleChar> self, COORD position, ReadOnlySpan<char> text, ushort attributes)
            => self.Text(position.X, position.Y, text, attributes);

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Text(this Renderer<ConsoleChar> self, Vector2 position, ReadOnlySpan<char> text, ushort attributes)
            => self.Text((int)MathF.Round(position.X), (int)MathF.Round(position.Y), text, attributes);

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Text(this Renderer<ConsoleChar> self, SmallRect rect, ReadOnlySpan<char> text, ushort attributes)
            => self.Text(rect.X, rect.Y, text[..Math.Min(text.Length, rect.Width)], attributes);

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Text(this Renderer<ConsoleChar> self, int x, int y, ReadOnlySpan<char> text, ushort attributes)
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
        public static void Text(this Renderer<ConsoleChar> self, ref int x, int y, ReadOnlySpan<char> text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
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

        #region Text() (pixel renderer)

        static Coord CharacterCoord(char @char, int sheetWidth, int sheetHeight, int charWidth, int charHeight)
        {
            int cols = sheetWidth / charWidth;
            int rows = sheetHeight / charHeight;

            int characterX = @char % cols;
            int characterY = @char / rows;

            return new COORD(characterX * charWidth, characterY * charHeight);
        }

        static Coord CharacterCoord(byte @char, int sheetWidth, int sheetHeight, int charWidth, int charHeight)
        {
            int cols = sheetWidth / charWidth;
            int rows = sheetHeight / charHeight;

            int characterX = @char % cols;
            int characterY = @char / rows;

            return new COORD(characterX * charWidth, characterY * charHeight);
        }

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Char<TPixel>(this Renderer<TPixel> self, int x, int y, char @char, ReadOnlySpan<TPixel> fontSheetBuffer, int fontSheetWidth, int fontSheetHeight, int charWidth, int charHeight)
        {
            COORD charCoord = CharacterCoord(@char, fontSheetWidth, fontSheetHeight, charWidth, charHeight);

            for (int offsetY = 0; offsetY < charHeight; offsetY++)
            {
                if (y + offsetY < 0) continue;
                if (y + offsetY >= self.Height) break;

                int i = charCoord.X + ((charCoord.Y + offsetY) * fontSheetHeight);
                self.Put(x, y + offsetY, fontSheetBuffer.Slice(i, charWidth), charWidth, 1);
            }
        }

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Char<TPixel>(this Renderer<TPixel> self, int x, int y, byte @char, ReadOnlySpan<TPixel> fontSheetBuffer, int fontSheetWidth, int fontSheetHeight, int charWidth, int charHeight)
        {
            COORD charCoord = CharacterCoord(@char, fontSheetWidth, fontSheetHeight, charWidth, charHeight);

            for (int offsetY = 0; offsetY < charHeight; offsetY++)
            {
                if (y + offsetY < 0) continue;
                if (y + offsetY >= self.Height) break;

                int i = charCoord.X + ((charCoord.Y + offsetY) * fontSheetHeight);
                self.Put(x, y + offsetY, fontSheetBuffer.Slice(i, charWidth), charWidth, 1);
            }
        }

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Text<TPixel>(this Renderer<TPixel> self, int x, int y, ReadOnlySpan<char> text, ReadOnlySpan<TPixel> fontSheetBuffer, int fontSheetWidth, int fontSheetHeight, int fontWidth, int fontHeight)
        {
            if (text.IsEmpty) return;
            if (y < 0 || y >= self.Height) return;

            for (int i = 0; i < text.Length; i++)
            {
                self.Char(x + (i * fontWidth), y, text[i], fontSheetBuffer, fontSheetWidth, fontSheetHeight, fontWidth, fontHeight);
            }
        }

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Text<TPixel>(this Renderer<TPixel> self, int x, int y, ReadOnlySpan<byte> text, ReadOnlySpan<TPixel> fontSheetBuffer, int fontSheetWidth, int fontSheetHeight, int fontWidth, int fontHeight)
        {
            if (text.IsEmpty) return;
            if (y < 0 || y >= self.Height) return;

            for (int i = 0; i < text.Length; i++)
            {
                self.Char(x + (i * fontWidth), y, text[i], fontSheetBuffer, fontSheetWidth, fontSheetHeight, fontWidth, fontHeight);
            }
        }

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Char<TPixel>(this Renderer<TPixel> self, int x, int y, char @char, BitmapFont<TPixel> font)
        {
            COORD charCoord = CharacterCoord(@char, font.Width, font.Height, font.CharWidth, font.CharHeight);

            for (int offsetY = 0; offsetY < font.CharHeight; offsetY++)
            {
                if (y + offsetY < 0) continue;
                if (y + offsetY >= self.Height) break;

                int i = charCoord.X + ((charCoord.Y + offsetY) * font.Height);
                self.Put(x, y + offsetY, font.Buffer.Slice(i, font.CharWidth), font.CharWidth, 1);
            }
        }

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Char<TPixel>(this Renderer<TPixel> self, int x, int y, byte @char, BitmapFont<TPixel> font)
        {
            COORD charCoord = CharacterCoord(@char, font.Width, font.Height, font.CharWidth, font.CharHeight);

            for (int offsetY = 0; offsetY < font.CharHeight; offsetY++)
            {
                if (y + offsetY < 0) continue;
                if (y + offsetY >= self.Height) break;

                int i = charCoord.X + ((charCoord.Y + offsetY) * font.Height);
                self.Put(x, y + offsetY, font.Buffer.Slice(i, font.CharWidth), font.CharWidth, 1);
            }
        }

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Text<TPixel>(this Renderer<TPixel> self, int x, int y, ReadOnlySpan<char> text, BitmapFont<TPixel> font)
        {
            if (text.IsEmpty) return;
            if (y < 0 || y >= self.Height) return;

            for (int i = 0; i < text.Length; i++)
            {
                self.Char(x + (i * font.CharWidth), y, text[i], font);
            }
        }

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Text<TPixel>(this Renderer<TPixel> self, int x, int y, ReadOnlySpan<byte> text, BitmapFont<TPixel> font)
        {
            if (text.IsEmpty) return;
            if (y < 0 || y >= self.Height) return;

            for (int i = 0; i < text.Length; i++)
            {
                self.Char(x + (i * font.CharWidth), y, text[i], font);
            }
        }

        #endregion

        #region Dropdown()

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Dropdown(this Renderer<ConsoleChar> self, COORD coord, ConsoleDropdown dropdown, ReadOnlySpan<char> text, ConsoleDropdownStyle style)
            => self.Dropdown(coord.X, coord.Y, dropdown, text, style);

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Dropdown(this Renderer<ConsoleChar> self, int x, int y, ConsoleDropdown dropdown, ReadOnlySpan<char> text, ConsoleDropdownStyle style)
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
        public static void Textbox(this Renderer<ConsoleChar> self, SMALL_RECT rect, string text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
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
        public static bool Button(this Renderer<ConsoleChar> self, SMALL_RECT rect, ReadOnlySpan<char> text, ConsoleButtonStyle style)
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

            self.Fill(rect, new ConsoleChar(' ', attributes));

            COORD labelCoord = Layout.Center(text, rect).Position;

            self.Text(labelCoord, text, attributes);

            return clicked;
        }

        #endregion

        #region SelectBox()

        public static bool SelectBox<TItem>(this Renderer<ConsoleChar> self, SmallRect rect, ConsoleSelectBox<TItem> selectBox, ConsoleSelectBoxStyle style)
        {
            WORD labelAttributes = style.LabelNormal;
            WORD leftButtonAttributes = style.ButtonNormal;
            WORD rightButtonAttributes = style.ButtonNormal;

            bool wasModified = false;

            COORD leftButtonPos = rect.Position;
            COORD rightButtonPos = new(rect.Right - 1, rect.Y);

            SmallRect labelRect = rect.Margin(0, 1);

            if (labelRect.Contains(Mouse.RecordedConsolePosition))
            { labelAttributes = style.LabelHover; }

            if (selectBox.IsActive)
            { labelAttributes = style.LabelActive; }

            if (Mouse.IsPressed(MouseButton.Left) &&
                labelRect.Contains(Mouse.LeftPressedAt))
            { labelAttributes = style.LabelDown; }

            if (Mouse.RecordedConsolePosition == leftButtonPos)
            { leftButtonAttributes = style.ButtonHover; }

            if (Mouse.IsPressed(MouseButton.Left) &&
                Mouse.LeftPressedAt == leftButtonPos)
            { leftButtonAttributes = style.ButtonDown; }

            if (Mouse.RecordedConsolePosition == rightButtonPos)
            { rightButtonAttributes = style.ButtonHover; }

            if (Mouse.IsPressed(MouseButton.Left) &&
                Mouse.LeftPressedAt == rightButtonPos)
            { rightButtonAttributes = style.ButtonDown; }

            if (!Mouse.WasUsed &&
                Mouse.IsUp(MouseButton.Left) &&
                Mouse.LeftPressedAt == leftButtonPos &&
                Mouse.RecordedConsolePosition == leftButtonPos)
            {
                Mouse.Use();
                selectBox.SelectedIndex--;
                wasModified = true;
            }

            if (!Mouse.WasUsed &&
                Mouse.IsUp(MouseButton.Left) &&
                Mouse.LeftPressedAt == rightButtonPos &&
                Mouse.RecordedConsolePosition == rightButtonPos)
            {
                Mouse.Use();
                selectBox.SelectedIndex++;
                wasModified = true;
            }

            if (wasModified) selectBox.ClampIndex();

            if (selectBox.IsActive)
            {
                if (Mouse.ScrollDelta < 0)
                {
                    selectBox.SelectedIndex--;
                    wasModified = true;
                }

                if (Mouse.ScrollDelta > 0)
                {
                    selectBox.SelectedIndex++;
                    wasModified = true;
                }

                if (Keyboard.IsActive(VirtualKeyCode.LEFT) ||
                    Keyboard.IsActive(VirtualKeyCode.UP) ||
                    Keyboard.IsActive(VirtualKeyCode.VK_A) ||
                    Keyboard.IsActive(VirtualKeyCode.VK_W))
                {
                    selectBox.SelectedIndex--;
                    wasModified = true;
                }

                if (Keyboard.IsActive(VirtualKeyCode.RIGHT) ||
                    Keyboard.IsActive(VirtualKeyCode.DOWN) ||
                    Keyboard.IsActive(VirtualKeyCode.VK_D) ||
                    Keyboard.IsActive(VirtualKeyCode.VK_S))
                {
                    selectBox.SelectedIndex++;
                    wasModified = true;
                }
            }

            if (Mouse.IsDown(MouseButton.Left))
            {
                if (labelRect.Contains(Mouse.LeftPressedAt))
                {
                    if (!Mouse.WasUsed)
                    {
                        Mouse.Use();
                        selectBox.IsActive = true;
                    }
                }
                else
                {
                    selectBox.IsActive = false;
                }
            }

            if (wasModified) selectBox.ClampIndex();

            if (self.IsVisible(leftButtonPos))
            { self[leftButtonPos] = new ConsoleChar(style.LeftChar, leftButtonAttributes); }

            if (self.IsVisible(rightButtonPos))
            { self[rightButtonPos] = new ConsoleChar(style.RightChar, rightButtonAttributes); }

            string labelText = selectBox.SelectedItem?.ToString() ?? "<empty>";
            self.Text(labelRect.X + Layout.Center(labelText, labelRect.Width), labelRect.Y, labelText, labelAttributes);

            return wasModified;
        }

        #endregion

        #region InputField()

        const string ShiftedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ§'\"+!%/=()?:_-+ ";
        const string Chars = "abcdefghijklmnopqrstuvwxyz0123456789,.-+ ";
        const string Keys = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789,.-+ ";

        /// <remarks>
        /// <para>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </para>
        /// <para>
        /// This uses <see cref="Mouse"/> and <see cref="Keyboard"/>
        /// </para>
        /// </remarks>
        public static void InputField(this Renderer<ConsoleChar> self, SmallRect rect, ConsoleInputFieldStyle style, ConsoleInputField textField)
        {
            WORD attributes = style.Normal;

            if (textField.IsActive || rect.Contains(Mouse.RecordedConsolePosition))
            { attributes = style.Active; }

            int labelOffsetY = rect.Top + (rect.Height / 2);

            if (Mouse.IsDown(MouseButton.Left))
            {
                if (rect.Contains(Mouse.RecordedConsolePosition) && !Mouse.WasUsed)
                {
                    Mouse.Use();
                    textField.IsActive = true;
                    VirtualKeyboard.Show(textField.Label, (value) =>
                    {
                        textField.Value.Clear();
                        textField.Value.Append(value);
                        textField.IsActive = false;
                    }, textField.Value.ToString());
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
                if (Keyboard.IsActive(VirtualKeyCode.BACK))
                {
                    if (textField.Value.Length > 0 && textField.CursorPosition > 0)
                    {
                        textField.CursorPosition = Math.Clamp(textField.CursorPosition, 0, textField.Value.Length);
                        textField.Value.Remove(textField.CursorPosition - 1, 1);
                        textField.CursorPosition--;
                    }

                    textField.CursorBlinker = (float)DateTime.UtcNow.TimeOfDay.TotalSeconds;
                }
                else if (Keyboard.IsActive(VirtualKeyCode.DELETE))
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
                        if (!Keyboard.IsActive(Keys[i]))
                        { continue; }

                        char c = (Keyboard.IsKeyPressed(VirtualKeyCode.SHIFT) || Keyboard.IsKeyPressed(VirtualKeyCode.LSHIFT) || Keyboard.IsKeyPressed(VirtualKeyCode.RSHIFT)) ? ShiftedChars[i] : Chars[i];

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

                    if (Keyboard.IsActive(VirtualKeyCode.UP))
                    {
                        textField.CursorPosition = 0;

                        textField.CursorBlinker = (float)DateTime.UtcNow.TimeOfDay.TotalSeconds;
                    }

                    if (Keyboard.IsActive(VirtualKeyCode.LEFT))
                    {
                        textField.CursorPosition = Math.Clamp(textField.CursorPosition - 1, 0, textField.Value.Length);

                        textField.CursorBlinker = (float)DateTime.UtcNow.TimeOfDay.TotalSeconds;
                    }

                    if (Keyboard.IsActive(VirtualKeyCode.DOWN))
                    {
                        textField.CursorPosition = textField.Value.Length;

                        textField.CursorBlinker = (float)DateTime.UtcNow.TimeOfDay.TotalSeconds;
                    }

                    if (Keyboard.IsActive(VirtualKeyCode.RIGHT))
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

        /// <inheritdoc cref="Box(Renderer{ConsoleChar}, SmallRect, ushort, in SideCharacters{char})"/>
        public static void Box(this Renderer<ConsoleChar> self, SMALL_RECT box, byte background, byte foreground)
            => self.Box(box, CharColor.Make(background, foreground), in SideCharacters.BoxSides);

        /// <inheritdoc cref="Box(Renderer{ConsoleChar}, SmallRect, ushort, in SideCharacters{char})"/>
        public static void Box(this Renderer<ConsoleChar> self, SMALL_RECT box, ushort attributes)
            => self.Box(box, attributes, in SideCharacters.BoxSides);

        /// <inheritdoc cref="Box(Renderer{ConsoleChar}, SmallRect, ushort, in SideCharacters{char})"/>
        public static void Box(this Renderer<ConsoleChar> self, SMALL_RECT box, byte background, byte foreground, in SideCharacters<char> sideCharacters)
            => self.Box(box, CharColor.Make(background, foreground), in sideCharacters);

        /// <inheritdoc cref="Box(Renderer{ConsoleChar}, SmallRect, ushort, in SideCharacters{char})"/>
        public static void Box(this Renderer<ConsoleChar> self, SMALL_RECT box, ushort attributes, in SideCharacters<char> sideCharacters)
        {
            int top = box.Top;
            int left = box.Left;
            int bottom = box.Bottom;
            int right = box.Right;

            if (top == bottom && left == right)
            { return; }

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

        /// <inheritdoc cref="Panel(Renderer{ConsoleChar}, ConsolePanel, ushort, in SideCharacters{char})"/>
        public static void Panel(this Renderer<ConsoleChar> self, ConsolePanel panel, ushort attributes)
            => self.Panel(panel, attributes, in SideCharacters.PanelSides);

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Panel(this Renderer<ConsoleChar> self, ConsolePanel panel, ushort attributes, in SideCharacters<char> sideCharacters)
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
                        int panelWidth = panel.Rect.Width + 1;
                        int panelHeight = panel.Rect.Height + 1;

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

                        if (newPosition.Y >= self.Height - panelHeight)
                        {
                            newPosition.Y = (short)(self.Height - panelHeight);
                            BitUtils.SetMask(ref panel.Dock, ConsolePanel.DockBottom);
                        }
                        else
                        {
                            BitUtils.ResetMask(ref panel.Dock, ConsolePanel.DockBottom);
                        }

                        if (newPosition.X >= self.Width - panelWidth)
                        {
                            newPosition.X = (short)(self.Width - panelWidth);
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

            self.Box(panel.Rect, attributes, in sideCharacters);
        }

        #endregion

        #region Line()

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Line(this Renderer<ConsoleChar> self, COORD a, COORD b, ushort attributes)
        {
            COORD min = COORD.Min(a, b);
            COORD max = COORD.Max(a, b);

            if (min.X >= 0 && min.X < self.Width)
            {
                for (int y = Math.Max((short)0, min.Y); y <= max.Y; y++)
                {
                    if (y >= self.Height) break;
                    self[min.X, y] = new ConsoleChar('|', attributes);
                }
            }

            if (max.Y >= 0 && max.Y < self.Height)
            {
                for (int x = Math.Max((short)0, min.X); x <= max.X; x++)
                {
                    if (x >= self.Width) break;
                    self[x, max.Y] = new ConsoleChar('-', attributes);
                }
            }
        }

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
        public static void Line<TPixel>(this Renderer<TPixel> self, COORD a, COORD b, TPixel v)
        {
            COORD min = COORD.Min(a, b);
            COORD max = COORD.Max(a, b);

            if (min.X >= 0 && min.X < self.Width)
            {
                for (int y = Math.Max((short)0, min.Y); y <= max.Y; y++)
                {
                    if (y >= self.Height) break;
                    self[min.X, y] = v;
                }
            }

            if (max.Y >= 0 && max.Y < self.Height)
            {
                for (int x = Math.Max((short)0, min.X); x <= max.X; x++)
                {
                    if (x >= self.Width) break;
                    self[x, max.Y] = v;
                }
            }
        }

        #endregion
    }
}
