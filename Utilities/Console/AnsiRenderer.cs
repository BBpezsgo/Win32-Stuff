using System.Numerics;
using System.Text;

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

        readonly StringBuilder Builder;

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

        [UnsupportedOSPlatform("android")]
        [UnsupportedOSPlatform("browser")]
        [UnsupportedOSPlatform("ios")]
        [UnsupportedOSPlatform("tvos")]
        /// <exception cref="System.Security.SecurityException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        /// <exception cref="IOException"/>
        /// <exception cref="PlatformNotSupportedException"/>
        /// <exception cref="WindowsException"/>
        public AnsiRenderer() : this((short)Console.WindowWidth, (short)Console.WindowHeight)
        { }

        [UnsupportedOSPlatform("android")]
        [UnsupportedOSPlatform("browser")]
        [UnsupportedOSPlatform("ios")]
        [UnsupportedOSPlatform("tvos")]
        /// <exception cref="System.Security.SecurityException"/>
        /// <exception cref="IOException"/>
        /// <exception cref="PlatformNotSupportedException"/>
        /// <exception cref="WindowsException"/>
        public AnsiRenderer(short bufferWidth, short bufferHeight)
        {
            BufferWidth = bufferWidth;
            BufferHeight = bufferHeight;

            ConsoleBuffer = new ConsoleChar[BufferWidth * BufferHeight];
            ConsoleRect = new SMALL_RECT((SHORT)0, (SHORT)0, BufferWidth, BufferHeight);

            if (OperatingSystem.IsWindows())
            { Ansi.EnableVirtualTerminalSequences(); }
            Console.CursorVisible = false;

            Builder = new StringBuilder(BufferWidth * BufferHeight);
        }

        public bool IsVisible(int x, int y) => x >= 0 && y >= 0 && x < BufferWidth && y < BufferHeight;
        public bool IsVisible(float x, float y) => MathF.Round(x) >= 0 && MathF.Round(y) >= 0 && MathF.Round(x) < BufferWidth && MathF.Round(y) < BufferHeight;
        public bool IsVisible(COORD position) => position.X >= 0 && position.Y >= 0 && position.X < BufferWidth && position.Y < BufferHeight;
        public bool IsVisible(POINT position) => position.X >= 0 && position.Y >= 0 && position.X < BufferWidth && position.Y < BufferHeight;
        public bool IsVisible(Vector2 position) => MathF.Round(position.X) >= 0 && MathF.Round(position.Y) >= 0 && MathF.Round(position.X) < BufferWidth && MathF.Round(position.Y) < BufferHeight;

        [UnsupportedOSPlatform("android")]
        [UnsupportedOSPlatform("browser")]
        [UnsupportedOSPlatform("ios")]
        [UnsupportedOSPlatform("tvos")]
        /// <exception cref="System.Security.SecurityException"/>
        /// <exception cref="IOException"/>
        /// <exception cref="PlatformNotSupportedException"/>
        /// <exception cref="WindowsException"/>
        public void Render()
        {
            Builder.Clear();

            byte prevForegroundColor = default;
            byte prevBackgroundColor = default;

            for (int y = 0; y < BufferHeight; y++)
            {
                for (int x = 0; x < BufferWidth; x++)
                {
                    Ansi.FromConsoleChar(
                        Builder,
                        this[x, y],
                        ref prevForegroundColor,
                        ref prevBackgroundColor,
                        x == 0 && y == 0);
                }
            }

            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            Console.Out.Write(Builder);
            Console.SetCursorPosition(0, 0);
        }

        public virtual void ClearBuffer() => Array.Clear(ConsoleBuffer);

        [UnsupportedOSPlatform("android")]
        [UnsupportedOSPlatform("browser")]
        [UnsupportedOSPlatform("ios")]
        [UnsupportedOSPlatform("tvos")]
        /// <exception cref="ArgumentOutOfRangeException"/>
        /// <exception cref="IOException"/>
        /// <exception cref="PlatformNotSupportedException"/>
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
