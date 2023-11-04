using Microsoft.Win32.SafeHandles;

namespace Win32
{
    public partial class ConsoleRenderer : IDisposable
    {
        protected SafeFileHandle? SafeHandle;
        protected HANDLE Handle;

        public short Width => BufferWidth;
        public short Height => BufferHeight;

        public COORD Rect => new(BufferWidth, BufferHeight);

        public int Size => BufferWidth * BufferHeight;

        protected short BufferWidth;
        protected short BufferHeight;

        protected CharInfo[] ConsoleBuffer;
        protected SmallRect ConsoleRect;

        public ref CharInfo this[int i] => ref ConsoleBuffer[i];
        public ref CharInfo this[int x, int y] => ref ConsoleBuffer[(y * BufferWidth) + x];
        public ref CharInfo this[COORD p] => ref ConsoleBuffer[(p.X * BufferWidth) + p.Y];
        public ref CharInfo this[POINT p] => ref ConsoleBuffer[(p.X * BufferWidth) + p.Y];

        public ConsoleRenderer(SafeFileHandle? safeHandle, int bufferWidth, int bufferHeight)
        {
            /*
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            _ = Kernel32.SetConsoleOutputCP(65001);
            _ = Kernel32.SetConsoleCP(65001);
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            */

            Handle = Kernel32.GetStdHandle(StdHandle.STD_OUTPUT_HANDLE);
            SafeHandle = safeHandle;

            BufferWidth = (short)bufferWidth;
            BufferHeight = (short)bufferHeight;

            ConsoleBuffer = new CharInfo[BufferWidth * BufferHeight];
            Array.Fill(ConsoleBuffer, new CharInfo(' ', 0));
            ConsoleRect = new SmallRect(0, 0, BufferWidth, BufferHeight);
        }

        public bool IsVisible(int x, int y) => x >= 0 && y >= 0 && x < BufferWidth && y < BufferHeight;

        public void Render()
        {
            if (SafeHandle != null)
            {
                if (SafeHandle.IsInvalid)
                {
                    System.Diagnostics.Debug.WriteLine("Console handle is invalid");
                    return;
                }

                if (SafeHandle.IsClosed)
                { return; }

                if (Kernel32.WriteConsoleOutputW(
                    SafeHandle,
                    ConsoleBuffer,
                    new Coord(BufferWidth, BufferHeight),
                    default,
                    ref ConsoleRect) == 0)
                { throw WindowsException.Get(); }
            }
            else
            {
                if (Kernel32.WriteConsoleOutput(Handle, ConsoleBuffer,
                    new Coord(BufferWidth, BufferHeight),
                    default,
                    ref ConsoleRect) == 0)
                { throw WindowsException.Get(); }
            }
        }

        public virtual void ClearBuffer() => Array.Clear(ConsoleBuffer);

        public void Resize()
        {
            BufferWidth = (short)Console.WindowWidth;
            BufferHeight = (short)Console.WindowHeight;

            ConsoleBuffer = new CharInfo[BufferWidth * BufferHeight];
            ConsoleRect = new SmallRect(0, 0, BufferWidth, BufferHeight);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (SafeHandle != null)
                {
                    SafeHandle.Dispose();
                    SafeHandle = null;
                }
            }
        }
    }
}
