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

        public void Text(COORD point, string text)
            => Text(point.X, point.Y, text, CharColor.Silver, CharColor.Black);

        public void Text(int x, int y, string text)
            => Text(x, y, text, CharColor.Silver, CharColor.Black);

        public void Text(COORD point, string text, byte foreground, byte background = CharColor.Black)
            => Text(point.X, point.Y, text, foreground, background);

        public void Text(int x, int y, string text, byte foreground, byte background = CharColor.Black)
        {
            if (y < 0 || y >= BufferHeight) return;

            for (int i = 0; i < text.Length; i++)
            {
                int x_ = x + i;
                if (x_ < 0) continue;
                if (x_ >= BufferWidth) return;

                this[x_, y] = new ConsoleChar(text[i], foreground, background);
            }
        }

        public void Text(ref int x, int y, string text)
            => Text(ref x, y, text, CharColor.Silver, CharColor.Black);
        public void Text(ref int x, int y, string text, byte foreground, byte background = CharColor.Black)
        {
            if (y < 0 || y >= BufferHeight) return;

            for (int i = 0; i < text.Length; i++)
            {
                if (x < 0) { x++; continue; }
                if (x >= BufferWidth) return;

                this[x, y] = new ConsoleChar(text[i], foreground, background);

                x++;
            }
        }

        public bool Button(SMALL_RECT rect, string text, ButtonStyle style)
        {
            WORD attributes = style.Normal;

            if (rect.Contains(Mouse.RecordedConsolePosition))
            { attributes = style.Hover; }

            if (Mouse.IsPressed(MouseButton.Left) && rect.Contains(Mouse.LeftPressedAt))
            { attributes = style.Down; }

            bool clicked = Mouse.IsUp(MouseButton.Left) && rect.Contains(Mouse.LeftPressedAt);

            int labelOffset = (rect.Width / 2) - (text.Length / 2);

            for (int y = rect.Top; y < rect.Bottom; y++)
            {
                if (y >= BufferHeight) break;
                if (y < 0) continue;

                for (int x = rect.Left; x < rect.Right; x++)
                {
                    if (x >= BufferWidth) break;
                    if (x < 0) continue;

                    char c = ' ';

                    int i = x - rect.Left - labelOffset;

                    if (i >= 0 && i < text.Length)
                    { c = text[i]; }

                    this[x, y] = new ConsoleChar(c, attributes);
                }
            }

            return clicked;
        }

        const string ShiftedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ§'\"+!%/=()?:_-+";
        const string Chars = "abcdefghijklmnopqrstuvwxyz0123456789,.-+";
        const string Keys = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789,.-+";

        public void InputField(SmallRect rect, TextFieldStyle style, ref bool active, StringBuilder value)
        {
            WORD attributes = style.Normal;

            if (active || rect.Contains(Mouse.RecordedConsolePosition))
            { attributes = style.Active; }

            if (Mouse.IsPressed(MouseButton.Left))
            { active = rect.Contains(Mouse.LeftPressedAt); }

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

                    if (i >= 0 && i < value.Length)
                    {
                        c = value[i];
                    }

                    this[x, y] = new ConsoleChar(c, attributes);
                }
            }
        }
    }
}
