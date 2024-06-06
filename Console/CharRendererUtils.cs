using System.Numerics;
using Win32.Console;

namespace Win32;

public static partial class RendererUtils
{
    #region Text

    /// <remarks>
    /// <b>Note:</b> This checks if the coordinate is out of range
    /// </remarks>
    public static int Text(this IOnlySetterRenderer<ConsoleChar> self, COORD position, ReadOnlySpan<char> text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
        => self.Text(position.X, position.Y, text, CharColor.Make(background, foreground));

    /// <remarks>
    /// <b>Note:</b> This checks if the coordinate is out of range
    /// </remarks>
    public static int Text(this IOnlySetterRenderer<ConsoleChar> self, Vector2 position, ReadOnlySpan<char> text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
        => self.Text((int)MathF.Round(position.X), (int)MathF.Round(position.Y), text, CharColor.Make(background, foreground));

    /// <remarks>
    /// <b>Note:</b> This checks if the coordinate is out of range
    /// </remarks>
    public static int Text(this IOnlySetterRenderer<ConsoleChar> self, int x, int y, ReadOnlySpan<char> text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
        => self.Text(x, y, text, CharColor.Make(background, foreground));

    /// <remarks>
    /// <b>Note:</b> This checks if the coordinate is out of range
    /// </remarks>
    public static int Text(this IOnlySetterRenderer<ConsoleChar> self, COORD position, ReadOnlySpan<char> text, ushort attributes)
        => self.Text(position.X, position.Y, text, attributes);

    /// <remarks>
    /// <b>Note:</b> This checks if the coordinate is out of range
    /// </remarks>
    public static int Text(this IOnlySetterRenderer<ConsoleChar> self, Vector2 position, ReadOnlySpan<char> text, ushort attributes)
        => self.Text((int)MathF.Round(position.X), (int)MathF.Round(position.Y), text, attributes);

    /// <remarks>
    /// <b>Note:</b> This checks if the coordinate is out of range
    /// </remarks>
    public static int Text(this IOnlySetterRenderer<ConsoleChar> self, SmallRect rect, ReadOnlySpan<char> text, ushort attributes)
        => self.Text(rect.X, rect.Y, text[..Math.Min(text.Length, rect.Width)], attributes);

    /// <remarks>
    /// <b>Note:</b> This checks if the coordinate is out of range
    /// </remarks>
    public static int Text(this IOnlySetterRenderer<ConsoleChar> self, int x, int y, ReadOnlySpan<char> text, ushort attributes)
    {
        if (text.IsEmpty) return 0;
        if (y < 0 || y >= self.Height) return text.Length;

        for (int i = 0; i < text.Length; i++)
        {
            int x_ = x + i;
            if (x_ < 0) continue;
            if (x_ >= self.Width) break;

            self.Set(x_, y, new ConsoleChar(text[i], attributes));
        }

        return text.Length;
    }

    /// <remarks>
    /// <b>Note:</b> This checks if the coordinate is out of range
    /// </remarks>
    public static void Text(this IOnlySetterRenderer<ConsoleChar> self, ref int x, int y, ReadOnlySpan<char> text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
    {
        if (text.IsEmpty) return;
        if (y < 0 || y >= self.Height) return;

        for (int i = 0; i < text.Length; i++)
        {
            if (x < 0) { x++; continue; }
            if (x >= self.Width) return;

            self.Set(x, y, new ConsoleChar(text[i], foreground, background));

            x++;
        }
    }

    #endregion

    #region Dropdown

    /// <remarks>
    /// <b>Note:</b> This checks if the coordinate is out of range
    /// </remarks>
    public static void Dropdown(this IOnlySetterRenderer<ConsoleChar> self, COORD coord, ConsoleDropdown dropdown, ReadOnlySpan<char> text, ConsoleDropdownStyle style)
        => self.Dropdown(coord.X, coord.Y, dropdown, text, style);

    /// <remarks>
    /// <b>Note:</b> This checks if the coordinate is out of range
    /// </remarks>
    public static void Dropdown(this IOnlySetterRenderer<ConsoleChar> self, int x, int y, ConsoleDropdown dropdown, ReadOnlySpan<char> text, ConsoleDropdownStyle style)
    {
        if (text.IsEmpty) return;
        if (y < 0 || y >= self.Height) return;

        short width = (short)(2 + text.Length);
        SmallRect rect = new(x, y, width, 1);

        WORD attributes = style.Normal;

        if (!ConsoleMouse.WasUsed)
        {
            if (rect.Contains(ConsoleMouse.RecordedConsolePosition))
            { attributes = style.Hover; }

            if (rect.Contains(ConsoleMouse.LeftPressedAt))
            {
                if (ConsoleMouse.IsPressed(MouseButton.Left))
                {
                    attributes = style.Down;
                    ConsoleMouse.Use();
                }

                if (ConsoleMouse.IsUp(MouseButton.Left) && rect.Contains(ConsoleMouse.RecordedConsolePosition))
                {
                    dropdown.IsActive = !dropdown.IsActive;
                    ConsoleMouse.Use();
                }
            }
        }

        if (x >= 0 && x < self.Width)
        { self.Set(x, y, new ConsoleChar(dropdown.IsActive ? '▼' : '►', attributes)); }

        for (int i = 0; i < text.Length; i++)
        {
            if (x + i + 2 < 0) { continue; }
            if (x + i + 2 >= self.Width) return;

            self.Set(x + i + 2, y, new ConsoleChar(text[i], attributes));
        }
    }

    #endregion

    #region Textbox

    /// <remarks>
    /// <b>Note:</b> This checks if the coordinate is out of range
    /// </remarks>
    public static void Textbox(this IOnlySetterRenderer<ConsoleChar> self, SMALL_RECT rect, string text, byte foreground = CharColor.Silver, byte background = CharColor.Black)
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
                    self.Set(actualX, actualY, new ConsoleChar(words[i][j], foreground, background));
                }

                x++;
            }
            x++;
        }
    }

    #endregion

    #region Button

    /// <remarks>
    /// <para>
    /// <b>Note:</b> This checks if the coordinate is out of range
    /// </para>
    /// <para>
    /// This uses <see cref="ConsoleMouse"/>
    /// </para>
    /// </remarks>
    public static bool Button(this IOnlySetterRenderer<ConsoleChar> self, SMALL_RECT rect, ReadOnlySpan<char> text, ConsoleButtonStyle style)
    {
        WORD attributes = style.Normal;
        bool clicked = false;

        if (!ConsoleMouse.WasUsed)
        {
            if (rect.Contains(ConsoleMouse.RecordedConsolePosition))
            { attributes = style.Hover; }

            if (rect.Contains(ConsoleMouse.LeftPressedAt))
            {
                if (ConsoleMouse.IsPressed(MouseButton.Left))
                {
                    attributes = style.Down;
                    ConsoleMouse.Use();
                }

                if (ConsoleMouse.IsUp(MouseButton.Left) && rect.Contains(ConsoleMouse.RecordedConsolePosition))
                {
                    clicked = true;
                    ConsoleMouse.Use();
                }
            }
        }

        self.Fill(rect, new ConsoleChar(' ', attributes));

        COORD labelCoord = Layout.Center(text, rect).Position;

        self.Text(labelCoord, text, attributes);

        return clicked;
    }

    #endregion

    #region SelectBox

    public static bool SelectBox<TItem>(this IOnlySetterRenderer<ConsoleChar> self, SmallRect rect, ConsoleSelectBox<TItem> selectBox, ConsoleSelectBoxStyle style)
    {
        WORD labelAttributes = style.LabelNormal;
        WORD leftButtonAttributes = style.ButtonNormal;
        WORD rightButtonAttributes = style.ButtonNormal;

        bool wasModified = false;

        COORD leftButtonPos = rect.Position;
        COORD rightButtonPos = new(rect.Right - 1, rect.Y);

        SmallRect labelRect = rect.Margin(0, 1);

        if (labelRect.Contains(ConsoleMouse.RecordedConsolePosition))
        { labelAttributes = style.LabelHover; }

        if (selectBox.IsActive)
        { labelAttributes = style.LabelActive; }

        if (ConsoleMouse.IsPressed(MouseButton.Left) &&
            labelRect.Contains(ConsoleMouse.LeftPressedAt))
        { labelAttributes = style.LabelDown; }

        if (ConsoleMouse.RecordedConsolePosition == leftButtonPos)
        { leftButtonAttributes = style.ButtonHover; }

        if (ConsoleMouse.IsPressed(MouseButton.Left) &&
            ConsoleMouse.LeftPressedAt == leftButtonPos)
        { leftButtonAttributes = style.ButtonDown; }

        if (ConsoleMouse.RecordedConsolePosition == rightButtonPos)
        { rightButtonAttributes = style.ButtonHover; }

        if (ConsoleMouse.IsPressed(MouseButton.Left) &&
            ConsoleMouse.LeftPressedAt == rightButtonPos)
        { rightButtonAttributes = style.ButtonDown; }

        if (!ConsoleMouse.WasUsed &&
            ConsoleMouse.IsUp(MouseButton.Left) &&
            ConsoleMouse.LeftPressedAt == leftButtonPos &&
            ConsoleMouse.RecordedConsolePosition == leftButtonPos)
        {
            ConsoleMouse.Use();
            selectBox.SelectedIndex--;
            wasModified = true;
        }

        if (!ConsoleMouse.WasUsed &&
            ConsoleMouse.IsUp(MouseButton.Left) &&
            ConsoleMouse.LeftPressedAt == rightButtonPos &&
            ConsoleMouse.RecordedConsolePosition == rightButtonPos)
        {
            ConsoleMouse.Use();
            selectBox.SelectedIndex++;
            wasModified = true;
        }

        if (wasModified) selectBox.ClampIndex();

        if (selectBox.IsActive)
        {
            if (ConsoleMouse.ScrollDelta < 0)
            {
                selectBox.SelectedIndex--;
                wasModified = true;
            }

            if (ConsoleMouse.ScrollDelta > 0)
            {
                selectBox.SelectedIndex++;
                wasModified = true;
            }

            if (ConsoleKeyboard.IsActive(VirtualKeyCode.Left) ||
                ConsoleKeyboard.IsActive(VirtualKeyCode.Up) ||
                ConsoleKeyboard.IsActive(VirtualKeyCode.A) ||
                ConsoleKeyboard.IsActive(VirtualKeyCode.W))
            {
                selectBox.SelectedIndex--;
                wasModified = true;
            }

            if (ConsoleKeyboard.IsActive(VirtualKeyCode.Right) ||
                ConsoleKeyboard.IsActive(VirtualKeyCode.Down) ||
                ConsoleKeyboard.IsActive(VirtualKeyCode.D) ||
                ConsoleKeyboard.IsActive(VirtualKeyCode.S))
            {
                selectBox.SelectedIndex++;
                wasModified = true;
            }
        }

        if (ConsoleMouse.IsDown(MouseButton.Left))
        {
            if (labelRect.Contains(ConsoleMouse.LeftPressedAt))
            {
                if (!ConsoleMouse.WasUsed)
                {
                    ConsoleMouse.Use();
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
        { self.Set(leftButtonPos, new ConsoleChar(style.LeftChar, leftButtonAttributes)); }

        if (self.IsVisible(rightButtonPos))
        { self.Set(rightButtonPos, new ConsoleChar(style.RightChar, rightButtonAttributes)); }

        string labelText = selectBox.SelectedItem?.ToString() ?? "<empty>";
        self.Text(labelRect.X + Layout.Center(labelText, labelRect.Width), labelRect.Y, labelText, labelAttributes);

        return wasModified;
    }

    #endregion

    #region InputField

    /// <remarks>
    /// <para>
    /// <b>Note:</b> This checks if the coordinate is out of range
    /// </para>
    /// <para>
    /// This uses <see cref="ConsoleMouse"/> and <see cref="ConsoleKeyboard"/>
    /// </para>
    /// </remarks>
    public static void InputField(this IOnlySetterRenderer<ConsoleChar> self, SmallRect rect, ConsoleInputFieldStyle style, ConsoleInputField textField)
    {
        WORD attributes = style.Normal;

        if (textField.IsActive || rect.Contains(ConsoleMouse.RecordedConsolePosition))
        { attributes = style.Active; }

        int labelOffsetY = rect.Top + (rect.Height / 2);

        if (ConsoleMouse.IsDown(MouseButton.Left))
        {
            if (rect.Contains(ConsoleMouse.RecordedConsolePosition) && !ConsoleMouse.WasUsed)
            {
                ConsoleMouse.Use();
                textField.IsActive = true;
                VirtualKeyboard.Show(textField.Label, (value) =>
                {
                    textField.Value.Clear();
                    textField.Value.Append(value);
                    textField.IsActive = false;
                }, textField.Value.ToString());
                textField.CursorPosition = Math.Clamp(ConsoleMouse.LeftPressedAt.X - rect.Left, 0, textField.Value.Length);
                textField.CursorBlinker = (float)DateTime.UtcNow.TimeOfDay.TotalSeconds;
            }
            else if (!textField.NeverLoseFocus)
            {
                textField.IsActive = false;
            }
        }

        if (textField.IsActive)
        {
            if (ConsoleKeyboard.IsActive(VirtualKeyCode.Back))
            {
                if (textField.Value.Length > 0 && textField.CursorPosition > 0)
                {
                    textField.CursorPosition = Math.Clamp(textField.CursorPosition, 0, textField.Value.Length);
                    textField.Value.Remove(textField.CursorPosition - 1, 1);
                    textField.CursorPosition--;
                }

                textField.CursorBlinker = (float)DateTime.UtcNow.TimeOfDay.TotalSeconds;
            }
            else if (ConsoleKeyboard.IsActive(VirtualKeyCode.Delete))
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
                    if (!ConsoleKeyboard.IsActive(Keys[i]))
                    { continue; }

                    char c = (ConsoleKeyboard.IsKeyPressed(VirtualKeyCode.Shift) || ConsoleKeyboard.IsKeyPressed(VirtualKeyCode.LShift) || ConsoleKeyboard.IsKeyPressed(VirtualKeyCode.RShift)) ? ShiftedChars[i] : Chars[i];

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

                if (ConsoleKeyboard.IsActive(VirtualKeyCode.Up))
                {
                    textField.CursorPosition = 0;

                    textField.CursorBlinker = (float)DateTime.UtcNow.TimeOfDay.TotalSeconds;
                }

                if (ConsoleKeyboard.IsActive(VirtualKeyCode.Left))
                {
                    textField.CursorPosition = Math.Clamp(textField.CursorPosition - 1, 0, textField.Value.Length);

                    textField.CursorBlinker = (float)DateTime.UtcNow.TimeOfDay.TotalSeconds;
                }

                if (ConsoleKeyboard.IsActive(VirtualKeyCode.Down))
                {
                    textField.CursorPosition = textField.Value.Length;

                    textField.CursorBlinker = (float)DateTime.UtcNow.TimeOfDay.TotalSeconds;
                }

                if (ConsoleKeyboard.IsActive(VirtualKeyCode.Right))
                {
                    textField.CursorPosition = Math.Clamp(textField.CursorPosition + 1, 0, textField.Value.Length);

                    textField.CursorBlinker = (float)DateTime.UtcNow.TimeOfDay.TotalSeconds;
                }
            }
        }

        for (int y = rect.Top; y <= rect.Bottom; y++)
        {
            if (y >= self.Height) break;
            if (y < 0) continue;

            for (int x = rect.Left; x <= rect.Right; x++)
            {
                if (x >= self.Width) break;
                if (x < 0) continue;

                char c = ' ';

                int i = x - rect.Left;

                if (i >= 0 && i < textField.Value.Length && y == labelOffsetY)
                { c = textField.Value[i]; }

                if (i == textField.CursorPosition && textField.IsActive && (int)((DateTime.UtcNow.TimeOfDay.TotalSeconds - textField.CursorBlinker) * 2) % 2 == 0)
                {
                    byte fg = (byte)(attributes & CharColor.MASK_FG);
                    byte bg = (byte)((attributes >> 4) & CharColor.MASK_FG);
                    self.Set(x, y, new ConsoleChar(c, CharColor.Make(fg, bg)));
                }
                else
                {
                    self.Set(x, y, new ConsoleChar(c, attributes));
                }
            }
        }
    }

    #endregion

    #region Box

    /// <inheritdoc cref="Box(IOnlySetterRenderer{ConsoleChar}, SmallRect, ushort, in SideCharacters{char})"/>
    public static void Box(this IOnlySetterRenderer<ConsoleChar> self, SMALL_RECT box, byte background, byte foreground)
        => self.Box(box, CharColor.Make(background, foreground), in SideCharacters.BoxSides);

    /// <inheritdoc cref="Box(IOnlySetterRenderer{ConsoleChar}, SmallRect, ushort, in SideCharacters{char})"/>
    public static void Box(this IOnlySetterRenderer<ConsoleChar> self, SMALL_RECT box, ushort attributes)
        => self.Box(box, attributes, in SideCharacters.BoxSides);

    /// <inheritdoc cref="Box(IOnlySetterRenderer{ConsoleChar}, SmallRect, ushort, in SideCharacters{char})"/>
    public static void Box(this IOnlySetterRenderer<ConsoleChar> self, SMALL_RECT box, byte background, byte foreground, in SideCharacters<char> sideCharacters)
        => self.Box(box, CharColor.Make(background, foreground), in sideCharacters);

    /// <inheritdoc cref="Box(IOnlySetterRenderer{ConsoleChar}, SmallRect, ushort, in SideCharacters{char})"/>
    public static void Box(this IOnlySetterRenderer<ConsoleChar> self, SMALL_RECT box, ushort attributes, in SideCharacters<char> sideCharacters)
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
            { self.Set(x, top, new ConsoleChar(sideCharacters.Top, attributes)); }

            if (bottom >= 0 && bottom < self.Height)
            { self.Set(x, bottom, new ConsoleChar(sideCharacters.Bottom, attributes)); }
        }

        for (int y = top + 1; y < bottom; y++)
        {
            if (y < 0) continue;
            if (y >= self.Height) break;

            if (left >= 0 && left < self.Width)
            { self.Set(left, y, new ConsoleChar(sideCharacters.Left, attributes)); }

            if (right >= 0 && right < self.Width)
            { self.Set(right, y, new ConsoleChar(sideCharacters.Right, attributes)); }
        }

        if (self.IsVisible(left, top))
        { self.Set(left, top, new ConsoleChar(sideCharacters.TopLeft, attributes)); }

        if (self.IsVisible(right, top))
        { self.Set(right, top, new ConsoleChar(sideCharacters.TopRight, attributes)); }

        if (self.IsVisible(left, bottom))
        { self.Set(left, bottom, new ConsoleChar(sideCharacters.BottomLeft, attributes)); }

        if (self.IsVisible(right, bottom))
        { self.Set(right, bottom, new ConsoleChar(sideCharacters.BottomRight, attributes)); }
    }

    #endregion

    #region Panel

    /// <inheritdoc cref="Panel(IOnlySetterRenderer{ConsoleChar}, ConsolePanel, ushort, in SideCharacters{char})"/>
    public static void Panel(this IOnlySetterRenderer<ConsoleChar> self, ConsolePanel panel, ushort attributes)
        => self.Panel(panel, attributes, in SideCharacters.PanelSides);

    /// <remarks>
    /// <b>Note:</b> This checks if the coordinate is out of range
    /// </remarks>
    public static void Panel(this IOnlySetterRenderer<ConsoleChar> self, ConsolePanel panel, ushort attributes, in SideCharacters<char> sideCharacters)
    {
        if (!ConsoleMouse.WasUsed)
        {
            if (ConsoleMouse.IsDown(MouseButton.Left) &&
                new SmallRect(panel.Rect.X, panel.Rect.Y, panel.Rect.Width, (short)1).Contains(ConsoleMouse.RecordedConsolePosition))
            {
                ConsoleMouse.Use();
                panel.IsActive = true;
                panel.PressedOffset = panel.Rect.Position - ConsoleMouse.RecordedConsolePosition;
            }
            else if (panel.IsActive)
            {
                ConsoleMouse.Use();
                if (ConsoleMouse.IsHold(MouseButton.Left))
                {
                    int panelWidth = panel.Rect.Width + 1;
                    int panelHeight = panel.Rect.Height + 1;

                    COORD newPosition = ConsoleMouse.RecordedConsolePosition + panel.PressedOffset;

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

    #region CorneredLine

    /// <remarks>
    /// <b>Note:</b> This checks if the coordinate is out of range
    /// </remarks>
    public static void CorneredLine(this IOnlySetterRenderer<ConsoleChar> self, COORD a, COORD b, ushort attributes)
    {
        COORD min = COORD.Min(a, b);
        COORD max = COORD.Max(a, b);

        if (min.X >= 0 && min.X < self.Width)
        {
            for (int y = Math.Max((short)0, min.Y); y <= max.Y; y++)
            {
                if (y >= self.Height) break;
                self.Set(min.X, y, new ConsoleChar('|', attributes));
            }
        }

        if (max.Y >= 0 && max.Y < self.Height)
        {
            for (int x = Math.Max((short)0, min.X); x <= max.X; x++)
            {
                if (x >= self.Width) break;
                self.Set(x, max.Y, new ConsoleChar('-', attributes));
            }
        }
    }

    #endregion
}
