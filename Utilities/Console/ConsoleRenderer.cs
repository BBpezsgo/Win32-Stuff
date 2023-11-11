namespace Win32
{
    public partial class ConsoleRenderer
    {
        protected HANDLE Handle;

        public short Width => BufferWidth;
        public short Height => BufferHeight;

        public COORD Rect => new(BufferWidth, BufferHeight);

        public int Size => BufferWidth * BufferHeight;

        protected short BufferWidth;
        protected short BufferHeight;

        protected CharInfo[] ConsoleBuffer;
        protected SMALL_RECT ConsoleRect;

        public ref CharInfo this[int i] => ref ConsoleBuffer[i];
        public ref CharInfo this[int x, int y] => ref ConsoleBuffer[(y * BufferWidth) + x];
        public ref CharInfo this[COORD p] => ref ConsoleBuffer[(p.X * BufferWidth) + p.Y];
        public ref CharInfo this[POINT p] => ref ConsoleBuffer[(p.X * BufferWidth) + p.Y];

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

            BufferWidth = bufferWidth;
            BufferHeight = bufferHeight;

            ConsoleBuffer = new CharInfo[Size];
            Array.Fill(ConsoleBuffer, CharInfo.Empty);
            ConsoleRect = new SMALL_RECT(0, 0, BufferWidth, BufferHeight);
        }

        public bool IsVisible(int x, int y) => x >= 0 && y >= 0 && x < BufferWidth && y < BufferHeight;

        public void Render()
        {
            if (Kernel32.WriteConsoleOutput(
                Handle,
                ConsoleBuffer,
                Rect,
                default,
                ref ConsoleRect) == FALSE)
            { throw WindowsException.Get(); }
        }

        public virtual void ClearBuffer() => Array.Clear(ConsoleBuffer);

        public void Resize()
        {
            BufferWidth = ConsoleHandler.WindowWidth;
            BufferHeight = ConsoleHandler.WindowHeight;

            ConsoleBuffer = new CharInfo[Size];
            ConsoleRect = new SMALL_RECT(0, 0, BufferWidth, BufferHeight);
        }
    }
}
