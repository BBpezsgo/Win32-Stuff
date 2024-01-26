﻿using System.Numerics;

namespace Win32
{
    public partial class AnsiRenderer : IRenderer<ConsoleChar>
    {
        public short Width => BufferWidth;
        public short Height => BufferHeight;

        public SmallSize Size => new(BufferWidth, BufferHeight);

        protected short BufferWidth;
        protected short BufferHeight;

        protected ConsoleChar[] ConsoleBuffer;
        protected SMALL_RECT ConsoleRect;

        readonly AnsiBuilder Builder;

        /// <exception cref="ArgumentOutOfRangeException"/>
        public ref ConsoleChar this[int i] => ref ConsoleBuffer[i];
        /// <exception cref="ArgumentOutOfRangeException"/>
        public ref ConsoleChar this[int x, int y] => ref ConsoleBuffer[(y * BufferWidth) + x];
        /// <exception cref="ArgumentOutOfRangeException"/>
        public ref ConsoleChar this[float x, float y] => ref ConsoleBuffer[((int)MathF.Round(y) * BufferWidth) + (int)MathF.Round(x)];
        /// <exception cref="ArgumentOutOfRangeException"/>
        public ref ConsoleChar this[COORD p] => ref ConsoleBuffer[(p.Y * BufferWidth) + p.X];
        /// <exception cref="ArgumentOutOfRangeException"/>
        public ref ConsoleChar this[POINT p] => ref ConsoleBuffer[(p.Y * BufferWidth) + p.X];
        /// <exception cref="ArgumentOutOfRangeException"/>
        public ref ConsoleChar this[Vector2 p] => ref ConsoleBuffer[((int)MathF.Round(p.Y) * BufferWidth) + (int)MathF.Round(p.X)];

        /// <exception cref="WindowsException"/>
        /// <exception cref="GeneralException"/>
        [SupportedOSPlatform("windows")]
        public AnsiRenderer() : this(ConsoleHandler.WindowWidth, ConsoleHandler.WindowHeight)
        { }

        public AnsiRenderer(short bufferWidth, short bufferHeight)
        {
            BufferWidth = bufferWidth;
            BufferHeight = bufferHeight;

            ConsoleBuffer = new ConsoleChar[BufferWidth * BufferHeight];
            ConsoleRect = new SMALL_RECT((SHORT)0, (SHORT)0, BufferWidth, BufferHeight);

            if (OperatingSystem.IsWindows())
            { Ansi.EnableVirtualTerminalSequences(); }

            Builder = new AnsiBuilder(BufferWidth * BufferHeight);
        }

        public bool IsVisible(int x, int y) => x >= 0 && y >= 0 && x < BufferWidth && y < BufferHeight;
        public bool IsVisible(float x, float y) => MathF.Round(x) >= 0 && MathF.Round(y) >= 0 && MathF.Round(x) < BufferWidth && MathF.Round(y) < BufferHeight;
        public bool IsVisible(COORD position) => position.X >= 0 && position.Y >= 0 && position.X < BufferWidth && position.Y < BufferHeight;
        public bool IsVisible(POINT position) => position.X >= 0 && position.Y >= 0 && position.X < BufferWidth && position.Y < BufferHeight;
        public bool IsVisible(Vector2 position) => MathF.Round(position.X) >= 0 && MathF.Round(position.Y) >= 0 && MathF.Round(position.X) < BufferWidth && MathF.Round(position.Y) < BufferHeight;

        public void Render()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            for (int y = 0; y < BufferHeight; y++)
            {
                for (int x = 0; x < BufferWidth; x++)
                {
                    ref ConsoleChar c = ref this[x, y];
                    Builder.BackgroundColor = CharColor.GetColor(c.Background);
                    Builder.ForegroundColor = CharColor.GetColor(c.Foreground);
                    if (c.Char == '\0')
                    { Builder.Append(' '); }
                    else
                    { Builder.Append(c.Char); }
                }
            }
            Console.Out.Write(Builder.ToString());
            Console.SetCursorPosition(0, 0);
        }

        public virtual void ClearBuffer() => Array.Clear(ConsoleBuffer);

        public void RefreshBufferSize() => RefreshBufferSize(Console.WindowWidth, Console.WindowHeight);
        public void RefreshBufferSize(int width, int height)
        {
            BufferWidth = (short)width;
            BufferHeight = (short)height;

            if (ConsoleBuffer.Length != BufferWidth * BufferHeight)
            { ConsoleBuffer = new ConsoleChar[BufferWidth * BufferHeight]; }
            ConsoleRect = new SMALL_RECT((SHORT)0, (SHORT)0, BufferWidth, BufferHeight);
        }

        /// <remarks>
        /// <b>Note:</b> This checks if the coordinate is out of range
        /// </remarks>
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

        public void Fill(ConsoleChar value) => Array.Fill(ConsoleBuffer, value);

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
            }
        }
    }
}
