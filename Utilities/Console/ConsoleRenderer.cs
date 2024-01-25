using System.Numerics;

namespace Win32
{
    [SupportedOSPlatform("windows")]
    public partial class ConsoleRenderer : IRenderer<ConsoleChar>
    {
        protected HANDLE Handle;

        public short Width => BufferWidth;
        public short Height => BufferHeight;

        public SmallSize Size => new(BufferWidth, BufferHeight);

        protected short BufferWidth;
        protected short BufferHeight;

        protected ConsoleChar[] ConsoleBuffer;
        protected SMALL_RECT ConsoleRect;

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
        public ConsoleRenderer() : this(ConsoleHandler.WindowWidth, ConsoleHandler.WindowHeight)
        { }

        /// <exception cref="WindowsException"/>
        /// <exception cref="GeneralException"/>
        public ConsoleRenderer(short bufferWidth, short bufferHeight)
        {
            /*
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            _ = Kernel32.SetConsoleOutputCP(65001);
            _ = Kernel32.SetConsoleCP(65001);
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            */

            Handle = Kernel32.GetStdHandle(StdHandle.STD_OUTPUT_HANDLE);

            if (Handle == Kernel32.INVALID_HANDLE_VALUE)
            { throw WindowsException.Get(); }

            if (Handle == 0)
            { throw new GeneralException($"Application does not have a standard output"); }

            BufferWidth = bufferWidth;
            BufferHeight = bufferHeight;

            ConsoleBuffer = new ConsoleChar[BufferWidth * BufferHeight];
            ConsoleRect = new SMALL_RECT((SHORT)0, (SHORT)0, BufferWidth, BufferHeight);
        }

        public bool IsVisible(int x, int y) => x >= 0 && y >= 0 && x < BufferWidth && y < BufferHeight;
        public bool IsVisible(float x, float y) => MathF.Round(x) >= 0 && MathF.Round(y) >= 0 && MathF.Round(x) < BufferWidth && MathF.Round(y) < BufferHeight;
        public bool IsVisible(COORD position) => position.X >= 0 && position.Y >= 0 && position.X < BufferWidth && position.Y < BufferHeight;
        public bool IsVisible(POINT position) => position.X >= 0 && position.Y >= 0 && position.X < BufferWidth && position.Y < BufferHeight;
        public bool IsVisible(Vector2 position) => MathF.Round(position.X) >= 0 && MathF.Round(position.Y) >= 0 && MathF.Round(position.X) < BufferWidth && MathF.Round(position.Y) < BufferHeight;

        /// <exception cref="WindowsException"/>
        public void Render()
        {
            if (Kernel32.WriteConsoleOutput(
                Handle,
                ConsoleBuffer,
                Size,
                default,
                ref ConsoleRect) == FALSE)
            { throw WindowsException.Get(); }
        }

        public virtual void ClearBuffer() => Array.Clear(ConsoleBuffer);

        /// <exception cref="WindowsException"/>
        public void RefreshBufferSize()
        {
            ConsoleScreenBufferInfo info = ConsoleHandler.ScreenBufferInfo;
            BufferWidth = info.Window.Width;
            BufferHeight = info.Window.Height;

            if (ConsoleBuffer.Length != BufferWidth * BufferHeight)
            { ConsoleBuffer = new ConsoleChar[BufferWidth * BufferHeight]; }
            ConsoleRect = new SMALL_RECT((SHORT)0, (SHORT)0, BufferWidth, BufferHeight);
        }
    }
}
